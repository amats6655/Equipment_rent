using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System.Windows;

namespace Equipment_rent.ViewModel
{
    internal class EditUserVM : UsersVM
    {
        public static string UserFirstName { get; set; }
        public static string? UserLastName { get; set; }
        public static string UserPhone { get; set; }
        public static User SelectedUser { get; set; }

        #region Commands to Edit
        private RelayCommand editUser;
        public RelayCommand EditUser
        {
            get
            {
                return editUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    if (UserFirstName == null || UserFirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_lastname");
                        SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                    }
                    else if (UserPhone == null)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_phone");
                    }
                    else
                    {
                        DataWorker.EditUser(SelectedUser, UserFirstName + " " + UserLastName, UserPhone, SelectedUser.Debt);
                        UpdateAllUsersView();
                        window.Close();
                    }
                });
            }
        }
        #endregion
    }
}