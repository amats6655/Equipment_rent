using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public EditOrder(Order orderToEdit)
        {
            InitializeComponent();
            DataContext = new EditOrderVM();
            EditOrderVM.SelectedOrder = orderToEdit;
            EditOrderVM.Equipment = orderToEdit.OrdersEquipment;
            EditOrderVM.Amount = orderToEdit.Amount;
            cb_models.SelectedValue = orderToEdit.OrdersEquipment;
            cb_models.SelectedValuePath = orderToEdit.OrdersEquipment.Model;
            cb_models.Text = orderToEdit.OrdersEquipment.Model;
            

            cb_users.SelectedValue = orderToEdit.OrdersUser;
            cb_users.SelectedValuePath = orderToEdit.OrdersUser.Name;
            cb_users.Text = orderToEdit.OrdersUser.Name;

            EditOrderVM.User = orderToEdit.OrdersUser;
            EditOrderVM.DateIssue = orderToEdit.DateIssue;
            EditOrderVM.DateReturn = orderToEdit.DateReturn;
            EditOrderVM.IsReturned = orderToEdit.IsReturned;

        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void cb_newUser_click(object sender, RoutedEventArgs e)
        {
            if (cb_newUser.IsChecked == true)
            {
                cb_users.Visibility = Visibility.Collapsed;
                tb_firstname.Visibility = Visibility.Visible;
                tb_lastname.Visibility = Visibility.Visible;
                tb_phone.Visibility = Visibility.Visible;
            }
            else
            {
                cb_users.Visibility = Visibility.Visible;
                tb_firstname.Visibility = Visibility.Collapsed;
                tb_lastname.Visibility = Visibility.Collapsed;
                tb_phone.Visibility = Visibility.Collapsed;
            }
        }
    }
}
