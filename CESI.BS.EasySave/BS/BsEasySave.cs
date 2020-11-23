using System;
using System.Collections.Generic;
using System.Text;
using CESI.BS.EasySave.DAL;

namespace CESI.BS.EasySave.BS
{
    public class Model
    {
        public List<Save> typeSave { get; set; }
        public Model()
        {
            typeSave.Add(new Differential());
            typeSave.Add(new Full());
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