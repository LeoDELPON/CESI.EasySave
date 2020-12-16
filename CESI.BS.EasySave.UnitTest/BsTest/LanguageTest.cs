using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CESI.BS.EasySave.BS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CESI.BS.EasySave.UnitTest.BsTest
{

    [TestClass()]
    public class LanguageTest
    {
        internal List<string> _languageTest;
        
        [TestInitialize()]
        public void Init()
        {
            _languageTest = new List<string>()
            {
                "fr",
                "en"
            };
        }

        [TestMethod]
        public void SetChosenLanguageTest()
        {
            for(int i = 0; i < _languageTest.Count(); ++i )
            {
                Assert.AreEqual(_languageTest[i], Language.languages[i]);
            }
            Assert.IsTrue(Language.SetChosenLanguage("en"));
            Assert.IsTrue(Language.SetChosenLanguage("fr"));
            Assert.IsFalse(Language.SetChosenLanguage("japanese"));
            Assert.IsFalse(Language.SetChosenLanguage("viet"));
        }

        [TestMethod]
        public void GetAllLanguageTest()
        {
            string lang = "viet";
            foreach (string langue in Language.languages)
            {
                Assert.IsFalse(lang == langue);
            }
        }

        [TestMethod]
        public void GetRequestedStringTest()
        {
            Assert.AreEqual(ExtractStringMockByIdMock(1, "fr"), "Bienvenue sur EasySave ! Choisissez une option ci-dessous");
            Assert.AreEqual(ExtractStringMockByIdMock(2, "en"), "1) Execute a work");
            Assert.AreEqual(ExtractStringMockByIdMock(3, "fr"), "2) Creer un travail");
            Assert.AreEqual(ExtractStringMockByIdMock(7, "fr"), "Il n'y a aucun travail pour le moment. Voulez-vous en créer un? (O/N)");
        }

        public string ExtractStringMockByIdMock(int stringID, string chosenLang)
        {
            Regex regex = new Regex(@"(?s)" + stringID + @"<.+?" + chosenLang + @">(.*?)<\/.{2}>");
            Match match = regex.Match(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Mock\LangMock.xml"));
            return match.Groups[1].Value;
        }
    }
}
