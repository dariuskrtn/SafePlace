using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public class ImageService : IImageService
    {
        private const string ImagesPath = "Images/";

        public bool DeleteImage(Guid id)
        {
            String filePath = $"{ImagesPath}{id.ToString()}.jpg";
            if (!File.Exists(filePath)) return false;
            File.Delete(filePath);
            return true;
        }

        public byte[] GetImage(Guid id)
        {
            String filePath = $"{ImagesPath}{id.ToString()}.jpg";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream.ToArray();

        }

        public Guid SaveImage(Stream ImageStream)
        {
            Guid id = Guid.NewGuid();
            String filePath = $"{ImagesPath}{id.ToString()}.jpg";

            Image image = Image.FromStream(ImageStream);

            image.Save(filePath);

            return id;
        }
    }
}
