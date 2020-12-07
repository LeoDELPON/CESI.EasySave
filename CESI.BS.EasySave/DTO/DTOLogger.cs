using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DTO
{
    public class DTOLogger
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string Size { get; set; }
        public string Duration { get; set; }
        public string EncryptDuration { get; set; }
    }
}
