using System;
using System.ComponentModel.DataAnnotations;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class MinValueAttribute : RangeAttribute
{
    public MinValueAttribute(int minimum) 
        : base(minimum, int.MaxValue)
    {
    }

    public MinValueAttribute(double minimum) 
        : base(minimum, double.MaxValue)
    {
    }
    
    public MinValueAttribute(long minimum) 
        : base(minimum, long.MaxValue)
    {
    }
}