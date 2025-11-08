using Microsoft.EntityFrameworkCore;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Username).IsUnique();
                b.Property(x => x.Username).IsRequired();
                b.Property(x => x.PasswordHash).IsRequired();
                b.Property(x => x.Role).HasMaxLength(20).HasDefaultValue("User");
            });

            // Products
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired();
                b.Property(x => x.Price).HasColumnType("decimal(18,2)");
                b.Property(x => x.ImageUrl).HasMaxLength(512);
                b.HasIndex(x => x.Name);
            });

            // Sales
            modelBuilder.Entity<Sale>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Date).IsRequired();
                b.HasOne(x => x.User)
                 .WithMany(u => u.Sales)
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasIndex(x => x.Date);
            });

            // SaleItems
            modelBuilder.Entity<SaleItem>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
                b.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");

                b.HasOne(x => x.Sale)
                 .WithMany(s => s.Items)
                 .HasForeignKey(x => x.SaleId);

                b.HasOne(x => x.Product)
                 .WithMany()
                 .HasForeignKey(x => x.ProductId);
            });
        }
    }
}
