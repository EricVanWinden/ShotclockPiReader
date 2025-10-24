using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class RestApiReader
    {
        private static readonly HttpClient _httpClient = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            return client;
        }

        /// <summary>
        /// Calls the ShotclockPi rest-api and gets the time.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>Response body as string.</returns>
        public static async Task<string> GetTimeAsync(string uri, CancellationToken cancellationToken)
        {          
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronous convenience wrapper. Blocks until the async call completes.
        /// </summary>
        public static string GetTime(string uri, CancellationToken cancellationToken = default)
        {
            return GetTimeAsync(uri, cancellationToken).GetAwaiter().GetResult();
        }
    }
}
