using System;
using System.Text.Json.Serialization;

namespace ShotclockPiReader.Converter
{
    /// <summary>
    /// The message returned by the ShotclockPi REST API.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// The time remaining on the shot clock in seconds.
        /// </summary>
        [JsonPropertyName("time")]
        public double? Time { get; set; }

        /// <summary>
        /// The UTC timestamp of the reading in ISO8601 format.
        /// </summary>
        [JsonPropertyName("utc_time_stamp")]
        public string UtcTimeStamp { get; set; } = DateTime.UtcNow.ToUniversalTime().ToString("u").Replace(" ", "T");

        /// <summary>
        /// The current status of the shotclock.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
