using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    interface IFaceRecognitionService
    {
        Task<bool> RegisterFace(string guid);
        Task<bool> AddFaceImage(string guid, Bitmap image);
        Task<List<string>> DetectFaces(Bitmap image);
    }
}
