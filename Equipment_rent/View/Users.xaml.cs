using System.Windows.Controls;
using Equipment_rent.ViewModel;

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

            AllUsers = UsersDataGrid;
        }
    }
}
