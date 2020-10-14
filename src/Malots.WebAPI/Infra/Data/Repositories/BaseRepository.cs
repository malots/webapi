using Malots.WebAPI.Domain.Enums;
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

        public async Task<IEnumerable<TRepositoryModel>> SelectTracked(QueryTakeEnum take, QuerySkipEnum skip)
        {
            return await DbSet
                .Skip((int)skip)
                .Take((int)take)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<TRepositoryModel> SelectTracked(Guid id)
        {
            return await DbSet
                .SingleOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TRepositoryModel>> SelectUntracked(QueryTakeEnum take, QuerySkipEnum skip)
        {
            return await DbSet
                .Skip((int)skip)
                .Take((int)take)
                .AsNoTracking()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<TRepositoryModel> SelectUntracked(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public Guid Insert(TRepositoryModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            DbSet.Add(model);

            return model.Id;
        }

        public IEnumerable<Guid> Insert(IEnumerable<TRepositoryModel> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            DbSet.AddRange(models);

            return models.Select(e => e.Id);
        }

        public void Update(TRepositoryModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            DbSet.Update(model);
        }

        public void Update(IEnumerable<TRepositoryModel> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            DbSet.UpdateRange(models);
        }

        public void Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var model = DbSet.SingleOrDefault(m => m.Id == id);

            DbSet.Remove(model);
        }

        //public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        //public int SaveChanges() => Context.SaveChanges();

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
