using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using CESI.BS.EasySave.DAL;
using CESI.Server.EasySave.Networking;
using CESI.Server.EasySave.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using static CESI.BS.EasySave.BS.ConfSaver.ConfSaver;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int terminatedThreads = 0;
        readonly AlertWindow alertWindow = new AlertWindow();
        readonly HighPriorityExtention highPriorityExtention = new HighPriorityExtention();
        readonly ProcessusChoosing processusChoosing = new ProcessusChoosing();
        readonly LanguageSelectionWindow languageSelectionWindow = new LanguageSelectionWindow();
        readonly AddWorkWindow addWorkWindow = new AddWorkWindow();
        public List<string> ForbProc { get; set; } = new List<string>();

        readonly List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        readonly List<WrkElements> weList = new List<WrkElements>();
        private readonly ResourceDictionary obj;

        public BSEasySave bs = new BSEasySave();
        public ThreadLifeManager threadLifeManager;
        readonly ModifyWorkWindow modifyWorkWindow;

        private readonly ServerSocket myServer;

        public MainWindow()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(processName).Length > 1)
            {
                Application.Current.Shutdown();
            }
            InitializeComponent();
            modifyWorkWindow = new ModifyWorkWindow();
            modifyWorkWindow.OkBtn.Click += ModifyOkBtn_Click;
            addWorkWindow.OkBtn.Click += OkBtn_Click;
            Closing += MainWindow_Closing;
            Show();
            listWorks = bs.confSaver.GetSavedWorks();
            AddExistingWorksToView();
            ChangeLanguage(languageSelectionWindow.GetLanguagePath());
            languageSelectionWindow.OkBtn.Click += LanguageOkBtn_Click;
            processusChoosing.OkBtn.Click += PcOkBtn;
            threadLifeManager = new ThreadLifeManager(bs, ForbProc);
            AddEventsWindows();
            threadLifeManager.StartObservingProcesses();
            PacketHandler.OnAbortSent += threadLifeManager.Abort;
            PacketHandler.OnResumeSent += threadLifeManager.Resume;
            PacketHandler.OnPauseSent += threadLifeManager.Pause;
            myServer = ServerSocket.Instance;
            myServer.StartConnection(1);
        }

        private void AddEventsWindows()
        {
            threadLifeManager.OnErrorRaised += AlertMsg;
        }



        private void PcOkBtn(object sender, RoutedEventArgs e)
        {
            processusChoosing.Hide();
            ForbProc.Clear();
            foreach (ComboBox cb in processusChoosing.ListCB.Items)
            {
                if (cb.SelectedIndex > -1)
                {
                    ForbProc.Add(cb.SelectedItem.ToString());
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
                alertWindow.ShowMessage("IsXorCheckedButKeyOrExtentionsNull", true);
                return;
            }
            if (modifyWorkWindow.WorkNameTB.Text.Equals(""))
            {
                modifyWorkWindow.WorkNameTB.Text = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            }

            if (Directory.Exists(modifyWorkWindow.WorkSourceTB.Text) && Directory.Exists(modifyWorkWindow.WorkTargetTB.Text))
            {
                modifyWorkWindow.Hide();
                WorkVar wv = new WorkVar
                {
                    name = modifyWorkWindow.WorkNameTB.Text,
                    source = modifyWorkWindow.WorkSourceTB.Text,
                    target = modifyWorkWindow.WorkTargetTB.Text,
                    typeSave = modifyWorkWindow.SaveTypeCB.SelectedIndex
                };
                int index = weList.IndexOf(modifyWorkWindow.we);
                if ((bool)modifyWorkWindow.CypherOptionsCHB.IsChecked)
                {
                    wv.key = modifyWorkWindow.KeyTB.Text;
                    //wv.extension = addWorkWindow.extention;
                    wv.cryptoExtensions = listExt;
                    wv.priorityExtensions = new List<string>
                    {
                        "null"
                    };


                }
                else
                {
                    wv.key = "null";
                    wv.cryptoExtensions = new List<string>
                    {
                        "null"
                    };
                    wv.priorityExtensions = new List<string>
                    {
                        "null"
                    };

                }
                weList[index].WV = wv;
                weList[index].InSvdList.UpdateWv(weList[index].WV);
                weList[index].InWrkList.UpdateWv(weList[index].WV);
                weList[index].Chiffrage = (bool)modifyWorkWindow.CypherOptionsCHB.IsChecked;
                bs.confSaver.ModifyEntireFile(bs.works[index].Name, weList[index].WV);
                bs.ModifyWork(bs.works[index], wv.name, wv.source, wv.target, SaveTypeMethods.GetSaveTypeFromInt(modifyWorkWindow.SaveTypeCB.SelectedIndex), wv.cryptoExtensions, wv.priorityExtensions, wv.key);// modification du travail




            }
            else
            {
                alertWindow.ShowMessage("DirectoryDoesNotExist", true);

            }



        }

        private void LanguageOkBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeLanguage(languageSelectionWindow.GetLanguagePath());


        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();//we will have to manage running work
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)addWorkWindow.isXor.IsChecked && (addWorkWindow.key.Length == 0 || addWorkWindow.extention.Count == 0))
            {
                alertWindow.ShowMessage("sXorCheckedButKeyOrExtentionsNull", true);
                return;

            }
            if (addWorkWindow.WorkNameTB.Text.Equals(""))
            {
                addWorkWindow.WorkNameTB.Text = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            }

            if (Directory.Exists(addWorkWindow.WorkSourceTB.Text) && Directory.Exists(addWorkWindow.WorkTargetTB.Text))
            {
                addWorkWindow.Hide();
                WorkVar wv = new WorkVar
                {
                    name = addWorkWindow.WorkNameTB.Text,
                    source = addWorkWindow.WorkSourceTB.Text,
                    target = addWorkWindow.WorkTargetTB.Text,
                    typeSave = addWorkWindow.SaveTypeCB.SelectedIndex
                };

                if ((bool)addWorkWindow.isXor.IsChecked)
                {
                    wv.key = addWorkWindow.key;
                    wv.cryptoExtensions = addWorkWindow.extention;


                }
                else
                {
                    wv.key = "null";
                    wv.cryptoExtensions = new List<string>
                    {
                        "null"
                    };

                }
                bs.AddWork(wv.name, wv.source, wv.target, ((ComboBoxItem)addWorkWindow.SaveTypeCB.SelectedItem).Name, wv.cryptoExtensions, new List<string>() /*fichiersPrivilégiés*/, wv.key);// ajout du travail
                WrkElements we = new WrkElements(wv, bs)
                {
                    Chiffrage = (bool)addWorkWindow.isXor.IsChecked
                };

                PrepareWrkElement(we);
                bs.confSaver.SaveWork(wv);


            }
            else
            {
                alertWindow.ShowMessage("DirectoryDoesNotExist", true);
            }
        }
        private void ToWorkList_Click(WrkElements we)
        {
            ToWorkList(we);

        }

        private void ToWorkList(WrkElements we)
        {
            SaveListLbl.Items.Remove(we.InSvdList);
            we.InWrkList.workProgressBar.Value = 0;
            WorkListLbl.Items.Add(we.InWrkList);
        }

        private void ToSaveList_Click(WrkElements we)
        {
            ToSaveList(we);

        }

        private void ToSaveList(WrkElements we)
        {
            WorkListLbl.Items.Remove(we.InWrkList);
            SaveListLbl.Items.Add(we.InSvdList);
        }

        private void AddExistingWorksToView()
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
            we.InSvdList.ToWorkList.Click += (sender, e) => ToWorkList_Click(we);
            we.InWrkList.ToSaveList.Click += (sender, e) => ToSaveList_Click(we);
            we.InWrkList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, weList[weList.IndexOf(we)]);
            we.InSvdList.MouseDoubleClick += (sender, e) => modifyWorkWindow.DoubleClickOnWorkElement(sender, e, weList[weList.IndexOf(we)]);
            SaveListLbl.Items.Add(we.InSvdList);

            bs.AddWork(we.WV.name, we.WV.source, we.WV.target, SaveTypeMethods.GetSaveTypeFromInt(we.WV.typeSave), we.WV.cryptoExtensions, new List<string>() /*priorityExtention*/, we.WV.key);
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
            alertWindow.Resources.MergedDictionaries.Remove(obj);
            alertWindow.Resources.MergedDictionaries.Add(objNewLanguageDictionary);
        }

        public void LaunchWorkList()
        {
            threadLifeManager.ClearThread();

            EnableButtonsAccess(false);

            foreach (WrkElements we in weList)
            {

                int count = weList.Count;
                if (WorkListLbl.Items.Contains(we.InWrkList))
                {
                    Thread saveThread = new Thread(launchWork =>
                    {
                        using (ThreadMutex.Canceller.Token.Register(Thread.CurrentThread.Abort)) { }

                        try
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                we.InWrkList.workProgressBar.Value = 0;
                            });
                            threadLifeManager.SubscribeToSaves(bs.works[weList.IndexOf(we)]);
                            bs.works[weList.IndexOf(we)].SaveType.Subscribe(we.InWrkList);
                            bs.works[weList.IndexOf(we)].SaveType._priorityExtension = highPriorityExtention.GetExtentions();
                            bs.works[weList.IndexOf(we)].SaveType.Subscribe(myServer);
                            bs.works[weList.IndexOf(we)].Perform();
                            bs.works[weList.IndexOf(we)].SaveType.Unsubscribe(we.InWrkList);//can be deleted
                            terminatedThreads++;
                            threadLifeManager.UnsubscribeToSaves(bs.works[weList.IndexOf(we)]);
                        }
                        catch (ThreadInterruptedException)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {

                                we.InWrkList.workProgressBar.Value = 0;
                                terminatedThreads++;
                            });
                        }
                        if (terminatedThreads == threadLifeManager.Count())
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                ResetButtons();
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


                we.InSvdList.ToWorkList.IsEnabled = access;
                if (WorkListLbl.Items.Contains(we.InWrkList))
                {
                    we.InWrkList.IsEnabled = access;
                }


            }
        }

        private void LaunchWorksButton(object sender, RoutedEventArgs e)
        {

            abortBtn.IsEnabled = true;
            pauseBtn.IsEnabled = true;
            LaunchWorkList();


        }




        private void AddWorkBtn_Click(object sender, RoutedEventArgs e)
        {
            addWorkWindow.Show();
        }

        private void LanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            languageSelectionWindow.Show();
        }

        private void MultipleAddBtn(object sender, RoutedEventArgs e)
        {
            foreach (WrkElements we in weList)
            {
                if ((bool)we.InSvdList.checkBox.IsChecked && SaveListLbl.Items.Contains(we.InSvdList))
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
                if ((bool)we.InSvdList.checkBox.IsChecked && SaveListLbl.Items.Contains(we.InSvdList))
                {
                    bs.DeleteWork(weList.IndexOf(we));
                    SaveListLbl.Items.Remove(we.InSvdList);
                    bs.confSaver.DeleteFile(we.WV.name);
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
            ResetButtons();
        }

        private void ResetButtons()
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
        public void AlertMsg(string message)
        {
            alertWindow.ShowMessage(message, true);
        }

        private void HPriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            highPriorityExtention.Show();
        }
    }
}

