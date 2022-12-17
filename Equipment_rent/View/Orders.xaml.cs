using Equipment_rent.ViewModel;
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
            }
        }
    }
}
