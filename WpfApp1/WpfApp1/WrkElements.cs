using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class WrkElements
    {
        public WrkElementInSavedList inSvdList { get; }
        public WrkElementInWrkList inWrkList { get; }
        public ConfSaver.WorkVar wv { get; set; }
        public WrkElements(ConfSaver.WorkVar workVar,  BSEasySave BS)
        {

            inSvdList = new WrkElementInSavedList(workVar, BS);
            inWrkList = new WrkElementInWrkList(workVar, BS);
                
            wv = workVar;
        
            
        }
        public WrkElements()
        {

          

        }

    }
}
