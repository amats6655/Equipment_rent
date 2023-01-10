using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

internal class AddUserVM : UsersVM
{
    public AddUserVM()
    {
        newUserCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
    }

    public string UserFirstName { get; set; } = "";

    public string UserLastName { get; set; } = "";

    public string UserPhone { get; set; }


    public ICommand newUserCommand { get; }

    private bool CanExecuteAddCommand(object obj)
    {
        bool validData;
        if (string.IsNullOrEmpty(UserFirstName.Replace(" ", "")) ||
            string.IsNullOrEmpty(UserLastName.Replace(" ", "")) || string.IsNullOrEmpty(UserPhone))
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
                var window = obj as Window;

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