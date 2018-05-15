using System;
using System.Collections.Generic;
using System.Text;

namespace Bangbezh.Core.Schedule
{
    public interface IClock
    {
        DateTime GetNow();
    }
}
