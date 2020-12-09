using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour ModifyWorkWindow.xaml
    /// </summary>
    public partial class ModifyWorkWindow : Window
    {
        public WrkElements we { get; set; } = new WrkElements();
        BSEasySave bs;
        private ResourceDictionary obj;

        public ModifyWorkWindow(BSEasySave BS)
        {
            InitializeComponent();
            Closing += ModifyWorkWindow_Closing;
            bs = BS;
            
        }

        private void ModifyWorkWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            
        }

        internal void DoubleClickOnWorkElement(object sender, MouseButtonEventArgs e,   WrkElements we)
        {
            if (!IsVisible)
            {
                this.we = we;
                UpdateWv();
                Show();

            }


        }

        private void UpdateWv()
        {
            WorkNameTB.Text = we.wv.name;
            WorkSourceTB.Text = we.wv.source;
            WorkTargetTB.Text = we.wv.target;
            SaveTypeCB.SelectedItem = we.wv.typeSave;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
