using FluentValidation.Internal;
using Malots.WebAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malots.WebAPI.Domain.Interfaces.Business
{
    public interface IBaseLogic<TWorkModel> where TWorkModel : IWorkModel
    {
        Task<IEnumerable<TWorkModel>> Get(QueryTakeEnum take, QuerySkipEnum skip, bool track = true);

        Task<TWorkModel> Get(Guid id, bool track = true);

        Task<Guid> Post(TWorkModel model);

        Task<IEnumerable<Guid>> Post(IEnumerable<TWorkModel> models);

        Task<int> Put(TWorkModel model);

        Task<int> Put(IEnumerable<TWorkModel> models);

        Task<int> Delete(Guid id);
    }
}
