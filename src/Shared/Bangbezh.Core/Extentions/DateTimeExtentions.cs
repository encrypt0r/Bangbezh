using System;

namespace System
{
    public static class DateTimeExtentions
    {
        public static bool IsEqual(this DateTime first, DateTime second, TimeSpan tolerance)
        {
            var duration = (first - second).Duration();
            return duration <= tolerance;
        }
    }
}
