using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1;

namespace ShotclockPiReader.UnitTests
{
    [TestClass]
    public class GetResponseTests
    {
        [TestMethod]
        public void Test1()
        {
            // Arrange
            const string uri = "http://192.168.1.102:8080/time";
            Console.WriteLine($"Reading from {uri}");

            for (var i = 0; i < 100; i++)
            {
                // Act
                var json = RestApiReader.GetTime(uri);

                // Assert
                Console.WriteLine($"{i} {DateTime.Now} {json}");
            }
        }
    }
}