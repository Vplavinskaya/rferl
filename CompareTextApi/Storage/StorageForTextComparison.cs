using Microsoft.Extensions.Caching.Memory;

namespace CompareTextApi.Storage
{
    /// <inheritdoc />
    public class StorageForTextComparison: IStorageForTextComparison
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Create storage for left and right text to compare using memory cache
        /// </summary>
        /// <param name="memoryCache"></param>
        public StorageForTextComparison(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private string LeftSideTextKey(Guid id)
        {
            return $"{id}-left";
        }

        private string RightSideTextKey(Guid id)
        {
            return $"{id}-right";
        }

        /// <inheritdoc />
        public void AddLeftSideText(Guid id, string text)
        {
            _memoryCache.Set(LeftSideTextKey(id), text);
        }

        /// <inheritdoc />
        public void AddRightSideText(Guid id, string text)
        {
            _memoryCache.Set(RightSideTextKey(id), text);
        }

        /// <inheritdoc />
        public string GetLeftSideText(Guid id)
        {
            _memoryCache.TryGetValue<string>(LeftSideTextKey(id), out string? leftText);
            return leftText;
        }

        /// <inheritdoc />
        public string GetRightSideText(Guid id)
        {
            _memoryCache.TryGetValue<string>(RightSideTextKey(id), out string? rightText);
            return rightText;
        }
    }
}
