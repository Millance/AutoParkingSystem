using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AutoParkingSystem.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkServiceController : ControllerBase
    {

        private readonly ILogger<ParkPositionController> _logger;
        private readonly IParkingService _parkingService;

        public ParkServiceController(ILogger<ParkPositionController> logger, IParkingService parkingService)
        {
            _parkingService = parkingService;
            _logger = logger;
        }

        [HttpPost("ParkVehicle")]
        public async Task<IActionResult> AddParkPosition(string vehiclePlateNumber)
        {
            var res = await _parkingService.ParkVehicleAsync(vehiclePlateNumber);
            _logger.Log(LogLevel.Information, "ParkVehicle: " + JsonConvert.SerializeObject(vehiclePlateNumber));
            return Ok(res);
        }

        [HttpPost("LeaveVehicle")]
        public async Task<IActionResult> LeaveVehicle(string vehiclePlateNumber)
        {
            var res = await _parkingService.ReleaseParkingSpaceAsync(vehiclePlateNumber);
            _logger.Log(LogLevel.Information, "LeaveVehicle: " + JsonConvert.SerializeObject(vehiclePlateNumber));
            return Ok(res);
        }

    }
}