namespace Workleap.ComponentModel.DataAnnotations.Tests;

public class MinTimeSpanValueAttributeTests
{
    [Theory]
    [InlineData("50:00:00", "49:00:00")]
    [InlineData("-50:00:00", "-51:00:00")]
    public void GivenValue_ShouldFailValidation_WhenValueIsInvalid(string minValue, string valueBeingValidated)
    {
        var maxValueAttribute = new MinTimeSpanValueAttribute(minValue);

        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }

    [Theory]
    [InlineData("49:00:00", "50:00:00")]
    [InlineData("49:00:00", "10675199.02:48:05.4775807")]
    [InlineData("-51:00:00", "-50:00:00")]
    [InlineData("-10675199.02:48:05.4775808", "10675199.02:48:05.4775807")]
    public void GivenValue_ShouldPassValidation_WhenValueIsValid(string minValue, string valueBeingValidated)
    {
        var maxValueAttribute = new MinTimeSpanValueAttribute(minValue);

        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
}
