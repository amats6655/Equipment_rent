using Equipment_rent.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public static ComboBox Models;
        public AddOrder()
        {
            InitializeComponent();
            DataContext = new AddOrderVM();
            cb_models.SelectedIndex = 0;
            cb_users.SelectedIndex = 0;
            Models = cb_models;
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

        //public void cb_models_source(List<Model.Equipment> sender, RoutedEventArgs e)
        //{
        //    cb_models.ItemsSource = sender;

        //}

    }
}
