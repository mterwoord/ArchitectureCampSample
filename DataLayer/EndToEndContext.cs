using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using EndToEnd.DataLayer.Model;

namespace EndToEnd.DataLayer
{
    public class EndToEndContext : DbContext
    {
        public EndToEndContext()
            : base("EndToEndContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
         
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SessionBase> Sessions { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Slot> Slots { get; set; }

        private void ObjectContext_SavingChanges(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var osm = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;

            foreach (var entry in osm.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (!entry.IsRelationship)
                {
                    var timestampedEntity = entry.Entity as EntityBase;

                    if (timestampedEntity != null)
                    {
                        timestampedEntity.ModifiedAt = now;
                        var entity = entry.Entity as EntityObject;
                    
                        if ((entity != null) && (entity.EntityState == EntityState.Added))
                        {
                            timestampedEntity.CreatedAt = now;
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Speaker>()
               .HasMany(s => s.Sessions)
               .WithMany(s => s.Speakers)
               .Map(s =>
               {
                   s.MapLeftKey("Session_Id");
                   s.MapRightKey("Speaker_Id");
                   s.ToTable("Speakers_x_Sessions");
               });

            modelBuilder.Entity<Track>()
               .HasMany(s => s.Sessions)
               .WithMany(s => s.Tracks)
               .Map(s =>
               {
                   s.MapLeftKey("Track_Id");
                   s.MapRightKey("Session_Id");
                   s.ToTable("Sessions_x_Tracks");
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}