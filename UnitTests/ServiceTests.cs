using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafePlace.Models;
using SafePlace.Service;
using System;
using System.Linq;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void MainServiceTest_GetCameraServiceInstance_ReturnsCameraServiceInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetCameraServiceInstance(), typeof(ICameraService));
        }
    

        [TestMethod]
        public void MainServiceTest_GetFloorServiceInstance_ReturnsFloorServiceInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetFloorServiceInstance(), typeof(IFloorService));
        }

        [TestMethod]
        public void MainServiceTest_GetLoggerInstance_ReturnsLoggerInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetLoggerInstance(), typeof(ILogger));
        }

        [TestMethod]
        public void MainServiceTest_GetPageCreatorInstance_ReturnsPageCreatorInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetPageCreatorInstance(), typeof(IPageCreator));
        }

        [TestMethod]
        public void MainServiceTest_GetPersonServiceInstance_ReturnsPersonServiceInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetPersonServiceInstance(), typeof(IPersonService));
        }

        [TestMethod]
        public void MainServiceTest_GetWindowCreatorInstance_ReturnsWindowCreatorInstance()
        {
            MainService mainservice = new MainService();
            Assert.IsInstanceOfType(mainservice.GetWindowCreatorInstance(), typeof(IWindowCreator));
        }

        [TestMethod]
        public void CameraServiceTest_CreateNewCameraWithPosition_PositionsAreSetCorretly()
        {
            ICameraService cameraService = new CameraService();
            Camera camera = cameraService.CreateCamera(true, 2, 5);
            Assert.AreEqual<int>(camera.PositionX, 2, " Camera posisitionX is set to " + camera.PositionX + " instead of 2");
            Assert.AreEqual<int>(camera.PositionY, 5, " Camera posisitionY is set to " + camera.PositionX + " instead of 5");
        }

        [TestMethod]
        public void CameraServiceTest_CreateAndAddToDictionaryNewCamera_CameraIsInTheDictionary()
        {
            ICameraService cameraService = new CameraService();
            Camera camera = cameraService.CreateCamera();
            Assert.IsNotNull(camera, "Failed to create camera instance");
            CollectionAssert.Contains(cameraService.GetAllCameras().ToList(), camera, "Camera was created but not added to dictionary");
        }

        [TestMethod]
        public void CameraServiceTest_GetCameraFromDictionary_ReturnsRightCamera()
        {
            ICameraService cameraService = new CameraService();
            Camera camera0 = cameraService.CreateCamera();
            Camera camera1 = cameraService.CreateCamera();
            Camera camera2 = cameraService.CreateCamera();
            Camera camera3 = cameraService.CreateCamera();

            Assert.IsNotNull(camera0, "Failed to create camera instance");
            Assert.IsNotNull(camera1, "Failed to create camera instance");
            Assert.IsNotNull(camera2, "Failed to create camera instance");
            Assert.IsNotNull(camera3, "Failed to create camera instance");

            Assert.AreEqual(cameraService.GetCamera(camera2.Guid), camera2, "Wrong Camera returned");
        }
    }
}
