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

        public ModifyWorkWindow()
        {
            InitializeComponent();
            Closing += ModifyWorkWindow_Closing;
            CypherOptionsCHB.Checked += printCypherOptions;
            CypherOptionsCHB.Unchecked += HideCypherOptions;


        }

        private void HideCypherOptions(object sender, RoutedEventArgs e)
        {
            HideCypher();

          

        }
        void HideCypher()
        {
            PlusMinusBtnSP.Visibility = Visibility.Collapsed;
            keyLbl.Visibility = Visibility.Collapsed;
            KeyTB.Visibility = Visibility.Collapsed;
            extLV.Visibility = Visibility.Collapsed;
        }
        private void printCypherOptions(object sender, RoutedEventArgs e)
        {
            ShowCypherOptions();
        }

        private void ShowCypherOptions()
        {
            PlusMinusBtnSP.Visibility = Visibility.Visible;
            keyLbl.Visibility = Visibility.Visible;
            KeyTB.Visibility = Visibility.Visible;
            extLV.Visibility = Visibility.Visible;
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
                
                Show();
                UpdateWv();

            }


        }

        private void UpdateWv()
        {
            WorkNameTB.Text = we.wv.name;
            WorkSourceTB.Text = we.wv.source;
            WorkTargetTB.Text = we.wv.target;
            SaveTypeCB.SelectedIndex = we.wv.typeSave;
            KeyTB.Text = we.wv.key;
            CypherOptionsCHB.IsChecked = we.chiffrage;
            /* if (we.chiffrage)
             {
                 ShowCypherOptions();
             }
             else
             {
                 HideCypher();
             }*/
            for (int i = extLV.Items.Count -1; i>=1; i--)
            {
                extLV.Items.RemoveAt(i);
            }

            foreach (string ext in we.wv.extension)
            {
                TextBox tb = new TextBox();
                tb.Width = 180;
                tb.Text = ext;
                extLV.Items.Add(tb);
            }
           

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Width = 180;
            extLV.Items.Add(tb);
        }
        private void minusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (extLV.Items.Count > 1)
            {
               ((TextBox) extLV.Items[extLV.Items.Count - 1]).Text = "";
                extLV.Items.RemoveAt(extLV.Items.Count - 1);
            }
        }
       
       
    }
}
