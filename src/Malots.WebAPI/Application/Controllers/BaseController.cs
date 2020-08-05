using AutoMapper;
using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Application;
using Malots.WebAPI.Domain.Interfaces.Business;
using Malots.WebAPI.Domain.ViewModels;
using Malots.WebAPI.Domain.WorkModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Malots.WebAPI.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [DebuggerDisplay("BaseController: {TViewModel} - {TWorkModel}")]
    public abstract class BaseController<TViewModel, TWorkModel> : ControllerBase, IBaseController<TViewModel, TWorkModel> where TViewModel : ViewModel where TWorkModel : WorkModel, new()
    {
        private readonly IBaseLogic<TWorkModel> _logic;
        private readonly IMapper _mapper;

        public BaseController(IBaseLogic<TWorkModel> logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/Model
        [HttpGet]
        public async Task<IEnumerable<TViewModel>> Get(QueryTakeEnum take = QueryTakeEnum.Fifteen, QuerySkipEnum skip = QuerySkipEnum.None, bool track = true)
        {
            return (await _logic.Get(take, skip, track).ConfigureAwait(false)).Select(m => _mapper.Map<TViewModel>(m));
        }

        // GET: api/Model/5
        [HttpGet("{id}")]
        public async Task<TViewModel> Get(string id, bool track)
        {
            return _mapper.Map<TViewModel>(await _logic.Get(Guid.Parse(id), track).ConfigureAwait(false));
        }

        // POST: api/Model
        [HttpPost]
        public async Task<string> Post([FromBody] TViewModel viewModel)
        {
            var entityId = await _logic.Post(_mapper.Map<TWorkModel>(viewModel)).ConfigureAwait(false);

            return entityId.ToString();
        }

        // POST: api/Model[]
        [HttpPost("bulk")]
        public async Task<IEnumerable<string>> Post([FromBody] IEnumerable<TViewModel> viewModels)
        {
            var entitiesId = await _logic.Post(viewModels.Select(vm => _mapper.Map<TWorkModel>(vm))).ConfigureAwait(false);

            return entitiesId.Select(e => e.ToString());
        }

        // PUT: api/Model/5
        [HttpPut("{id}")]
        public async Task<int> Put(string id, [FromBody] TViewModel viewModel)
        {
            if (viewModel != null && id != viewModel.Id)
                throw new ArgumentException(nameof(id));

            return await _logic.Put(_mapper.Map<TWorkModel>(viewModel)).ConfigureAwait(false);
        }

        // DELETE: api/Model/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(string id)
        {
            return await _logic.Delete(Guid.Parse(id)).ConfigureAwait(false);
        }
    }
}
