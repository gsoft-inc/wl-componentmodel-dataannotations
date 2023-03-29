using System;
using System.ComponentModel.DataAnnotations;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public sealed class MaxValue : RangeAttribute
{
    public MaxValue(int maximum) : base(int.MinValue, maximum)
    {
    }

    public MaxValue(double maximum) : base(double.MinValue, maximum)
    {
    }
    
    public MaxValue(long maximum) : base(long.MinValue, maximum)
    {
    }
}