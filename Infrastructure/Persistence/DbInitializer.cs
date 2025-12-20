using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer( StoreDbContext context)
        {
            _context = context;
        }
       public async Task InitializeAsync()
        {
            try
            { 
                //Create Database It Doesn't Exists && Apply To Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                //Data Seeding
                //Seeding ProductType From Json File
                if (!_context.ProductTypes.Any())
                {
                    //1.Read All Data From types Json File as String
                    var typeData = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistence\\Data\\Seeding\\types.json");
                    //2.Transform String To C# Objects [List<ProductTypes>]
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    //3.Add List<ProductTypes> To Database
                    if (Types is not null && Types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(Types);
                        await _context.SaveChangesAsync();
                    }

                }




                //Seeding ProductBrand From Json File

                if (!_context.ProductBrands.Any())
                {
                    //1.Read All Data From Brands Json File as String
                    var typedata=await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    //..\Infrastructure\Persistence\Data\Seeding\brands.json

                    //2.Transform String To C# Objects [List<ProductBrands>]
                   var types= JsonSerializer.Deserialize<List<ProductBrand>>(typedata);

                    //3.Add List<ProductTypes> To Database
                    if(types is not null && types.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }


                }

                //Seeding Products From Json File


                if (!_context.Products.Any())
                {
                    //1.Read All Data From Brands Json File as String
                    var productData = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistence\\Data\\Seeding\\products.json");
                    //2.Transform String To C# Objects [List<ProductTypes>]
                    var product = JsonSerializer.Deserialize<List<Product>>(productData);
                    //3.Add List<ProductTypes> To Database
                    if (product is not null && product.Any())
                    {
                        await _context.Products.AddRangeAsync(product);
                        await _context.SaveChangesAsync();
                    }

                }

            }
            catch
            {
                throw;
            }
        }
    }
}
