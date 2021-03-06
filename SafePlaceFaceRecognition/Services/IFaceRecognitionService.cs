﻿using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceFaceRecognition.Service
{
    public interface IFaceRecognitionService
    {
        Task<bool> TrainAI();
        Task<IEnumerable<Guid>> RecognizePeople(Bitmap image);
        Task<bool> RegisterPerson(string guid, IEnumerable<Bitmap> images);
    }
}
