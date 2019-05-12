using System.Collections.Generic;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// Grouping of items by key into ObservableRange
    /// </summary>
    public class Grouping<TKey, TItem> : ObservableRangeCollection<TItem>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Grouping class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="items">Items.</param>
        public Grouping(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            AddRange(items);
        }
    }
}
