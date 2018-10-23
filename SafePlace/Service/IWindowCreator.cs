using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IWindowCreator
    {
        void CreateCameraWindow(Camera camera);
    }
}
