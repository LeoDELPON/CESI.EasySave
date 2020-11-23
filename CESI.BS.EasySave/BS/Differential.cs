using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public class Differential : Save
    {
        override
             public void perform()
        {
            Console.WriteLine("Sauvegarde incrémentielle");
        }

        override
        public string getName()
        {
            return Language.getDifferentialName();
        }
    }
}