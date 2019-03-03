using System.Collections.Generic;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// Grouping of items by key into ObservableRange
    /// </summary>
    public class Grouping<TKey, TSubKey, TItem> : ObservableRangeCollection<TItem>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; private set; }

        /// <summary>
        /// Gets the subkey of the grouping
        /// </summary>
        public TSubKey SubKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Grouping class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="subkey">Subkey</param>
        /// <param name="items">Items.</param>
        public Grouping( TKey key, TSubKey subkey, IEnumerable<TItem> items )
        {
            Key = key;
            SubKey = subkey;
            AddRange( items );
        }
    }
}
