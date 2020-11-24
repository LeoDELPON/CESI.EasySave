using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    public class BSEasySave
    {
        public List<Save> typeSave { get; set; } = new List<Save>();
        public BSEasySave()
        {

            typeSave.Add(new Differential());
            typeSave.Add(new Full());
        }



        List<Work> works = new List<Work>();
        // à remplire de tous les enfants de save

        public List<Work> GetWorks()
        {
            return works;
        }
        public void AddWork(string name, string source, string target, Save save)
        {
            works.Add(new Work(name, source, target, save));
        }
        public void ModifyWork(Work work, int field, string newField)
        {
            switch (field)
            {
                case 1:
                    work.name = newField;
                    break;
                case 2:
                    work.source = newField;
                    break;
                case 3:
                    work.target = newField;
                    break;
                default:
                    break;
            }
        }
        public void ModifyWork(Work work, int field, int typeSaveChoosen)
        {
            if (field == 4)
            {
                work.save = typeSave[typeSaveChoosen];
            }

        }
        public void DeleteWork(int idWork)
        {

            works.Remove(works[idWork]);
        }

    }
}