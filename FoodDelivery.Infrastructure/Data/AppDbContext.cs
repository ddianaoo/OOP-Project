using Microsoft.EntityFrameworkCore;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================
        // USER (TPH)
        // =====================
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Client>("Client")
            .HasValue<Admin>("Admin")
            .HasValue<Courier>("Courier");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // =====================
        // DISH
        // =====================
        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasPrecision(18, 2);

        // =====================
        // CLIENT -> ORDERS
        // =====================
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================
        // COURIER -> ORDERS
        // =====================
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Courier)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CourierId)
            .OnDelete(DeleteBehavior.SetNull);

        // =====================
        // ORDER -> ORDER ITEMS
        // =====================
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // =====================
        // ORDER ITEM -> DISH
        // =====================
        modelBuilder.Entity<OrderItem>()
            .HasOne(i => i.Dish)
            .WithMany(d => d.OrderItems)
            .HasForeignKey(i => i.DishId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================
        // CLIENT -> CART (1 : 1)
        // =====================
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Cart)
            .WithOne(ca => ca.Client)
            .HasForeignKey<Cart>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        // =====================
        // CART -> CART ITEMS (1 : many)
        // =====================
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        // =====================
        // CART ITEM -> DISH
        // =====================
        modelBuilder.Entity<CartItem>()
            .HasOne(i => i.Dish)
            .WithMany()
            .HasForeignKey(i => i.DishId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}