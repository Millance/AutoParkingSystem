﻿using AutoParkingSystem.Model;

namespace AutoParkingSystem.Core.Services
{
    public interface IParkingService
    {
        // 库位增删查改
        Task<bool> AddParkPositionAsync(ParkPosition parkPosition);
        Task<bool> UpdateParkPositionAsync(ParkPosition parkPosition);
        Task<bool> DeleteParkPositionAsync(int id);
        Task<ParkPosition> GetParkPositionAsync(ParkPosition parkPosition);

        // 停车
        Task<bool> ParkVehicleAsync(string vehiclePlateNumber);
        // 取车
        Task<bool> ReleaseParkingSpaceAsync(string vehiclePlateNumber);
    }
}
