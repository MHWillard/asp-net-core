using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace WebApplication1
{
    public class Program
    {
        void DeleteFruit(string id)
        {
            Fruit.All.Remove(id);
        }
        record Fruit(string Name, int Stock)
        {
            public static readonly Dictionary<string, Fruit> All = new();
        };
        class Handlers
        {
            public void ReplaceFruit(string id, Fruit fruit)
            {
                Fruit.All[id] = fruit;
            }
            public static void AddFruit(string id, Fruit fruit)
            {
                Fruit.All.Add(id, fruit);
            }
        }
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
                app.UseExceptionHandler("/error");
            };

            //Adding more middleware in order to make a pipeline. 
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            //5. Map the endpoints in your application.
            var getFruit = (string id) => Fruit.All[id];
            Handlers handlers = new();

            app.MapGet("/fruit", () => Fruit.All);
            app.MapGet("/fruit/{id}", getFruit);
            app.MapPost("/fruit/{id}", Handlers.AddFruit);
            app.MapPut("/fruit/{id}", handlers.ReplaceFruit);
            app.MapDelete("/fruit/{id}", DeleteFruit);

            //6. Call Run() on the WebApplication to start the server and handle requests.
            app.Run();

            
        }
    }
}
