using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{ 
    public static class ThreadLifeManager
    {
        public static object pause = new object();
        public static object writeLogger = new object();
        public static object writeStatusLogger = new object();

    }
}
