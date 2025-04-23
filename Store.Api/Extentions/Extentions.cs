using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ModelErrors;
using Store.Api.Middlewares;

namespace Store.Api.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices();

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                        .Select(m => new ValidationError()
                                        {
                                            Feild = m.Key,
                                            Errors = m.Value.Errors.Select(m => m.ErrorMessage)
                                        });
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }


        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            app.AddSeedingMiddlewares();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            return app;
        }

        private static WebApplication AddSeedingMiddlewares(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.InitializeAsync().Wait();
            return app;
        }
    }
}
