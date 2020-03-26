// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;
using Common_Library.Logger;
using Common_Library.Utils.IO;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Download
{
    public partial class DownloadTask
    {
        public event SearchHandler ImageSaved;

        private async Task SaveImageAsync(Search search, Byte[] image, String formatedSavePath = null)
        {
            if (IsInvalid)
            {
                return;
            }

            await WaitAsync(_token).ConfigureAwait(true);

            String formatedDirectoryPath = GetFormatedDirectoryPath(search);

            if (String.IsNullOrEmpty(formatedDirectoryPath))
            {
                Log.Add(new LogMessage(Globals.Localization.FormatDirectoryError, MessageType.Warning, new[] {search.id.ToString()}));
                return;
            }

            if (!DirectoryUtils.TryCreateDirectory(formatedDirectoryPath, PathAction.None))
            {
                Log.Add(new LogMessage(Globals.Localization.CreateDirectoryError, MessageType.CriticalWarning));
                IsInvalid = true;
                return;
            }

            if (_token.IsCancellationRequested)
            {
                return;
            }

            try
            {
                if (String.IsNullOrEmpty(formatedSavePath))
                {
                    formatedSavePath = GetFormatedFilePath(search);
                    if (String.IsNullOrEmpty(formatedSavePath))
                    {
                        Log.Add(new LogMessage(Globals.Localization.FormatFileNameError,
                            MessageType.Warning, new[] {search.id.ToString()}));
                        return;
                    }
                }

                await using FileStream sourceStream = new FileStream(formatedSavePath, FileMode.Create, FileAccess.Write, FileShare.None);

                await sourceStream.WriteAsync(image, 0, image.Length, _token).ConfigureAwait(true);

                ImageSaved?.Invoke(search);
            }
            catch (UnauthorizedAccessException)
            {
                Log.Add(new LogMessage(Globals.Localization.WriteAccessDeniedError, MessageType.Error));
                IsInvalid = true;
            }
            catch (Exception e)
            {
                Log.Add(new LogMessage($"{search.file_name} error {e.Message}", MessageType.Error));
            }
        }
    }
}