// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
        
        private static readonly Regex RegexExt = new Regex($"^.*\\.({String.Join("|", AllowedExtensions.Select(ext => ext.Split(".")[1]))})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public const String DefaultDerpiBooruNamePattern = "^(?<id>\\d+)__.+(artist-colon-(?<artist>[a-zA-Z0-9-+]+))+.*$";
        public const String DefaultDeviantArtNamePattern = "^(?<name>.+)_by_(?<artist>[a-zA-Z0-9]+)_(?<id>[a-zA-Z0-9]+).*$";
        
        public static IEnumerable<String> GetFiles(IEnumerable<FSWatcher> includedPaths, IEnumerable<FSWatcher> excludedPaths)
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

            if (excludedPaths != null)
            {
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
            }

            foreach (String folder in includedFolders.Except(excludedFolders))
            {
                foreach (String file in DirectoryUtils.GetFiles(folder, RegexExt))
                {
                    yield return Path.GetFileNameWithoutExtension(file);
                }
            }
        }
    }
}