using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShotclockPiReader.Messaging
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
        /// Synchronous convenience wrapper. Blocks until the async call completes.
        /// </summary>
        /// <param name="uri">The uri (e.g. http://192.168.1.102:8080/time)</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>Response body as string.</returns>
        public static string GetMessage(string uri, CancellationToken cancellationToken = default)
        {
            return GetMessageAsync(uri, cancellationToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Calls the ShotclockPi rest-api and gets the time.
        /// </summary>
        /// <param name="uri">The uri (e.g. http://192.168.1.102:8080/time)</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>Response body as string.</returns>
        public static async Task<string> GetMessageAsync(string uri, CancellationToken cancellationToken = default)
        {          
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronous convenience wrapper. Blocks until the async call completes.
        /// </summary>
        /// <param name="uri">The uri (e.g. https://livescore.easingyou.com/api/shotclock)</param>
        /// <param name="uuid">The uuid (e.g. d19c485f-98a9-4cbb-853d-6ff68d21a050 for TOP)</param>
        /// <param name="message">The message to send</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if a valid (successful) HTTP response is received, otherwise false.</returns>
        public static bool PostMessage(string uri, string uuid, string message, CancellationToken cancellationToken = default)
        {
            return PostMessageAsync(uri, uuid, message, cancellationToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Send the time to the provided uri and uuid.
        /// </summary>
        /// <param name="uri">The uri (e.g. https://livescore.easingyou.com/api/shotclock)</param>
        /// <param name="uuid">The uuid (e.g. d19c485f-98a9-4cbb-853d-6ff68d21a050 for TOP)</param>
        /// <param name="message">The message to send</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if a valid (successful) HTTP response is received, otherwise false.</returns>
        public static async Task<bool> PostMessageAsync(string uri, string uuid, string message, CancellationToken cancellationToken = default)
        {
            var finalUri = $"{(uri).TrimEnd('/')}/{uuid}";

            try
            {
                using var content = new StringContent(message, Encoding.UTF8, "text/plain");
                using var request = new HttpRequestMessage(HttpMethod.Post, finalUri) { Content = content };
                using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Exception during SendMessage: {exception.Message}");
                return false;
            }
        }
    }
}