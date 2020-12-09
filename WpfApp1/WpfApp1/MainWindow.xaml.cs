using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        ProcessusChoosing processusChoosing = new ProcessusChoosing(Process.GetProcesses());
        LanguageSelectionWindow languageSelectionWindow = new LanguageSelectionWindow();
        
        AddWorkWindow addWorkWindow = new AddWorkWindow();
        List<string> forbProc = new List<string>();
        
        List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        List<WrkElements> weList = new List<WrkElements>(); 
        private ResourceDictionary obj;
   
        public BSEasySave bs = new BSEasySave();
        ModifyWorkWindow modifyWorkWindow;
        public List<string> extentionList = new List<string>();


        public MainWindow()        {
         
            InitializeComponent();
            modifyWorkWindow = new ModifyWorkWindow(bs);
            modifyWorkWindow.OkBtn.Click += ModifyOkBtn_Click;
            addWorkWindow.OkBtn.Click += OkBtn_Click;
            Closing += MainWindow_Closing;
            this.Show();
            listWorks = bs.confSaver.GetSavedWorks();
            addExistingWorksToView();
            ChangeLanguage(languageSelectionWindow.getLanguagePath());
            languageSelectionWindow.OkBtn.Click += LanguageOkBtn_Click;
            processusChoosing.OkBtn.Click += pcOkBtn;
        }

        private void pcOkBtn(object sender, RoutedEventArgs e)
        {
            processusChoosing.Hide();
            forbProc.Clear();
            foreach(ComboBox cb in processusChoosing.ListCB.Items)
            {
                if (cb.SelectedIndex > -1)
                {
                    forbProc.Add(cb.SelectedItem.ToString());
                }
            }
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
            ChangeLanguage(languageSelectionWindow.getLanguagePath());
          
          
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();//we will have to manage running work
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if((bool)addWorkWindow.isXor.IsChecked && (addWorkWindow.key.Length == 0 || addWorkWindow.extention.Length == 0))
            {
               
                return;
            }
            if (addWorkWindow.WorkNameTB.Text.Equals(""))
            {
                addWorkWindow.WorkNameTB.Text = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            }

            if (FolderBuilder.CheckFolder(addWorkWindow.WorkSourceTB.Text) && FolderBuilder.CheckFolder(addWorkWindow.WorkTargetTB.Text))
            {
                addWorkWindow.Hide();
                WorkVar wv = new WorkVar();
                wv.name = addWorkWindow.WorkNameTB.Text;
                wv.source = addWorkWindow.WorkSourceTB.Text;
                wv.target = addWorkWindow.WorkTargetTB.Text;
                wv.typeSave = addWorkWindow.SaveTypeCB.SelectedIndex;
                
                if ((bool)addWorkWindow.isXor.IsChecked)
                {
                    wv.key = addWorkWindow.key;
                    wv.extension = addWorkWindow.extention;
                    extentionList.Clear();
                    extentionList.Add(wv.extension);

                }
                else
                {
                    wv.key = "null";
                    wv.extension = "null";
                    extentionList.Clear();
                    extentionList.Add(wv.extension);
                }
                bs.AddWork(wv.name, wv.source, wv.target, ((ComboBoxItem)addWorkWindow.SaveTypeCB.SelectedItem).Name, extentionList, wv.key);// ajout du travail
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
            we.inWrkList.workProgressBar.Value = 0;
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
        
            we.inSvdList.ToWorkList.Click += (sender, e) => ToWorkList_Click(sender, e, we);
            we.inWrkList.ToSaveList.Click += (sender, e) => ToSaveList_Click(sender, e, we);
            we.inWrkList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, we);
            we.inSvdList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, we);
            SaveListLbl.Items.Add(we.inSvdList);
            weList.Add(we);
            List<string> bouchon = new List<string>();
            bouchon.Add(we.wv.extension);
            bs.AddWork(we.wv.name, we.wv.source, we.wv.target, SaveTypeMethods.GetSaveTypeFromInt(we.wv.typeSave), bouchon, we.wv.key);
        }

      



  

        public void ChangeLanguage(Uri dictionnaryUri)
        {
            if (String.IsNullOrEmpty(dictionnaryUri.OriginalString) == false)
            {
                ResourceDictionary objNewLanguageDictionary = (ResourceDictionary)(Application.LoadComponent(dictionnaryUri));

                if (objNewLanguageDictionary != null)
                {
                    ChanheLanguageRessourcesOfAllWindows(objNewLanguageDictionary);

                    CultureInfo culture =
                       new CultureInfo((string)Application.Current.Resources["Culture"]);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;


                }
            }
        }

        private void ChanheLanguageRessourcesOfAllWindows(ResourceDictionary objNewLanguageDictionary)
        {
            Resources.MergedDictionaries.Remove(obj);
            Resources.MergedDictionaries.Add(objNewLanguageDictionary);
            addWorkWindow.Resources.MergedDictionaries.Remove(obj);
            addWorkWindow.Resources.MergedDictionaries.Add(objNewLanguageDictionary);
            addWorkWindow.cipherWindow.Resources.MergedDictionaries.Remove(obj);
            addWorkWindow.cipherWindow.Resources.MergedDictionaries.Add(objNewLanguageDictionary);
            modifyWorkWindow.Resources.MergedDictionaries.Remove(obj);
            modifyWorkWindow.Resources.MergedDictionaries.Add(objNewLanguageDictionary);
        }

        public void launchWorkList()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                launchWorkBtn.IsEnabled = false;
            });

            foreach (WrkElements we in weList)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                  
                
                we.inWrkList.workProgressBar.Value = 0; //reset progress Bar             
            
                we.inSvdList.ToWorkList.IsEnabled = false; //disable clicks
                    if (WorkListLbl.Items.Contains(we.inWrkList))
                    {
                        we.inWrkList.IsEnabled = false;
                    }

                });
            }
          
            foreach (WrkElements we in weList)
            {
                int count = weList.Count;
                if (WorkListLbl.Items.Contains(we.inWrkList))
                {
                   
                   
                   
                    bs.works[weList.IndexOf(we)]._saveType.handler.Subscribe(we.inWrkList);
                    bs.works[weList.IndexOf(we)].Perform();
                    bs.works[weList.IndexOf(we)]._saveType.handler.Unsubscribe(we.inWrkList);
                    
                   
                }
            }
            foreach (WrkElements we in weList)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
         
                    we.inSvdList.ToWorkList.IsEnabled = true;//enable clicks
                    if (WorkListLbl.Items.Contains(we.inWrkList))
                    {
                        we.inWrkList.IsEnabled = true;
                    }

                });
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
           
                launchWorkBtn.IsEnabled = true;
            });
        }
        private void launchWorksButton(object sender, RoutedEventArgs e)
        {
            Process[] aProc;
           
            Thread worksThreads = new Thread(launchWorkList);
            if (forbProc.Count == 0)
            {
                worksThreads.Start();
            }
            else
            {
                foreach(string name in forbProc)
                {
                    aProc = Process.GetProcessesByName(name);
                   if (aProc.Length > 0) { 
                        return;

                    }
                }
                worksThreads.Start();
            }
            
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
            int offset = 0;
            int goal = weList.Count();
            for (int i = 0; i< goal; i++) 
            {
                WrkElements we = weList[i-offset];
                if ((bool)we.inSvdList.checkBox.IsChecked && SaveListLbl.Items.Contains(we.inSvdList))
                {
                    bs.DeleteWork(weList.IndexOf(we));
                    SaveListLbl.Items.Remove(we.inSvdList);
                    bs.confSaver.DeleteFile(we.wv.name);
                    weList.Remove(we);
                    offset++;
                }
            }
        }

        private void CriticalProcessesBtn_Click(object sender, RoutedEventArgs e)
        {
            processusChoosing.Show();
        }


    }
}

