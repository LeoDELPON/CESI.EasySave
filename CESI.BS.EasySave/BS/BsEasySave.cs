using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.BS.Factory;
using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.BS.ConfSaver;

namespace CESI.BS.EasySave.BS
{
    public class BSEasySave
    {
        public ConfSaver.ConfSaver confSaver = new ConfSaver.ConfSaver();
        public List<Save> typeSave { get; set; } = new List<Save>();
        public BSEasySave()
        {

            typeSave.Add(new WorkFactory().CreateSaveObject("dif","", null, null, ""));
            typeSave.Add(new WorkFactory().CreateSaveObject("ful","", null, null, ""));
        }

        public List<Work> works = new List<Work>();
        // à remplir de tous les enfants de save

        public List<Work> GetWorks()
        {
            return works;
        }
        public void AddWork(string name, string source, string target, string save, List<string> cryptoExtensions, List<string> priorityExtensions, string key)
        {
            Dictionary<WorkProperties, object> propertiesWork = createPropertiesWork(name, source, target, save, cryptoExtensions, priorityExtensions, key);
            works.Add(new WorkFactory().CreateWorkObject(propertiesWork));
        }
        public void AddWorkAt(string name, string source, string target, string save, List<string> cryptoExtensions, List<string> priorityExtensions, string key, int index)
        {
            Dictionary<WorkProperties, object> propertiesWork = createPropertiesWork(name, source, target, save, cryptoExtensions, priorityExtensions, key);
            works.Insert(index, new WorkFactory().CreateWorkObject(propertiesWork));
        }

        private static Dictionary<WorkProperties, object> createPropertiesWork(string name, string source, string target, string save, List<string> cryptoExtensions, List<string> priorityExtensions, string key)
        {
            return new Dictionary<WorkProperties, object>
            {
                [WorkProperties.Name] = name,
                [WorkProperties.Source] = source,
                [WorkProperties.Target] = target,
                [WorkProperties.TypeSave] = save,
                [WorkProperties.CryptoExtensions] = cryptoExtensions,
                [WorkProperties.PriorityExtensions] = priorityExtensions,
                [WorkProperties.Key] = key
            };
        }

        public void ModifyWork(Work work, string name, string source, string target, string save, List<string> cryptoExtensions, List<string> priorityExtensions, string key)
        {
            int index = works.IndexOf(work);
            works.Remove(work);
            AddWorkAt(name, source, target, save, cryptoExtensions, priorityExtensions, key, index);
        }
        public void ModifyWork(Work work, int field, int typeSaveChoosen)
        {
            if (field == 4)
            {
                work.SaveType = typeSave[typeSaveChoosen];
            }

        }
        public void DeleteWork(int idWork)
        {

            try
            {
                string temp = works[idWork].Name;
                works.Remove(works[idWork]);
                confSaver.DeleteFile(temp);
            } catch(Exception e)
            {
                Console.WriteLine("[-] An error occured, can't delete work: {0}", e);
            }
        }


    }
}