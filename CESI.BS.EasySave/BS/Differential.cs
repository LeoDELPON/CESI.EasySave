using EasySave.Business.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Business.BS
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