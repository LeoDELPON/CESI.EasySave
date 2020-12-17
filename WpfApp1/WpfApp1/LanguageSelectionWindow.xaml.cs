﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{

    /// <summary>
    /// Logique d'interaction pour LanguageSelectionWindow.xaml
    /// </summary>
    public partial class LanguageSelectionWindow : Window
    {
        readonly List<InfoLanguage> listLanguage = new List<InfoLanguage>();
        struct InfoLanguage
        {
            public string name;
            public Uri path;

        }
        private readonly ResourceDictionary obj;
        public LanguageSelectionWindow()
        {
            InitializeComponent();
            Closing += LanguageSelectionWindow_Closing;
            listLanguage.Add(new InfoLanguage { name = "Français", path = new Uri(@"\Language\fr-FR.xaml", UriKind.Relative) });
            listLanguage.Add(new InfoLanguage { name = "English", path = new Uri(@"\Language\en-US.xaml", UriKind.Relative) });
            foreach (InfoLanguage il in listLanguage)
            {
                LanguageCB.Items.Add(il.name);
            }
            LanguageCB.SelectedIndex = 1;
            LanguageCB.SelectionChanged += LanguageCB_SelectionChanged;


        }

        private void LanguageCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ChangeLangage(GetLanguagePath());
        }

        private void LanguageSelectionWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;
            Hide();

        }

        public Uri GetLanguagePath()
        {
            return listLanguage[LanguageCB.SelectedIndex].path;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        public void ChangeLangage(Uri dictionnaryUri)
        {
            if (String.IsNullOrEmpty(dictionnaryUri.OriginalString) == false)
            {
                ResourceDictionary objNewLanguageDictionary = (ResourceDictionary)(Application.LoadComponent(dictionnaryUri));

                if (objNewLanguageDictionary != null)
                {
                    Resources.MergedDictionaries.Remove(obj);
                    Resources.MergedDictionaries.Add(objNewLanguageDictionary);

                    CultureInfo culture =
                       new CultureInfo((string)Application.Current.Resources["Culture"]);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
        }
    }
}
