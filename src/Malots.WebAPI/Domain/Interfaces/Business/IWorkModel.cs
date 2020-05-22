using Malots.WebAPI.Domain.Enums;
using System;

namespace Malots.WebAPI.Domain.Interfaces.Business
{
    public interface IWorkModel
    {
        Guid Id { get; set; }
        StatusEnum Status { get; set; }
    }
}
