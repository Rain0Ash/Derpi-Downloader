// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common_Library.Attributes;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Localization;
using Common_Library.Logger;
using Common_Library.LongPath;
using Common_Library.Utils.IO;
using Common_Library.Utils.Math;
using Common_Library.Utils.Network;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Download
{
    public partial class DownloadTask
    {
        public event LogHandler Initialized;
        private Boolean _isInitialized;

        public Boolean IsInitialized
        {
            get
            {
                return _isInitialized;
            }
            set
            {
                if (_isInitialized == value || !value)
                {
                    return;
                }

                _isInitialized = true;
                Initialized?.Invoke(Log);
            }
        }

        private DerpiImage _firstPage;

        public delegate void SearchHandler(Image image);

        public event SearchHandler ImageDownloaded;

        public async Task InitializeTaskAsync()
        {
            if (IsInvalid)
            {
                Log.Add(new LogMessage(Globals.Localization.InitializedInvalidTaskError, MessageType.CriticalError));
                return;
            }

            if (IsInitialized || IsCompleted || _token.IsCancellationRequested)
            {
                return;
            }

            _firstPage = await GetPageAsync(1).ConfigureAwait(true);

            if (_firstPage == null)
            {
                IsInvalid = true;
                return;
            }

            ImagesPerPage = _firstPage.images.Count;
            MaximumImages = _firstPage.total;
            Pages = (Int32) Math.Ceiling((Single) MaximumImages / ImagesPerPage);

            if (_countOfPages == 0)
            {
                Log.Add(new LogMessage(Globals.Localization.ZeroDownloadPagesArgument, MessageType.Warning));
                IsCompleted = true;
                return;
            }

            if (_firstPageNumber > Pages)
            {
                Log.Add(new LogMessage(Globals.Localization.NoImagesFound, MessageType.Warning));
                IsCompleted = true;
                return;
            }

            if (_countOfPages > 0)
            {
                Pages = MathUtils.ToRange(Math.Min(Pages, _firstPageNumber + _countOfPages - 1), 0, Pages);
            }

            Int32 lastPageImageCount = MaximumImages <= ImagesPerPage ? MaximumImages : MathUtils.ToRange(MaximumImages % ImagesPerPage, 1, ImagesPerPage, true);

            MaximumImages = MathUtils.ToRange((Pages - 1) * ImagesPerPage + lastPageImageCount, 0, MaximumImages);

            if (MaximumImages <= 0 || Pages <= 0)
            {
                Log.Add(new LogMessage(Globals.Localization.NoImagesFound, MessageType.Warning));
                IsCompleted = true;
                return;
            }

            if (SaveToDisk && !DirectoryUtils.TryCreateDirectory(Regex.Replace(_saveDirectory, @"\{[^\{\}]+\}", "test")))
            {
                Log.Add(new LogMessage(Globals.Localization.CreateDirectoryError, MessageType.CriticalError));
                IsInvalid = true;
                return;
            }

            IsInitialized = true;
        }

        private async Task GenerateTasksAsync()
        {
            Task[] tasks = new Task[ImagesPerPage];
            DerpiImage nextPage = new DerpiImage();

            for (Int32 page = _firstPageNumber; page <= Pages; page++)
            {
                if (page == _firstPageNumber)
                {
                    await Task.Delay(2000, _token).ConfigureAwait(true);
                }

                await WaitAsync(_token).ConfigureAwait(true);

                Task<DerpiImage> nextPageTask = null;

                if (_token.IsCancellationRequested)
                {
                    return;
                }

                if (page < Pages)
                {
                    nextPageTask = GetPageAsync(page + 1);
                }

                DerpiImage derpiPage = page == _firstPageNumber ? _firstPage : nextPage;

                if (nextPage == null)
                {
                    if (nextPageTask != null)
                    {
                        nextPage = await nextPageTask.ConfigureAwait(true);
                    }

                    continue;
                }

                for (Int32 index = 0; index < derpiPage.images.Count; index++)
                {
                    if (IsInvalid || _token.IsCancellationRequested)
                    {
                        return;
                    }

                    Image image = derpiPage.images[index];

                    while (image?.duplicate_of != null)
                    {
                        Log.Add(new LogMessage("Image id:{0} duplicate of id:{1}", MessageType.Warning,
                            new[] {image.id.ToString(), image.duplicate_of.ToString()}));
                        try
                        {
                            image = (await JsonAPI.GetDerpiImageAsync($"id:{image.duplicate_of}", token: _token).ConfigureAwait(true)).images[0];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Log.Add(new LogMessage(Globals.Localization.CantFoundDuplicateImage, MessageType.Warning));
                        }
                    }

                    if (image == null)
                    {
                        continue;
                    }

                    tasks[index] = DownloadImageAsync(image);
                }

                await Task.WhenAll(tasks).ConfigureAwait(true);

                if (page >= Pages)
                {
                    continue;
                }

                if (nextPageTask != null)
                {
                    nextPage = await nextPageTask.ConfigureAwait(true);
                }
            }

            IsCompleted = true;
        }

        private async Task<DerpiImage> GetPageAsync(Int32 page)
        {
            try
            {
                const Int32 maximumCounts = 10;
                const Int32 waitDelay = 30000;
                Int32 count = 0;
                do
                {
                    using CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;

                    if (_token.IsCancellationRequested)
                    {
                        source.Cancel();
                    }

                    await WaitAsync(token).ConfigureAwait(true);

                    token.ThrowIfCancellationRequested();

                    Task<DerpiImage> pageTask = JsonAPI.GetDerpiImageAsync(SearchQuery, page, token: token);

                    try
                    {
                        if (await Task.WhenAny(pageTask, Task.Delay(waitDelay, token)).ConfigureAwait(true) == pageTask)
                        {
                            DerpiImage derpiImage = await pageTask.ConfigureAwait(true);

                            try
                            {
                                pageTask.Dispose();
                            }
                            catch (InvalidOperationException)
                            {
                                //ignored
                            }
                            
                            return derpiImage;
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        //ignored
                    }

                    Log.Add(new LogMessage(Globals.Localization.PageWaitToLongRetry, MessageType.Warning, new[] {page.ToString()}));
                    source.Cancel();

                    try
                    {
                        pageTask.Dispose();
                    }
                    catch (InvalidOperationException)
                    {
                        //ignored
                    }

                    count++;
                } while (count < maximumCounts);

                Log.Add(new LogMessage(Globals.Localization.GetPageError, MessageType.CriticalWarning, new[] {page.ToString()}));
                return null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        private async Task<Byte[]> GetImageAsync(Image image)
        {
            try
            {
                const Int32 maximumCounts = 10;
                const Int32 waitDelay = 30000;
                Int32 count = 0;
                do
                {
                    using CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;

                    if (_token.IsCancellationRequested)
                    {
                        source.Cancel();
                    }

                    await WaitAsync(token).ConfigureAwait(true);

                    token.ThrowIfCancellationRequested();

                    try
                    {
                        using WebClient client = new WebClient
                        {
                            Encoding = Encoding.UTF8,
                            Proxy = Globals.WebProxy
                        };

                        Task<Byte[]> imageTask = client.DownloadDataTaskAsync(
                            $"{(String.IsNullOrEmpty(image.representations.full) ? image.view_url : image.representations.full)}",
                            token);

                        try
                        {
                            if (await Task.WhenAny(imageTask, Task.Delay(waitDelay, token)).ConfigureAwait(true) == imageTask)
                            {
                                Byte[] imageByte = await imageTask.ConfigureAwait(true);

                                if (CheckImageHash(image, imageByte, out String hash))
                                {
                                    try
                                    {
                                        imageTask.Dispose();
                                    }
                                    catch (InvalidOperationException)
                                    {
                                        //ignored
                                    }

                                    return imageByte;
                                }

                                Log.Add(new LogMessage(
                                    Globals.Localization.InvalidImageHashError,
                                    MessageType.Warning,
                                    new[] {image.name, image.sha512_hash, hash}));
                                source.Cancel();
                                try
                                {
                                    imageTask.Dispose();
                                }
                                catch (InvalidOperationException)
                                {
                                    //ignored
                                }

                                count++;
                                continue;
                            }
                        }
                        catch (TaskCanceledException)
                        {
                            try
                            {
                                imageTask.Dispose();
                            }
                            catch (Exception)
                            {
                                //ignored
                            }
                        }
                    }
                    catch (WebException e)
                    {
                        Log.Add(new LogMessage(
                            $"{e.Message}{LocalizationBase.NewLine}{e.StackTrace}",
                            MessageType.CriticalWarning));

                        source.Cancel();
                        count++;
                        continue;
                    }

                    Log.Add(new LogMessage(Globals.Localization.ImageWaitToLongRetry, MessageType.Warning, new[] {image.id.ToString()}));

                    source.Cancel();
                    count++;
                } while (count < maximumCounts);

                Log.Add(new LogMessage(Globals.Localization.GetImageError, MessageType.CriticalWarning, new[] {image.id.ToString()}));
                return null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception)
            {
                Log.Add(new LogMessage(Globals.Localization.GetImageError, MessageType.CriticalWarning, new[] {image.id.ToString()}));
                return null;
            }
        }

        private static Boolean CheckImageHash(Image image, Byte[] imageByte, out String hash)
        {
            return CheckImageHash(imageByte, image.sha512_hash, image.orig_sha512_hash, out hash);
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Boolean CheckImageHash(Byte[] image, String imageHash, String originalImageHash, out String hash)
        {
#if HashImage
            hash = Cryptography.Hash.Sha512String(image);
            return hash.Equals(imageHash, StringComparison.OrdinalIgnoreCase) ||
                   hash.Equals(originalImageHash, StringComparison.OrdinalIgnoreCase);

#else

            hash = String.Empty;
            return true;

#endif
        }

        private String GetFormatedFilePath(Image image)
        {
            return FormatedField.Format(image, _savePath);
        }

        private String GetFormatedDirectoryPath(Image image)
        {
            return FormatedField.Format(image, _saveDirectory);
        }

        private async Task DownloadImageAsync(Image image)
        {
            try
            {
                if (IsInvalid || _token.IsCancellationRequested)
                {
                    return;
                }

                await WaitAsync(_token).ConfigureAwait(true);

                String formatedSavePath = null;
                if (SaveToDisk)
                {
                    formatedSavePath = GetFormatedFilePath(image);

                    if (String.IsNullOrEmpty(formatedSavePath))
                    {
                        Log.Add(new LogMessage(Globals.Localization.FormatFileNameError,
                            MessageType.Warning, new[] {image.id.ToString()}));
                        ImageDownloaded?.Invoke(image);
                        ImageSaved?.Invoke(image);
                        return;
                    }

                    if (File.Exists(formatedSavePath) && !Globals.ExistFileRewrite.GetValue())
                    {
                        ImageDownloaded?.Invoke(image);
                        ImageSaved?.Invoke(image);
                        return;
                    }
                }

                Byte[] imageByte = await GetImageAsync(image).ConfigureAwait(true);

                if (_token.IsCancellationRequested || imageByte == null)
                {
                    return;
                }

                ImageDownloaded?.Invoke(image);

                if (SaveToDisk)
                {
                    try
                    {
                        await SaveImageAsync(image, imageByte, formatedSavePath).ConfigureAwait(true);
                    }
                    catch (Exception e)
                    {
                        Log.Add(new LogMessage(e.Message, MessageType.CriticalWarning));
                        IsInvalid = true;
                        MessageForm.GetDialogResultOnException(e, null, null, MessageBoxButtons.OK, new[] {Globals.Localization.Accept});
                    }
                }
            }
            catch (Exception e)
            {
                Log.Add(new LogMessage($"{e.Message}{LocalizationBase.NewLine}{e.StackTrace}", MessageType.CriticalWarning));
                throw;
            }
        }
    }
}