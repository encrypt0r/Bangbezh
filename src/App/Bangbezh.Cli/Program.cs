using Bangbezh.Core.Models;
using Bangbezh.Core.Providers;
using Bangbezh.Core.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangbezh.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = File.OpenRead("sample.txt");
            IPrayerTimesProvider prayerTimesProvider = new LocalFilePrayerTimesProvider(stream);
            prayerTimesProvider.Initialize();

            var day = prayerTimesProvider.GetPrayerTimes(DateTime.Now.Month, DateTime.Now.Day);
            var clock = new FakeClock();
            clock.Set(day.GetPrayerDate(PrayerType.Fajr));
            var scheduler = new PrayerScheduler(day, clock);

            scheduler.PrayerTime += (s, e) =>
            {
                Console.WriteLine($"It's time for {Enum.GetName(typeof(PrayerType), e.Prayer)}.");
            };

            foreach(var time in day.PrayerTimes)
            {
                Console.WriteLine($"{time:g}");
            }

            Console.ReadLine();
        }
    }
}
