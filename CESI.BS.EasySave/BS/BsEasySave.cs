using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    public class BsEasySave
    {
        public List<Save> typeSave { get; set; }
        public BsEasySave()
        {
            typeSave.Add(new Differential());
            typeSave.Add(new Full());
            Console.WriteLine("test pour le webhook");
        }



        List<Work> works;
        // à remplire de tous les enfants de save

        public List<Work> getWorks()
        {
            return works;
        }
        public void addWork(string name, string source, string target, Save save)
        {
            works.Add(new Work(name, source, target, save));
        }


    }
}