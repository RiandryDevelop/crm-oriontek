using CRM_OrionTek.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_OrionTek.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        { }
     
        public DbSet<Client> Client { get; set; }
        public DbSet<Location> Location { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Sector> Sector { get; set; }

        public DbSet<Province> Province { get; set; }

        public DbSet<Municipality> Municipality { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<UserPermission> UserPermission { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<PermissionGroup> PermissionGroup { get; set; }

        public DbSet<Module> Module { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
                }
            }
        }
    }
}
