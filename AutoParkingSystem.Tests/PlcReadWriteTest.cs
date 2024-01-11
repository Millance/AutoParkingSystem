using AutoParkingSystem.Infrastructure.Services;

namespace AutoParkingSystem.Tests
{
    public class Tests
    {
        private IPlcCommunicationService _plcCommunicationService;
        [SetUp]
        public void Setup()
        {
            _plcCommunicationService = new SiemensPlcCommunicationService();
        }

        [TestCase(1, 2)]
        public async Task Read_Plc_Test(int dbArea, int offset)
        {
            if (_plcCommunicationService.IsConnected == false)
            {
                await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            }
            var val = await _plcCommunicationService.ReadIntAsync(dbArea, offset);
            Assert.Pass(val.ToString());
        }

        [TestCase(1, 2, 3)]
        [TestCase(1, 2, 4)]
        public async Task Read_and_Write_Plc_Test(int dbArea, int offset, short value)
        {

            if (_plcCommunicationService.IsConnected == false)
            {
                await _plcCommunicationService.ConnectAsync("192.168.0.100", 0, 1);
            }
            await _plcCommunicationService.WriteIntAsync(dbArea, offset, value);

            var read_val = await _plcCommunicationService.ReadIntAsync(dbArea, offset);

            Assert.That(read_val, Is.EqualTo(value));
        }
    }
}