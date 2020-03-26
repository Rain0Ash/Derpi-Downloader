// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common_Library;
using Common_Library.LongPath;
using Common_Library.Types.Other;
using Common_Library.Utils;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    public class AuthorsList : IDisposable
    {
        private const String Artist = "artist";

        public event Handlers.EmptyHandler FileAnalyzed;
        public Int32 FilesForAnalyzeFound { get; }
        public Int32 CurrentFilesAnalyzed { get; private set; }

        private readonly IEnumerable<PathObject> _includedPaths;
        private readonly Dictionary<String, String> _regexDictionary;

        private readonly IEnumerable<String> _files;

        public AuthorsList(IEnumerable<PathObject> includedPaths, IEnumerable<PathObject> excludedPaths = null,
            Dictionary<String, String> regexDictionary = null)
        {
            includedPaths = includedPaths?.ToArray();

            if (includedPaths?.Any() != true)
            {
                _includedPaths = null;
                return;
            }

            _includedPaths = includedPaths;

            if (regexDictionary?.Keys.Any() != true)
            {
                regexDictionary = new Dictionary<String, String>
                {
                    {@"^\d+__.+$", $@"(?<id>\d+)|(artist-colon-(?<{Artist}>[a-zA-Z0-9-+]+))"}
                };
            }

            _regexDictionary = regexDictionary;

            _files = AdditionalsAPI.GetFiles(_includedPaths, excludedPaths?.ToList() ?? new List<PathObject>())
                .Where(file => AdditionalsAPI.AllowedExtensions.Contains(file.Extension?.ToUpper()))
                .Select(file => Path.GetFileNameWithoutExtension(file.Name));

            FilesForAnalyzeFound = _files.Count();

            FileAnalyzed += () => CurrentFilesAnalyzed++;
        }

        public async Task<IEnumerable<String>> GetArtistsSetAsync()
        {
            if (_includedPaths == null)
            {
                return new String[0];
            }

            //speed solution => file => Task.Run(() => AnalyzeFileAsync(file));
            Task<String[]>[] tasks = _files.Select(AnalyzeFileAsync).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(true);

            List<String> artists = tasks
                .Select(task => task.ConfigureAwait(true).GetAwaiter().GetResult())
                .SelectMany(result => result)
                .Except(new[] {null, String.Empty})
                .ToHashSet()
                .ToList();

            artists.Sort();

            return artists;
        }

        private async Task<String[]> AnalyzeFileAsync(String file)
        {
            try
            {
                String pattern = _regexDictionary
                    .FirstOrDefault(regex => regex.Value != null && Regex.IsMatch(file, regex.Key)).Value;

                Regex selectArtistRegex;
                try
                {
                    if (pattern == null)
                    {
                        FileAnalyzed?.Invoke();
                        return new[] {String.Empty};
                    }

                    selectArtistRegex = new Regex(pattern);
                }
                catch (ArgumentException)
                {
                    FileAnalyzed?.Invoke();
                    return new[] {String.Empty};
                }

                FileAnalyzed?.Invoke();

                return selectArtistRegex
                    .MatchNamedCaptures(file)
                    .Where(artistPair => artistPair.Key == Artist)
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

        public void Dispose()
        {
            FileAnalyzed = null;
        }
    }
}