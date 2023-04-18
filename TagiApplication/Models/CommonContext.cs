using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    public class CommonContext<T> : DbContext where T : class
    {
        protected readonly IConfiguration Configuration;

        public CommonContext(DbContextOptions<TagiContext> options)
            : base(options)
        {
        }

        public DbSet<Tagi> Tagit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Type T is added as a DbSet to the context, but without a Key. 
            // A key is not required as this will be used only for data 
            // retrieval and with AsNoTracking
            var t = modelBuilder.Entity<T>();

            // Using reflection, the relevant properties of type T 
            // are added to the DbSet entity
            foreach (var prop in typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!prop.CustomAttributes
                    .Any(a => a.AttributeType == typeof(NotMappedAttribute)))
                {
                    t.Property(prop.Name);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        /**
         * Soft delete override
         **/
        public override int SaveChanges()
        {
            HandleDelete();
            return base.SaveChanges();
        }
        private void HandleDelete()
        {
            var entities = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                var jee = entity.Entity;
                if (typeof(ISoftDelete).IsAssignableFrom(entity.Entity.GetType()))
                {
                    entity.State = EntityState.Modified;
                    entity.Entity.GetType().GetProperty("Poistettu").SetValue(entity.Entity, DateTime.UtcNow, null);
                }
            }
        }
    }
}
