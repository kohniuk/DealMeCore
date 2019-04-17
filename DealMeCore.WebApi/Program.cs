using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DealMeCore.WebApi
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">Array of arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        /// <summary>
        /// Create web host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration();
    }
}
