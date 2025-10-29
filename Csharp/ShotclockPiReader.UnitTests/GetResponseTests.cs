using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShotclockPiReader.Converter;
using ShotclockPiReader.Messaging;
using System;
using System.Text.Json;

namespace ShotclockPiReader.UnitTests
{
    [TestClass]
    public class GetResponseTests
    {
        [TestMethod]
        public void Read100MessagesTest()
        {
            // Arrange
            const string uri = "http://192.168.1.102:8080/time";
            Console.WriteLine($"Reading from {uri}");

            for (var i = 0; i < 100; i++)
            {
                // Act
                var json = RestApiReader.GetMessage(uri);

                // Assert
                Console.WriteLine($"{i} {DateTime.Now} {json}");
            }
        }

        [TestMethod]
        public void ConvertJsonTest1()
        {
            // Arrange
            var json = "{\"status\": \"OK\",\"time\": 17.77646541595459}";

            // Act
            var response = JsonSerializer.Deserialize<ApiResponse>(json);

            // Assert
            Assert.IsNotNull(response);
            Console.WriteLine($"Status: {response.Status}, Time: {response.Time}, UtcTimeStamp: {response.UtcTimeStamp}");
        }

        [TestMethod]
        public void ConvertJsonTest2()
        {
            // Arrange
            var json = "{\"status\": \"OK\",\"time\": 17.77646541595459, \"utc_time_stamp\": \"dummy\"}";

            // Act
            var response = JsonSerializer.Deserialize<ApiResponse>(json);

            // Assert
            Assert.IsNotNull(response);
            Console.WriteLine($"Status: {response.Status}, Time: {response.Time}, UtcTimeStamp: {response.UtcTimeStamp}");
        }
    }
}