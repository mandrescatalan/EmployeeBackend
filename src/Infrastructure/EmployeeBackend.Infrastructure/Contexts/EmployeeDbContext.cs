using EmployeeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBackend.Infrastructure.Contexts
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
             : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("EmployeeId")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Phone)
                      .HasMaxLength(15)
                      .IsRequired();

                entity.Property(e => e.HireDate)
                      .IsRequired();
            });
        }

    }
}
