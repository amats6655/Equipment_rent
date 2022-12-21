using Equipment_rent.ViewModel;
using System.Windows.Controls;
using System.Windows;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public static DataGrid AllUsers;
        public Users()
        {
            InitializeComponent();
            if (NavigationVM.AuthUser.Role.Role != ("Эксперт"))
            {
                dgColumn_delete.Visibility = Visibility.Collapsed;
                if (NavigationVM.AuthUser.Role.Role.Equals("test"))
                {
                    dgColumn_edit.Visibility = Visibility.Collapsed;
                }
            }

            AllUsers = UsersDataGrid;
        }
    }
}
