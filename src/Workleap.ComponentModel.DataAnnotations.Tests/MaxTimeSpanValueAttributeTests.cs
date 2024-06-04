using System;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public class MaxTimeSpanValueAttributeTests
{
    [Theory]
    [InlineData("49:00:00", "50:00:00")]
    [InlineData("-51:00:00", "-50:00:00")]
    public void GivenValue_ShouldFailValidation_WhenValueIsInvalid(string maxValue, string valueBeingValidated)
    {
        var maxValueAttribute = new MaxTimeSpanValueAttribute(maxValue);

        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }

    [Theory]
    [InlineData("50:00:00", "49:00:00")]
    [InlineData("10675199.02:48:05.4775807", "49:00:00")]
    [InlineData("-50:00:00", "-51:00:00")]
    [InlineData("10675199.02:48:05.4775807", "-10675199.02:48:05.4775808")]
    public void GivenValue_ShouldPassValidation_WhenValueIsValid(string maxValue, string valueBeingValidated)
    {
        var maxValueAttribute = new MaxTimeSpanValueAttribute(maxValue);

        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
}
