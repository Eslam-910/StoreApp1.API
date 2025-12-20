using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecification:BaseSpecification<Product,int>
    {
        public ProductWithCountSpecification(ProductspecificationsParameters specparams) :base(

                P =>
                (string.IsNullOrEmpty(specparams.Search) || P.Name.ToLower().Contains(specparams.Search.ToLower())) &&
                (!specparams.BrandId.HasValue || P.BrandId == specparams.BrandId)
                && (!specparams.TypeId.HasValue || P.TypeId == specparams.TypeId)
            )
        {
            
        }
    }
}
