using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System.Windows;
using System.Windows.Input;

namespace Equipment_rent.ViewModel
{
    internal class AddUserVM : UsersVM
    {
        private string _userFirstName = "";
        private string _userLastName = "";
        private string _userPhone;
        public string UserFirstName { get => _userFirstName; set { _userFirstName = value; } }
        public string UserLastName { get => _userLastName; set { _userLastName = value; } }
        public string UserPhone { get => _userPhone; set { _userPhone = value; } }


        public ICommand newUserCommand { get; }
        public AddUserVM()
        {
            newUserCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private bool CanExecuteAddCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrEmpty(UserFirstName.Replace(" ", "")) || string.IsNullOrEmpty(UserLastName.Replace(" ", "")) || string.IsNullOrEmpty(UserPhone))
                validData = false;
            else
                validData = true;
            return validData;
        }
        private void ExecuteAddCommand(object obj)
        {
            AddNewUser.Execute(obj);
        }

        #region Commands to add
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
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
                        DataWorker.CreateUser(UserLastName + " " + UserFirstName, UserPhone, false);
                        UpdateAllUsersView();
                        window.Close();
                    }
                });
            }
        }
        #endregion
    }
}