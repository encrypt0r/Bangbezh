using System;
using System.Collections.Generic;
using System.Text;

namespace Bangbezh.Core.Schedule
{
    public class SystemClock : IClock
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
