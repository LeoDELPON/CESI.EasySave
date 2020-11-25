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
                    saveT = "Differential";
                    break;
                case SaveType.FULL:
                    saveT = "Full";
                    break;
                default:
                    saveT = "Differential";
                    break;
            }
            return saveT;
        }

        public static SaveType GetSaveTypeFromString(this string saveType)
        {
            SaveType saveT;
            switch (saveType)
            {
                case "Differential":
                    saveT = SaveType.DIFFERENTIAL;
                    break;
                case "Full":
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
