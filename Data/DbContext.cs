using Microsoft.EntityFrameworkCore;
using Triplann.Models;

namespace Triplann.Data
{
    //Naming conventions for this kind of file?
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
        public DbSet<ChecklistItem> ChecklistItem { get; set; }


    }
}