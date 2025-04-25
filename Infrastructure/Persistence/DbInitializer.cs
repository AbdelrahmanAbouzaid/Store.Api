using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreContext context,
        StoreIdentityDbContext identityDbContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager) 
        : IDbInitializer
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

        public async Task InitializeIdentityAsync()
        {
            if (identityDbContext.Database.GetPendingMigrations().Any())
            {
                await identityDbContext.Database.MigrateAsync();
            }

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin"});
                await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin"});
            }

            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01234567893",

                };
                var superAdmin = new AppUser
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01234567893",

                };

                await userManager.CreateAsync(admin, "P@ssw0rd");
                await userManager.CreateAsync(superAdmin, "P@ssw0rd");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            }
        }
    }
}
