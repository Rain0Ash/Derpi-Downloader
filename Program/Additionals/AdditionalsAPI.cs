// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common_Library.LongPath;
using Common_Library.Utils;
using Common_Library.Utils.IO;
using Common_Library.Watchers;

namespace Derpi_Downloader.Additionals
{
    public static class AdditionalsAPI
    {
        public static readonly IReadOnlyCollection<String> AllowedExtensions = new[]
        {
            ".BMP", ".ICO", ".JPG", ".JPEG", ".PNG", ".TIFF",
            ".PSD", ".SAI",
            ".SVG",
            ".ZIP", ".RAR", ".7Z", ".7ZIP",
            ".SWF",
            ".MP4", ".MPEG"
        };

        public static IEnumerable<FileInfo> GetFiles(IEnumerable<FSWatcher> includedPaths, IEnumerable<FSWatcher> excludedPaths)
        {
            HashSet<String> includedFolders = new HashSet<String>();
            HashSet<String> excludedFolders = new HashSet<String>();

            foreach (FSWatcher path in includedPaths)
            {
                if (!path.IsExistAsFolder())
                {
                    continue;
                }

                if (path.Recursive)
                {
                    includedFolders.UnionWith(path.GetEntries(PathType.Folder, true));
                }
                else
                {
                    includedFolders.Add(path.Path);
                }
            }

            foreach (FSWatcher path in excludedPaths)
            {
                if (!path.IsExistAsFolder())
                {
                    continue;
                }

                if (path.Recursive)
                {
                    excludedFolders.UnionWith(path.GetEntries(PathType.Folder, true));
                }
                else
                {
                    excludedFolders.Add(path.Path);
                }
            }

            IEnumerable<FileInfo> files = includedFolders
                .Except(excludedFolders)
                .Select(folder => DirectoryUtils.GetFiles(folder, false).Select(file => new FileInfo(file)))
                .SelectMany(info => info);

            return files;
        }
    }
}