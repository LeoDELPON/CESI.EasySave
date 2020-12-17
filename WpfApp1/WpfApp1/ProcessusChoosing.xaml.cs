using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour ProcessusChoosing.xaml
    /// </summary>
    public partial class ProcessusChoosing : Window
    {
        public Process[] listeProcess { get; set; }
        public ProcessusChoosing()
        {
            InitializeComponent();
            Closing += ProcessusChoosing_Closing;
            listeProcess = Process.GetProcesses();
            AddNewCB();


        }
        public


        void ProcessusChoosing_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void AddNewCB()
        {
            ComboBox cb = new ComboBox();
            cb.Width = 165;
            cb.IsEditable = true;
            ListCB.Items.Add(cb);
            foreach (Process process in listeProcess)
            {
                cb.Items.Add(process.ProcessName);
            }
        }
        public void RemoveCB()
        {
            if (ListCB.Items.Count > 0)
            {
                ListCB.Items.Remove(ListCB.Items.GetItemAt(ListCB.Items.Count - 1));
            }
        }


        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewCB();
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveCB();
        }
    }
}
