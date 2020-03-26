// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Common_Library.Utils.IO;
using Derpi_Downloader.API;

namespace Derpi_Downloader.Settings
{
    public enum RequestImageType : Byte
    {
        Image,
        Full,
        Tall,
        Large,
        Medium,
        Small,
        Thumb,
        ThumbSmall,
        ThumbTiny
    }

    public sealed class DownloadRequest
    {
        public readonly String SearchQuery;
        public readonly String DownloadPath;
        public readonly Boolean AutoDownload;
        public readonly RequestImageType ImageType;

        public DownloadRequest(String searchQuery)
            : this(searchQuery, Globals.QueueAutoDownload.GetValue())
        {
        }

        public DownloadRequest(String searchQuery, Boolean autoDownload)
            : this(searchQuery, Globals.CurrentDownloadPath, autoDownload)
        {
        }

        public DownloadRequest(String searchQuery, String downloadPath, RequestImageType imageType)
            : this(searchQuery, downloadPath, Globals.QueueAutoDownload.GetValue(), imageType)
        {
        }

        public DownloadRequest(String searchQuery, String downloadPath, Boolean autoDownload, RequestImageType imageType = RequestImageType.Image)
        {
            SearchQuery = DerpiAPI.CheckSearchRequest(searchQuery) ? searchQuery : String.Empty;
            DownloadPath = PathUtils.IsValidPath(downloadPath, PathType.File) ? downloadPath : Globals.CurrentDownloadPath;
            AutoDownload = autoDownload;
            ImageType = imageType;
        }

        public override String ToString()
        {
            return $"{(AutoDownload ? "(auto) " : String.Empty)}{SearchQuery}";
        }
    }
}