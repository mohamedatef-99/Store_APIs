using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add your application services here
            // e.g. services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(AssembleReference).Assembly); // Allow DI for AutoMapper
            services.AddScoped<IServiceManager, ServiceManager>(); // Allow DI for ServiceManager


            return services;
        }
    }
}
