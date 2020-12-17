using CESI.BS.EasySave.BS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CESI.BS.EasySave.UnitTest.BsTest
{
    [TestClass()]
    public class BsEasySaveTest
    {
        internal BSEasySave _bsTest;
        internal List<string> _listMock;
        internal List<string> _listPriority;

        [TestInitialize()]
        public void Init()
        {
            _bsTest = new BSEasySave();
            _listMock = new List<string>()
            {
                ".htmlTest",
                ".jsText"
            };
            _listPriority = new List<string>
            {
                ".txt",
                ".test"
            };
        }

        [TestMethod]
        public void GetWorksTest()
        {
            Assert.IsNotNull(_bsTest.GetWorks());
        }

        [TestMethod]
        public void AddWorkTest()
        {
            _bsTest.AddWork(
                "work-test",
                "path",
                "pathDest",
                "ful",
                _listMock,
                _listPriority,
                "keyMock");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Name, "work-test");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Source, "path");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Target, "pathDest");
            _bsTest.DeleteWork(_bsTest.works.Count - 1);
        }

        [TestMethod]
        public void AddWorkAt()
        {
            int indexer = 0;
            _bsTest.AddWorkAt(
                "work-test2",
                "path2",
                "pathDest2",
                "ful",
                _listMock,
                _listPriority,
                "keyMock2",
                indexer);
            Assert.AreEqual(_bsTest.works[0].Name, "work-test2");
            Assert.AreEqual(_bsTest.works[0].Source, "path2");
            Assert.AreEqual(_bsTest.works[0].Target, "pathDest2");
            _bsTest.DeleteWork(indexer);
        }

        [TestMethod]
        public void ModifyWorkTest()
        {
            _bsTest.AddWork(
               "work-test",
               "path",
               "pathDest",
               "ful",
               _listMock,
               _listPriority,
               "keyMock");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Name, "work-test");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Source, "path");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Target, "pathDest");
            _bsTest.ModifyWork(
                _bsTest.works.LastOrDefault(),
                "newName",
                "newSource",
                "newTarget",
                "dif",
                _listMock,
                _listPriority,
                "newKey"
                );
            Assert.AreEqual(_bsTest.works.LastOrDefault().Name, "newName");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Source, "newSource");
            Assert.AreEqual(_bsTest.works.LastOrDefault().Target, "newTarget");
            _bsTest.DeleteWork(_bsTest.works.Count - 1);
        }

        [TestMethod]
        public void DeleteWorkTest()
        {
            int indexer = 0;
            _bsTest.AddWorkAt(
                "work-test2",
                "path2",
                "pathDest2",
                "ful",
                _listMock,
                _listPriority,
                "keyMock2",
                indexer);
            Assert.AreEqual(_bsTest.works[0].Name, "work-test2");
            Assert.AreEqual(_bsTest.works[0].Source, "path2");
            Assert.AreEqual(_bsTest.works[0].Target, "pathDest2");
            _bsTest.DeleteWork(indexer);
            Assert.IsFalse(_bsTest.works.Any());
        }
    }
}
