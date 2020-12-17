using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.DAL;
using System.Collections.Generic;

namespace CesiEasySave.View.Interface
{
    public interface IEasySaveView
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
        public void TooMuchWorks();
        void unreachablePath();
    }
}

