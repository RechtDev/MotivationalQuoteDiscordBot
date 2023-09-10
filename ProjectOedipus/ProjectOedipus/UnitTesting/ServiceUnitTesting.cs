using ProjectOedipus.Models.Responses;
using Moq;
using NUnit.Framework;
using ProjectOedipus.Commands;
using ProjectOedipus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Contrib.HttpClient;
using ProjectOedipus.Common;
using Microsoft.Extensions.Configuration;

namespace ProjectOedipus.UnitTesting
{
    [TestFixture]
    public class ServiceUnitTesting
    {
        [Test]
        public async Task CanGetQuote()
        {
            // Arrange
            
            QuoteResponse expected = new() { Author = "Kahlil Gibran", Quote = "Tenderness and kindness are not signs of weakness and despair, but manifestations of strength and resolution." };
         
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var configMock = new Mock<IConfiguration>();

            handler.SetupRequest(HttpMethod.Get, @"https://zenquotes.io/api/today/Success")
            .ReturnsJsonResponse(expected);

            var client = handler.CreateClient();
            client.BaseAddress = Utility.CreateUri(@"https://zenquotes.io/api/today");

            IQuoteService service = new ZenQuoteService(client, configMock.Object);
            IQuoteCommand command = new QuoteCommand(service);

            // Act

            var result = await command.Execute(Common.Enums.QuoteType.inspiration);

            // Assert

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Quote, result.Quote);
                Assert.AreEqual(expected.Author, result.Author);
            });
        }
    }

}
