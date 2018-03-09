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

                // ****
                // Ask about this Tomorrow
                // var userstore = new UserStore<ApplicationUser> (context);
                // **** 

                if (!context.ApplicationUser.Any ()) {
                    var ApplicationUsers = new ApplicationUser[] {
                        new ApplicationUser {
                        UserId = 1,
                        FirstName = "Chaz",
                        LastName = "Vanderbilt",
                        },
                        new ApplicationUser {
                        UserId = 2,
                        FirstName = "Peyton",
                        LastName = "FuckingManning",
                        },
                        new ApplicationUser {
                        UserId = 3,
                        FirstName = "Chazzette",
                        LastName = "Belmont",
                        }
                    };
                    foreach (ApplicationUser u in ApplicationUsers) {
                        context.ApplicationUser.Add (u);
                    }
                    context.SaveChanges ();
                }

                if (!context.ChecklistItem.Any ()) {
                    var ChecklistItems = new ChecklistItem[] {
                        new ChecklistItem {
                        ChecklistAction = "Lock your house.",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Toothbrush",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Skii Jacket",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Skii Pants",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Sandals",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Swimsuit",
                        },
                        new ChecklistItem {
                        ChecklistAction = "SKii Boots",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Sunscreen",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Hair Dryer",
                        },
                        new ChecklistItem {
                        ChecklistAction = "Fleece Jacket",
                        },
                    };

                    foreach (ChecklistItem c in ChecklistItems) {
                        context.ChecklistItem.Add (c);
                    }
                    context.SaveChanges ();
                }

                if (!context.TripType.Any ()) {
                    var TripTypes = new TripType[] {
                        new TripType {
                        ActivityType = "Skii"
                        },
                        new TripType {
                        ActivityType = "Relax/Vacation"
                        },
                        new TripType {
                        ActivityType = "Business"
                        },
                        new TripType {
                        ActivityType = "Hike/Climb"
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

            }
        }
    }
}