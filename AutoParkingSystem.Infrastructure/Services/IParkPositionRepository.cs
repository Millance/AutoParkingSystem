using AutoParkingSystem.Model;

namespace AutoParkingSystem.Infrastructure.Services
{
    public interface IParkPositionRepository
    {
        // 库位增删查改
        Task<bool> AddParkPositionAsync(ParkPosition parkPosition);
        Task<bool> UpdateParkPositionAsync(ParkPosition parkPosition);
        Task<bool> DeleteParkPositionAsync(int id);
        Task<ParkPosition> GetParkPositionAsync(ParkPosition parkPosition);
        // 获取空车位
        Task<ParkPosition> GetIdleParkPositionAsync();
    }
}
