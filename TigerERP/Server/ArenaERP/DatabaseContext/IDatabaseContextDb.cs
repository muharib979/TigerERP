using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;

namespace DatabaseContext
{
    public interface IDatabaseContextDb
    {
        DbSet<AccChart> AccChart { get; set; }
        DbSet<Branch> Branch { get; set; }
        DbSet<Brand> Brand { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<ChallanDetails> ChallanDetail { get; set; }
        DbSet<ChallanMaster> ChallanMaster { get; set; }
        DbSet<Color> Color { get; set; }
        DbSet<Godown> Godown { get; set; }
        DbSet<Modules> Modules { get; set; }
        DbSet<Pages> Pages { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductCategories> ProductCategory { get; set; }
        DbSet<ProductSupplier> ProductSupplier { get; set; }
        DbSet<ProductUnit> ProductUnitConv { get; set; }
        DbSet<Unit> Unit { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}