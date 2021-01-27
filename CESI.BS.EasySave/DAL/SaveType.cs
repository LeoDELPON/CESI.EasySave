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
                    saveT = "dif";
                    break;
                case SaveType.FULL:
                    saveT = "ful";
                    break;
                default:
                    Console.WriteLine("defaut");
                    saveT = "dif";
                    break;
            }
            return saveT;
        }

        public static SaveType GetSaveTypeFromString(this string saveType)
        {
            SaveType saveT;
            switch (saveType)
            {
                case "dif":
                    saveT = SaveType.DIFFERENTIAL;
                    break;
                case "ful":
                    saveT = SaveType.FULL;
                    break;
                default:
                    Console.WriteLine("defaut");
                    saveT = SaveType.DIFFERENTIAL;
                    break;
            }
            return saveT;
        }
        public static string GetSaveTypeStrFromInt(int i)
        {
            return GetString((SaveType) i);
        }
        public static SaveType GetSaveTypeFromint(int i)
        {
            SaveType saveT;
            switch (i)
            {
                case 0:
                    saveT = SaveType.DIFFERENTIAL;
                    break;
                case 1:
                    saveT = SaveType.FULL;
                    break;
                default:
                    Console.WriteLine("defaut");
                    saveT = SaveType.DIFFERENTIAL;
                    break;
            }
            return saveT;
        }
    }
}
