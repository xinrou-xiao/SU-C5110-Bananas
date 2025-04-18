using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// The entry point for the application.
    /// This class contains the Main method that starts the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that serves as the application's entry point.
        /// It creates and runs the web host for the application.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {

            // Create the host builder, build it, and run the application
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a host builder for the web application with default settings.
        /// Configures the web host to use the specified Startup class.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        /// <returns>An IHostBuilder instance configured for the application.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                // Configure the web host to use the default settings and specified Startup class
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specify the Startup class to be used for configuring services and the request pipeline
                    webBuilder.UseStartup<Startup>();
                });
    }
}