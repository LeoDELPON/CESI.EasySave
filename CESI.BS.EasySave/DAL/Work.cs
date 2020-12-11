using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CESI.BS.EasySave.DAL
{

    public class Work
    {
        public Save SaveType { get; set; }
        public Guid Id { get; private set; }
        public WorkState State { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }

        internal Work(string name, string source, string destination, Save saveType)
        {
            Id = Guid.NewGuid();
            State = WorkState.Exist;
            SaveType = saveType;
            Name = name;
            Source = source;
            Target = destination;
        }

        public void Perform()
        {
            SaveType.SaveProcess(Source, Target);
        }
    }
}