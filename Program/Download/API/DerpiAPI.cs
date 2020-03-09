// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common_Library.Utils;
using Derpi_Downloader.Settings;
using Derpi_Downloader.Json;
using Derpi_Downloader.Localization;

namespace Derpi_Downloader.API
{
    public static class DerpiAPI
    {
        public const String SiteRegexPart = @"(((http(s)?:\/\/)|(www\.))?derpibooru\.org\/)";
        public const String SearchWordRegexPart = @"((\d+|search)\?q=)";
        public const String SearchSiteRegexPart = @"(" + SiteRegexPart + "?" + SearchWordRegexPart + ")?";
        public const String SearchRegexPart = @"(((\w|\+|\%|\-|_)+:)?(\w|\*|\+|\%|\-|\ |_|\.|\(|\)|\\|\/|\@)+,?)+";
        public const String ParametersRegexPart = @"((&\w+=\w+)+)?";
        
        public static Boolean CheckSearchRequest(String search)
        {
            try
            {
                const String matchPattern = "^" + SearchSiteRegexPart + SearchRegexPart + ParametersRegexPart + "$";
                
                return StringUtils.CheckWellFormed(search) && Regex.IsMatch(Uri.UnescapeDataString(search),
                           matchPattern, RegexOptions.Compiled,
                           new TimeSpan(0, 0, 3));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        
        public static String CastSearchRequest(String search)
        {
            return CheckSearchRequest(search) ? Regex.Replace(search, @"(^.+(?=\/\w+\?q)\/|\w+\?q=|&.+$)", String.Empty, RegexOptions.IgnoreCase) : null;
        }
        
        public static String CastSearchRequest(String search, Boolean escape)
        {
            search = CastSearchRequest(search);
            
            if (String.IsNullOrEmpty(search))
            {
                return null;
            }
            
            return escape ? Uri.EscapeDataString(search) : search;
        }
        
        private static readonly Dictionary<String, Object> ReplaceDictionary = new Dictionary<String, Object>
        {
            {"-dash-", "-"},
            {"%2B", "+"}
        };
        
        public static String CastToDerpiSearch(String search)
        {
            return Uri.EscapeDataString(CastSearchRequest(search)?.Replace("%", String.Empty) ?? String.Empty).ReplaceFromDictionary(ReplaceDictionary);
        }

        public static String CastToAPISearchRequest(String search, Int32 page = 1, String apiKey = null)
        {
            search = CastToDerpiSearch(search);
            return String.IsNullOrEmpty(search) ? CastToAPIMyWatchedRequest(page, apiKey) : CastToAPISearch(search, page, apiKey);
        }
        
        public static String CastToAPIMyWatchedRequest(Int32 page = 1, String apiKey = null)
        {
            return $"{ProgramLocalization.DerpiBooruLink}/images/watched.json?key={apiKey ?? Globals.APIKey}&page={page}";
        }

        public static String CastToAPISearch(String search, Int32 page = 1, String apiKey = null)
        {
            return $"{ProgramLocalization.DerpiBooruLink}/search.json?q={search}&key={apiKey ?? Globals.APIKey}&page={page}";
        }
        
        public const Int32 LengthAPI = 20;
        private static readonly String PatternAPI = $@"^[a-zA-Z0-9]{{{LengthAPI}}}$";

        public static Boolean CheckAPI()
        {
            return CheckAPI(Globals.APIKey);
        }
        
        public static Boolean CheckAPI(String api)
        {
            return !String.IsNullOrEmpty(api) && Regex.IsMatch(api, PatternAPI);
        }
        
        public static async Task<Boolean> CheckValidAPIAsync(String apiKey)
        {
            return CheckAPI(apiKey) && await JsonAPI.GetJsonMyWatchedAsync(apiKey, 1, false).ConfigureAwait(true) != null;
        }
    }
}