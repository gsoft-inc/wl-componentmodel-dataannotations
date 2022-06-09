using Xunit;

namespace ShareGate.ComponentModel.DataAnnotations.Tests;

public class ContainsAttributeTests
{
    [Fact]
    public void IsValid_Returns_True_When_Value_Is_Null()
    {
        var attr = new ContainsAttribute("whatever");
        Assert.True(attr.IsValid(null));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_Not_A_String()
    {
        var attr = new ContainsAttribute("whatever");
        Assert.False(attr.IsValid(new object()));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Not_Found()
    {
        var attr = new ContainsAttribute("42");
        Assert.False(attr.IsValid("foobar"));
        Assert.False(attr.IsValid("FOOBAR"));
    }

    [Fact]
    public void IsValid_Works_Case_Sensitive()
    {
        var attr = new ContainsAttribute("oo");
        Assert.True(attr.IsValid("foobar"));
        Assert.False(attr.IsValid("FOOBAR"));
    }

    [Fact]
    public void IsValid_Works_Case_Insensitive()
    {
        var attr = new ContainsAttribute("oo") { IgnoreCase = true };
        Assert.True(attr.IsValid("foobar"));
        Assert.True(attr.IsValid("FOOBAR"));
    }
}