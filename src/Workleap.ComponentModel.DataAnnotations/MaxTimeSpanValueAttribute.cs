using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class MaxTimeSpanValueAttribute : RangeAttribute
{
    public MaxTimeSpanValueAttribute(string maximum)
        : base(typeof(TimeSpan), TimeSpan.MinValue.ToString("c", CultureInfo.InvariantCulture), maximum)
    {
    }
}