using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using ProplanetReplicationTool.Data;
using ProplanetReplicationTool.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using ProplanetReplicationTool.Services.Interfaces;
using ProplanetReplicationTool.Services;

namespace ProplanetReplicationTool
{
    // Program class has to be public always, it does not accepts protected, private or static
    /// <summary>
    /// Program class of the application, where the main method is located, here the application is configured and the services are added
    /// </summary>
    public class Program
    {
        ///<summary>
        /// Main method of the application, this is the entry point of the application
        ///</summary>
        ///<param name="args">Arguments that are going to be passed to the application</param>
        public static void Main(string[] args)
        {
            //Declare variables
            string? connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTIONSTRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("POSTGRES_CONNECTIONSTRING environment variable is not set");
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add the database context to the application
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Transient);

            //DBContextFactory, this means that we can create a new instance of the context whenever we need it
            builder.Services.AddDbContextFactory<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                }, ServiceLifetime.Transient);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<INovaService, NovaService>();
            builder.Services.AddBlazoredToast();
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            // All the services that are going to be used in the application
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            var app = builder.Build();

            if (app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllers();
            CreateDbIfNotExists(app);
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        /// <param name="host">The instance of the host that is going to be used to create the database</param>
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    DbInitializer.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}