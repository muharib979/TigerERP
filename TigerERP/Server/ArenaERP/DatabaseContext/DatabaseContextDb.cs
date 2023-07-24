using DatabaseContext.Seed;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext
{
    public class DatabaseContextDb : DbContext
    {
        public DatabaseContextDb(DbContextOptions<DatabaseContextDb> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<AccChart> AccChart { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ChallanDetails> ChallanDetail { get; set; }
        public DbSet<ChallanMaster> ChallanMaster { get; set; }
        public DbSet<Color> Color { get; set; }
        public virtual DbSet<Godown> Godown { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategories> ProductCategory { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<ProductUnit> ProductUnitConv { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modules>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.IsParent);
                entity.Property(e => e.ParentModuleId);
                entity.Property(e => e.ModuleRoutePath);
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<AccChart>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            //modelBuilder.Entity<Brand>(entity =>
            //{
            //    entity.HasData(new DataSeed().Branch);
            //});
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {
                entity.HasKey(e => e.ProductSuplierId);
                entity.HasOne(p => p.Product)
                .WithMany(d => d.ProductSupplier)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.AccChart)
                .WithMany(d => d.ProductSupplier)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(p => p.Product)
                .WithMany(d => d.ProductCategory)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Category)
                .WithMany(d => d.ProductCategory)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
