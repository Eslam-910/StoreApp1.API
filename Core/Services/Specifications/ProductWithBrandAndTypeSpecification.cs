using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications:BaseSpecification<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id==id)
        {
            ApplyIncludes();
        }
        public ProductWithBrandAndTypeSpecifications(ProductspecificationsParameters specparams) :base
            (
                P=>
                (string.IsNullOrEmpty(specparams.Search)||P.Name.ToLower().Contains(specparams.Search.ToLower()))&&
                (!specparams.BrandId.HasValue||P.BrandId==specparams.BrandId)
                &&(!specparams.TypeId.HasValue||P.TypeId==specparams.TypeId)
            
            )
        {
            ApplyIncludes();
            ApplySort(specparams.Sort);
            ApplyPagination(specparams.PageIndex,specparams.PageSize);
            
        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        private void ApplySort(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrederBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrederBy(p => p.Name);
                        break;
                }
            }
        }

        
    }
}
