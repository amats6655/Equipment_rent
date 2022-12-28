using Equipment_rent.ViewModel;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

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

        private void search(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtSearch.Text != "" && txtSearch.Text != " ")
            {
                var filtred = UsersVM.AllUsers.Where(u => u.Name.ToLower().Contains(txtSearch.Text.ToLower()) || u.Phone.Contains(txtSearch.Text));
                UsersDataGrid.ItemsSource = filtred;
            }
            else UsersDataGrid.ItemsSource = UsersVM.FirstUsers;
        }
    }
}
