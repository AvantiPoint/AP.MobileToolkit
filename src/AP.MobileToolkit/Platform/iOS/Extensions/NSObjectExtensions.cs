using Foundation;

namespace AP.MobileToolkit.Platform.iOS.Extensions
{
    /// <summary>
    /// NSObject extensions.
    /// </summary>
    public static class NSObjectExtensions
    {
        /// <summary>
        /// Converts System.Object to Foundation.NSObject
        /// </summary>
        /// <returns>The NSO bject.</returns>
        /// <param name="obj">Object.</param>
        public static NSObject ToNSObject( this object obj )
        {
            return NSObject.FromObject( obj );
        }

        /// <summary>
        /// Converts Foundation.NSObject to System.Int32 (int)
        /// </summary>
        /// <returns>The int.</returns>
        /// <param name="obj">Object.</param>
        public static int ToInt( this NSObject obj )
        {
            if( obj == null ) return default( int );

            int result = 0;
            int.TryParse( obj.ToString(), out result );
            return result;
        }

        /// <summary>
        /// Converts Foundation.NSObject to System.Boolean (bool)
        /// </summary>
        /// <returns><c>true</c>, if bool was toed, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        public static bool ToBool( this NSObject obj )
        {
            if ( obj == null ) return default( bool );

            bool result = false;
            bool.TryParse( obj.ToString(), out result );
            return result;
        }
    }
}
