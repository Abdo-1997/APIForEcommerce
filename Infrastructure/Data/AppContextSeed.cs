using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext ,ILoggerFactory loggerFactory)
        {
            try
            {
                if (!dbContext.ProductBrands.Any())
                {
                    var BrandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    foreach (var brand in brands)
                    {
                       dbContext.ProductBrands.Add(brand); 
                    }
                    await dbContext.SaveChangesAsync();
                }  
             
                if (!dbContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                      await  dbContext.ProductTypes.AddAsync(type); 
                    }
                    await dbContext.SaveChangesAsync();

                }
                if (!dbContext.Products.Any())
                {
                
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        dbContext.Products.Add(product);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppContextSeed>();
                logger.LogError(ex, "error occured when trying seeding data");
            }


        }  
    }
}
