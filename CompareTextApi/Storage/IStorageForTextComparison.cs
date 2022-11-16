namespace CompareTextApi.Storage
{
    /// <summary>
    /// Storage for left and right text to compare
    /// </summary>
    public interface IStorageForTextComparison
    {
        /// <summary>
        /// Add left side text to storage
        /// </summary>
        /// <param name="id">Id of the input</param>
        /// <param name="text">Text</param>
        public void AddLeftSideText(Guid id, string text);

        /// <summary>
        /// Add right side text to storage
        /// </summary>
        /// <param name="id">Id of the input</param>
        /// <param name="text">Text</param>
        public void AddRightSideText(Guid id, string text);

        /// <summary>
        /// Get left side text from storage 
        /// </summary>
        /// <param name="id">Id of the input</param>
        /// <returns>Text</returns>
        public string GetLeftSideText(Guid id);

        /// <summary>
        /// Get right side text from storage 
        /// </summary>
        /// <param name="id">Id of the input</param>
        /// <returns>Text</returns>
        public string GetRightSideText(Guid id);
    }
}
