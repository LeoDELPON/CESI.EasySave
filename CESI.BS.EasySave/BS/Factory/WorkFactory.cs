using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    class WorkFactory : Factory
    {
        public override Work CreateWorkObject(Dictionary<WorkProperties, object> properties)
        {
            return new Work(
                properties[WorkProperties.Name].ToString(),
                properties[WorkProperties.Source].ToString(),
                properties[WorkProperties.Target].ToString(),
                CreateSaveObject(properties[WorkProperties.TypeSave].ToString(), 
                    properties[WorkProperties.Name].ToString(), 
                    (IList<string>)properties[WorkProperties.Extensions],
                    properties[WorkProperties.Key].ToString())
            );
        }

        public override Save CreateSaveObject(string _saveType, string prop, IList<string> extensions, string key)
        {
            Save _save;
            switch(_saveType.GetSaveTypeFromString())
            {
                case SaveType.DIFFERENTIAL:
                    _save = new Differential(prop, extensions, key);
                    break;
                case SaveType.FULL:
                    _save = new Full(prop, extensions, key);
                    break;
                default:
                    _save = new Differential(prop, extensions, key);
                    break;
            }
            return _save;
        }
    }
}
