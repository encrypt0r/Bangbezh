using Bangbezh.Core.Models;
using System.Threading.Tasks;

namespace Bangbezh.Core.Providers
{
    public interface IPrayerTimesProvider
    {
        PrayerDay GetPrayerTimes(int month, int day);
        Task<PrayerDay> GetPrayerTimesAsync(int month, int day);
    }
}
