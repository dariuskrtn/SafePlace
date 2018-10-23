using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly string _groupId;
        private readonly FaceServiceClient _faceServiceClient;
        private readonly ILogger _logger;
        public FaceRecognitionService(string apiKey, string endpoint, string groupId, ILogger logger)
        {
            _faceServiceClient = new FaceServiceClient(apiKey, endpoint);
            _groupId = groupId;
            _logger = logger;

            
            //CreateGroup();
        }

        //Required only for the first time to register user group.
        private async void CreateGroup()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_groupId, "Users");
        }

        //Adds new face image to registered user and trains user group with already added image.
        public async Task<bool> AddFaceImage(Guid guid, Bitmap image)
        {
            try
            {
                await _faceServiceClient.AddPersonFaceInPersonGroupAsync(_groupId, guid, BitmapToJpegStream(image));
            } catch (Exception ex)
            {
                _logger.LogWarning("Failed to add face image.");
                _logger.LogWarning(ex.Message);
                return false;
            }

            return true;
        }

        public async void TrainAI()
        {
            await _faceServiceClient.TrainPersonGroupAsync(_groupId);
        }

        //Returns IEnumerable of detected faces GUID's.
        public async Task<IEnumerable<Guid>> DetectFaces(Bitmap image)
        {

            Face[] faces = null;

            try
            {
                faces = await _faceServiceClient.DetectAsync(BitmapToJpegStream(image),
                true,
                true,
                new FaceAttributeType[] {
                        FaceAttributeType.Gender,
                        FaceAttributeType.Age,
                        FaceAttributeType.Emotion
                });
            } catch (Exception e)
            {
                _logger.LogWarning("Failed to detect faces.");
                _logger.LogError(e.Message);
            }

            return faces?.Select(item => item.FaceId);
        }

        //Registers new face to API and returns GUID.
        public async Task<Guid> RegisterFace(string name)
        {
            CreatePersonResult person = null;
            try
            {
                person = await _faceServiceClient.CreatePersonInPersonGroupAsync(_groupId, name);
            } catch (Exception ex)
            {
                _logger.LogWarning("Failed to register new face.");
                _logger.LogWarning(ex.Message);
            }

            if (person != null) return person.PersonId;
            return Guid.Empty;
        }

        private MemoryStream BitmapToJpegStream(Bitmap bitmap)
        {
            MemoryStream ms = null;
            ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            return ms;
        }
    }
}
