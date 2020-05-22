using AutoMapper;
using Malots.WebAPI.Domain.RepositoryModels;
using Malots.WebAPI.Domain.ViewModels;
using Malots.WebAPI.Domain.WorkModels;

namespace Malots.WebAPI.Business.Profiles
{
    public abstract class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<ViewModel, WorkModel>().ReverseMap();
            CreateMap<WorkModel, RepositoryModel>().ReverseMap();
        }
    }
}
