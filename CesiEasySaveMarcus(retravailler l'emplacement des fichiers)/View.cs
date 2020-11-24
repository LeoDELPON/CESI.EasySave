using System;
using System.Collections.Generic;
using System.Text;

namespace Console_EasySave
{
    interface View
    {
        public string PrintMainMenu();
        public string PrintWorks(List<Work> works);
        public string PrintNoWork();
        public string AskName();
        public string AskTarget();
        public string AskSource();
        public string AskSaveType(List<Save> typeSave);
        public string WichWorkField();
        public string AskStr();
        public bool ConfirmDelete(string name);
        public string AskLanguage(); 
    }
        
        
}
