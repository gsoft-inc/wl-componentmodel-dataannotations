namespace GSoft.ComponentModel.DataAnnotations.Tests;

public class MinValueAttributeTests
{
    [Theory]
    [InlineData(50, 49)]
    [InlineData(int.MaxValue, 49)]
    [InlineData(-50, -51)]
    public void GivenIntValue_ShouldFailValidation_WhenValueIsLessThanMin(int minValue, int valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49, 50)]
    [InlineData(49, int.MaxValue)]
    [InlineData(-51, -50)]
    [InlineData(int.MinValue, int.MinValue)]
    public void GivenIntValue_ShouldPassValidation_WhenMoreOrEqualThanMin(int minValue, int valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(50.0, 49.0)]
    [InlineData(double.MaxValue, 49.0)]
    [InlineData(-50.0, -51.0)]
    public void GivenDoubleValue_ShouldFailValidation_WhenValueIsLessThanMin(double minValue, double valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49.0, 50.0)]
    [InlineData(49.0, double.MaxValue)]
    [InlineData(-51.0, -50.0)]
    [InlineData(double.MinValue, double.MinValue)]
    public void GivenDoubleValue_ShouldPassValidation_WhenMoreOrEqualThanMin(double minValue, double valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(50, 49)]
    [InlineData(long.MaxValue, 49)]
    [InlineData(-50, -51)]
    public void GivenLongValue_ShouldFailValidation_WhenValueIsLessThanMin(long minValue, long valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49, 50)]
    [InlineData(49, long.MaxValue)]
    [InlineData(-51, -50)]
    [InlineData(long.MinValue, long.MinValue)]
    public void GivenLongValue_ShouldPassValidation_WhenMoreOrEqualThanMin(long minValue, long valueBeingValidated)
    {
        var maxValueAttribute = new MinValue(minValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
}
