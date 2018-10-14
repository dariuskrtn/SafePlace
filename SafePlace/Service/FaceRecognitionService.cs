using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Task<bool> AddFaceImage(string guid, Bitmap image)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> DetectFaces(Bitmap image)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterFace(string guid)
        {
            try
            {
                CreatePersonResult person = await _faceServiceClient.CreatePersonInPersonGroupAsync(_groupId, guid);
            } catch (Exception ex)
            {
                _logger.LogWarning("Failed to register new face.");
                _logger.LogWarning(ex.Message);
                return false;
            }
            return true;
        }
    }
}
