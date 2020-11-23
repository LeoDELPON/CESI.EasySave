using System;
using System.Collections.Generic;
using CESI.BS.EasySave.DAL;
using System.Text;

namespace CesiEasySave.View.Interface
{
    interface IEasySaveView
    {
        public string printMainMenu();
        public string printWorks(List<Work> works);
        public string printNoWork();
        string askName();
        string askTarget();
        string askSource();
        string askSaveType(List<Save> typeSave);

    }


        