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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
                }
            }
        }
    }
}
