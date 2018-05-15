using Bangbezh.Core.Models;
using Bangbezh.Core.Providers;
using Bangbezh.Core.Schedule;
using Bangbezh.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bangbezh.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields
        private readonly PrayerScheduler _scheduler;
        private IPrayerTimesProvider _provider;
        private Timer _timer;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            var clock = new SystemClock();
            Initialize();
        }
        #endregion

        #region Properties
        public PrayerDay Day { get; private set; }

        private TimeSpan _nextPrayerRemainingTime;
        public TimeSpan NextPrayerRemainingTime
        {
            get { return _nextPrayerRemainingTime; }
            set { SetProperty(ref _nextPrayerRemainingTime, value); }
        }

        private PrayerType _nextPrayerType;
        public PrayerType NextPrayerType
        {
            get { return _nextPrayerType; }
            set { SetProperty(ref _nextPrayerType, value); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            var stream = File.OpenRead("sample.txt");
            _provider = new StreamPrayerTimesProvider(stream);
            await _provider.InitializeAsync();
            Day = _provider.GetPrayerTimes(DateTime.Now.Month, DateTime.Now.Day);
            _timer = new Timer(new TimerCallback(Tick), null, 0, 1000);

            RaisePropertyChanged(nameof(Day));
        }

        private void Tick(object state)
        {
            var nextPrayer = Day.Prayers.Where(p => DateTime.Now.TimeOfDay > p.Time).HasMinOrDefault(p => p.Time);

            NextPrayerType = nextPrayer.Type;
            NextPrayerRemainingTime = Day.GetRemainingTimeTo(nextPrayer.Type);
        }
        #endregion
    }
}
