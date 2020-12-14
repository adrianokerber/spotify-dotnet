using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Kerber.SpotifyLibrary.Specs.ServerIntegration
{
    public static class TestServerHelper
    {
        public static TestServer CreateTestServer(Action<IServiceCollection> services)
        {
            var host = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<TestStartup>()
                .ConfigureServices(services);
            return new TestServer(host);
        }
    }
}
