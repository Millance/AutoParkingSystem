using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoParkingSystem.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlcConnectionController : ControllerBase
    {

        private readonly ILogger<PlcConnectionController> _logger;
        private readonly IPlcCommunicationService _plcCommunicationService;

        public PlcConnectionController(ILogger<PlcConnectionController> logger, IPlcCommunicationService plcCommunicationService)
        {
            _plcCommunicationService = plcCommunicationService;
            _logger = logger;
        }

        [HttpPost("ConnectPlc")]
        public async Task<IActionResult> ConnectAsync()
        {
            bool val = await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            return Ok(val);
        }

        [HttpGet("ReadInt")]
        public async Task<IActionResult> ReadIntAsync(int dbArea, int offset)
        {
            if (_plcCommunicationService.IsConnected == false)
            {
                await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            }
            var val = await _plcCommunicationService.ReadIntAsync(dbArea, offset);
            return Ok(val);
        }

        [HttpPost("WriteInt")]
        public async Task<IActionResult> WriteIntAsync(int dbArea, int offset, short value)
        {
            if (_plcCommunicationService.IsConnected == false)
            {
                await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            }
            await _plcCommunicationService.WriteIntAsync(dbArea, offset, value);
            return Ok("OK");
        }
    }
}