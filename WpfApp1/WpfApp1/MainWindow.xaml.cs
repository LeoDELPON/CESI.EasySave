using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
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
using CESI.Server.EasySave.Networking;
using CESI.Server.EasySave.Services;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;
using static CESI.BS.EasySave.BS.ThreadLifeManager;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int terminatedThreads = 0;
        AlertWindow alertWindow = new AlertWindow();
        ProcessusChoosing processusChoosing = new ProcessusChoosing();
        LanguageSelectionWindow languageSelectionWindow = new LanguageSelectionWindow();

        AddWorkWindow addWorkWindow = new AddWorkWindow();
        public List<string> forbProc { get; set; } = new List<string>();

        List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        List<WrkElements> weList = new List<WrkElements>();
        private ResourceDictionary obj;

        public BSEasySave bs = new BSEasySave();
        public ThreadLifeManager threadLifeManager;
        ModifyWorkWindow modifyWorkWindow;

        private readonly ServerSocket myServer;

        public MainWindow()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(processName).Length > 1)
            {
                Application.Current.Shutdown();
            }
            PacketHandler.OnAbortSent +=threadLifeManager.Abort;
            PacketHandler.OnResumeSent += threadLifeManager.Resume;
            PacketHandler.OnPauseSent += threadLifeManager.Pause;
            InitializeComponent();
            modifyWorkWindow = new ModifyWorkWindow();
            modifyWorkWindow.OkBtn.Click += ModifyOkBtn_Click;
            addWorkWindow.OkBtn.Click += OkBtn_Click;
            Closing += MainWindow_Closing;
            Show();
            listWorks = bs.confSaver.GetSavedWorks();
            addExistingWorksToView();
            ChangeLanguage(languageSelectionWindow.getLanguagePath());
            languageSelectionWindow.OkBtn.Click += LanguageOkBtn_Click;
            processusChoosing.OkBtn.Click += pcOkBtn;
            threadLifeManager = new ThreadLifeManager(bs, forbProc);
            threadLifeManager.OnNotAdmin += ThreadAlertMsg;
            threadLifeManager.StartObservingProcesses();
            myServer = ServerSocket.Instance;
            myServer.StartConnection(1);
        }
        public void ThreadAlertMsg()
        {
            alertWindow.showMessage("NotAdminMsg", true);
        }

        private void pcOkBtn(object sender, RoutedEventArgs e)
        {
            processusChoosing.Hide();
            forbProc.Clear();
            foreach (ComboBox cb in processusChoosing.ListCB.Items)
            {
                if (cb.SelectedIndex > -1)
                {
                    forbProc.Add(cb.SelectedItem.ToString());
                }
            }
        }

        private void ModifyOkBtn_Click(object sender, RoutedEventArgs e)
        {

            List<string> listExt = new List<string>();
            for (int i = 1; i < modifyWorkWindow.extLV.Items.Count; i++)
            {
                if (!((TextBox)modifyWorkWindow.extLV.Items[i]).Text.Equals(""))
                {
                    listExt.Add(((TextBox)modifyWorkWindow.extLV.Items[i]).Text);
                }
            }
            if ((bool)modifyWorkWindow.CypherOptionsCHB.IsChecked && (modifyWorkWindow.KeyTB.Text.Length == 0 || listExt.Count == 0))
            {

                return;
            }
            if (modifyWorkWindow.WorkNameTB.Text.Equals(""))
            {
                modifyWorkWindow.WorkNameTB.Text = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            }

            if (Directory.Exists(modifyWorkWindow.WorkSourceTB.Text) && Directory.Exists(modifyWorkWindow.WorkTargetTB.Text))
            {
                modifyWorkWindow.Hide();
                WorkVar wv = new WorkVar();
                wv.name = modifyWorkWindow.WorkNameTB.Text;
                wv.source = modifyWorkWindow.WorkSourceTB.Text;
                wv.target = modifyWorkWindow.WorkTargetTB.Text;
                wv.typeSave = modifyWorkWindow.SaveTypeCB.SelectedIndex;
                int index = weList.IndexOf(modifyWorkWindow.we);
                if ((bool)modifyWorkWindow.CypherOptionsCHB.IsChecked)
                {
                    wv.key = modifyWorkWindow.KeyTB.Text;
                    //wv.extension = addWorkWindow.extention;
                    wv.cryptoExtensions = listExt;
                    wv.priorityExtensions = new List<string>();
                    wv.priorityExtensions.Add("null");


                }
                else
                {
                    wv.key = "null";
                    wv.cryptoExtensions = new List<string>();
                    wv.cryptoExtensions.Add("null");
                    wv.priorityExtensions = new List<string>();
                    wv.priorityExtensions.Add("null");

                }
                weList[index].wv = wv;
                weList[index].inSvdList.UpdateWv(weList[index].wv);
                weList[index].inWrkList.UpdateWv(weList[index].wv);
                weList[index].chiffrage = (bool)modifyWorkWindow.CypherOptionsCHB.IsChecked;
                bs.confSaver.modifyEntireFile(bs.works[index].Name, weList[index].wv);
                bs.ModifyWork(bs.works[index], wv.name, wv.source, wv.target, SaveTypeMethods.GetSaveTypeFromInt(modifyWorkWindow.SaveTypeCB.SelectedIndex), wv.cryptoExtensions, wv.priorityExtensions, wv.key);// modification du travail




            }
            else
            {
                //error Message
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
            if ((bool)addWorkWindow.isXor.IsChecked && (addWorkWindow.key.Length == 0 || addWorkWindow.extention.Count == 0))
            {

                return;
            }
            if (addWorkWindow.WorkNameTB.Text.Equals(""))
            {
                addWorkWindow.WorkNameTB.Text = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            }

            if (Directory.Exists(addWorkWindow.WorkSourceTB.Text) && Directory.Exists(addWorkWindow.WorkTargetTB.Text))
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
                    //wv.extension = addWorkWindow.extention;
                    wv.cryptoExtensions = addWorkWindow.extention;
                    wv.priorityExtensions = new List<string>();
                    wv.priorityExtensions.Add("null");

                }
                else
                {
                    wv.key = "null";
                    wv.cryptoExtensions = new List<string>();
                    wv.cryptoExtensions.Add("null");
                    wv.priorityExtensions = new List<string>();
                    wv.priorityExtensions.Add("null");

                }
                bs.AddWork(wv.name, wv.source, wv.target, ((ComboBoxItem)addWorkWindow.SaveTypeCB.SelectedItem).Name, wv.cryptoExtensions, wv.priorityExtensions, wv.key);// ajout du travail
                WrkElements we = new WrkElements(wv, bs);
                we.chiffrage = (bool)addWorkWindow.isXor.IsChecked;

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
            weList.Add(we);
            we.inSvdList.ToWorkList.Click += (sender, e) => ToWorkList_Click(sender, e, we);
            we.inWrkList.ToSaveList.Click += (sender, e) => ToSaveList_Click(sender, e, we);
            we.inWrkList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, weList[weList.IndexOf(we)]);
            we.inSvdList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, weList[weList.IndexOf(we)]);
            SaveListLbl.Items.Add(we.inSvdList);

            bs.AddWork(we.wv.name, we.wv.source, we.wv.target, SaveTypeMethods.GetSaveTypeFromInt(we.wv.typeSave), we.wv.cryptoExtensions,we.wv.priorityExtensions, we.wv.key);
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
            threadLifeManager.ClearThread();

            EnableButtonsAccess(false);

            foreach (WrkElements we in weList)
            {

                int count = weList.Count;
                if (WorkListLbl.Items.Contains(we.inWrkList))
                {
                    /* Task test = Task.Factory.StartNew(() =>
                     {
                         try
                         {
                             using (ThreadMutex.Canceller.Token.Register(Thread.CurrentThread.Abort)) ;
                             while (true)
                             {
                                 Application.Current.Dispatcher.Invoke(() =>
                                 {
                                     WorkListLbl.Items.Add("en cours");
                                 });
                             }
                         }
                         catch (ThreadAbortException)
                         {
                             Application.Current.Dispatcher.Invoke(() =>
                             {
                                 WorkListLbl.Items.Add("fini");
                             });
                         }
                     }, ThreadMutex.Canceller.Token);
                     Thread.Sleep(5000);
                     ThreadMutex.Canceller.Cancel();*/
                    Thread saveThread = new Thread(launchWork =>
                    {
                        using (ThreadMutex.Canceller.Token.Register(Thread.CurrentThread.Abort)) { }

                        try
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                we.inWrkList.workProgressBar.Value = 0;
                            });
                            threadLifeManager.SubscribeToSaves(bs.works[weList.IndexOf(we)]);
                            bs.works[weList.IndexOf(we)].SaveType.Subscribe(we.inWrkList);
                            bs.works[weList.IndexOf(we)].Perform();
                            bs.works[weList.IndexOf(we)].SaveType.Unsubscribe(we.inWrkList);//can be deleted
                            terminatedThreads++;
                            threadLifeManager.UnsubscribeToSaves(bs.works[weList.IndexOf(we)]);
                        }
                        catch (ThreadInterruptedException)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {

                                we.inWrkList.workProgressBar.Value = 0;
                                terminatedThreads++;
                            });
                        }
                        if (terminatedThreads == threadLifeManager.Count())
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                resetButtons();
                                EnableButtonsAccess(true);
                                terminatedThreads = 0;
                            });
                        }



                    });

                    threadLifeManager.AddThread(saveThread);
                    saveThread.Priority = ThreadPriority.BelowNormal;
                    saveThread.Start();

                }
            }
        }


        private void EnableButtonsAccess(bool access)
        {


            launchWorkBtn.IsEnabled = access;


            foreach (WrkElements we in weList)
            {


                we.inSvdList.ToWorkList.IsEnabled = access;
                if (WorkListLbl.Items.Contains(we.inWrkList))
                {
                    we.inWrkList.IsEnabled = access;
                }


            }
        }

        private void launchWorksButton(object sender, RoutedEventArgs e)
        {

            abortBtn.IsEnabled = true;
            pauseBtn.IsEnabled = true;
            launchWorkList();


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
            for (int i = 0; i < goal; i++)
            {
                WrkElements we = weList[i - offset];
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

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {

            if (threadLifeManager.Pause())
            {
                pauseBtn.IsEnabled = false;
                resumeBtn.IsEnabled = true;
            }
        }

        private void AbortBtn_Click(object sender, RoutedEventArgs e)
        {
            threadLifeManager.Abort();
            resetButtons();
        }

        private void resetButtons()
        {
            foreach (Work w in bs.works)
            {
                if (Monitor.IsEntered(w.SaveType.pause))
                {
                    Monitor.Exit(w.SaveType.pause);
                }


            }
            pauseBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            abortBtn.IsEnabled = false;
        }

        private void ResumeBtnClick(object sender, RoutedEventArgs e)
        {

            if (threadLifeManager.Resume())
            {
                pauseBtn.IsEnabled = true;
                resumeBtn.IsEnabled = false;
            }


        }


    }
}

