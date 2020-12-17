using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.ConfSaver;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour wrkElement.xaml
    /// </summary>
    public partial class WrkElementInSavedList : UserControl
    {
        readonly BSEasySave bs;




        public WrkElementInSavedList(ConfSaver.WorkVar workVar, BSEasySave BS)
        {
            InitializeComponent();
            bs = BS;
            UpdateWv(workVar);

        }


        public void UpdateWv(ConfSaver.WorkVar workVar)
        {
            workNameLbl.Content = workVar.name;
            workSourceLbl.Content = workVar.source;
            workTargetLbl.Content = workVar.target;
            workTypeLbl.SetResourceReference(Label.ContentProperty, bs.TypeSave[workVar.typeSave].IdTypeSave);
        }
    }
}
