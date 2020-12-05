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
    /// Logique d'interaction pour wrkElement.xaml
    /// </summary>
    public partial class WrkElementInSavedList : UserControl
    {
        FrameworkElement tb;
        private ConfSaver.WorkVar workVar;
        public int index { get; set; }
        BSEasySave BS;
        public WrkElementInSavedList(ConfSaver.WorkVar workVar, BSEasySave BS)
        {
            InitializeComponent();
            this.workVar = workVar;
            this.BS = BS;
            workNameLbl.Content = workVar.name;
            workSourceLbl.Content = workVar.source;
            workTargetLbl.Content = workVar.target;
            workTypeLbl.SetResourceReference(Label.ContentProperty, BS.typeSave[workVar.typeSave].GetNameTypeWork());


            //workTypeLbl.Content = (string)Application.Current.Resources[BS.typeSave[workVar.typeSave].GetNameTypeWork()];
            this.index=index;
        }
    }
}
