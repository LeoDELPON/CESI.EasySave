using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DAL
{
    public abstract class Save
    {
        public string name;
        public abstract void Perform();
        public abstract string GetName();
    }
}