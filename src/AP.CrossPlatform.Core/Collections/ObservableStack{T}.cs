using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// Observable stack.
    /// </summary>
    public class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.CrossPlatform.Collections.ObservableStack`1"/> class.
        /// </summary>
        public ObservableStack()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.CrossPlatform.Collections.ObservableStack`1"/> class.
        /// </summary>
        /// <param name="collection">Collection.</param>
        public ObservableStack( IEnumerable<T> collection )
        {
            foreach ( var item in collection )
                base.Push( item );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.CrossPlatform.Collections.ObservableStack`1"/> class.
        /// </summary>
        /// <param name="list">List.</param>
        public ObservableStack( List<T> list )
        {
            foreach ( var item in list )
                base.Push( item );
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public new virtual void Clear()
        {
            base.Clear();
            this.OnCollectionChanged( new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        /// <summary>
        /// Pop this instance.
        /// </summary>
        /// <returns>The pop.</returns>
        public new virtual T Pop()
        {
            var item = base.Pop();
            this.OnCollectionChanged( new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Remove, item ) );
            return item;
        }

        /// <summary>
        /// Push the specified item.
        /// </summary>
        /// <returns>The push.</returns>
        /// <param name="item">Item.</param>
        public new virtual void Push( T item )
        {
            base.Push( item );
            this.OnCollectionChanged( new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Add, item ) );
        }

        /// <summary>
        /// Occurs when collection changed.
        /// </summary>
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// On the collection changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected virtual void OnCollectionChanged( NotifyCollectionChangedEventArgs e )
        {
            this.RaiseCollectionChanged( e );
        }

        /// <summary>
        /// On the property changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected virtual void OnPropertyChanged( PropertyChangedEventArgs e )
        {
            this.RaisePropertyChanged( e );
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        private void RaiseCollectionChanged( NotifyCollectionChangedEventArgs e )
        {
            this.CollectionChanged?.Invoke( this, e );
        }

        private void RaisePropertyChanged( PropertyChangedEventArgs e )
        {
            this.PropertyChanged?.Invoke( this, e );
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this.PropertyChanged += value; }
            remove { this.PropertyChanged -= value; }
        }
    }
}