using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1


{
    public partial class CipherWindow : Window
    {
        readonly List<InfoLanguage> listLanguage = new List<InfoLanguage>();
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
            AddExtention();
        }

        private void AddExtentionBoxButton_Click(object sender, RoutedEventArgs e)
        {
            AddExtention();
        }

        private void AddExtention()
        {
            TextBox extention = new TextBox();
            extention.Width = 180;
            SPExtention.Items.Add(extention);
            extentionList.Add(extention);
        }

        private void Cipher_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            keyTextBox.Text = "";
            extentionList.Clear();
            SPExtention.Items.Clear();
            //  if ((keyTextBox.Text.Length==0 && extentionList.Count ==0) || (keyTextBox.Text.Length > 0 && extentionList.Count > 0))
            //{
            Hide();
            //}

        }

        private void RemoveExtentionButon_Click(object sender, RoutedEventArgs e)
        {
            if (extentionList.Count > 0)
            {
                SPExtention.Items.RemoveAt(SPExtention.Items.Count - 1);
                extentionList.RemoveAt(extentionList.Count - 1);
            }
        }
    }
}
