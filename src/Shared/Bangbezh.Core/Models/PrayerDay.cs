using System;
using System.Collections.Generic;

namespace Bangbezh.Core.Models
{
    public class PrayerDay
    {
        public PrayerDay(DateTime date, IReadOnlyList<Prayer> prayers)
        {
            if (prayers.Count != Constants.PrayersTimesCount)
                throw new ArgumentException("You have to provide all of the prayer times as well as sunrise time.");

            Date = date;
            Prayers = prayers;
        }

        public DateTime Date { get; }
        public IReadOnlyList<Prayer> Prayers { get; }

        public Prayer Fajr => GetPrayer(PrayerType.Fajr);
        public Prayer Sunrise => GetPrayer(PrayerType.Sunrise);
        public Prayer Dhuhur => GetPrayer(PrayerType.Dhuhur);
        public Prayer Asr => GetPrayer(PrayerType.Asr);
        public Prayer Maghrib => GetPrayer(PrayerType.Maghrib);
        public Prayer Isha => GetPrayer(PrayerType.Isha);

        public Prayer GetPrayer(PrayerType prayer)
        {
            return Prayers[(int)prayer];
        }

        public DateTime GetPrayerDate(PrayerType type)
        {
            var prayer = GetPrayer(type);
            return prayer.GetDate(Date.Year, Date.Month, Date.Day);
        }

        public bool HasTimePassed(PrayerType type)
        {
            var prayer = GetPrayer(type);
            return (DateTime.Now - GetPrayerDate(type)).CompareTo(TimeSpan.Zero) > 0;
        }

        public TimeSpan GetRemainingTimeTo(PrayerType type)
        {
            var prayer = GetPrayer(type);

            if (HasTimePassed(type))
            {
                var tomorrow = Date.AddDays(1);
                return (DateTime.Now - prayer.GetDate(tomorrow.Year, tomorrow.Month, tomorrow.Day)).Duration();
            }
            else
            {
                return (DateTime.Now - GetPrayerDate(type)).Duration();
            }
        }
    }
}
