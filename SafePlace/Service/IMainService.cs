using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IMainService
    {
        ILogger GetLoggerInstance();
        IPageCreator GetPageCreatorInstance();
        SynchronizationContext GetSynchronizationContext();
        IFloorService GetFloorServiceInstance();
        ICameraService GetCameraServiceInstance();
        IPersonService GetPersonServiceInstance();
        IWindowCreator GetWindowCreatorInstance();
    }
}
