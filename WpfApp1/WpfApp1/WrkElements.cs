using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;

namespace WpfApp1
{
    public class WrkElements
    {
        public WrkElementInSavedList InSvdList { get; }
        public WrkElementInWrkList InWrkList { get; }
        public ConfSaver.WorkVar WV { get; set; }
        public bool Chiffrage { get; set; }
        public WrkElements(ConfSaver.WorkVar workVar, BSEasySave BS)
        {
            if (workVar.cryptoExtensions.Count > 0 && workVar.key.Length > 0)
            {
                Chiffrage = true;
            }
            else
            {
                Chiffrage = false;
            }
            InSvdList = new WrkElementInSavedList(workVar, BS);
            InWrkList = new WrkElementInWrkList(workVar);

            WV = workVar;


        }
        public WrkElements()
        {



        }

    }
}
