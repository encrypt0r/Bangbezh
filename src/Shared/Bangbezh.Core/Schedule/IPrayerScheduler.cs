using System;

namespace Bangbezh.Core.Schedule
{
    public interface IPrayerScheduler
    {
        event EventHandler<PrayerTimeEventArgs> PrayerTime;
    }
}