using System.Windows.Controls;
using Equipment_rent.ViewModel;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Equipments.xaml
    /// </summary>
    public partial class Equipments : UserControl
    {
        public static DataGrid AllEquipments;
        public Equipments()
        {
            InitializeComponent();
            DataContext = new EquipmentsVM();
            AllEquipments = EquipmentsDataGrid;
        }
    }
}
