namespace BusinessLogic
{
    public class OffsetDetails
    {
        public List<int> OffsetIndexes { get; set; } = new List<int>();

        public int DifferenceLength => OffsetIndexes.Count;
    }
}
