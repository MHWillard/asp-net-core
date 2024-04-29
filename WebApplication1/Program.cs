using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1
{
    public class Program
    {
        public record Person(string FirstName, string LastName) { }
        public static void Main(string[] args)
        {

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);
            builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseHttpLogging();
            };

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/person", () => new Person("Mister", "Johnson"));

            app.Run();

            
        }
    }
}

/*
 * ASP.NET Core entry point: 
1. Create a WebApplicationBuilder instance.
2. Register the required services and configuration with
the WebApplicationBuilder.
3. Call Build() on the builder instance to create a
WebApplication instance.
4. Add middleware to the WebApplication to
create a pipeline.
5. Map the endpoints in your application.
6. Call Run() on the WebApplication to start
the server and handle requests.
 */
