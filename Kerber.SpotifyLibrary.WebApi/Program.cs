using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Kerber.SpotifyLibrary.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                BuildWebHost(args).Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
