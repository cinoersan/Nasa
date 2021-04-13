using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nasa.Business.Data
{
    public partial class NasaDbContext : DbContext
    {
        public virtual IQueryable<T> All<T>() where T : class, IEntity
        {
            return this.Set<T>().AsQueryable();
        }
        public virtual async Task UpdateInsert<T>(T entity) where T : class, IEntity
        {
            if (entity.Id == 0)
            {
                await this.Set<T>().AddAsync(entity);
            }
            else
            {
                var item = this.Set<T>().FirstOrDefault(t => t.Id == entity.Id);
                if (item == null) return;
                this.Entry(item).CurrentValues.SetValues(entity);
            }

            await this.SaveChangesAsync();
        }
        public virtual async Task AddAsync<T>(T entity) where T : class, IEntity
        {
            await this.Set<T>().AddAsync(entity);
        }
        public virtual void Add<T>(T entity) where T : class, IEntity
        {
            this.Set<T>().Add(entity);
        }
        public virtual void Update<T>(T entity) where T : class, IEntity
        {
            var entry = this.Entry(entity);
            this.Set<T>().Attach(entity);
            entry.State = EntityState.Modified;
        }
        public virtual void Delete<T>(T entity) where T : class, IEntity
        {
            if (this.Entry(entity).State == EntityState.Detached)
            {
                this.Set<T>().Attach(entity);
            }
            this.Set<T>().Remove(entity);
        }
        public virtual T Find<T>(object id) where T : class, IEntity
        {
            return this.Set<T>().Find(id);
        }
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                where e.State == EntityState.Added
                      || e.State == EntityState.Modified
                select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext); // throw exception when error...
            }

            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = from e in ChangeTracker.Entries()
                where e.State == EntityState.Added
                      || e.State == EntityState.Modified
                select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext); // throw exception when error...
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
