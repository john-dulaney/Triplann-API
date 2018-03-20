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
                var userstore = new UserStore<ApplicationUser> (context);
                ApplicationUser user;

                if (userstore.FindByNameAsync ("Chaz") == null) {
                    user =
                    new ApplicationUser {
                    FirstName = "Chaz",
                    LastName = "Vanderbilt",
                        };

                    var passwordHash = new PasswordHasher<ApplicationUser> ();
                    user.PasswordHash = passwordHash.HashPassword (user, "Abc123!");
                    await userstore.CreateAsync (user);

                }
                context.SaveChanges ();

                if (!context.TripType.Any ()) {
                    var TripTypes = new TripType[] {
                        new TripType {
                        WeatherType = "Snow",
                        TravelMethod= "Car",
                        ActivityType = "Skii",
                        UserId = "1"
                        },
                        new TripType {
                        WeatherType = "Tropical",
                        TravelMethod= "Plane",
                        ActivityType = "Relax/Vacation",
                        UserId = "2"
                        },
                        new TripType {
                        WeatherType = "Temperate",
                        TravelMethod= "Car",
                        ActivityType = "Business",
                        UserId = "1"
                        },
                        new TripType {
                        WeatherType = "Temperate",
                        TravelMethod= "Car",
                        ActivityType = "Hike/Climb",
                        UserId = "3"
                        }
                    };

                    foreach (TripType tt in TripTypes) {
                        context.TripType.Add (tt);
                    }
                    context.SaveChanges ();
                }
                 if (!context.Trip.Any ()) {
                    var Trips = new Trip[] {
                        new Trip {
                        Location = "Alaska",
                        Duration = "1 Week",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        User = context.ApplicationUser.Single (u => u.FirstName == "Chaz")
                        },
                        new Trip {
                        Location = "Belize",
                        Duration = "2 Week",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Relax/Vacation").TripTypeId,
                        User = context.ApplicationUser.Single (u => u.FirstName == "Marko")
                        },
                        new Trip {
                        Location = "Denmark",
                        Duration = "3 Week",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Business").TripTypeId,
                        User = context.ApplicationUser.Single (u => u.FirstName == "Steve")
                        },
                        new Trip {
                        Location = "Nashville",
                        Duration = "4 Week",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Business").TripTypeId,
                        User = context.ApplicationUser.Single (u => u.FirstName == "John")
                        },
                    };

                    foreach (Trip t in Trips) {
                        context.Trip.Add (t);
                    }

                    context.SaveChanges ();
                }

                if (!context.ChecklistItem.Any ()) {
                    var ChecklistItems = new ChecklistItem[] {
                        new ChecklistItem {
                        ChecklistAction = "Lock your house.",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Toothbrush",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Business").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Skii Jacket",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Skii Pants",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Sandals",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Relax/Vacation").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Swimsuit",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Relax/Vacation").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "SKii Boots",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Sunscreen",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Relax/Vacation").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Hair Dryer",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                        new ChecklistItem {
                        ChecklistAction = "Fleece Jacket",
                        TripTypeId = context.TripType.Single (t => t.ActivityType == "Skii").TripTypeId,
                        },
                    };

                    foreach (ChecklistItem c in ChecklistItems) {
                        context.ChecklistItem.Add (c);
                    }
                    context.SaveChanges ();
                }

               
            }
        }
    }
};