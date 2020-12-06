using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public void reactProgression(double progress)
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
            workTypeLbl.SetResourceReference(Label.ContentProperty, bs.typeSave[workVar.typeSave].GetNameTypeWork());
        }
    }
}
