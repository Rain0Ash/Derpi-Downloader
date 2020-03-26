// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common_Library;
using Common_Library.Utils.Network;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;
using Newtonsoft.Json;

namespace Derpi_Downloader.Json
{
    public static class JsonAPI
    {
        public static event Handlers.HttpStatusCodeHandler OnExceptionResponce;

        public static Task<String> GetJsonMyWatchedAsync(String apiKey = null, Int32 page = 1, Boolean isInvoke = true, CancellationToken token = default)
        {
            return GetJsonAsync(DerpiAPI.CastToAPIMyWatchedRequest(page, apiKey), isInvoke, token);
        }

        public static Task<String> GetJsonForSearchAsync(String search, Int32 page = 1, String apiKey = null, Boolean isInvoke = true,
            CancellationToken token = default)
        {
            return GetJsonAsync(DerpiAPI.CastToAPISearchRequest(search, page, apiKey), isInvoke, token);
        }

        private static async Task<String> GetJsonAsync(String apiRequest, Boolean isInvoke, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(apiRequest))
            {
                return null;
            }

            try
            {
                using WebClient client = new WebClient
                {
                    Encoding = Encoding.UTF8,
                    Proxy = Globals.WebProxy
                };

                return await client.DownloadStringTaskAsync(apiRequest, token).ConfigureAwait(true);
            }
            catch (WebException ex)
            {
                switch (ex.Response)
                {
                    case HttpWebResponse response when isInvoke:
                        OnExceptionResponce?.Invoke(response.StatusCode);
                        break;
                    default:
                        return null;
                }
            }

            return null;
        }

        public static async Task<DerpiImage> GetDerpiImageAsync(String search, Int32 page = 1, String apiKey = null, Boolean isInvoke = true,
            CancellationToken token = default)
        {
            String json = await GetJsonForSearchAsync(search, page, apiKey, isInvoke, token).ConfigureAwait(true);
            return json == null ? null : JsonConvert.DeserializeObject<DerpiImage>(json);
        }
    }
}