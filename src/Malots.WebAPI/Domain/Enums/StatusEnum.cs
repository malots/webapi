using System.ComponentModel;

namespace Malots.WebAPI.Domain.Enums
{
    public enum StatusEnum
    {
        [Description("Is Active")]
        IsActive,
        [Description("Is Removed")]
        IsDeleted
    }
}
