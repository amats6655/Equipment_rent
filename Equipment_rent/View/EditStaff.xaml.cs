using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для EditStaff.xaml
    /// </summary>
    public partial class EditStaff : Window
    {
        public EditStaff(Auth_user staffToEdit)
        {
            InitializeComponent();
            DataContext = new EditStaffVM();
            EditStaffVM.SelectedStaff = staffToEdit;
            EditStaffVM.StaffUsername = staffToEdit.Username;
            EditStaffVM.StaffFirstname = staffToEdit.FirstName;
            EditStaffVM.StaffLastname = staffToEdit.LastName;
            EditStaffVM.StaffEmail = staffToEdit.Email;
            EditStaffVM.StaffRole_Id = staffToEdit.Role_Id;
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
