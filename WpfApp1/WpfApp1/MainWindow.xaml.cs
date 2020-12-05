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
        ConfSaver confSaver = new ConfSaver();
        List<ConfSaver.WorkVar> listWorks = new List<ConfSaver.WorkVar>();
        ResourceManager rm = new ResourceManager("fr-FR", Assembly.GetExecutingAssembly());
        private ResourceDictionary obj;
        BSEasySave bs = new BSEasySave(); 
        

        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            listWorks = confSaver.GetSavedWorks();
            addExistingWorksToView();
        }

        private void addExistingWorksToView()
        {
           foreach(ConfSaver.WorkVar work in listWorks)
            {
                WrkElements we = new WrkElements(work, listWorks.IndexOf(work), bs);
                we.inSvdList.toWorkList.Click += (sender, e) =>//exporter ces 
                {
                    SaveListLbl.Items.Remove(we.inSvdList);
                    WorkListLbl.Items.Add(we.inWrkList);
                };
                we.inWrkList.ToSaveList.Click += (sender, e) =>
                {
                    WorkListLbl.Items.Remove(we.inWrkList);
                    SaveListLbl.Items.Add(we.inSvdList);

                    
                };
               

                SaveListLbl.Items.Add(we.inSvdList); 

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ChangeLangage(new Uri(@"\Language\en-US.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          
            ChangeLangage(new Uri(@"\Language\fr-FR.xaml", UriKind.Relative));
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
       

    }
}

