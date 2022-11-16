using BusinessLogic;

namespace BL.UnitTests;

public class TextComparerTests
{
    [Theory]
    [InlineData(null, "rightText")]
    [InlineData("leftText", null)]
    [InlineData(null, null)]
    public void Compare_InvalidLeftTextParam_ReturnError(string leftText, string rightText)
    {
        Assert.Throws<ArgumentException>(() => TextComparer.Compare(leftText, rightText));
    }

    [Theory]
    [InlineData("text", "text")]
    [InlineData("Text2", "Text2")]
    [InlineData("!@#$!@%$!154rsA", "!@#$!@%$!154rsA")]
    public void Compare_EqualText_ReturnEqualAndTheSameSize(string leftText, string rightText)
    {
        CompareResult result = TextComparer.Compare(leftText, rightText);

        Assert.True(result.IsEqual);
        Assert.True(result.IsSameSize);
    }

    [Theory]
    [InlineData("text", "test")]
    [InlineData("123", "321")]
    [InlineData("Test", "test")]
    public void Compare_SameSizeButDifferentText_ReturnNotEqualButSameSize(string leftText, string rightText)
    {
        CompareResult result = TextComparer.Compare(leftText, rightText);

        Assert.False(result.IsEqual);
        Assert.True(result.IsSameSize);
    }

    [Theory]
    [InlineData("text", "test123")]
    [InlineData("123", "321312@")]
    [InlineData("Test", "test!")]
    public void Compare_DifferentLengthText_ReturnNotEqualAndNotTheSameSize(string leftText, string rightText)
    {
        CompareResult result = TextComparer.Compare(leftText, rightText);

        Assert.False(result.IsEqual);
        Assert.False(result.IsSameSize);
    }
}