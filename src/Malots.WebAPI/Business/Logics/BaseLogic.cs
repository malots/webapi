using AutoMapper;
using FluentValidation;
using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Business;
using Malots.WebAPI.Domain.Interfaces.Infra;
using Malots.WebAPI.Domain.RepositoryModels;
using Malots.WebAPI.Domain.WorkModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Malots.WebAPI.Business.Logics
{
    [DebuggerDisplay("BaseLogic: {TWorkModel} - {TRepositoryModel}")]
    public abstract class BaseLogic<TWorkModel, TRepositoryModel> : IBaseLogic<TWorkModel> where TWorkModel : WorkModel where TRepositoryModel : RepositoryModel, new()
    {
        private readonly IBaseRepository<TRepositoryModel> _repository;
        private readonly IValidator<TWorkModel> _validator;
        private readonly IMapper _mapper;

        public ILogger<BaseLogic<TWorkModel, TRepositoryModel>> Logger { get; set; }

        public BaseLogic(IBaseRepository<TRepositoryModel> repository, IValidator<TWorkModel> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            Logger = NullLogger<BaseLogic<TWorkModel, TRepositoryModel>>.Instance;
        }

        public virtual async Task<IEnumerable<TWorkModel>> Get(QueryTakeEnum take, QuerySkipEnum skip)
        {
            var result = await _repository.Select().Skip((int)skip).Take((int)take).ToArrayAsync().ConfigureAwait(false);
            return result.Select(r => _mapper.Map<TWorkModel>(r));
        }

        public virtual async Task<TWorkModel> Get(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Logger.LogInformation(
                $"Get entity information with Id = {id}");

            var result = await _repository.Select(id).ToArrayAsync().ConfigureAwait(false);

            return _mapper.Map<TWorkModel>(result.FirstOrDefault());
        }

        public virtual async Task<Guid> Post(TWorkModel model)
        {
            Validate(model, _validator);

            var entity = _mapper.Map<TRepositoryModel>(model);

            _repository.Insert(entity);

            await _repository.SaveChangesAsync().ConfigureAwait(false);

            Logger.LogInformation(
                $"It was created the entity = {entity}");

            return entity.Id;
        }

        public virtual async Task<IEnumerable<Guid>> Post(IEnumerable<TWorkModel> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            foreach (var model in models)
            {
                Validate(model, _validator);
            }

            var entities = models.Select(m => _mapper.Map<TRepositoryModel>(m));

            _repository.Insert(entities);

            await _repository.SaveChangesAsync().ConfigureAwait(false);

            Logger.LogInformation(
                $"It was created a range of entities");

            return entities.Select(e => e.Id);
        }

        public virtual async Task<int> Put(TWorkModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Validate(model, _validator);

            var entity = _mapper.Map<TRepositoryModel>(model);

            _repository.Update(entity);

            Logger.LogInformation(
                $"Updated an entity with id = {entity.Id}");

            return await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> Put(IEnumerable<TWorkModel> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            foreach (var model in models)
            {
                Validate(model, _validator);
            }

            var entities = models.Select(m => _mapper.Map<TRepositoryModel>(m));

            _repository.Update(entities);

            Logger.LogInformation(
                 $"It was Updated a range of entities");

            return await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Logger.LogInformation(
                $"Deleted an entity with id = {id}");

            _repository.Delete(id);

            return await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        private void Validate(TWorkModel model, IValidator<TWorkModel> validator)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            validator.ValidateAndThrow(model);
        }
    }
}
