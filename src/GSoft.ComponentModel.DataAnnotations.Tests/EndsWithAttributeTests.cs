namespace GSoft.ComponentModel.DataAnnotations.Tests;

public class EndsWithAttributeTests
{
    [Fact]
    public void IsValid_Returns_True_When_Value_Is_Null()
    {
        var attr = new EndsWithAttribute("whatever");
        Assert.True(attr.IsValid(null));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Is_Not_A_String()
    {
        var attr = new EndsWithAttribute("whatever");
        Assert.False(attr.IsValid(new object()));
    }

    [Fact]
    public void IsValid_Returns_False_When_Value_Not_Found()
    {
        var attr = new EndsWithAttribute("oo");
        Assert.False(attr.IsValid("foobar"));
        Assert.False(attr.IsValid("FOOBAR"));
    }

    [Fact]
    public void IsValid_Works_Case_Sensitive()
    {
        var attr = new EndsWithAttribute("bar");
        Assert.True(attr.IsValid("foobar"));
        Assert.False(attr.IsValid("FOOBAR"));
    }

    [Fact]
    public void IsValid_Works_Case_Insensitive()
    {
        var attr = new EndsWithAttribute("bar") { IgnoreCase = true };
        Assert.True(attr.IsValid("foobar"));
        Assert.True(attr.IsValid("FOOBAR"));
    }
}