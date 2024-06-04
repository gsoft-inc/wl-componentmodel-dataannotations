using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class MinTimeSpanValueAttribute : RangeAttribute
{
    public MinTimeSpanValueAttribute(string minimum)
        : base(typeof(TimeSpan), minimum, TimeSpan.MaxValue.ToString("c", CultureInfo.InvariantCulture))
    {
    }
}