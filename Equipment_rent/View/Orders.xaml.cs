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

        private void txtSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OrdersVM.Filter = txtSearch.Text;
            OrdersVM.Search(sender, e);
        }
    }
}
