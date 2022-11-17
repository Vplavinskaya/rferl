using BusinessLogic;

namespace BL.UnitTests;

public class TextComparerTests
{
    public static IEnumerable<object[]> DiffTextData =>
        new List<object[]>
        {
            new object[] { "text", "test", new OffsetDetails() { OffsetIndexes = { 2 } } },
            new object[] { "123", "321", new OffsetDetails() { OffsetIndexes = { 0, 2 } } },
            new object[] { "Test", "test", new OffsetDetails() { OffsetIndexes = { 0 } } }
        };

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
        Assert.Null(result.OffsetDetails);
    }

    [Theory, MemberData(nameof(DiffTextData))]
    public void Compare_SameSizeButDifferentText_ReturnNotEqualButSameSize(string leftText, string rightText, OffsetDetails offsetDetails)
    {
        CompareResult result = TextComparer.Compare(leftText, rightText);

        Assert.False(result.IsEqual);
        Assert.True(result.IsSameSize);

        Assert.Equal(offsetDetails.DifferenceLength, result.OffsetDetails.DifferenceLength);
        Assert.Equal(offsetDetails.OffsetIndexes, result.OffsetDetails.OffsetIndexes);
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