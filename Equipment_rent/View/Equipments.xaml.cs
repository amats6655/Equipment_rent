using Equipment_rent.ViewModel;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

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
            if (NavigationVM.AuthUser.Role.Role != "Эксперт")
            {
                dgColumn_delete.Visibility = Visibility.Collapsed;
                if (NavigationVM.AuthUser.Role.Role.Equals("test"))
                {
                    dgColumn_edit.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void search(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtSearch.Text != "" && txtSearch.Text != " ")
            {
                var text = txtSearch.Text.ToLower();
                var filtred = EquipmentsVM.AllEquipments.Where(u => u.Model.ToLower().Contains(text) || u.EquipType.Name.ToLower().Contains(text));
                EquipmentsDataGrid.ItemsSource = filtred;
            }
            else EquipmentsDataGrid.ItemsSource = EquipmentsVM.FirstEquipments;
        }
    }
}
