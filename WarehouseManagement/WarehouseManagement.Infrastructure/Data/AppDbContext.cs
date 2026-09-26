using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockMovement> StockMovements { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(200);
                entity.Property(p => p.SKU).HasMaxLength(100);
                entity.HasIndex(p => p.SKU).IsUnique();
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(w => w.Name).HasMaxLength(200);
                entity.Property(w => w.Location).HasMaxLength(500);

                // Products belong to warehouses through Stock, not a direct foreign key.
                entity.Ignore(w => w.Products);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasIndex(s => new { s.ProductId, s.WarehouseId }).IsUnique();

                entity.HasOne(s => s.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(s => s.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Warehouse)
                    .WithMany(w => w.Stocks)
                    .HasForeignKey(s => s.WarehouseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StockMovement>(entity =>
            {
                entity.Property(sm => sm.MovementType).HasConversion<string>().HasMaxLength(20);

                entity.HasOne(sm => sm.Product)
                    .WithMany(p => p.StockMovements)
                    .HasForeignKey(sm => sm.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sm => sm.Warehouse)
                    .WithMany(w => w.StockMovements)
                    .HasForeignKey(sm => sm.WarehouseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(100);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(100);
                entity.HasIndex(r => r.Name).IsUnique();
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(rt => rt.Token).IsUnique();

                entity.HasOne(rt => rt.User)
                    .WithMany()
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Ignore(rt => rt.IsExpired);
                entity.Ignore(rt => rt.IsRevoked);
                entity.Ignore(rt => rt.IsActive);
            });
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == Guid.Empty)
                    {
                        entry.Entity.Id = Guid.NewGuid();
                    }

                    entry.Entity.CreatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
        }
    }
}
