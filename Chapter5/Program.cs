using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace WebApplication1
{
    public class Program
    {
        public record Person(string FirstName, string LastName) { }
        public static void Main(string[] args)
        {
            //Add data to pull from an endpoint.
            var people = new List<Person> {
new("Tom", "Hanks"),
new("Denzel", "Washington"),
new("Leondardo", "DiCaprio"),
new("Al", "Pacino"),
new("Morgan", "Freeman"),
            };

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
                app.UseExceptionHandler("/error");
            };

            //Adding more middleware in order to make a pipeline. 
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            //5. Map the endpoints in your application.
            app.MapGet("/person/{name}", (string name) => people.Where(p => p.FirstName.StartsWith(name)));

            //6. Call Run() on the WebApplication to start the server and handle requests.
            app.Run();

            
        }
    }
}
