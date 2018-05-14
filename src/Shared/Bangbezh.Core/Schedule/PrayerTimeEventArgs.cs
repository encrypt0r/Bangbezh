using Bangbezh.Core.Models;
using System;

namespace Bangbezh.Core.Schedule
{
    public class PrayerTimeEventArgs : EventArgs
    {
        public PrayerTimeEventArgs(PrayerDay day, PrayerType prayer)
        {
            Day = day;
            Prayer = prayer;
        }

        public PrayerDay Day { get; }
        public PrayerType Prayer { get; }
    }
}
