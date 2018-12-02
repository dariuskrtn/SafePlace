using SafePlace.DTO;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceFaceRecognition.Service
{
    public interface ICameraAnalyzeService
    {
        void Start();
        void Stop();
        IObservable<SpottedPeople> GetCameraUpdateObservable();
    }
}
