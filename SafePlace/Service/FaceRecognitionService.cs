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
    class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly string _groupId;
        private readonly FaceServiceClient _faceServiceClient;
        private readonly ILogger _logger;
        public FaceRecognitionService(string apiKey, string endpoint, string groupId, ILogger logger)
        {
            _faceServiceClient = new FaceServiceClient(apiKey, endpoint);
            _groupId = groupId;
            _logger = logger;

            
            CreateGroup();
        }

        //Required only for the first time to register user group.
        private async void CreateGroup()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_groupId, "Users");
        }

        public async Task<bool> AddFaceImage(Guid guid, Bitmap image)
        {
            MemoryStream ms = null;
            ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            await _faceServiceClient.AddPersonFaceInPersonGroupAsync(_groupId, guid, ms);

            return true;
        }

        public async Task<IEnumerable<Guid>> DetectFaces(Bitmap image)
        {
            MemoryStream ms = null;
            ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            var faces = await _faceServiceClient.DetectAsync(ms,
                        true,
                        true,
                        new FaceAttributeType[] {
                    FaceAttributeType.Gender,
                    FaceAttributeType.Age,
                    FaceAttributeType.Emotion
                        });
            return faces.Select(item => item.FaceId);
        }

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
    }
}
