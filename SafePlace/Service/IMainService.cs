﻿using SafePlace.Models;
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
        IFloorService GetFloorServiceInstance();
        ICameraService GetCameraServiceInstance();
        IPersonService GetPersonServiceInstance();
    }
}
