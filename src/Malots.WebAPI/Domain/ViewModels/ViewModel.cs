using Malots.WebAPI.Domain.Interfaces;
using Malots.WebAPI.Domain.Interfaces.Application;
using System.Diagnostics;

namespace Malots.WebAPI.Domain.ViewModels
{
    [DebuggerDisplay("ViewModel : {IViewModel}")]
    public abstract class ViewModel : IViewModel
    {
        public string Id { get; set; }
        public int Status { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
