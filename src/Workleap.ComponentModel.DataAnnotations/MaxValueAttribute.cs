using System;
using System.ComponentModel.DataAnnotations;

namespace Workleap.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class MaxValueAttribute : RangeAttribute
{
    public MaxValueAttribute(int maximum) 
        : base(int.MinValue, maximum)
    {
    }

    public MaxValueAttribute(double maximum) 
        : base(double.MinValue, maximum)
    {
    }
    
    public MaxValueAttribute(long maximum) 
        : base(long.MinValue, maximum)
    {
    }
}