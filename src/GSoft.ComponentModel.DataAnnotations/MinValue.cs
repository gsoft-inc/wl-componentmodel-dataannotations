using System;
using System.ComponentModel.DataAnnotations;

namespace GSoft.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class MinValue : RangeAttribute
{
    public MinValue(int minimum) : base(minimum, int.MaxValue)
    {
    }

    public MinValue(double minimum) : base(minimum, double.MaxValue)
    {
    }
    
    public MinValue(long minimum) : base(minimum, long.MaxValue)
    {
    }
}