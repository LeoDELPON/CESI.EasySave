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
                CreateSaveObject(properties[WorkProperties.TypeSave])
            );
        }

        public override Save CreateSaveObject(string _saveType)
        {
            Save _save;
            switch(_saveType.GetSaveTypeFromString())
            {
                case SaveType.DIFFERENTIAL:
                    _save = new Differential();
                    break;
                case SaveType.FULL:
                    _save = new Full();
                    break;
                default:
                    _save = new Differential();
                    break;
            }
            return _save;
        }
    }
}
