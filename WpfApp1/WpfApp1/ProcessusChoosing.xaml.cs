using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour ProcessusChoosing.xaml
    /// </summary>
    public partial class ProcessusChoosing : Window
    {
        Process[] listeProcess;
        public ProcessusChoosing(Process[] listeProcess)
        {
            InitializeComponent();
            Closing += ProcessusChoosing_Closing;
            this.listeProcess = listeProcess;
            AddNewCB();   
          
            
        }

        private void ProcessusChoosing_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
