using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CESI.BS.EasySave.BS
{ 
    public static class ThreadMutex
    {
        public static object writeLogger = new object();
        public static object writeStatusLogger = new object();
        public static object filePause = new object();
        public static object threadPauseWhenProcess = new object();
        public static object bigFile = new object();


    }
}
