using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface IMainService
    {
        ILogger GetLoggerInstance();
        SynchronizationContext GetSynchronizationContext();
    }
}
