using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Adding Seed Data
            modelBuilder.Entity<Project>().HasData(
                new Project() { ProjectID  = 1, ProjectName = "Learning Management System", DateOfStart = DateTime.Now,TeamSize = 10},
                new Project() { ProjectID = 2, ProjectName = "Payroll Application", DateOfStart = DateTime.Now, TeamSize = 5 }
            );


        }


    }
}
