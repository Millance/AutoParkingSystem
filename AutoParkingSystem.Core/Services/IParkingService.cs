using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkingSystem.Core.Services
{
    public interface IParkingService
    {
        Task<bool> ParkVehicleAsync(string vehicleId);
        Task<bool> ReleaseParkingSpaceAsync(string vehicleId);

        // 其他服务接口...
    }
}
