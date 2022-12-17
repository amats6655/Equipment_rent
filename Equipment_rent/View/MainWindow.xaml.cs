using Equipment_rent.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (NavigationVM.AuthUser.Role.Role.Equals("Эксперт"))
            {
                btn_staff.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HideMenu_Button_Click(object sender, RoutedEventArgs e)
        {
            if (border_menu.Visibility == Visibility.Visible)
            {
                border_menu.Visibility = Visibility.Collapsed;
            }
            else border_menu.Visibility = Visibility.Visible;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
