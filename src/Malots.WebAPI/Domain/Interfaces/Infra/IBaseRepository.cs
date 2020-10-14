using Malots.WebAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malots.WebAPI.Domain.Interfaces.Infra
{
    public interface IBaseRepository<TRepositoryModel> where TRepositoryModel : IRepositoryModel
    {
        Task<IEnumerable<TRepositoryModel>> SelectTracked(QueryTakeEnum take, QuerySkipEnum skip);

        Task<TRepositoryModel> SelectTracked(Guid id);

        Task<IEnumerable<TRepositoryModel>> SelectUntracked(QueryTakeEnum take, QuerySkipEnum skip);

        Task<TRepositoryModel> SelectUntracked(Guid id);

        Guid Insert(TRepositoryModel model);

        IEnumerable<Guid> Insert(IEnumerable<TRepositoryModel> models);

        void Update(TRepositoryModel model);

        void Update(IEnumerable<TRepositoryModel> models);

        void Delete(Guid id);

        void Dispose();
    }
}
