using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1


{
    public partial class CipherWindow : Window
    {

        List<InfoLanguage> listLanguage = new List<InfoLanguage>();
        public List<TextBox> extentionList = new List<TextBox>();
        

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
            AddExtentionBoxButton.Click += AddExtentionBoxButton_Click;





        }

        private void AddExtentionBoxButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox extention = new TextBox();
            SPExtention.Children.Add(extention);
            extentionList.Add(extention);





        }

        private void Cipher_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if ((keyTextBox.Text.Length==0 && extentionTextBox.Text.Length ==0) || (keyTextBox.Text.Length > 0 && extentionTextBox.Text.Length > 0))
            {
                Hide();
            }
            
        }

       

    }
}
