using System;
using System.Collections.Generic;
using System.Text;

namespace Console_EasySave
{
    abstract class Save
    {
        public string name;
        public abstract void Perform();
        public abstract string GetName();
    }
}
