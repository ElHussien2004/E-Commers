using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModules;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModules;
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
    public class DataSeeding(StoreDbContext _dbContext ,UserManager<ApplicationUser> _userManager                           
        ,RoleManager<IdentityRole> _roleManager
        , StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
    

        async Task IDataSeeding.DataSeedingAsync()
        {
            try
            {
                var Pending_migration = await _dbContext.Database.GetPendingMigrationsAsync();

                if (Pending_migration.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrand = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\brands.json");
                    //convert string => c# Objects
                    var pr =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrand);

                    if (pr is not null && pr.Any()) 
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(pr);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var PType = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\types.json");
                    //convert string =>c# Object 
                    var LPType = await JsonSerializer.DeserializeAsync<List<ProductType>>(PType);

                    if (LPType is not null && LPType.Any())
                    {
                       await _dbContext.ProductTypes.AddRangeAsync(LPType);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var Pro = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\products.json");
                    //convert string =>C# Object 
                    var Lpro = await JsonSerializer.DeserializeAsync<List<Product>>(Pro);

                    if (Lpro is not null && Lpro.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Lpro);
                    }
                }
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var Pro = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\delivery.json");
                    //convert string =>C# Object 
                    var Lpro = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(Pro);

                    if (Lpro is not null && Lpro.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(Lpro);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
              //TODO
            }

        }

        public async Task IdentityDataSeedingAsync()
        {
            try
            {
               if(!_roleManager.Roles.Any())
               {
                  await _roleManager.CreateAsync(new IdentityRole ("Admin"));
                  await  _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

               }
               if(!_userManager.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        Email="Mohamed@gmail.com",
                        UserName="MohamedTarek",
                        PhoneNumber="0123456789",
                        DisplayName="Mohamed Tarek"

                    };
                    var User2 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        UserName = "SalmaMohamed",
                        PhoneNumber = "0123456789",
                        DisplayName = "Salma Mohamed"

                    };
                    await _userManager.CreateAsync(User1 ,"P@ssw0rd");
                    await _userManager.CreateAsync(User2, "P@ssw0rd");
                     await _userManager.AddToRoleAsync(User1, "Admin");
                    await _userManager.AddToRoleAsync(User2, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
