using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bangbezh.Core.Models;

namespace Bangbezh.Core.Providers
{
    public class FakePrayerTimesProvider : IPrayerTimesProvider
    {
        PrayerDay _day = new PrayerDay(DateTime.Today, new List<TimeSpan>
        {
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 15),
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 20),
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 30),
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 35),
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 40),
           new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 45),
        });

        public PrayerDay GetPrayerTimes(int month, int day)
        {
            return _day;
        }

        public Task<PrayerDay> GetPrayerTimesAsync(int month, int day)
        {
            return Task.FromResult<PrayerDay>(_day);
        }
    }
}
