using Equipment_rent.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        public static DataGrid AllOrders;
        public Orders()
        {
            InitializeComponent(); // Второй раз пошли получать данные
            AllOrders = OrdersDataGrid;
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
                var filtred = OrdersVM.AllOrders.Where(u => u.OrdersUser.Name.ToLower().Contains(txtSearch.Text.ToLower()) || u.OrdersUser.Phone.Contains(txtSearch.Text) || u.OrdersEquipment.Model.ToLower().Contains(txtSearch.Text.ToLower()) || u.OrdersEquipment.EquipType.Name.ToLower().Contains(txtSearch.Text.ToLower()));
                OrdersDataGrid.ItemsSource = filtred;
            }
            else OrdersDataGrid.ItemsSource = OrdersVM.FirstOrders;
        }
    }
}
