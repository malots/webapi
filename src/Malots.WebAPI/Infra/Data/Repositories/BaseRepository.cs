using Malots.WebAPI.Domain.Interfaces.Infra;
using Malots.WebAPI.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Malots.WebAPI.Infra.Data.Repositories
{
    [DebuggerDisplay("BaseRepository: {TRepositoryModel}")]
    public abstract class BaseRepository<TRepositoryModel> : IBaseRepository<TRepositoryModel> where TRepositoryModel : RepositoryModel, new()
    {
        protected DbContext Context { get; }

        protected DbSet<TRepositoryModel> DbSet { get; }

        public BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = Context != null ? Context.Set<TRepositoryModel>() : throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<TRepositoryModel> Select() => DbSet;

        public IQueryable<TRepositoryModel> Select(Guid id) => DbSet.Where(e => e.Id == id);

        public Guid Insert(TRepositoryModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Add(entity);

            return entity.Id;
        }

        public IEnumerable<Guid> Insert(IEnumerable<TRepositoryModel> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            DbSet.AddRange(entities);

            return entities.Select(e => e.Id);
        }

        public void Update(TRepositoryModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Update(entity);
        }

        public void Update(IEnumerable<TRepositoryModel> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            DbSet.UpdateRange(entities);
        }

        public void Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            DbSet.Remove(Select(id).First());
        }

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        public int SaveChanges() => Context.SaveChanges();

        public void Dispose() => Context.Dispose();
    }
}
