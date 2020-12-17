using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour HighPriorityExtention.xaml
    /// </summary>
    public partial class HighPriorityExtention : Window
    {
        public List<TextBox> listExtensionPriority = new List<TextBox>();

        public HighPriorityExtention()
        {
            InitializeComponent();
            Closing += ProcessusChoosing_Closing;
            AddNewTB();
        }



        void ProcessusChoosing_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void AddNewTB()
        {
            TextBox tb = new TextBox
            {
                Width = 165
            };
            ListTB.Items.Add(tb);
            listExtensionPriority.Add(tb);
        }
        public void RemoveTB()
        {
            if (ListTB.Items.Count > 0)
            {
                ListTB.Items.Remove(ListTB.Items.GetItemAt(ListTB.Items.Count - 1));
                listExtensionPriority.Remove((TextBox)ListTB.Items.GetItemAt(ListTB.Items.Count - 1));
            }
        }
        public List<string> GetExtentions()
        {
            List<string> listString = new List<string>();
            Dispatcher.Invoke(() =>
            {
                foreach (TextBox tb in listExtensionPriority)
                {
                    if (tb.Text.Length > 0)
                    {
                        listString.Add(tb.Text);
                    }
                }
            });
            return listString;
        }


        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewTB();
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveTB();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
