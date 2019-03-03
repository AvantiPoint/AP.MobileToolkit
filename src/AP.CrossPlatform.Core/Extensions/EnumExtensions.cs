using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AP.CrossPlatform.Extensions
{
    /// <summary>
    /// Enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute on an Enum value.
        /// </summary>
        /// <typeparam name="T">The type of attribute to return.</typeparam>
        /// <param name="enumVal">The Enum to evaluate.</param>
        /// <returns>The specified attribute.</returns>
        public static T GetAttribute<T>( this Enum enumVal ) where T : Attribute
        {
            return enumVal.GetMemberInfo().GetCustomAttribute<T>();
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns>The attributes.</returns>
        /// <param name="enumVal">Enum value.</param>
        public static IEnumerable<Attribute> GetAttributes( this Enum enumVal )
        {
            return enumVal.GetMemberInfo().GetCustomAttributes();
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns>The attributes.</returns>
        /// <param name="enumVal">Enum value.</param>
        /// <typeparam name="TAttribute">Attribute Type to Return.</typeparam>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>( this Enum enumVal )
            where TAttribute : Attribute
        {
            IEnumerable<object> rawResults = enumVal.GetAttributes().Where( attr => attr.GetType() == typeof( TAttribute ) );
            List<TAttribute> results = new List<TAttribute>();

            foreach( object result in rawResults )
            {
                if( result is TAttribute )
                    results.Add( ( TAttribute )result );
            }

            return results;
        }


        /// <summary>
        /// Gets the member info.
        /// </summary>
        /// <returns>The member info.</returns>
        /// <param name="enumVal">Enum value.</param>
        public static MemberInfo GetMemberInfo( this Enum enumVal )
        {
            var typeInfo = enumVal.GetType().GetTypeInfo();
            return typeInfo.DeclaredMembers.First( x => x.Name == enumVal.ToString() );
        }

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <returns>The flags.</returns>
        /// <param name="input">Input.</param>
        public static IEnumerable<Enum> GetFlags( this Enum input )
        {
            foreach ( Enum value in Enum.GetValues( input.GetType() ) )
                if ( input.HasFlag( value ) )
                    yield return value;
        }
    }
}
