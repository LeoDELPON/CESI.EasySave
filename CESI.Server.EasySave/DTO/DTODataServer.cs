using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.Server.EasySave
{
    public class DTODataServer
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string Size { get; set; }
        public string Duration { get; set; }
        public string TypeSave { get; set; }
        public string EncryptDuration { get; set; }
        public string State { get; set; }
        public string EligibleFiles { get; set; }
        public string Progress { get; set; }
        public string RemaningFiles { get; set; }
        public string RemainingSize { get; set; }
    }
}
