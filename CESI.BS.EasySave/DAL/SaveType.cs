using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DAL
{
    public enum SaveType
    {
        
        DIFFERENTIAL,
        FULL
    }

    public static class SaveTypeMethods
    {
        public static string GetString(this SaveType saveType)
        {
            string saveT;
            switch (saveType)
            {
                case SaveType.DIFFERENTIAL:
                    saveT = "differential";
                    break;
                case SaveType.FULL:
                    saveT = "mirror";
                    break;
                default:
                    saveT = "differential";
                    break;
            }
            return saveT;
        }

        public static SaveType GetSaveTypeFromString(this string saveType)
        {
            SaveType saveT;
            switch (saveType)
            {
                case "differential":
                    saveT = SaveType.DIFFERENTIAL;
                    break;
                case "mirror":
                    saveT = SaveType.FULL;
                    break;
                default:
                    saveT = SaveType.DIFFERENTIAL;
                    break;
            }
            return saveT;
        }
    }
}
