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
            //1.Create a WebApplicationBuilder instance.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            //2. Register the required services and configuration with the WebApplicationBuilder.
            builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);
            builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

            //3. Call Build() on the builder instance to create a WebApplication instance.
            WebApplication app = builder.Build();

            //4. Add middleware to the WebApplication to create a pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseHttpLogging();
            };

            //Adding more middleware in order to make a pipeline. 
            app.UseWelcomePage("/");
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            //5. Map the endpoints in your application.
            app.MapGet("/", () => "Hello World!");
            app.MapGet("/person", () => new Person("Mister", "Johnson"));

            //6. Call Run() on the WebApplication to start the server and handle requests.
            app.Run();

            
        }
    }
}
