using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DAL
{
    abstract class Save
    {
        public string name;
        public abstract void perform();
        public abstract string getName();
    }
}