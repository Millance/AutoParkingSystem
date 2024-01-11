using AutoParkingSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace AutoParkingSystem.Repository
{
    public class ParkDbContext : DbContext
    {
        // 添加车位累
        public DbSet<ParkPosition> ParkPositions { get; set; }

        public ParkDbContext(DbContextOptions<ParkDbContext> options) : base(options) { }
    }
}