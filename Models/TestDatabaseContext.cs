using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearningLINQWithSQL.Models;

public partial class TestDatabaseContext : DbContext
{
    public TestDatabaseContext()
    {
    }

    public TestDatabaseContext(DbContextOptions<TestDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employee2> Employees2 { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost, 1433; Initial Catalog=TestDatabase;User ID=sa;Password=abc1234+;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>()
           .HasOne(e => e.SalaryNav)
           .WithMany(s => s.EmployeesWithSalary)
           .HasForeignKey(e => e.Salary);

        modelBuilder.Entity<Employee2>()
            .HasNoKey();

        modelBuilder.Entity<Employee2>(entity =>
        {
            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever();

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId)
                .ValueGeneratedNever()
                .HasColumnName("SalaryID");
            entity.Property(e => e.SalaryGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
