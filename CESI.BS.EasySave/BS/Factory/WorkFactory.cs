using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS.Factory
{
    class WorkFactory : Factory
    {
        public override Work CreateWorkObject(Dictionary<WorkProperties, string> properties)
        {
            return new Work(
                properties[WorkProperties.Name],
                properties[WorkProperties.Source],
                properties[WorkProperties.Target],
                CreateSaveObject(properties[WorkProperties.TypeSave], properties[WorkProperties.Name])
            );
        }

        public override Save CreateSaveObject(string _saveType, string prop)
        {
            Save _save;
            switch(_saveType.GetSaveTypeFromString())
            {
                case SaveType.DIFFERENTIAL:
                    _save = new Differential(prop);
                    break;
                case SaveType.FULL:
                    _save = new Full(prop);
                    break;
                default:
                    _save = new Differential(prop);
                    break;
            }
            return _save;
        }
    }
}
