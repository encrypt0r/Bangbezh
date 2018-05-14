using System;
using System.Collections.Generic;

namespace Bangbezh.Core.Models
{
    public class PrayerDay
    {
        public PrayerDay(DateTime date, IReadOnlyList<TimeSpan> times)
        {
            if (times.Count != Constants.PrayersTimesCount)
                throw new ArgumentException("You have to provide all of the prayer times as well as sunrise time.");

            Date = date;
            PrayerTimes = times;
        }

        public DateTime Date { get; }
        public IReadOnlyList<TimeSpan> PrayerTimes { get; }

        public TimeSpan Fajr => GetPrayer(PrayerType.Fajr);
        public TimeSpan Sunrise => GetPrayer(PrayerType.Sunrise);
        public TimeSpan Dhuhur => GetPrayer(PrayerType.Dhuhur);
        public TimeSpan Asr => GetPrayer(PrayerType.Asr);
        public TimeSpan Maghrib => GetPrayer(PrayerType.Maghrib);
        public TimeSpan Isha => GetPrayer(PrayerType.Isha);

        public TimeSpan GetPrayer(PrayerType prayer)
        {
            return PrayerTimes[(int)prayer];
        }
        public DateTime GetPrayerDate(PrayerType prayer)
        {
            var time = GetPrayer(prayer);
            return Date.AddSeconds(time.TotalSeconds);
        }
    }
}
