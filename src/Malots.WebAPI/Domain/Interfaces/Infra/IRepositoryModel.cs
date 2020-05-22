using Malots.WebAPI.Domain.Enums;
using System;

namespace Malots.WebAPI.Domain.Interfaces.Infra
{
    public interface IRepositoryModel
    {
        Guid Id { get; set; }
        StatusEnum Status { get; set; }
    }
}
