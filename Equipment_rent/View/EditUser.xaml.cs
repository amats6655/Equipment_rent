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
using Equipment_rent.Model;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser(User userToEdit)
        {
            InitializeComponent();
            string[] username = userToEdit.Name.Split();
            DataContext = new EditUserVM();
            EditUserVM.SelectedUser = userToEdit;
            EditUserVM.UserFirstName = username[0];
            EditUserVM.UserLastName = username[1];
            EditUserVM.UserPhone = userToEdit.Phone;
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
