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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // USER (TPH inheritance)
        // =========================
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Client>("Client")
            .HasValue<Admin>("Admin")
            .HasValue<Courier>("Courier");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // =========================
        // CLIENT CART (ignored)
        // =========================
        modelBuilder.Entity<Client>()
            .Ignore(c => c.Cart);

        // =========================
        // DISH
        // =========================
        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasPrecision(18, 2);

        // =========================
        // ORDER -> ORDER ITEMS
        // =========================
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // =========================
        // ORDER ITEM -> DISH
        // =========================
        modelBuilder.Entity<OrderItem>()
            .HasOne(i => i.Dish)
            .WithMany()
            .HasForeignKey(i => i.DishId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}