namespace BusinessLogic
{
    public static class TextComparer
    {

        /// <summary>
        /// Compare texts. 
        /// If it is equal - returns IsEqual = true, IsSameSize = true
        /// if it is not equal size - returns IsEqual = false, IsSameSize = false without OffsetDetails.
        /// If it is not equal and the same size - returns IsEqual = false, IsSameSize = true and offsets.
        /// </summary>
        /// <param name="leftText">left text to compare</param>
        /// <param name="rightText">right text to compare</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">text to compare is null</exception>
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

            if (leftText.Length != rightText.Length)
            {
                return new CompareResult() { IsEqual = false, IsSameSize = false };
            }

            var compareDetails = new OffsetDetails();
            for (int i = 0; i < leftText.Length; i++)
            {
                if (leftText[i] != rightText[i])
                {
                    compareDetails.OffsetIndexes.Add(i);
                }
            }

            return new CompareResult() { IsEqual = false, IsSameSize = true, OffsetDetails = compareDetails };
        }
    }
}