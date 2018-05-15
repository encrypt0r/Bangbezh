using System;

namespace Bangbezh.Core.Models
{
    public class Prayer
    {
        public Prayer(PrayerType type, TimeSpan time)
        {
            Type = type;
            Time = time;
        }

        public TimeSpan Time { get; }
        public PrayerType Type { get; set; }

        public DateTime GetDate()
        {
            return DateTime.Today.Add(Time);
        }

        public DateTime GetDate(int year, int month, int day)
        {
            return new DateTime(year, month, day).Add(Time);
        }
    }
}
