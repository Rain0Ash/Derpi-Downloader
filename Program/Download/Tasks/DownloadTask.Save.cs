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

        private async Task SaveImageAsync(Image image, Byte[] imageByte, String formatedSavePath = null)
        {
            if (IsInvalid)
            {
                return;
            }

            await WaitAsync(_token).ConfigureAwait(true);

            String formatedDirectoryPath = GetFormatedDirectoryPath(image);

            if (String.IsNullOrEmpty(formatedDirectoryPath))
            {
                Log.Add(new LogMessage(Globals.Localization.FormatDirectoryError, MessageType.Warning, new[] {image.id.ToString()}));
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
                    formatedSavePath = GetFormatedFilePath(image);
                    if (String.IsNullOrEmpty(formatedSavePath))
                    {
                        Log.Add(new LogMessage(Globals.Localization.FormatFileNameError,
                            MessageType.Warning, new[] {image.id.ToString()}));
                        return;
                    }
                }

                await using FileStream sourceStream = new FileStream(formatedSavePath, FileMode.Create, FileAccess.Write, FileShare.None);

                await sourceStream.WriteAsync(imageByte, 0, imageByte.Length, _token).ConfigureAwait(true);

                ImageSaved?.Invoke(image);
            }
            catch (UnauthorizedAccessException)
            {
                Log.Add(new LogMessage(Globals.Localization.WriteAccessDeniedError, MessageType.Error));
                IsInvalid = true;
            }
            catch (Exception e)
            {
                Log.Add(new LogMessage($"{image.name} error {e.Message}", MessageType.Error));
            }
        }
    }
}