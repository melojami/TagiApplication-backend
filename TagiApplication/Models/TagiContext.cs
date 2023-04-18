using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection.Emit;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    public class TagiContext : DbContext
    {

        protected readonly IConfiguration? Configuration;

        public TagiContext(DbContextOptions<TagiContext> options)
            : base(options)
        {
        }

        public DbSet<Tagi> Tagit { get; set; }
        public DbSet<Tagityyppi> Tagityypit { get; set; }
        
        public DbSet<Resurssi> Resurssit { get; set; }
        public DbSet<ResurssiTagi> ResurssiTagit { get; set; }
        public DbSet<Jono> Jonot { get; set; }
        public DbSet<Jonottaja> Jonottajat { get; set; }
        public DbSet<JonottajaTagi> JonottajaTagit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //builder.Services.AddDbContext<TagiContext>(opt =>
            //                                  opt.UseSqlServer(System.Configuration.GetConnectionString("TagiDb")));
            
            
            /* Dynaaminen malli, puuttuu ID:n käsittely vielä *
            var softDeletableEntities = typeof(ISoftDelete).Assembly.GetTypes()
                 .Where(type => typeof(ISoftDelete)
                                 .IsAssignableFrom(type)
                                 && type.IsClass
                                 && !type.IsAbstract);

            foreach (var entity in softDeletableEntities)
            {
                modelBuilder.Entity(entity)
                            .HasQueryFilter(
                                GenerateSoftDeleteFilter(entity)
                                );
            }

            LambdaExpression GenerateSoftDeleteFilter(Type type)
            {
                var param = Expression.Parameter(type, "e"); // e =>
                var nullParm = Expression.Constant(null); // null
                var property = Expression.PropertyOrField(param, nameof(ISoftDelete.Poistettu)); // e.Poistettu
                var expression = Expression.Equal(nullParm, property); // e.Poistettu == null
                var lambda = Expression.Lambda(expression, param); // e => e.Poistettu == null

                return lambda;
            }
            */


            modelBuilder.Entity<Tagi>(entity =>
            {
                entity.HasQueryFilter(e => e.Poistettu == null);
                // Identity-kentän käsittely
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .Metadata
                    .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<ResurssiTagi>(entity =>
            {
                entity.HasQueryFilter(e => e.Poistettu == null);
                // Identity-kentän käsittely
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .Metadata
                    .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<JonottajaTagi>(entity =>
            {
                entity.HasQueryFilter(e => e.Poistettu == null);
                // Identity-kentän käsittely
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .Metadata
                    .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            });

            /*
            // TODO: Dynaamiseksi, nämä ovat alkuperäiset          
            modelBuilder.Entity<Tagi>(entity =>
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    {
                        entity.HasQueryFilter(e => e.Poistettu == null);
                    }

                    // Identity-kentän käsittely
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .Metadata
                        .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                }
            });

            modelBuilder.Entity<ResurssiTagi>(entity =>
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    {
                        entity.HasQueryFilter(e => e.Poistettu == null);
                    }

                    // Identity-kentän käsittely
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .Metadata
                        .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                }
            });

            modelBuilder.Entity<JonottajaTagi>(entity =>
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    {
                        entity.HasQueryFilter(e => e.Poistettu == null);
                    }

                    // Identity-kentän käsittely
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .Metadata
                        .SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                }
            });

            */
        }

        /**
         * Soft delete override
         **/
        public override int SaveChanges()
        {
            HandleDelete();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            HandleDelete();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        private void HandleDelete()
        {
            var entities = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entity.Entity.GetType()))
                {
                    entity.State = EntityState.Modified;
                    entity.Entity.GetType().GetProperty("Poistettu").SetValue(entity.Entity, DateTime.UtcNow, null);
                }
            }
        }

    }
}
