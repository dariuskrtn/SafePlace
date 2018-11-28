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
        private const string ImagesPath = "/Images";
        
        //Deletes an image from the server given its id.
        public bool DeleteImage(Guid id)
        {
            String filePath = $"{ImagesPath}{id.ToString()}.jpg";
            if (!File.Exists(filePath)) return false;
            File.Delete(filePath);
            return true;
        }

        //Returns the byte array of an image, which is named as {id}.jpg.
        public byte[] GetImage(Guid id)
        {
            //The path of current users home folder. In windows it's similar to: C:/users/myUser
            string HomeFolderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            String filePath = $"{HomeFolderPath}{ImagesPath}/{id.ToString()}.jpg";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream.ToArray();

        }
        //Saves the image in a specified place inside the server.
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
