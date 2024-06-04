using System;
using System.ComponentModel.DataAnnotations;

namespace Workleap.ComponentModel.DataAnnotations.Tests;

public sealed class ValidatePropertiesAttributeTests
{
    [Fact]
    public void IsValid_Returns_False_When_Value_Is_Null()
    {
        var attribute = new ValidatePropertiesAttribute();
        Assert.True(attribute.IsValid(null));
    }

    [Fact]
    public void IsValid_Returns_True_When_Object_Without_Properties_With_Validation_Attributes()
    {
        var attribute = new ValidatePropertiesAttribute();
        Assert.True(attribute.IsValid(new object()));
    }

    [Fact]
    public void IsValid_Returns_False_When_Validation_Rules_Are_Violated()
    {
        var attribute = new ValidatePropertiesAttribute();
        Assert.False(attribute.IsValid(new ApplicationOptions()));
    }

    [Fact]
    public void ValidateObject_Throws_Exception_That_Contain_Each_Violated_Validation_Rule()
    {
        var ex = Assert.Throws<ValidationException>(() =>
        {
            var options = new ApplicationOptions();
            Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
        });

        Assert.Contains("Authentication.Protocols", ex.Message);
        Assert.Contains("Authentication.Microsoft.Azure.ClientId", ex.Message);
        Assert.Contains("Authentication.Microsoft.Azure.ClientSecret", ex.Message);
    }

    private sealed class ApplicationOptions
    {
        [Required]
        [ValidateProperties]
        public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();

        [Range(1, 5)]
        public int Count { get; set; } = 0;
    }

    private sealed class AuthenticationOptions
    {
        [Required]
        [NotEmpty]
        public string[] Protocols { get; set; } = Array.Empty<string>();

        [Required]
        [ValidateProperties]
        public MicrosoftOptions Microsoft { get; set; } = new MicrosoftOptions();
    }

    private sealed class MicrosoftOptions
    {
        [Required]
        [ValidateProperties]
        public AzureOptions Azure { get; set; } = new AzureOptions();
    }

    private sealed class AzureOptions
    {
        [Required]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        public string ClientSecret { get; set; } = string.Empty;
    }
}