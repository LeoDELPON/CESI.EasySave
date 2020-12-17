using System.Net.Sockets;
using System.Text.Json;

namespace CESI.BS.EasySave.UnitTest.BsTest.Mock
{
    public class ServerSocketObserverMock : IObserverMock
    {
        public bool ReactDataLogServMock(DTODataServer dto, Socket socket)
        {
            string _dataSent = JsonSerializer.Serialize(dto);
            if (socket != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
