
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using ServicesAbstractions;
using Shared;
using Store.Api.Extensions;
using Store.Api.Middlewares;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration); // Register all services



            var app = builder.Build();


            // After build
            // Configure the HTTP request pipeline.


            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
