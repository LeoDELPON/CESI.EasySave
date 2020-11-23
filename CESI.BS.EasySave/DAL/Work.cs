using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Business.DAL
{

    public class Work
    {
        public Work(string name, string source, string target, Save save)
        {
            this.name = name;
            this.source = source;
            this.target = target;
            this.save = save;
        }
        public string name { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public Save save { get; set; }
    }
}