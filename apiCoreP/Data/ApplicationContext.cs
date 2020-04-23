using apiCoreP.Enums;
using apiCoreP.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace apiCoreP.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserRoleType>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<UserRoleType>();

            UserConfig(modelBuilder);

            modelBuilder.Entity<UserRole>().HasMany(x => x.Users);
        }

        private static void UserConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                            .HasIndex(x => x.Name);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
