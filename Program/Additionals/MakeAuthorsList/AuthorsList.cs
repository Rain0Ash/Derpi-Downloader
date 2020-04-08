// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common_Library;
using Common_Library.Utils;
using Common_Library.Watchers;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    public class AuthorsList
    {
        public Int32 FilesForAnalyzeFound { get; }

        private Int32 _currentFilesAnalyzed;

        public Int32 CurrentFilesAnalyzed
        {
            get
            {
                return _currentFilesAnalyzed;
            }
            set
            {
                _currentFilesAnalyzed = value;
            }
        }

        private readonly IEnumerable<FSWatcher> _includedPaths;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IEnumerable<FSWatcher> _excludedPaths;
        private readonly IEnumerable<String> _regexPatterns;

        private readonly IEnumerable<String> _files;

        public AuthorsList(IEnumerable<FSWatcher> includedPaths, IEnumerable<FSWatcher> excludedPaths = null,
            IEnumerable<String> regexPatterns = null)
        {
            includedPaths = includedPaths?.ToArray();

            if (includedPaths?.Any() != true)
            {
                _includedPaths = null;
                return;
            }

            _includedPaths = includedPaths;
            _excludedPaths = excludedPaths;

            if (regexPatterns?.Any() != true)
            {
                regexPatterns = new List<String>
                {
                    AdditionalsAPI.DefaultDerpiBooruNamePattern
                };
            }

            _regexPatterns = regexPatterns;

            _files = AdditionalsAPI.GetFiles(_includedPaths, _excludedPaths);

            FilesForAnalyzeFound = _files.Count();
        }

        public async Task<IEnumerable<String>> GetArtistsSetAsync()
        {
            if (_includedPaths == null)
            {
                return new String[0];
            }

            Task<IEnumerable<String>>[] tasks = _files.Chunk(Environment.ProcessorCount).Select(chunk => Task.Run(() => AnalyzeFileAsync(chunk))).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(true);

            return tasks
                .Select(task => task.ConfigureAwait(true).GetAwaiter().GetResult())
                .SelectMany(result => result)
                .Except(new[] {null, String.Empty})
                .ToHashSet()
                .Sort();
        }

        private IEnumerable<String> AnalyzeFileAsync(IEnumerable<String> files)
        {
            return files.SelectMany(AnalyzeFileAsync);
        }
        
        private String[] AnalyzeFileAsync(String file)
        {
            try
            {
                String pattern = _regexPatterns
                    .FirstOrDefault(regex => Regex.IsMatch(file, regex));

                Regex selectArtistRegex;
                try
                {
                    if (pattern == null)
                    {
                        Interlocked.Add(ref _currentFilesAnalyzed, 1);
                        return new[] {String.Empty};
                    }

                    selectArtistRegex = new Regex(pattern);
                }
                catch (ArgumentException)
                {
                    Interlocked.Add(ref _currentFilesAnalyzed, 1);
                    return new[] {String.Empty};
                }

                Interlocked.Add(ref _currentFilesAnalyzed, 1);

                return selectArtistRegex
                    .MatchNamedCaptures(file)
                    .Where(artistPair => artistPair.Key == "artist")
                    .SelectMany(artistPair => artistPair.Value)
                    .Select(artist => artist.Replace("-dash-", "-").Replace("+", " ")).ToArray();
            }
            catch
            {
                return new[] {String.Empty};
            }
        }

        public async Task<String> GetArtistsAsync()
        {
            IEnumerable<String> task = await GetArtistsSetAsync().ConfigureAwait(true);
            StringBuilder watchArtists = new StringBuilder();

            String[] artists = task.ToArray();
            for (Int32 i = 0; i < artists.Length; i++)
            {
                watchArtists.Append($"artist:{artists[i]}" + (i != artists.Length - 1 ? ", " : String.Empty));
            }

            return watchArtists.ToString();
        }
    }
}