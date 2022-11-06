using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Models;

namespace ProductCatalogue.Data;
public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<SubCategory> SubCategories => Set<SubCategory>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<SubCategory>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Product>().HasIndex(x => x.Code).IsUnique();
    }
}
