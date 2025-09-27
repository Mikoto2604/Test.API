using PaymentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PaymentApp.Infrastructure.Drivers
{
    public class PgDbContext: DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public PgDbContext(DbContextOptions<PgDbContext> options) : 
            base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Настройки сущности Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp without time zone");
                entity.Property(e => e.Amount).HasPrecision(10, 2);
            });

            // Настройки сущности UserToken
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("user_token");
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Token).IsRequired();
            });

            // Настройки сущности User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e=>e.Login).HasMaxLength(20).IsRequired();
                entity.Property(e => e.IsBlocked).HasDefaultValue(false);
                entity.Property(e=>e.MaxAuthAttempts).HasDefaultValue(3);
                entity.Property(e=>e.FailedLoginAttempts).HasDefaultValue(0);
                entity.HasMany(e => e.Tokens)
                             .WithOne(e => e.User)
                             .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Payments)
                             .WithOne(e => e.User)
                             .HasForeignKey(e => e.UserId);
            });
        }
    }
}
