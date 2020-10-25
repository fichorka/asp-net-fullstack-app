using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Login>().ToTable("Login");
        //    modelBuilder.Entity<Department>().ToTable("Department");
        //    modelBuilder.Entity<Employee>().ToTable("Employee");
        //}
    }
}