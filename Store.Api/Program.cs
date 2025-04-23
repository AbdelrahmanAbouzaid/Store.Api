using Domain.Contracts;
using Store.Api.Middlewares;
using Store.Api.Extentions;

namespace Store.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
