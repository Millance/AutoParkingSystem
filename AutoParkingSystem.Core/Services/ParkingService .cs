namespace AutoParkingSystem.Core.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IPlcCommunicationService plcCommunicationService;

        public ParkingService(IPlcCommunicationService plcCommunicationService)
        {
            this.plcCommunicationService = plcCommunicationService;
        }

        public async Task<bool> ParkVehicleAsync(string vehicleId)
        {
            // 调用PLC通讯服务进行停车操作
            await plcCommunicationService.WriteIntAsync(1, 2, 1);

            // 其他停车逻辑...

            return true;
        }

        public async Task<bool> ReleaseParkingSpaceAsync(string vehicleId)
        {
            // 调用PLC通讯服务进行释放停车位操作
            await plcCommunicationService.WriteIntAsync(1, 2, 0);

            // 其他释放停车位逻辑...

            return true;
        }

        // 其他服务方法...
    }
}
