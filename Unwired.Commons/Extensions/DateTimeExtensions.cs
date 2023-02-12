using Unwired.Commons.Enumarators;

namespace Unwired.Commons.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Set the Kind of a datetime
        /// </summary>
        /// <param name="value">A nullable datetime</param>
        /// <param name="kind">DateTimeKind. Default: Utc </param>
        /// <returns>A nullable datetime.</returns>
        public static DateTime? SetKind(this DateTime? value, DateTimeKind kind = DateTimeKind.Utc)        
            => (value.HasValue) ? value.Value.SetKind(kind) : null;

        /// <summary>
        /// Set the Kind of a datetime
        /// </summary>
        /// <param name="value">A datetime</param>
        /// <param name="kind">DateTimeKind. Default: Utc </param>
        /// <returns>A datetime.</returns>
        public static DateTime SetKind(this DateTime value, DateTimeKind kind = DateTimeKind.Utc)        
            => (value.Kind.Equals(kind)) ? value : DateTime.SpecifyKind(value, kind);

        /// <summary>
        /// Validate whether a datetime is null. It will be considered null if the datetime value is the MinValue
        /// </summary>
        /// <param name="value">A datetime</param>
        /// <returns>A bool value</returns>
        public static bool IsNullOrEmpty(this DateTime value)
            => value == DateTime.MinValue;
  
        /// <summary>
        /// Validate whether a datetime is null. It will be considered null if the datetime value is Null or the MinValue
        /// </summary>
        /// <param name="value">A datetime</param>
        /// <returns>A bool value</returns>
        public static bool IsNullOrEmpty(this DateTime? value)
            => value == DateTime.MinValue || value == null;

        /// <summary>
        /// Convert a nullable datetime to non-nullable <br/>
        /// </summary>
        /// <param name="value">A datetime</param>
        /// <param name="kind">DateTimeKind. Default: Utc </param>
        /// <param name="ifNullReturnCurrentDate">If the value of datetime is null, should it return the current date? Default: true </param>
        /// <param name="changeKindIfDifferent">Change kind if it is different between what is in the value and what was passed by parameter. Default: True </param>
        /// <returns>A bool value</returns>
        public static DateTime ToDate(this DateTime? value, DateTimeKind kind = DateTimeKind.Utc, bool ifNullReturnCurrentDate = true, bool changeKindIfDifferent = true)
        {
            DateTime result;
            try
            {
                if (value == null)
                    result = UnwiredMethods.Now(kind);
                else
                    result = (DateTime)value;                
            }
            catch
            {

                if (ifNullReturnCurrentDate)
                    result = UnwiredMethods.Now(kind);
                else
                    result = DateTime.MinValue;                
            }

            if (changeKindIfDifferent && result.Kind != kind)
                result = result.SetKind(kind);

            return result;
        }

        /// <summary>
        /// Checks if the date is within a range of dates <br/>
        /// </summary>
        /// <param name="value">A datetime</param>
        /// <param name="lower">A lower datetime of períod</param>
        /// <param name="upper">A upper datetime of períod</param>
        /// <param name="dateOnly">If true, time is discarded in the comparison. Default: True </param>
        /// <returns>A bool value</returns>
        public static bool Between(this DateTime value, DateTime lower, DateTime upper, bool dateOnly = true)
        {
            if (dateOnly)
            {
                value = value.Date;
                lower = lower.Date;
                upper = upper.Date;
            }

            return value >= lower && value <= upper;
        }

        /// <summary>
        /// Calculate elapsed time in days between two dates
        /// </summary>
        /// <param name="value">Final datetime</param>
        /// <param name="baseDate">Inicial datetime</param>
        /// <param name="roundType">The round type. Default: Floor</param>
        /// <returns>a Int</returns>
        public static TResult ElapsedDays<TResult>(this DateTime value, DateTime baseDate, RoundType roundType = RoundType.Floor, int precision = 2)
            => CalculateElapsedTime(baseDate, value).TotalDays.ToGenericType<TResult>(roundType, precision);
        
        /// <summary>
        /// Calculate elapsed time in Hours between two dates
        /// </summary>
        /// <param name="value">Final datetime</param>
        /// <param name="baseDate">Inicial datetime</param>
        /// <param name="roundType">The round type. Default: Floor</param>
        /// <returns>a Int</returns>
        public static TResult ElapsedHours<TResult>(this DateTime value, DateTime baseDate, RoundType roundType = RoundType.Floor, int precision = 2)
            => CalculateElapsedTime(baseDate, value).TotalHours.ToGenericType<TResult>(roundType, precision);
        public static TResult ElapsedMinutes<TResult>(this DateTime value, DateTime baseDate, RoundType roundType = RoundType.Floor, int precision = 2)
            => CalculateElapsedTime(baseDate, value).TotalMinutes.ToGenericType<TResult>(roundType, precision);
        public static TResult ElapsedSeconds<TResult>(this DateTime value, DateTime baseDate, RoundType roundType = RoundType.Floor, int precision = 2)
            => CalculateElapsedTime(baseDate, value).TotalSeconds.ToGenericType<TResult>(roundType, precision);
        private static TimeSpan CalculateElapsedTime(DateTime? dateBase = null, DateTime? currentDate = null)
        {
            if (dateBase.IsNullOrEmpty())            
                dateBase = UnwiredMethods.Now().Date;

            if (currentDate.IsNullOrEmpty())
                currentDate = UnwiredMethods.Now().Date;

            var startDate = dateBase.ToDate();
            var endDate = currentDate.ToDate();

            if (endDate < startDate)
                return TimeSpan.Zero;

            if (startDate.Kind != endDate.Kind)
            {
                startDate = startDate.ToUniversalTime();
                endDate = endDate.ToUniversalTime();
            }

            return endDate - startDate;
        }
    }
}