using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.Observers;
using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using CESI.BS.EasySave.UnitTest.BsTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CESI.BS.EasySave.UnitTest.BsTest
{

    [TestClass()]
    public class DataHandlerTest
    {
        internal DataHandler _data;
        internal Dictionary<WorkProperties, object> _dict;
        internal List<IObserverMock> serverSubscriberMock;

        [TestInitialize()]
        public void Init()
        {
            serverSubscriberMock = new List<IObserverMock>();
            _data = DataHandler.Instance;
            _dict = new Dictionary<WorkProperties, object>();
            _dict[WorkProperties.Name] = "TestName";
            _dict[WorkProperties.Source] = "TestSource";
            _dict[WorkProperties.Target] = "TestTarget";
            _dict[WorkProperties.Size] = 99999;
            _dict[WorkProperties.TypeSave] = "ful";
            _dict[WorkProperties.EligibleFiles] = 99999;
        }

        [TestMethod]
        public void InitTest()
        {
            _data.Init(_dict);
            Assert.AreEqual(_data.Dictionary[WorkProperties.Name], "TestName");
            Assert.AreEqual(_data.Dictionary[WorkProperties.Source], "TestSource");
            Assert.AreEqual(_data.Dictionary[WorkProperties.Target], "TestTarget");
            Assert.AreEqual(_data.Dictionary[WorkProperties.Size], 99999);
            Assert.AreEqual(_data.Dictionary[WorkProperties.EligibleFiles], 99999);
        }

        [TestMethod]
        public void OnStopTest()
        {
            _data.Init(_dict);
            _data.OnStop(false);
            Assert.AreEqual(_data.Dictionary[WorkProperties.Duration], "-1");
            Assert.AreEqual(_data.Dictionary[WorkProperties.RemainingSize], "Error");
            Assert.AreEqual(_data.Dictionary[WorkProperties.RemainingFiles], "Error");
            Assert.AreEqual(_data.Dictionary[WorkProperties.Progress], "Error");
            Assert.AreEqual(_data.Dictionary[WorkProperties.State], "Not Running");
            Assert.IsTrue(_data.OnStop(false));
            Assert.IsTrue(_data.OnStop(true));
        } 

        [TestMethod]
        public void OnNextTest()
        {
            Dictionary<WorkProperties, object> nonInstanceObj = null;
            Assert.IsTrue(_data.OnNext(nonInstanceObj) == -1);
        }

        [TestMethod]
        public void SubscribeServerTest()
        {
            ServerSocketObserverMock obs = new ServerSocketObserverMock();
            SubscribeServerMock(obs);
            Assert.IsTrue(serverSubscriberMock.Count == 1);
        }

        [TestMethod]
        public void NotifyServerTest()
        {
            Socket badSock = null;
            ServerSocketObserverMock obs = new ServerSocketObserverMock();
            SubscribeServerMock(obs);
            Assert.IsTrue(NotifyServerMock(_dict, new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp)));
            Assert.IsFalse(NotifyServerMock(_dict, badSock));
        }

        [TestMethod]
        public void UnSubscribeServerTest()
        {

        }

        public void SubscribeServerMock(IObserverMock obs)
        {
            serverSubscriberMock.Add(obs);
        }

        public bool NotifyServerMock(Dictionary<WorkProperties, object> dict, Socket socket)
        {
            DTODataServer dto = new DTODataServer();
            try
            {
                return serverSubscriberMock[0].ReactDataLogServMock(dto, socket);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
