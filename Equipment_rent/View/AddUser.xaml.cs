using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            DataContext = new AddUserVM();
            if (NavigationVM.AuthUser.Role.Role.Equals("test"))
            {
                btn_add.IsEnabled = false;
            }
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }

}
