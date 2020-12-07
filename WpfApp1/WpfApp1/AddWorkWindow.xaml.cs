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
        private ResourceDictionary obj;
        public CipherWindow cipherWindow = new CipherWindow();


        public string key { get; set; } = "";
        public string extention { get; set; } = "";

        public AddWorkWindow(Uri dictionnaryUri)


        {

            InitializeComponent();
            Closing += AddWorkWindow_Closing;
            ChangeLanguage(dictionnaryUri);
            cipherWindow.OkBtn.Click += OkBtn_Click;




        }

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
        public void ChangeLanguage(Uri dictionnaryUri)
        {
            if (String.IsNullOrEmpty(dictionnaryUri.OriginalString) == false)
            {
                ResourceDictionary objNewLanguageDictionary = (ResourceDictionary)(Application.LoadComponent(dictionnaryUri));

                if (objNewLanguageDictionary != null)
                {
                    this.Resources.MergedDictionaries.Remove(obj);
                    this.Resources.MergedDictionaries.Add(objNewLanguageDictionary);

                    CultureInfo culture =
                       new CultureInfo((string)Application.Current.Resources["Culture"]);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
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
