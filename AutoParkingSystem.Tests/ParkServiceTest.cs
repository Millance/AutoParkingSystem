using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Infrastructure.Services;
using AutoParkingSystem.Model;
using AutoParkingSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace AutoParkingSystem.Tests
{
    public class ParkServiceTest
    {
        private IParkingService _parkingService;
        private IParkPositionRepository _parkPositionRepository;
        private IPlcCommunicationService _plcCommunicationService;

        [SetUp]
        public async Task SetupAsync()
        {
            // 创建 DbContextOptions 对象
            var options = new DbContextOptionsBuilder<ParkDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // 使用 DbContextOptions 实例化 DbContext
            ParkDbContext _dbContext = new ParkDbContext(options);

            _parkPositionRepository = new ParkPositionRepository(_dbContext);
            _plcCommunicationService = new SiemensPlcCommunicationService();
            _parkingService = new ParkingService(_plcCommunicationService, _parkPositionRepository);

            if (_plcCommunicationService.IsConnected == false)
            {
                await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            }
        }

        public static IEnumerable<ParkPosition> ParkPositionCases
        {
            get
            {
                yield return new ParkPosition { Id = 1, PositionName = "A001", LicensePlateNumber = "", ParkInTime = DateTime.Now };
                yield return new ParkPosition { Id = 2, PositionName = "A002", LicensePlateNumber = "", ParkInTime = DateTime.Now };
                yield return new ParkPosition { Id = 3, PositionName = "A003", LicensePlateNumber = "", ParkInTime = DateTime.Now };
                // 可以添加更多的测试用例
            }
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Add_ParkPosition(ParkPosition parkPosition)
        {
            var val = await _parkingService.AddParkPositionAsync(parkPosition);
            Assert.IsTrue(val);
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Delete_ParkPosition(ParkPosition parkPosition)
        {
            await _parkingService.AddParkPositionAsync(parkPosition);

            var val = await _parkingService.DeleteParkPositionAsync(parkPosition.Id);
            Assert.IsTrue(val);
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Update_ParkPosition(ParkPosition parkPosition)
        {
            await _parkingService.AddParkPositionAsync(parkPosition);
            parkPosition.LicensePlateNumber = "ABC";
            parkPosition.ParkInTime = DateTime.Now;
            var val = await _parkingService.UpdateParkPositionAsync(parkPosition);
            Assert.IsTrue(val);
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Get_ParkPosition(ParkPosition parkPosition)
        {
            await _parkingService.AddParkPositionAsync(parkPosition);
            ParkPosition parkPosition2 = new ParkPosition { PositionName = parkPosition.PositionName };
            var val = await _parkingService.GetParkPositionAsync(parkPosition2);
            Assert.NotNull(val);
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Park_ParkPosition(ParkPosition parkPosition)
        {
            var val = await _parkingService.ParkVehicleAsync("ABC");
            Assert.IsFalse(val);

            await _parkingService.AddParkPositionAsync(parkPosition);
            val = await _parkingService.ParkVehicleAsync("ABC");
            Assert.IsTrue(val);
        }


        [TestCaseSource(nameof(ParkPositionCases))]
        public async Task Release_ParkPosition(ParkPosition parkPosition)
        {
            var val = await _parkingService.ReleaseParkingSpaceAsync("ABC");
            Assert.IsFalse(val);

            await _parkingService.AddParkPositionAsync(parkPosition);
            val = await _parkingService.ParkVehicleAsync("ABC");
            Assert.IsTrue(val);

            val = await _parkingService.ReleaseParkingSpaceAsync("ABC");
            Assert.IsTrue(val);
        }

    }
}
