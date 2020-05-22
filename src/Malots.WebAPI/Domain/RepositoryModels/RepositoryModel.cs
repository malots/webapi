using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Infra;
using System;
using System.Diagnostics;

namespace Malots.WebAPI.Domain.RepositoryModels
{
    [DebuggerDisplay("RepositoryModel : {IRepositoryModel}")]
    public abstract class RepositoryModel : IRepositoryModel
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
