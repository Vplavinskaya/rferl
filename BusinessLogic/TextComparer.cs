namespace BusinessLogic
{
    public static class TextComparer
    {
        public static CompareResult Compare(string leftText, string rightText)
        {
            if (leftText == null)
            {
                throw new ArgumentException($"{nameof(leftText)} cannot be null", nameof(leftText));
            }

            if (rightText == null)
            {
                throw new ArgumentException($"{nameof(leftText)} cannot be null", nameof(leftText));
            }

            if (leftText.Equals(rightText))
            {
                return new CompareResult() { IsEqual = true, IsSameSize = true };
            }

            if (leftText.Length == rightText.Length)
            {
                return new CompareResult() { IsEqual = false, IsSameSize = true };
            }

            // TODO: implement finding offsets
            return new CompareResult() { IsEqual = false, IsSameSize = false, Details = null };
        }
    }
}