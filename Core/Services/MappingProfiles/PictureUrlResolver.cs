using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            //source.PictureUrl = source.PictureUrl ?? string.Empty; // Check if PictureUrl is null
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty; // Reutrn empty string if PictureUrl is null or empty
            return $"{configuration["BaseUrl"]}/{source.PictureUrl}"; // Return the URL
        }
    }
}
