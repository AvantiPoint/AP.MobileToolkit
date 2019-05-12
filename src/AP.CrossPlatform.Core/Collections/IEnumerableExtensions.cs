using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// IEnumerable extensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Converts <see cref="IEnumerable{T}"/> to a <see cref="IReadOnlyCollection{T}"/>
        /// </summary>
        /// <param name="enumerable">Enumerable object.</param>
        /// <typeparam name="T">Type of item.</typeparam>
        /// <returns>IReadOnlyCollection{T}</returns>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>((enumerable as IList<T>) ?? enumerable.ToList());
        }

        /// <summary>
        /// Allows you to carryout a specified action for each item in a collection of IEnumerable
        /// </summary>
        /// <typeparam name="T">The type in the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to perform.</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Performs a specified action on each element of an IEnumerable
        /// </summary>
        /// <param name="collection">Collection.</param>
        /// <param name="action">Action.</param>
        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Counts the specified collection. Returns 0 if the collection is null
        /// </summary>
        /// <returns>The count.</returns>
        /// <param name="collection">Collection.</param>
        public static int Count(this IEnumerable collection) =>
            collection == null ? 0 : collection.Cast<object>().Count();

        /// <summary>
        /// Returns the index of the specified object in the collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="obj">The object.</param>
        /// <returns>If found returns index otherwise -1</returns>
        public static int IndexOf(this IEnumerable collection, object obj)
        {
            int index = -1;

            var enumerator = collection.GetEnumerator();
            enumerator.Reset();
            int i = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == obj)
                {
                    index = i;
                    break;
                }

                i++;
            }

            return index;
        }

        /// <summary>
        /// Gets the previous element.
        /// </summary>
        /// <returns>The previous element.</returns>
        /// <param name="collection">Collection.</param>
        /// <param name="element">Element.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetPreviousElement<T>(this IEnumerable<T> collection, T element)
        {
            int index = collection.IndexOf(element) - 1;

            if (index < 0)
            {
                return collection.FirstOrDefault();
            }

            return collection.ElementAt(index);
        }

        /// <summary>
        /// Gets the next element.
        /// </summary>
        /// <returns>The next element.</returns>
        /// <param name="collection">Collection.</param>
        /// <param name="element">Element.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetNextElement<T>(this IEnumerable<T> collection, T element)
        {
            int index = collection.IndexOf(element) + 1;

            if (index >= collection.Count())
            {
                return collection.LastOrDefault();
            }

            return collection.ElementAt(index);
        }

        /// <summary>
        /// Nullables the count.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>System.Int32.</returns>
        public static int NullableCount<T>(this IEnumerable<T> list)
        {
            return list == null ? 0 : list.Count();
        }

        /// <summary>
        /// Allows Null Safe IEnumerable Counts
        /// </summary>
        /// <returns>The count.</returns>
        /// <param name="enumerable">Enumerable.</param>
        public static int NullableCount(this IEnumerable enumerable) =>
            enumerable.Cast<object>().NullableCount<object>();

        /// <summary>
        /// Determines whether [is null or empty] [the specified list].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified list]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() == 0;
        }

        /// <summary>
        /// Indicates if the IEnumerable is Null or Empty
        /// </summary>
        /// <returns><c>true</c>, if null or empty was ised, <c>false</c> otherwise.</returns>
        /// <param name="collection">Collection.</param>
        public static bool IsNullOrEmpty(this IEnumerable collection) =>
            collection == null || collection.Count() == 0;

        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        public static void AddIfNotExists<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Joins the specified seperator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>System.String.</returns>
        public static string Join<T>(this IEnumerable<T> values, string seperator)
        {
            var sb = new StringBuilder();
            foreach (var value in values)
            {
                if (sb.Length > 0)
                {
                    sb.Append(seperator);
                }

                sb.Append(value);
            }
            return sb.ToString();
        }
    }
}
