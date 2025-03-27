using Microsoft.EntityFrameworkCore; 
namespace eShopLegacyMVC.Models 
{ 
    public class CatalogDBContext : DbContext 
    { 
        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options) 
        { 
        } 
        public DbSet<CatalogItem> CatalogItems { get; set; } 
        public DbSet<CatalogBrand> CatalogBrands { get; set; } 
        public DbSet<CatalogType> CatalogTypes { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<CatalogType>(entity => 
            { 
                entity.ToTable("CatalogType"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id) 
                    .IsRequired(); 
                entity.Property(e => e.Type) 
                    .IsRequired() 
                    .HasMaxLength(100); 
            }); 
            modelBuilder.Entity<CatalogBrand>(entity => 
            { 
                entity.ToTable("CatalogBrand"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id) 
                    .IsRequired(); 
                entity.Property(e => e.Brand) 
                    .IsRequired() 
                    .HasMaxLength(100); 
            }); 
            modelBuilder.Entity<CatalogItem>(entity => 
            { 
                entity.ToTable("Catalog"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id) 
                    .ValueGeneratedNever() 
                    .IsRequired(); 
                entity.Property(e => e.Name) 
                    .IsRequired() 
                    .HasMaxLength(50); 
                entity.Property(e => e.Price) 
                    .IsRequired(); 
                entity.Property(e => e.PictureFileName) 
                    .IsRequired(); 
                entity.Ignore(e => e.PictureUri); 
                entity.HasOne(e => e.CatalogBrand) 
                    .WithMany() 
                    .HasForeignKey(e => e.CatalogBrandId) 
                    .IsRequired(); 
                entity.HasOne(e => e.CatalogType) 
                    .WithMany() 
                    .HasForeignKey(e => e.CatalogTypeId) 
                    .IsRequired(); 
            }); 
        } 
    } 
}