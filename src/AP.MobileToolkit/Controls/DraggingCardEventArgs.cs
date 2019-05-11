using System;
using System.Collections.Generic;
using System.Text;

namespace AP.MobileToolkit.Controls
{
    /// <summary>Arguments for swipe events.</summary>
    public class DraggingCardEventArgs : System.EventArgs
    {
        public DraggingCardEventArgs(object item, object parameter, SwipeCardDirection direction, DraggingCardPosition position, double distanceDraggedX, double distanceDraggedY)
        {
            Item = item;
            Parameter = parameter;
            Direction = direction;
            Position = position;
            DistanceDraggedX = distanceDraggedX;
            DistanceDraggedY = distanceDraggedY;
        }

        /// <summary>Gets the item parameter.</summary>
        public object Item { get; }

        /// <summary>Gets the command parameter.</summary>
        public object Parameter { get; }

        /// <summary>Gets the direction of the swipe.</summary>
        public SwipeCardDirection Direction { get; }

        /// <summary>Gets the dragging position.</summary>
        public DraggingCardPosition Position { get; }

        /// <summary>Gets the distance dragged on X axis.</summary>
        public double DistanceDraggedX { get; }

        /// <summary>Gets the distance dragged on Y axis.</summary>
        public double DistanceDraggedY { get; }
    }
}
