using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Triplann.Models;

namespace Triplann.Data {
    public static class DbInitializer {
        public async static void Initialize (IServiceProvider serviceProvider) {
            using (var context = new ApplicationDbContext (serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>> ())) {
                var roleStore = new RoleStore<IdentityRole> (context);
                var userstore = new UserStore<ApplicationUser> (context);

                if (!context.Roles.Any (r => r.Name == "Administrator")) {
                    var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                    await roleStore.CreateAsync (role);
                }

                if (!context.TripType.Any ()) {
                    var TripTypes = new TripType[] {
                        new TripType {
                            PolarVeryCold = "",
                            PolarMildCold = "",
                            AridVeryHot = "",
                            AridCold = "",
                            TropicalModerate = "",
                            TropicalVeryHot = "",
                            TemperateVeryHot = "",
                            TemperateCold = ""
                        },
                        new TripType {
                            
                        }
                    };

                    foreach (TripType i in TripTypes) {
                        context.TripType.Add (i);
                    }
                    context.SaveChanges ();
                }

                if (!context.ProductType.Any ()) {
                    var productTypes = new ProductType[] {
                        new ProductType {
                        Label = "Electronics"
                        },
                        new ProductType {
                        Label = "Appliances"
                        },
                        new ProductType {
                        Label = "Sporting Goods"
                        },
                        new ProductType {
                        Label = "Housewares"
                        },
                    };

                    foreach (ProductType i in productTypes) {
                        context.ProductType.Add (i);
                    }
                    context.SaveChanges ();
                }

                if (!context.Product.Any ()) {
                    var products = new Product[] {
                        new Product {
                        Title = "Kite",
                        Description = "It flies high",
                        Price = 9.99,
                        ProductType = context.ProductType.Single (t => t.Label == "Sporting Goods"),
                        User = context.ApplicationUser.Single (u => u.Email == "admin@admin.com")
                        },
                        new Product {
                        Title = "Curtains",
                        Description = "They make it dark",
                        Price = 140.00,
                        ProductTypeId = context.ProductType.Single (t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single (u => u.Email == "admin@admin.com")
                        },
                        new Product {
                        Title = "Macbook Pro",
                        Description = "It's powerful",
                        Price = 1278.00,
                        ProductTypeId = context.ProductType.Single (t => t.Label == "Electronics").ProductTypeId,
                        User = context.ApplicationUser.Single (u => u.Email == "admin@admin.com")
                        },
                        new Product {
                        Title = "Refrigerator",
                        Description = "It keep things cool",
                        Price = 1149.00,
                        ProductTypeId = context.ProductType.Single (t => t.Label == "Appliances").ProductTypeId,
                        User = context.ApplicationUser.Single (u => u.Email == "admin@admin.com")
                        },
                    };

                    foreach (Product i in products) {
                        context.Product.Add (i);
                    }

                    context.SaveChanges ();
                }

            }
        }
    }
}