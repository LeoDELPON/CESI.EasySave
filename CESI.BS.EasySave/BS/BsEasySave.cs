using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.BS.Factory;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    public class BSEasySave
    {
        public List<Save> typeSave { get; set; } = new List<Save>();
        public BSEasySave()
        {
            typeSave.Add(new WorkFactory().CreateSaveObject("Differential", ""));
            typeSave.Add(new WorkFactory().CreateSaveObject("Full", ""));
        }



        List<Work> works = new List<Work>();
        // à remplire de tous les enfants de save

        public List<Work> GetWorks()
        {
            return works;
        }
        public void AddWork(string name, string source, string target, string save)
        {
            Dictionary<WorkProperties, string> propertiesWork = new Dictionary<WorkProperties, string>
            {
                [WorkProperties.Name] = name,
                [WorkProperties.Source] = source,
                [WorkProperties.Target] = target,
                [WorkProperties.TypeSave] = save
            };
            works.Add(new WorkFactory().CreateWorkObject(propertiesWork));
        }
        public void ModifyWork(Work work, int field, string newField)
        {
            switch (field)
            {
                case 1:
                    work.Name = newField;
                    break;
                case 2:
                    work.Source = newField;
                    break;
                case 3:
                    work.Target = newField;
                    break;
                default:
                    break;
            }
        }
        public void ModifyWork(Work work, int field, int typeSaveChoosen)
        {
            if (field == 4)
            {
                work._saveType = typeSave[typeSaveChoosen];
            }

        }
        public void DeleteWork(int idWork)
        {

            works.Remove(works[idWork]);
        }


    }
}