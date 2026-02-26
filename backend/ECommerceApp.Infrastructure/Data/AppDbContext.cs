using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Clothing" },
            new Category { Id = 3, Name = "Books" },
            new Category { Id = 4, Name = "Home & Kitchen" }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Admin User",
                Email = "admin@ecommerce.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.Admin
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Wireless Headphones", Description = "Premium noise-cancelling headphones with 30h battery", Price = 79.99m, Stock = 50, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400", CategoryId = 1 },
            new Product { Id = 2, Name = "Smart Watch", Description = "Feature-rich smartwatch with health tracking", Price = 149.99m, Stock = 30, ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=400", CategoryId = 1 },
            new Product { Id = 3, Name = "Running Shoes", Description = "Lightweight and comfortable running shoes", Price = 59.99m, Stock = 100, ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400", CategoryId = 2 },
            new Product { Id = 4, Name = "Classic T-Shirt", Description = "100% cotton premium quality t-shirt", Price = 19.99m, Stock = 200, ImageUrl = "https://images.unsplash.com/photo-1529374255404-311a2a4f1fd9?w=400", CategoryId = 2 },
            new Product { Id = 5, Name = "Clean Code", Description = "A Handbook of Agile Software Craftsmanship by Robert C. Martin", Price = 34.99m, Stock = 75, ImageUrl = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400", CategoryId = 3 },
            new Product { Id = 6, Name = "Coffee Maker", Description = "12-cup programmable coffee maker with thermal carafe", Price = 89.99m, Stock = 40, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400", CategoryId = 4 }
        );
    }
}
