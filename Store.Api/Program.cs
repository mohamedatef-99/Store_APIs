
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using ServicesAbstractions;
using Store.Api.Middlewares;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbInitializer, DbInitializer>(); // Alow DI for DbInitializer
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Allow DI for UnitOfWork
            builder.Services.AddScoped<IServiceManager, ServiceManager>(); // Allow DI for ServiceManager
            builder.Services.AddAutoMapper(typeof(AssembleReference).Assembly); // Allow DI for AutoMapper


            var app = builder.Build();

            #region Seeding
            // Code for seeding the database
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask clr create object from DbInitializer
            await dbInitializer.InitializedAsync(); 
            #endregion

            app.UseMiddleware<GlobalErrorHandlingMiddleware>(); // Register the global error handling middleware

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

            app.Run();
        }
    }
}
