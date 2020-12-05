using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    class WrkElements
    {
        public int index { get;  }
        public WrkElementInSavedList inSvdList { get; }
        public WrkElementInWrkList inWrkList { get; }

        public WrkElements(ConfSaver.WorkVar workVar, int index, BSEasySave BS)
        {
            this.index = index;
            inSvdList = new WrkElementInSavedList(workVar, BS);
            inWrkList = new WrkElementInWrkList(workVar, BS);
            
        }
       
    }
}
