using System;
using System.Collections.Generic;
using EasySave.Business.DAL;
using System.Text;

namespace EasySave.View.Interface
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


        