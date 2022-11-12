using Equipment_rent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public EditOrder()
        {
            InitializeComponent();
            DataContext = new EditOrderVM();
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
