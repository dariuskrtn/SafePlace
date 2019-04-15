using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public interface IImageService
    {
        Guid SaveImage(Stream ImageStream);
        Byte[] GetImage(Guid id);
        void SaveFaceImage(Stream ImageStream, Guid personID, int number);
        Byte[] GetFaceImage(Guid id, int number);
        bool DeleteImage(Guid id);
    }
}
