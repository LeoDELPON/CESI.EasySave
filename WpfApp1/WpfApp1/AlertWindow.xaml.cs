using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {

        public AlertWindow()
        {
            InitializeComponent();
            Closing += AlertWindow_Closing;

        }

        private void AlertWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void ShowMessage(string msg, bool isADynamicRessource)
        {
            if (isADynamicRessource)
            {
                Dispatcher.Invoke(() =>
                {
                    Text.SetResourceReference(Label.ContentProperty, msg);
                });


            }
            else
            {
                Text.Content = msg;
            }
            Dispatcher.Invoke(() =>
            {
                Show();
            });

        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
