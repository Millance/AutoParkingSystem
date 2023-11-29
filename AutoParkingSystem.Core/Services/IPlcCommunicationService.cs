using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParkingSystem.Core.Services
{
    public interface IPlcCommunicationService
    {
        bool IsConnected { get; }
        Task<bool> ConnectAsync(string ipAddress, int rack, int slot);
        Task<int> ReadIntAsync(int dbArea, int offset);
        Task WriteIntAsync(int dbArea, int offset, short value);

        // 其他通讯方法...
    }
}
