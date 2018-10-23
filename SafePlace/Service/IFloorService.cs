﻿using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface IFloorService
    {
        void Add(Floor floor);
        Floor GetFloor(Guid guid);
        Floor CreateFloor();
        Floor CreateEmptyFloor(string name);
        Floor CreateFloor(string path);
        IEnumerable<Floor> GetFloorList();
    }
}
