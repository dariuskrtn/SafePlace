using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = SafePlace.Models;

namespace SafePlace.Service
{
    class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly object _lock = new object();

        private readonly IPersonService _personService;
        private readonly string _groupId;
        private readonly List<FaceServiceClient> _faceServiceClients = new List<FaceServiceClient>();
        private readonly ILogger _logger;
        private int clientId = -1;
        public FaceRecognitionService(string apiKeys, string endpoint, string groupId, ILogger logger, IPersonService personService)
        {
            _personService = personService;
            foreach (var key in apiKeys.Split(';'))
            {
                _faceServiceClients.Add(new FaceServiceClient(key, endpoint));
            }
            _groupId = groupId;
            _logger = logger;
            
            CreateGroup();
        }

        private FaceServiceClient GetNextClient()
        {
            lock (_lock)
            {
                clientId = (clientId + 1) % _faceServiceClients.Count;
                return _faceServiceClients[clientId];
            }
        }

        //Required only for the first time to register user group.
        private async void CreateGroup()
        {
            foreach(var client in _faceServiceClients)
            {
                try
                {
                    await client.CreatePersonGroupAsync(_groupId, "Users");
                } catch (Exception ex)
                {
                    _logger.LogWarning("Failed to create recognition service group.");
                    _logger.LogWarning(ex.Message);
                }
            }
        }

        //Adds new face image to registered user and trains user group with already added image.
        private async void AddFaceImage(Guid guid, Bitmap image, FaceServiceClient client)
        {
            try
            {
                await client.AddPersonFaceInPersonGroupAsync(_groupId, guid, BitmapToJpegStream(image));
            } catch (Exception ex)
            {
                _logger.LogWarning("Failed to add face image.");
                _logger.LogWarning(ex.Message);
            }
        }

        public async Task<bool> TrainAI()
        {
            foreach (var client in _faceServiceClients)
            {
                try
                {
                    await client.TrainPersonGroupAsync(_groupId);
                } catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                    return false;
                }
            }
            return true;
        }

        //Returns IEnumerable of detected faces GUID's.
        private async Task<IEnumerable<Guid>> DetectFaces(Bitmap image, FaceServiceClient currentClient)
        {
            if (image == null) return Enumerable.Empty<Guid>();

            Face[] faces = null;

            try
            {
                faces = await currentClient.DetectAsync(BitmapToJpegStream(image),
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

        public async Task<IEnumerable<Models.Person>> RecognizePeople(Bitmap image)
        {
            var client = GetNextClient();

            var faceIds = await DetectFaces(image, client);
            var people = new Collection<Models.Person>();
            
            if (faceIds != null)
            foreach (var faceId in faceIds)
            {
                var res = await client.IdentifyAsync(_groupId, new Guid[] { faceId });
                Guid? candidate = null;
                
                if (res?.Count()>0 && res[0].Candidates?.Count()>0)
                {
                    candidate = res[0].Candidates[0].PersonId;
                }
                if (candidate == null) continue;
                var person = await client.GetPersonInPersonGroupAsync(_groupId, candidate.Value);
                var personGuid = Guid.Parse(person.Name);

                var personObj = _personService.GetPerson(personGuid);
                if (personObj != null)
                {
                    people.Add(personObj);
                }
            }
            return people.AsEnumerable();
        }

        //Registers new face to API and returns GUID.
        private async Task<Guid> RegisterFace(string guid, FaceServiceClient client)
        {
            CreatePersonResult person = null;
            try
            {
                person = await client.CreatePersonInPersonGroupAsync(_groupId, guid);
            } catch (Exception ex)
            {
                _logger.LogWarning("Failed to register new face.");
                _logger.LogWarning(ex.Message);
            }

            if (person != null) return person.PersonId;
            return Guid.Empty;
        }

        public async Task<bool> RegisterPerson(string guid, IEnumerable<Bitmap> images)
        {
            foreach (var client in _faceServiceClients)
            {
                var serviceGuid = await RegisterFace(guid, client);
                foreach (var img in images)
                {
                    AddFaceImage(serviceGuid, img, client);
                }
            }
            return true;
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
