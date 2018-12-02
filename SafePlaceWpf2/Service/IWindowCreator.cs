using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceWpf.Service
{
    public interface IWindowCreator
    {
        void CreateCameraWindow(Camera camera);
    }
}
