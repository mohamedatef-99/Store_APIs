﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }

        public ProductWithBrandsAndTypesSpecifications(int? brandId, int? typeId, string? sort, int pageIndex, int pageSize) : base
            (
                P => (!brandId.HasValue || P.BrandId == brandId) && (!typeId.HasValue || P.TypeId == typeId)
            )
        {
            ApplyInclude();
            ApplySorting(sort);
            ApplyPagination(pageIndex, pageSize);

        }
        private void ApplyInclude()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);

            }
        }
    }
}
