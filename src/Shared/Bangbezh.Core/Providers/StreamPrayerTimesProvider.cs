using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bangbezh.Core.Models;

namespace Bangbezh.Core.Providers
{
    public class StreamPrayerTimesProvider : IPrayerTimesProvider
    {
        #region Fields
        private readonly Stream _data;
        private bool _initialized;
        private readonly Dictionary<int, PrayerDay> _days = new Dictionary<int, PrayerDay>();
        #endregion

        #region Constructor
        public StreamPrayerTimesProvider(Stream data)
        {
            _data = data;
        }
        #endregion

        #region Methods
        public PrayerDay GetPrayerTimes(int month, int day)
        {
            if (_days == null)
                throw new InvalidOperationException("Provider is not initialized.");

            var key = Hash(month, day);
            if (_days.ContainsKey(key))
                return _days[key];

            throw new ArgumentException($"No prayer times found for the specified date ({day}/{month}).");
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;

            using (_data)
            using (var reader = new StreamReader(_data))
            {
                int lineNumber = 1;
                var year = DateTime.Now.Year;

                while (true)
                {
                    var line = await reader.ReadLineAsync();
                    if (line == null)
                        break;

                    ParseLine(line, lineNumber, year);
                }
            }

            _initialized = true;
        }

        public void Initialize()
        {
            if (_initialized) return;

            using (_data)
            using (var reader = new StreamReader(_data))
            {
                int lineNumber = 1;
                var year = DateTime.Now.Year;

                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;

                    ParseLine(line, lineNumber, year);
                }
            }

            _initialized = true;
        }
        #endregion

        #region PrivateMethods
        private int Hash(int month, int day)
        {
            return (month * 1000) + day;
        }

        private void ParseLine(string line, int lineNumber, int year)
        {
            var parts = line.Split('\t');

            if (parts.Length != Constants.PrayersTimesCount + 2)
                throw new FormatException($"Invalid number of tokens ({parts.Length}) on line {lineNumber}.");

            var prayers = new List<Prayer>(Constants.PrayersTimesCount);

            var successful = int.TryParse(parts[0], out var month);
            successful = int.TryParse(parts[1], out var day) ? successful : false;

            if (!successful)
                throw new FormatException($"Invalid token on line {lineNumber}.");

            var type = PrayerType.Fajr;
            for (int i = 2; i < parts.Length; i++)
            {
                var values = parts[i].Split(':');

                if (values.Length != 2)
                    throw new FormatException($"Invalid prayer time on line {lineNumber}.");

                successful = int.TryParse(values[0], out var hour) ? successful : false;
                successful = int.TryParse(values[1], out var minute) ? successful : false;

                if (!successful)
                    throw new FormatException($"Invalid prayer time on line {lineNumber}.");

                prayers.Add(new Prayer(type++, new TimeSpan(hour, minute, 0)));
            }

            try
            {
                _days.Add(Hash(month, day), new PrayerDay(new DateTime(year, month, day), prayers));
            }
            catch (ArgumentOutOfRangeException)
            {
                // Most years don't have 29th of February, just ignore it
            }
        }
        #endregion
    }
}
