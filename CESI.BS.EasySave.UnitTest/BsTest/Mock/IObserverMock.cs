using CESI.BS.EasySave.DTO;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CESI.BS.EasySave.UnitTest.BsTest.Mock
{
    public interface IObserverMock
    {
        public bool ReactDataLogServMock(DTODataServer dict, Socket socket);
    }
}
