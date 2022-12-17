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
            AllEquipments = EquipmentsDataGrid;
            if(NavigationVM.AuthUser.Role.Role != "Эксперт")
            {
                dgColumn_delete.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
