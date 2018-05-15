using Bangbezh.Core.Models;
using Bangbezh.Core.Schedule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bangbezh.Cli
{
    public class PrayerScheduler : IPrayerScheduler
    {
        private readonly Timer _timer;
        private readonly PrayerDay _prayerDay;
        private readonly IClock _clock;

        public event EventHandler<PrayerTimeEventArgs> PrayerTime;

        public PrayerScheduler(PrayerDay prayerDay, IClock clock)
        {
            _clock = clock;
            _timer = new Timer(new TimerCallback(Tick), null, 1000 % DateTime.Now.Millisecond, 1000);
            _prayerDay = prayerDay;
        }

        private void Tick(object state)
        {
            var currentDate = _clock.GetNow();
            var tolerance = TimeSpan.FromMilliseconds(500);

            for (var i = PrayerType.Fajr; i <= PrayerType.Isha; i++)
            {
                var prayerDate = _prayerDay.GetPrayerDate(i);

                if (prayerDate.IsEqual(currentDate, tolerance))
                {
                    OnPrayerTime(_prayerDay, i);
                    return; // Multiple Prayer times can never be at the same time
                }
            }
        }

        private void OnPrayerTime(PrayerDay day, PrayerType prayer)
        {
            PrayerTime?.Invoke(this, new PrayerTimeEventArgs(day, prayer));
        }
    }
}
