using System.ComponentModel;

namespace Malots.WebAPI.Domain.Enums
{
    public enum QueryTakeEnum
    {
        [Description("Take 1")]
        One = 1,
        [Description("Take 5")]
        Five = 5,
        [Description("Take 15")]
        Fifteen = 15,
        [Description("Take 50")]
        Fifty = 50,
        [Description("Take 100")]
        OneHundred = 100,
        [Description("Take 1000")]
        OneThousand = 1000,
        [Description("Take 5000")]
        FiveThousand = 5000,
        [Description("Take 15000")]
        FifteenThousand = 15000
    }
}
