using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malots.WebAPI.Domain.Interfaces.Application
{
    public interface IBaseController<TViewModel, TWorkModel> where TViewModel : IViewModel where TWorkModel : IWorkModel
    {
        Task<IEnumerable<TViewModel>> Get(QueryTakeEnum take = QueryTakeEnum.Fifteen, QuerySkipEnum skip = QuerySkipEnum.None, bool track = true);

        Task<TViewModel> Get(string id, bool track  = true);

        Task<string> Post([FromBody] TViewModel viewModel);

        Task<IEnumerable<string>> Post([FromBody] IEnumerable<TViewModel> viewModels);

        Task<int> Put(string id, [FromBody] TViewModel viewModel);

        Task<int> Delete(string id);
    }
}
