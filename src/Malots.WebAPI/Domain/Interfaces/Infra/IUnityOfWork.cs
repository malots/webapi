using System.Threading.Tasks;

namespace Malots.WebAPI.Domain.Interfaces.Infra
{
    public interface IUnityOfWork
    {
        Task<int> Commit();
    }
}
