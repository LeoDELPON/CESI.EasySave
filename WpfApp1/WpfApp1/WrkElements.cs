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
        public bool chiffrage { get; set; }
        public WrkElements(ConfSaver.WorkVar workVar,  BSEasySave BS)
        {
            if (workVar.cryptoExtensions.Count > 0 && workVar.key.Length > 0)
            {
                chiffrage = true;
            }else
            {
                chiffrage = false;
            }
            inSvdList = new WrkElementInSavedList(workVar, BS);
            inWrkList = new WrkElementInWrkList(workVar, BS);
            
            wv = workVar;
        
            
        }
        public WrkElements()
        {

          

        }

    }
}
