using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.DependencyHandling
{
    public static class DependencyHandler
    {
        public static Lazy<IMainService> MainService = new Lazy<IMainService>(() => new MainService());

        public static Lazy<IFaceRecognitionService> FaceRecognitionService = new Lazy<IFaceRecognitionService>(() => MainService.Value.GetFaceRecognitionServiceInstance());

        public static Lazy<IPersonService> PersonService = new Lazy<IPersonService>(() => MainService.Value.GetPersonServiceInstance());

        public static Lazy<ICameraService> CameraService = new Lazy<ICameraService>(() => MainService.Value.GetCameraServiceInstance());

        public static Lazy<IFloorService> FloorService = new Lazy<IFloorService>(() => MainService.Value.GetFloorServiceInstance());

        public static Lazy<IImageService> ImageService = new Lazy<IImageService>(() => new ImageService());
    }
}