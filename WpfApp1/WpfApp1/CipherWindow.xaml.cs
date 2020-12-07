using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;

namespace WpfApp1


{
    public partial class CipherWindow : Window
    {

        List<InfoLanguage> listLanguage = new List<InfoLanguage>();
        private ResourceDictionary obj;

        struct InfoLanguage
        {
            public string name;
            public Uri path;

        }
        public CipherWindow()
        {
            InitializeComponent();
            listLanguage.Add(new InfoLanguage { name = "Français", path = new Uri(@"\Language\fr-FR.xaml", UriKind.Relative) });
            listLanguage.Add(new InfoLanguage { name = "English", path = new Uri(@"\Language\en-US.xaml", UriKind.Relative) });
            Closing += Cipher_Window_Closing;




        }

        private void Cipher_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
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

    }
}
