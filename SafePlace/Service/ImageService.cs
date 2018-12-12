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
        //Images are stored and read from the c:/Images folder.
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
            //string HomeFolderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            String filePath = $"{ImagesPath}/{id.ToString()}.jpg";
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
            //string HomeFolderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            String filePath = $"{ImagesPath}/{id.ToString()}.jpg";

            Image image = Image.FromStream(ImageStream);

            image.Save(filePath);

            return id;
        }
        //Returns the byte array of an image, which is named as {id}.jpg.
        public byte[] GetFaceImage(Guid id, int number)
        {
            //The path of current users home folder. In windows it's similar to: C:/users/myUser
            //string HomeFolderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            String filePath = $"{ImagesPath}/{id.ToString()}{number.ToString()}.jpg";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream.ToArray();

        }
        //A saved face image receives
        public void SaveFaceImage(Stream ImageStream, Guid personID, int number)
        {
            //string HomeFolderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            String filePath = $"{ImagesPath}/{personID.ToString()}{number.ToString()}.jpg";

            Image image = Image.FromStream(ImageStream);

            image.Save(filePath);
        }
    }
}
