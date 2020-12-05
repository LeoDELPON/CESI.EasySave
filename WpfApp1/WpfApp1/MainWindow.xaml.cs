using System;
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

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        LanguageSelectionWindow languageSelectionWindow = new LanguageSelectionWindow();
        AddWorkWindow addWorkWindow = new AddWorkWindow();
        ConfSaver confSaver = new ConfSaver();
        List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        List<WrkElements> weList = new List<WrkElements>(); 
        private ResourceDictionary obj;
        BSEasySave bs = new BSEasySave(); 
        

        public MainWindow()        {
         
            InitializeComponent();
            addWorkWindow.OkBtn.Click += OkBtn_Click;
            Closing += MainWindow_Closing;
            this.Show();
            listWorks = confSaver.GetSavedWorks();
            addExistingWorksToView();
            ChangeLangage(languageSelectionWindow.getLanguagePath());
            languageSelectionWindow.OkBtn.Click += LanguageOkBtn_Click;
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
                confSaver.SaveWork(wv);

              
            }
            else
            {
                //error Message
            }
        }
        private void toWorkList_Click(object sender, RoutedEventArgs e, WrkElements we)
        {
            ToWorkList(we);

        }

        private void ToWorkList(WrkElements we)
        {
            SaveListLbl.Items.Remove(we.inSvdList);
            WorkListLbl.Items.Add(we.inWrkList);
        }

        private void toSaveList_Click(object sender, RoutedEventArgs e, WrkElements we)
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
        
            we.inSvdList.toWorkList.Click += (sender, e) => toWorkList_Click(sender, e, we);
            we.inWrkList.ToSaveList.Click += (sender, e) => toSaveList_Click(sender, e, we);
            SaveListLbl.Items.Add(we.inSvdList);
            weList.Add(we);
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
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
         

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
                    confSaver.DeleteFile(we.wv.name);
                    weList.Remove(we);
                }
            }
        }
    }
}

