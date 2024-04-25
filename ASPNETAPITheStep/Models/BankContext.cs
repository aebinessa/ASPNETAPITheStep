using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace  ASPNETAPITheStep.Models

{

    public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
    {
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.CivilId) // Create an index for CivilId
                .IsUnique(); // Make CivilId unique
            // Seed sample blog data:::::::::::::::::::::::::::
            modelBuilder.Entity<BankBranch>().HasData(
                new BankBranch { Id = 1, Name = "Khaldiya", Location = "https://maps.app.goo.gl/s2aCpoGSUFZHa4KS8", BranchManager = "Majed", EmployeeCount = 4 },
                new BankBranch{ Id = 2, Name = "Mansouriya", Location = "https://maps.app.goo.gl/N1AwujroTFMhVbVj9", BranchManager = "Ahmad", EmployeeCount = 3  }
            );
        }


    }
}
