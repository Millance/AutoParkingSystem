using AutoParkingSystem.Model;
using AutoParkingSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace AutoParkingSystem.Infrastructure.Services
{
    public class ParkPositionRepository : IParkPositionRepository
    {
        private readonly DbContext _dbContext;

        public ParkPositionRepository(ParkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddParkPositionAsync(ParkPosition parkPosition)
        {
            try
            {
                _dbContext.Set<ParkPosition>().Add(parkPosition);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateParkPositionAsync(ParkPosition parkPosition)
        {
            try
            {
                _dbContext.Set<ParkPosition>().Update(parkPosition);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteParkPositionAsync(int id)
        {
            try
            {
                var parkPosition = await _dbContext.Set<ParkPosition>().FindAsync(id);
                if (parkPosition == null)
                {
                    return false; // 或者处理实体不存在的情况
                }

                _dbContext.Set<ParkPosition>().Remove(parkPosition);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ParkPosition> GetParkPositionAsync(ParkPosition parkPosition)
        {
            try
            {
                var parkPos = await _dbContext.Set<ParkPosition>()
                                              .Where(p => p.Id == parkPosition.Id ||
                                                  p.PositionName == parkPosition.PositionName ||
                                                  p.LicensePlateNumber == parkPosition.LicensePlateNumber)
                                              .FirstOrDefaultAsync();
                return parkPos ?? new ParkPosition();
            }
            catch (Exception)
            {
                return await Task.FromResult(new ParkPosition());
            }
        }

        /// <summary>
        /// 获取空车位
        /// </summary>
        /// <param name="parkPosition"></param>
        /// <returns></returns>
        public async Task<ParkPosition> GetIdleParkPositionAsync()
        {
            try
            {
                var parkPos = await _dbContext.Set<ParkPosition>()
                                              .Where(p => string.IsNullOrEmpty(p.LicensePlateNumber))
                                              .FirstOrDefaultAsync();
                return parkPos ?? new ParkPosition();
            }
            catch (Exception)
            {
                return await Task.FromResult(new ParkPosition());
            }
        }
    }
}
