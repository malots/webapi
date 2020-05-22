using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malots.WebAPI.Domain.Interfaces.Infra
{
    public interface IBaseRepository<TRepositoryModel> where TRepositoryModel : IRepositoryModel
    {
        IQueryable<TRepositoryModel> Select();

        IQueryable<TRepositoryModel> Select(Guid id);

        Guid Insert(TRepositoryModel entity);

        IEnumerable<Guid> Insert(IEnumerable<TRepositoryModel> entities);

        void Update(TRepositoryModel entity);

        void Update(IEnumerable<TRepositoryModel> entities);

        void Delete(Guid id);

        Task<int> SaveChangesAsync();

        int SaveChanges();

        void Dispose();
    }
}
