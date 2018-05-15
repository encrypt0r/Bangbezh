using System;
using System.Collections.Generic;
using System.Text;

namespace Bangbezh.Core.Schedule
{
    public class FakeClock : IClock
    {
        private DateTime _now;

        public void Set(DateTime date)
        {
            _now = date;
        }

        public void Advance(TimeSpan time)
        {
            _now = _now + time;
        }

        public DateTime GetNow()
        {
            return _now;
        }
    }
}
