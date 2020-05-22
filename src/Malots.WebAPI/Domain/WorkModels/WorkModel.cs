using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Business;
using System;
using System.Diagnostics;

namespace Malots.WebAPI.Domain.WorkModels
{
    [DebuggerDisplay("WorkModel : {IModel}")]
    public abstract class WorkModel : IWorkModel
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
