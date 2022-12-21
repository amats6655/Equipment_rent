using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        public AddStaff()
        {
            InitializeComponent();
            DataContext = new AddStaffVM();
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
