using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using EmptyApp;

namespace EmptyApp.IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            // _server = new TestServer(WebHost.CreateDefaultBuilder(null).UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public void RootShouldReturnHelloWorld()
        {
            var response = _client.GetAsync("/").Result;
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

            var content = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual("Hello World!", content);
        }
    }
}
