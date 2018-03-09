using Microsoft.EntityFrameworkCore;
using Triplann.Models;

namespace Triplann.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TripType> TripType { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<ChecklistItem> ChecklistItem { get; set; }


    }
}