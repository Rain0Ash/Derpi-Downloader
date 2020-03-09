// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using Common_Library.LongPath;
using Common_Library.Types.Other;

namespace Derpi_Downloader.Additionals
{
    public static class AdditionalsAPI
    {
        public static readonly IReadOnlyCollection<String> AllowedExtensions = new[]{
            ".BMP", ".ICO", ".JPG", ".JPEG", ".PNG", ".TIFF", 
            ".PSD", ".SAI", 
            ".SVG", 
            ".ZIP", ".RAR", ".7Z", ".7ZIP", 
            ".SWF",
            ".MPEG"
        };
        
        public static IEnumerable<FileInfo> GetFiles(IEnumerable<PathObject> includedPaths, IEnumerable<PathObject> excludedPaths)
        {
            HashSet<String> includedFolders = new HashSet<String>();
            HashSet<String> excludedFolders = new HashSet<String>();

            foreach (PathObject path in includedPaths)
            {
                if (!path.IsExistAsFolder())
                {
                    continue;
                }

                if (path.Recursive)
                {
                    includedFolders.UnionWith(path.GetFolders(System.IO.SearchOption.AllDirectories));
                }
                else
                {
                    includedFolders.Add(path.Path);
                }
            }
            
            foreach (PathObject path in excludedPaths)
            {
                if (!path.IsExistAsFolder())
                {
                    continue;
                }

                if (path.Recursive)
                {
                    excludedFolders.UnionWith(path.GetFolders(System.IO.SearchOption.AllDirectories));
                }
                else
                {
                    excludedFolders.Add(path.Path);
                }
            }
            
            IEnumerable<FileInfo> files = includedFolders
                .Except(excludedFolders)
                .Select(folder => new DirectoryInfo(folder).EnumerateFiles())
                .SelectMany(file => file);

            return files;
        }
    }
}