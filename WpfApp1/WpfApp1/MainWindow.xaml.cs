﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using CESI.BS.EasySave.DAL;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        LanguageSelectionWindow languageSelectionWindow = new LanguageSelectionWindow();
        AddWorkWindow addWorkWindow = new AddWorkWindow();
        
        List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        List<WrkElements> weList = new List<WrkElements>(); 
        private ResourceDictionary obj;
   
        public BSEasySave bs = new BSEasySave();
        ModifyWorkWindow modifyWorkWindow;


        public MainWindow()        {
         
            InitializeComponent();
            modifyWorkWindow = new ModifyWorkWindow(bs);
            modifyWorkWindow.OkBtn.Click += ModifyOkBtn_Click;
            addWorkWindow.OkBtn.Click += OkBtn_Click;
            Closing += MainWindow_Closing;
            this.Show();
            listWorks = bs.confSaver.GetSavedWorks();
            addExistingWorksToView();
            ChangeLangage(languageSelectionWindow.getLanguagePath());
            languageSelectionWindow.OkBtn.Click += LanguageOkBtn_Click;
        }

        private void ModifyOkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FolderBuilder.CheckFolder(modifyWorkWindow.WorkSourceTB.Text) && FolderBuilder.CheckFolder(modifyWorkWindow.WorkTargetTB.Text))
            {
                modifyWorkWindow.Hide();
                int index = weList.IndexOf(modifyWorkWindow.we);
                int Size = bs.GetWorks().Count();
                bs.ModifyWork(bs.GetWorks()[index], 1, modifyWorkWindow.WorkNameTB.Text);
                bs.ModifyWork(bs.GetWorks()[index], 2, modifyWorkWindow.WorkSourceTB.Text);
                bs.ModifyWork(bs.GetWorks()[index], 3, modifyWorkWindow.WorkTargetTB.Text);
                bs.ModifyWork(bs.GetWorks()[index], 4, modifyWorkWindow.SaveTypeCB.SelectedIndex);
                WorkVar workVar = new WorkVar();

                bs.confSaver.ModifyFile(weList[index].wv.name, 2, modifyWorkWindow.WorkSourceTB.Text);
                bs.confSaver.ModifyFile(weList[index].wv.name, 3, modifyWorkWindow.WorkTargetTB.Text);
                bs.confSaver.ModifyFile(weList[index].wv.name, 4, modifyWorkWindow.SaveTypeCB.SelectedIndex.ToString());
                bs.confSaver.ModifyFile(weList[index].wv.name, 1, modifyWorkWindow.WorkNameTB.Text);
                workVar.name = modifyWorkWindow.WorkNameTB.Text;
                workVar.source = modifyWorkWindow.WorkSourceTB.Text;
                workVar.target = modifyWorkWindow.WorkTargetTB.Text;
                workVar.typeSave = modifyWorkWindow.SaveTypeCB.SelectedIndex;

                weList[index].wv = workVar;
                weList[index].inSvdList.UpdateWv(weList[index].wv);
                weList[index].inWrkList.UpdateWv(weList[index].wv);
            }




        }

        private void LanguageOkBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeLangage(languageSelectionWindow.getLanguagePath());
            addWorkWindow.ChangeLangage(languageSelectionWindow.getLanguagePath());
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();//we will have to manage running work
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {

            if (FolderBuilder.CheckFolder(addWorkWindow.WorkSourceTB.Text) && FolderBuilder.CheckFolder(addWorkWindow.WorkTargetTB.Text))
            {
                addWorkWindow.Hide();
                ConfSaver.WorkVar wv = new ConfSaver.WorkVar();
                wv.name = addWorkWindow.WorkNameTB.Text;
                wv.source = addWorkWindow.WorkSourceTB.Text;
                wv.target = addWorkWindow.WorkTargetTB.Text;
                wv.typeSave = addWorkWindow.SaveTypeCB.SelectedIndex;
                bs.AddWork(wv.name, wv.source, wv.target, ((ComboBoxItem)addWorkWindow.SaveTypeCB.SelectedItem).Name);// ajout du travail
                WrkElements we = new WrkElements(wv, bs);
                
            
                PrepareWrkElement(we);
                bs.confSaver.SaveWork(wv);

              
            }
            else
            {
                //error Message
            }
        }
        private void ToWorkList_Click(object sender, RoutedEventArgs e, WrkElements we)
        {
            ToWorkList(we);

        }

        private void ToWorkList(WrkElements we)
        {
            SaveListLbl.Items.Remove(we.inSvdList);
            WorkListLbl.Items.Add(we.inWrkList);
        }

        private void ToSaveList_Click(object sender, RoutedEventArgs e, WrkElements we)
        {
            ToSaveList(we);

        }

        private void ToSaveList(WrkElements we)
        {
            WorkListLbl.Items.Remove(we.inWrkList);
            SaveListLbl.Items.Add(we.inSvdList);
        }

        private void addExistingWorksToView()
        {
         
            foreach (ConfSaver.WorkVar work in listWorks)
            {
                
                WrkElements we = new WrkElements(work, bs);
                PrepareWrkElement(we);

               

            }
        }

        private void PrepareWrkElement(WrkElements we)
        {
        
            we.inSvdList.toWorkList.Click += (sender, e) => ToWorkList_Click(sender, e, we);
            we.inWrkList.ToSaveList.Click += (sender, e) => ToSaveList_Click(sender, e, we);
            we.inWrkList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, we);
            we.inSvdList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, we);
            SaveListLbl.Items.Add(we.inSvdList);
            weList.Add(we);
            bs.AddWork(we.wv.name, we.wv.source, we.wv.target, SaveTypeMethods.GetSaveTypeFromInt(we.wv.typeSave));
        }

      

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void ChangeLangage(Uri dictionnaryUri)
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

                    //test
                }
            }
        }

        public void launchWorkList()
        {
            


            foreach (WrkElements we in weList)
            {
                if (WorkListLbl.Items.Contains(we.inWrkList))
                {
                    bs.works[weList.IndexOf(we)]._saveType.handler.Subscribe(we.inWrkList);
                    bs.works[weList.IndexOf(we)].Perform();
                    bs.works[weList.IndexOf(we)]._saveType.handler.Unsubscribe(we.inWrkList);
                }
            }
        }
        private void launchWorksButton(object sender, RoutedEventArgs e)            
        {
            Thread worksThreads = new Thread(launchWorkList);
            worksThreads.Start();
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void AddWorkBtn_Click(object sender, RoutedEventArgs e)
        {            
                addWorkWindow.Show();
        }

        private void languageBtn_Click(object sender, RoutedEventArgs e)
        {
            languageSelectionWindow.Show();
        }

        private void MultipleAddBtn(object sender, RoutedEventArgs e)
        {
            foreach (WrkElements we in weList)
            {
                if ((bool)we.inSvdList.checkBox.IsChecked && SaveListLbl.Items.Contains(we.inSvdList))
                {
                    ToWorkList(we);
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i< weList.Count(); i++) 
            {
                WrkElements we = weList[i];
                if ((bool)we.inSvdList.checkBox.IsChecked && SaveListLbl.Items.Contains(we.inSvdList))
                {
                    bs.DeleteWork(weList.IndexOf(we));
                    SaveListLbl.Items.Remove(we.inSvdList);
                    bs.confSaver.DeleteFile(we.wv.name);
                    weList.Remove(we);
                }
            }
        }
    }
}
