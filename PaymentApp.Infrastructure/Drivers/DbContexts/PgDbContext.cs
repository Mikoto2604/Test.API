using PaymentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PaymentApp.Infrastructure.Drivers.DbContexts
{
    public class PgDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public PgDbContext(DbContextOptions<PgDbContext> options, IConfiguration configuration) : 
            base(options) 
        {
            string connectionString = configuration.GetConnectionString("PgDatabase") ?? throw new Exception ("PgDatabase not found");
            Database.SetConnectionString(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Настройки сущности Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp without time zone");
                entity.Property(e => e.Amount).HasPrecision(10, 2);
            });

            // Настройки сущности UserToken
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("User_tokens");
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Token).IsRequired();
                entity.Property(e => e.Expiry).HasColumnType("timestamp without time zone");
                entity.Property(e => e.RevokedAt).HasColumnType("timestamp without time zone");
            });

            // Настройки сущности User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Balance).HasPrecision(10, 2);
                entity.Property(e => e.IsBlocked).HasDefaultValue(false);
                entity.Property(e=>e.MaxLoginAttempts).HasDefaultValue(3);
                entity.Property(e => e.BalanceCcy).HasMaxLength(3).HasDefaultValue("USD");
                entity.Property(e => e.Login).HasMaxLength(20).IsRequired();
                entity.Property(e=>e.FailedLoginAttempts).HasDefaultValue(0);

                entity.HasMany(e => e.Tokens)
                             .WithOne(e => e.User)
                             .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Payments)
                             .WithOne(e => e.User)
                             .HasForeignKey(e => e.UserId);

                entity.HasData(new User
                {
                    Id = 1,
                    Login = "User 1",
                    PasswordHash = "AQAAAAIAAYagAAAAEPbRfMBiYwm+9MONMvIoRP7A0hYRdfe+yTcttw9VBNRIoOvgy6ddy6duQEiZv2RF4Q==",
                    Balance = 8m,
                    IsBlocked = false,
                    MaxLoginAttempts = 5,
                    BalanceCcy = "USD",
                    FailedLoginAttempts = 0
                }, 
                new User
                {
                    Id = 2,
                    Login = "User 2",
                    PasswordHash = "AQAAAAIAAYagAAAAECoIOO2jYvViIx1T/msKb7dD6jvMp6RoPr631yV8iQHORYjcNzlJSr3rhBuKB6Y0+A==",
                    Balance = 8m,
                    IsBlocked = false,
                    MaxLoginAttempts = 5,
                    BalanceCcy = "USD",
                    FailedLoginAttempts = 0
                },
                new User
                {
                    Id = 3,
                    Login = "User 3",
                    PasswordHash = "AQAAAAIAAYagAAAAEBQqVyF/C2q5tJdQfFjQakwEGU0dmTXWLLE20ULWdAcr522uBdO/8epQ/HuHff/tJQ==",
                    Balance = 8m,
                    IsBlocked = false,
                    MaxLoginAttempts = 5,
                    BalanceCcy = "USD",
                    FailedLoginAttempts = 0
                },
                new User
                {
                    Id = 4,
                    Login = "User 4",
                    PasswordHash = "AQAAAAIAAYagAAAAECq3e2ltZC0mPfLglCJjeUmq6a4CRzOrstJn1gf1NaOs9jHJSJZslgVaXkorLQJvNg==",
                    Balance = 8m,
                    IsBlocked = false,
                    MaxLoginAttempts = 5,
                    BalanceCcy = "USD",
                    FailedLoginAttempts = 0
                },
                new User
                {
                    Id = 5,
                    Login = "User 5",
                    PasswordHash = "AQAAAAIAAYagAAAAEEDAW4Y3xVZY1hFPspXX/PF+gANWffip1fmv+07fO8Xf2Tll/XfAoYveGXd2wiaUWw==",
                    Balance = 8m,
                    IsBlocked = false,
                    MaxLoginAttempts = 5,
                    BalanceCcy = "USD",
                    FailedLoginAttempts = 0
                });
            });
        }
    }
}
