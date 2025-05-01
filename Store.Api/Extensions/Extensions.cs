using Microsoft.AspNetCore.Mvc;
using Shared;
using Persistence;
using Services;
using Domain.Contracts;
using Store.Api.Middlewares;

namespace Store.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register all services  
            services.AddBuiltInServices(); // Register Built in services

            services.AddSwaggerServices(); // Register Swagger Services
            

            // Ensure the AddInfrastructureServices method is available by referencing the correct namespace  
            services.AddInfrastructureServices(configuration); // Corrected method name  
            services.AddApplicationServices(); // Add the application services  

            
            services.ConfigureService(); // Configure any services


            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            // Register Builtin services  
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new ValidationError()
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
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


        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            await app.InitializeDatabaseAsync();

            app.UseGlobalErrorHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(); // Midelware for serving static files

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> InitializeDatabaseAsync (this WebApplication app)
        {

            #region Seeding
            // Code for seeding the database
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask clr create object from DbInitializer
            await dbInitializer.InitializedAsync();
            #endregion

            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {

            app.UseMiddleware<GlobalErrorHandlingMiddleware>(); // Register the global error handling middleware

            return app;
        }
    }
}
