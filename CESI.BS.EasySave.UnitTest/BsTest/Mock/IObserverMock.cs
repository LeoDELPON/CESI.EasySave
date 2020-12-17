using System.Net.Sockets;

namespace CESI.BS.EasySave.UnitTest.BsTest.Mock
{
    public interface IObserverMock
    {
        public bool ReactDataLogServMock(DTODataServer dict, Socket socket);
    }
}
