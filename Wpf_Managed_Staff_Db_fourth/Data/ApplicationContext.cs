using Microsoft.EntityFrameworkCore;
using System;
using Wpf_Managed_Staff_Db_fourth.Models;


namespace Wpf_Managed_Staff_Db_fourth.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ManageStaffDb;Trusted_Connection=True");
        }
    }
}
