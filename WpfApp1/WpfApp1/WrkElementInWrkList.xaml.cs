﻿using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using CESI.BS.EasySave.BS.Observers;
using CESI.BS.EasySave.DAL;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour WrkElementInWrkList.xaml
    /// </summary>
    public partial class WrkElementInWrkList : UserControl, IObserver
    {
        public WrkElementInWrkList(ConfSaver.WorkVar workVar)
        {
            InitializeComponent();
            UpdateWv(workVar);

        }

        public void ReactDataUpdate(Dictionary<WorkProperties, object> dict)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                long value = (long)dict[WorkProperties.Progress];
                workProgressBar.Value = value;
            });



        }

        public void UpdateWv(ConfSaver.WorkVar workVar)
        {
            workNameLbl.Content = workVar.name;
            workSourceLbl.Content = workVar.source;
            workTargetLbl.Content = workVar.target;
            workTypeLbl.SetResourceReference(Label.ContentProperty, SaveTypeMethods.GetSaveTypeFromInt(workVar.typeSave));
        }
    }
}
