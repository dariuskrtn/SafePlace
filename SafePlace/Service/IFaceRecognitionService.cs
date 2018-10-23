using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IFaceRecognitionService
    {
        Task<Guid> RegisterFace(string name);
        Task<bool> AddFaceImage(Guid guid, Bitmap image);
        Task<IEnumerable<Guid>> DetectFaces(Bitmap image);
        void TrainAI();
    }
}
