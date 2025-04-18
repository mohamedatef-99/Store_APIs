using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public int BrandId { get; set; } // Fk
        public ProductBrand ProductBrand { get; set; } // Navigation property
        
        public int TypeId { get; set; } // FK
        public ProductType ProductType { get; set; } // Navigation property
    }
}