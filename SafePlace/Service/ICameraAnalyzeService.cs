using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface ICameraAnalyzeService
    {
        void Start();
        void Stop();
        IObservable<Camera> GetCameraUpdateObservable();
    }
}
