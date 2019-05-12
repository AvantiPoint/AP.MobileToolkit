using System;

namespace AP.CrossPlatform.Extensions
{
    /// <summary>
    /// Date time extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Subtracts the months.
        /// </summary>
        /// <returns>The months.</returns>
        /// <param name="dt">Dt.</param>
        /// <param name="months">Months.</param>
        public static DateTime SubtractMonths(this DateTime dt, int months) => dt.AddMonths(months * -1);

        /// <summary>
        /// Subtracts the days.
        /// </summary>
        /// <returns>The days.</returns>
        /// <param name="dt">Dt.</param>
        /// <param name="days">Days.</param>
        public static DateTime SubtractDays(this DateTime dt, double days) => dt.AddDays(days * -1);

        /// <summary>
        /// Subtracts the hours.
        /// </summary>
        /// <returns>The hours.</returns>
        /// <param name="dt">Dt.</param>
        /// <param name="hours">Hours.</param>
        public static DateTime SubtractHours(this DateTime dt, double hours) => dt.AddHours(hours * -1);

        /// <summary>
        /// Subtracts the minutes.
        /// </summary>
        /// <returns>The minutes.</returns>
        /// <param name="dt">Dt.</param>
        /// <param name="minutes">Minutes.</param>
        public static DateTime SubtractMinutes(this DateTime dt, double minutes) => dt.AddMinutes(minutes * -1);
    }
}
