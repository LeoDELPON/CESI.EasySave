﻿using System;
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

            typeSave.Add(new WorkFactory().CreateSaveObject("dif","", null, ""));
            typeSave.Add(new WorkFactory().CreateSaveObject("ful","", null, ""));
        }

        public List<Work> works = new List<Work>();
        // à remplir de tous les enfants de save

        public List<Work> GetWorks()
        {
            return works;
        }
        public void AddWork(string name, string source, string target, string save, List<string> extensions, string key)
        {
            Dictionary<WorkProperties, object> propertiesWork = new Dictionary<WorkProperties, object>
            {
                [WorkProperties.Name] = name,
                [WorkProperties.Source] = source,
                [WorkProperties.Target] = target,
                [WorkProperties.TypeSave] = save,
                [WorkProperties.Extensions] = extensions,
                [WorkProperties.Key] = key
            };
            works.Add(new WorkFactory().CreateWorkObject(propertiesWork));
        }
        public void ModifyWork(Work work, string name, string source, string target, string save, List<string> extensions, string key)
        {
            Dictionary<WorkProperties, object> propertiesWork = new Dictionary<WorkProperties, object>
            {
                [WorkProperties.Name] = name,
                [WorkProperties.Source] = source,
                [WorkProperties.Target] = target,
                [WorkProperties.TypeSave] = save,
                [WorkProperties.Extensions] = extensions,
                [WorkProperties.Key] = key
            };
            works[works.IndexOf(work)] = new WorkFactory().CreateWorkObject(propertiesWork);
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