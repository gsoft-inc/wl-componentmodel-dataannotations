using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GSoft.ComponentModel.DataAnnotations.Tests;

public class ValidatePropertiesAttributeTests
{
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

    private class ApplicationOptions
    {
        [Required]
        [ValidateProperties]
        public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();

        [Range(1, 5)]
        public int Count { get; set; } = 0;
    }

    private class AuthenticationOptions
    {
        [Required]
        [NotEmpty]
        public string[] Protocols { get; set; } = Array.Empty<string>();

        [Required]
        [ValidateProperties]
        public MicrosoftOptions Microsoft { get; set; } = new MicrosoftOptions();
    }

    private class MicrosoftOptions
    {
        [Required]
        [ValidateProperties]
        public AzureOptions Azure { get; set; } = new AzureOptions();
    }

    private class AzureOptions
    {
        [Required]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        public string ClientSecret { get; set; } = string.Empty;
    }
}