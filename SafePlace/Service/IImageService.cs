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
        bool DeleteImage(Guid id);
    }
}
