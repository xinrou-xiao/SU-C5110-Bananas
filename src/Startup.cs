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
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Razor Pages
            // Enable runtime compilation
            services.AddRazorPages().AddRazorRuntimeCompilation();
            // Add Blazor support
            services.AddServerSideBlazor();
            // Add HTTP Client
            services.AddHttpClient();
            // Add MVC controllers
            services.AddControllers();
            // Register Service
            services.AddTransient<JsonFileProductService>();
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
  
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

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
