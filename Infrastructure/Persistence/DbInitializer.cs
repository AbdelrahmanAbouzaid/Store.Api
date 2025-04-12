using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreContext context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            //Create database if it dosen't exist and apply to any pending migration
            if (context.Database.GetPendingMigrations().Any())
                await context.Database.MigrateAsync();

            //Data Seeding
            //seeding ProductType from json file
            if (!context.ProductTypes.Any())
            {
                //reading data from file as json string
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                //transform strings to c# objects
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Any())
                {
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }

            //seeding ProductBrand from json file
            if (!context.ProductBrands.Any())
            {
                //reading data from file as json string
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                //transform strings to c# objects
                var types = JsonSerializer.Deserialize<List<ProductBrand>>(typesData);

                if (types is not null && types.Any())
                {
                    await context.ProductBrands.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }

            //seeding Product from json file
            if (!context.Products.Any())
            {
                //reading data from file as json string
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                //transform strings to c# objects
                var types = JsonSerializer.Deserialize<List<Product>>(typesData);

                if (types is not null && types.Any())
                {
                    await context.Products.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
