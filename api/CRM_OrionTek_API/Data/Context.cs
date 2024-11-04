using CRM_OrionTek_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_OrionTek_API.Data
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
