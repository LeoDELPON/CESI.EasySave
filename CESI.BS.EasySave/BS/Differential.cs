using CESI.BS.EasySave.DAL;
using LanguageClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public class Differential : Save
    {
        override
             public void Perform()
        {
            Console.WriteLine("Sauvegarde différentielle");
        }

        override
        public string GetName()
        {
            return Language.GetDifferentialName();
        }
    }
}