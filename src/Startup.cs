using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// Configures services and the request pipeline for the web application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes the Startup class with configuration.
        /// </summary>
        /// <param name="configuration">App configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            // Store configuration
            Configuration = configuration;
        }

        // Configuration property
        public IConfiguration Configuration { get; }

       /// <summary>
       /// Method called by the runtime to add services to the DI container. 
       /// </summary>
       /// <param name="services">Service collection for dependency</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Razor Pages with runtime compilation
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Add Blazor support
            services.AddServerSideBlazor();

            // Add HTTP Client
            services.AddHttpClient();

            // Add MVC controllers
            services.AddControllers();

            // Register application Service
            services.AddTransient<JsonFileProductService>();
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Development error page
                app.UseDeveloperExceptionPage();
            }
                // Production error handler
                app.UseExceptionHandler("/Error");

            // redirect to error page on page not found
            app.UseStatusCodePagesWithRedirects("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable serving static files
            app.UseStaticFiles();

            // Set up request routing
            app.UseRouting();

            // Enable authorization middleware
            app.UseAuthorization();

            // Map endpoints for various components
            app.UseEndpoints(endpoints =>
            {
                // Route for Razor Pages
                endpoints.MapRazorPages();

                // Route for API controllers
                endpoints.MapControllers();

                // Route for Blazor Hub
                endpoints.MapBlazorHub();

                // Uncomment to provide custom endpoint for product data
                // endpoints.MapGet("/products", (context) => 
                // {
                //     var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                //     var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
                //     return context.Response.WriteAsync(json);
                // });
            });
        }
    }
}