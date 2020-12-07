using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour AddWorkWindow.xaml
    /// </summary>
    public partial class AddWorkWindow : Window
    {

        public CipherWindow cipherWindow = new CipherWindow();


        public string key { get; set; } = "";
        public string extention { get; set; } = "";

      

        private void AddWorkWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public AddWorkWindow()
        {

            InitializeComponent();
            Closing += AddWorkWindow_Closing;
            cipherWindow.OkBtn.Click += OkBtn_Click;

        }
        
        private void CipherOptions(object sender, RoutedEventArgs e)
        {
            if ((bool)isXor.IsChecked)
            {
                cipherWindow.Show();
            }

        }


        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((cipherWindow.keyTextBox.Text.Length == 0 && cipherWindow.extentionTextBox.Text.Length == 0) ||
                (cipherWindow.keyTextBox.Text.Length > 0 && cipherWindow.extentionTextBox.Text.Length > 0))
            {


                cipherWindow.Hide();

                key = cipherWindow.keyTextBox.Text;
                extention = cipherWindow.extentionTextBox.Text;
            }


        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
