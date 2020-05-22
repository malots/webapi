using System.ComponentModel;

namespace Malots.WebAPI.Domain.Enums
{
    public enum QuerySkipEnum
    {
        [Description("No skip")]
        None = 0,
        [Description("Skip 1")]
        One = 1,
        [Description("Skip 5")]
        Five = 5,
        [Description("Skip 15")]
        Fifteen = 15,
        [Description("Skip 50")]
        Fifty = 50,
        [Description("Skip 100")]
        OneHundred = 100,
        [Description("Skip 1000")]
        OneThousand = 1000,
        [Description("Skip 5000")]
        FiveThousand = 5000,
        [Description("Skip 15000")]
        FifteenThousand = 15000
    }
}
