using EndToEnd.DataLayer.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EndToEnd.DataLayer.Context {
  public class EndToEndContext : DbContext, IUnitOfWork {

    public EndToEndContext()
      : base("EndToEndContext") {
      ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
      ((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
      base.Configuration.ProxyCreationEnabled = false;
    }

    public DbSet<Speaker> Speakers { get; set; }

    public DbSet<SessionBase> Sessions { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Track> Tracks { get; set; }

    public DbSet<Slot> Slots { get; set; }

    /// <summary>
    /// Assure that the modified at field is always written if an object has been touched.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ObjectContext_SavingChanges(object sender, EventArgs e) {
      var now = DateTime.Now;
      var osm = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;
      foreach (var entry in osm.GetObjectStateEntries(EntityState.Added | EntityState.Modified)) {
        if (!entry.IsRelationship) {
          var timestampedEntity = entry.Entity as EntityBase;
          if (timestampedEntity != null) {
            timestampedEntity.ModifiedAt = now;
            var entity = entry.Entity as EntityObject;
            if (entity != null && entity.EntityState == EntityState.Added) {
              timestampedEntity.CreatedAt = now;
            }
          }
        }
      }
    }

    public EndToEndContext(string cnt)
      : base(cnt) {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {

      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

      modelBuilder.Entity<Speaker>()
         .HasMany(s => s.Sessions)
         .WithMany(s => s.Speakers)
         .Map(s => {
           s.MapLeftKey("Session_Id");
           s.MapRightKey("Speaker_Id");
           s.ToTable("Speakers_x_Sessions");
         });

      modelBuilder.Entity<Track>()
         .HasMany(s => s.Sessions)
         .WithMany(s => s.Tracks)
         .Map(s => {
           s.MapLeftKey("Track_Id");
           s.MapRightKey("Session_Id");
           s.ToTable("Sessions_x_Tracks");
         });


      base.OnModelCreating(modelBuilder);
    }

    # region API

    public void ApplyValues<T>(T entity) where T : class {
      var ctx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext;
      var eset = ctx.MetadataWorkspace.GetEntityContainer(ctx.DefaultContainerName, DataSpace.CSpace)
        .BaseEntitySets.Single(es => es.ElementType.Name == typeof(T).Name);
      var key = ctx.CreateEntityKey(eset.Name, entity);
      ctx.GetObjectByKey(key);
      ctx.ApplyCurrentValues<T>(eset.Name, entity);
    }

    public void Detach(object entity) {
      ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.Detach(entity);
    }

    public void Attach(object entity) {
      ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.Attach(entity as IEntityWithKey);
    }

    public object Clone(object entity) {
      var type = entity.GetType();
      var clone = Activator.CreateInstance(type);

      foreach (var property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.SetProperty)) {
        if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(EntityReference<>)) continue;
        if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>)) continue;
        if (property.PropertyType.IsSubclassOf(typeof(EntityObject))) continue;

        if (property.CanWrite) {
          property.SetValue(clone, property.GetValue(entity, null), null);
        }
      }

      return clone;
    }

    public void LoadProperty(object entity, string navProperty) {
      ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.LoadProperty(entity, navProperty);
    }

    public void LoadProperty<T>(T entity, Expression<Func<T, object>> expression) where T : EntityBase {
      var exp = expression.Body;
      string name = null;
      if (exp.NodeType == ExpressionType.MemberAccess) {
        name = ((MemberExpression)exp).Member.Name;
      }
      if (exp.NodeType == ExpressionType.Convert) {
        name = ((MemberExpression)((UnaryExpression)exp).Operand).Member.Name;

      }
      if (!String.IsNullOrEmpty(name)) {
        LoadProperty(entity, name);
      }
    }

    public void LoadProperty<T>(T entity, params Expression<Func<T, object>>[] expressions) where T : EntityBase {
      foreach (var expression in expressions) {
        LoadProperty(entity, expression);
      }
    }

    public void UndoChanges() {
      foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified)) {
        entry.State = EntityState.Unchanged;
      }
    }

    public void DeleteObject(object entity) {
      ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.DeleteObject(entity);
    }

    public IDbTransaction BeginTransaction() {
      var conn = ((IObjectContextAdapter)this).ObjectContext.Connection;
      // always start transaction on a fresh connection
      if (conn.State == ConnectionState.Open) {
        conn.Close();
      }
      if (conn.State == ConnectionState.Closed) {
        conn.Open();
      }
      return ((IObjectContextAdapter)this).ObjectContext.Connection.BeginTransaction();
    }

    public int Save() {
      int result = 0;
      try {
        result = SaveChanges();
      } catch (DbEntityValidationException ex) {
        var message = GetValidationErrors().SelectMany(v => v.ValidationErrors.Select(i =>
          String.Format("{0} in Entity {1} for Property {2}",
            i.ErrorMessage, v.Entry.Entity, i.PropertyName)));
        throw new Exception(String.Join(" | ", message));
      }
      return result;
    }

    # endregion

  }

}