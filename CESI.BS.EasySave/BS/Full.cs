using CESI.BS.EasySave.DAL;
using LanguageClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public class Full : Save
    {
        override
        public void Perform()
        {
            Console.WriteLine("Sauvegarde Complete");
        }
        override
     public string GetName()
        {
            return Language.GetRequestedString(16); //getFullName
        }
    }

}