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
    public partial class WrkElementInWrkList : UserControl
    {
        private ConfSaver.WorkVar workVar;
        BSEasySave BS;
        public WrkElementInWrkList(ConfSaver.WorkVar workVar, BSEasySave BS)
        {
            InitializeComponent();
            this.workVar = workVar;
            this.BS = BS;
            workNameLbl.Content = workVar.name;
            workSourceLbl.Content = workVar.source;
            workTargetLbl.Content = workVar.target;
            workTypeLbl.SetResourceReference(Label.ContentProperty, BS.typeSave[workVar.typeSave].GetNameTypeWork());

        }
    }
}
