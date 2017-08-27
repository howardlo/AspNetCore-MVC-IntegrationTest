using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace WebApplicationMvc.IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationTest()
        {
            var wwwroot = new FileInfo(@"../../../../WebApplicationMvcApp"); // TODO: there might be a better way

            var contentRoot = wwwroot.FullName;
            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseEnvironment("Develoment")
                .UseStartup<Startup>();
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [TestMethod]
        public void Banner1SvgShouldBeReturned()
        {
            var response = _client.GetAsync("/images/banner1.svg").Result;
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            var content = response.Content.ReadAsByteArrayAsync().Result;
            Assert.AreEqual(9679, content.Length);
        }

    }
}
