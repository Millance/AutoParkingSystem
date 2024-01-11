using AutoParkingSystem.Infrastructure.Services;
using AutoParkingSystem.Model;

namespace AutoParkingSystem.Core.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkPositionRepository _parkPositionRepository;
        private readonly IPlcCommunicationService _plcCommunicationService;

        public ParkingService(IPlcCommunicationService plcCommunicationService,
                              IParkPositionRepository parkPositionRepository)
        {
            _plcCommunicationService = plcCommunicationService;
            _parkPositionRepository = parkPositionRepository;
        }

        public async Task<bool> AddParkPositionAsync(ParkPosition parkPosition)
        {
            return await _parkPositionRepository.AddParkPositionAsync(parkPosition);
        }

        public async Task<bool> UpdateParkPositionAsync(ParkPosition parkPosition)
        {
            return await _parkPositionRepository.UpdateParkPositionAsync(parkPosition);
        }

        public async Task<bool> DeleteParkPositionAsync(int id)
        {
            return await _parkPositionRepository.DeleteParkPositionAsync(id);
        }

        public async Task<ParkPosition> GetParkPositionAsync(ParkPosition parkPosition)
        {
            return await _parkPositionRepository.GetParkPositionAsync(parkPosition);
        }

        // 停车
        public async Task<bool> ParkVehicleAsync(string vehiclePlateNumber)
        {
            ParkPosition parkPosition = await _parkPositionRepository.GetIdleParkPositionAsync();

            if (parkPosition.Id == 0)
            {
                return false;
            }
            parkPosition.LicensePlateNumber = vehiclePlateNumber;

            await _parkPositionRepository.UpdateParkPositionAsync(parkPosition);

            // 调用PLC通讯服务进行停车操作
            await _plcCommunicationService.WriteIntAsync(1, 2, 1);

            // 其他停车逻辑...

            return true;
        }

        // 取车
        public async Task<bool> ReleaseParkingSpaceAsync(string vehiclePlateNumber)
        {
            ParkPosition parkPosition = new ParkPosition();
            parkPosition.LicensePlateNumber = vehiclePlateNumber;
            ParkPosition parkedVehicle = await _parkPositionRepository.GetParkPositionAsync(parkPosition);

            if (parkedVehicle.Id == 0)
            {
                return false;
            }
            parkedVehicle.LicensePlateNumber = string.Empty;

            await _parkPositionRepository.UpdateParkPositionAsync(parkedVehicle);

            // 调用PLC通讯服务进行释放停车位操作
            await _plcCommunicationService.WriteIntAsync(1, 2, 0);

            // 其他释放停车位逻辑...

            return true;
        }

    }
}
