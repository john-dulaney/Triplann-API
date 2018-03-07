using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Models;

namespace thoughtless_eels.Data
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

        public DbSet<Computer> Computer { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeComputer> EmployeeComputer { get; set; }
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<TrainingProgram> TrainingProgram { get; set; }
        public DbSet<CurrentOrder> CurrentOrder { get; set; }


    }
}