﻿using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using CESI.BS.EasySave.BS.Observers;
using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using System.Windows;
using System.Windows.Controls;
namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour WrkElementInWrkList.xaml
    /// </summary>
    public partial class WrkElementInWrkList : UserControl, Observer
    {
        BSEasySave bs;

        public WrkElementInWrkList(ConfSaver.WorkVar workVar,BSEasySave BS)
        {
            InitializeComponent();
            bs = BS;
            UpdateWv(workVar);

        }

        public void ReactDataLogServ(DTODataServer dict)
        {
        }

        public void ReactProgression(double progress)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                workProgressBar.Value = progress;
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
