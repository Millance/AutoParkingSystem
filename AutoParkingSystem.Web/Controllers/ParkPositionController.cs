using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AutoParkingSystem.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkPositionController : ControllerBase
    {

        private readonly ILogger<ParkPositionController> _logger;
        private readonly IParkingService _parkingService;

        public ParkPositionController(ILogger<ParkPositionController> logger, IParkingService parkingService)
        {
            _parkingService = parkingService;
            _logger = logger;
        }

        [HttpPost("AddParkPosition")]
        public async Task<IActionResult> AddParkPosition(ParkPosition parkPosition)
        {
            var res = await _parkingService.AddParkPositionAsync(parkPosition);
            _logger.Log(LogLevel.Information, "AddParkPosition: " + JsonConvert.SerializeObject(parkPosition));
            return Ok(res);
        }

        [HttpPost("UpdateParkPosition")]
        public async Task<IActionResult> UpdateParkPosition(ParkPosition parkPosition)
        {
            var res = await _parkingService.UpdateParkPositionAsync(parkPosition);
            _logger.Log(LogLevel.Information, "UpdateParkPosition: " + JsonConvert.SerializeObject(parkPosition));
            return Ok(res);
        }

        [HttpPost("DaleteParkPosition")]
        public async Task<IActionResult> DaleteParkPosition(int id)
        {
            var res = await _parkingService.DeleteParkPositionAsync(id);
            _logger.Log(LogLevel.Information, "DaleteParkPosition: " + JsonConvert.SerializeObject(id));
            return Ok(res);
        }
    }
}