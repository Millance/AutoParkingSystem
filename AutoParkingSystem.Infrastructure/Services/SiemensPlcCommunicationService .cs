using AutoParkingSystem.Core.Services;
using S7.Net;

namespace AutoParkingSystem.Infrastructure.Services
{
    public class SiemensPlcCommunicationService : IPlcCommunicationService
    {
        private Plc? plc;

        public bool IsConnected
        {
            get
            {
                if (plc == null) { return false; }
                return plc.IsConnected;
            }
        }

        public async Task<bool> ConnectAsync(string ipAddress, int rack, int slot)
        {
            plc = new Plc(CpuType.S71500, ipAddress, (short)rack, (short)slot);
            await plc.OpenAsync();
            return plc.IsConnected;
        }

        public async Task<int> ReadIntAsync(int dbArea, int offset)
        {
            if (plc == null || !plc.IsConnected)
            {
                throw new InvalidOperationException("PLC is not connected.");
            }

            var result = await plc.ReadAsync(DataType.DataBlock, dbArea, offset, VarType.Int, 1);
            if (result == null)
            {
                return 0;
            }
            else
            {
                return (short)result;
            }
        }

        public async Task WriteIntAsync(int dbArea, int offset, short value)
        {
            if (plc == null || !plc.IsConnected)
            {
                throw new InvalidOperationException("PLC is not connected.");
            }

            await plc.WriteAsync(DataType.DataBlock, dbArea, offset, value);
        }

        // 其他通讯方法的实现...
    }
}
