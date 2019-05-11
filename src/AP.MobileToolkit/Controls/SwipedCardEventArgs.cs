namespace AP.MobileToolkit.Controls
{
    /// <summary>Arguments for swipe events.</summary>
    public class SwipedCardEventArgs : System.EventArgs
    {
        public SwipedCardEventArgs(object item, object parameter, SwipeCardDirection direction)
        {
            Item = item;
            Parameter = parameter;
            Direction = direction;
        }

        /// <summary>Gets the item parameter.</summary>
        public object Item { get; }

        /// <summary>Gets the command parameter.</summary>
        public object Parameter { get; }

        /// <summary>Gets the direction of the swipe.</summary>
        public SwipeCardDirection Direction { get; }
    }
}
