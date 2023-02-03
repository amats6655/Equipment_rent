using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

internal class ProfileVM : INotifyPropertyChanged
{
    public static string Message = "";
    private string _confurmpass;
    private string _errorMessage;
    private string _lastpass;
    private string _newpass;
    private Auth_user _user = NavigationVM.AuthUser;

    private List<Auth_role> all_Roles = DataWorker.GetAllAuthRoles();

    public ProfileVM()
    {
        ChangePasswordCommand = new ViewModelCommand(ExecutedLoginCommand, CanExecuteLoginCommand);
    }

    public Auth_user User
    {
        get => _user;
        set
        {
            _user = value;
            NotifyPropertyChaged(nameof(User));
        }
    }

    public List<Auth_role> AllRoles
    {
        get => all_Roles;
        set
        {
            all_Roles = value;
            NotifyPropertyChaged(nameof(AllRoles));
        }
    }

    public static string Username { get; set; }

    public static string Firstname { get; set; }

    public static string Lastname { get; set; }

    public static string Email { get; set; }

    public static Auth_role Role { get; set; }

    public string LastPass
    {
        get => _lastpass;
        set
        {
            _lastpass = value;
            NotifyPropertyChaged(nameof(LastPass));
        }
    }

    public string NewPass
    {
        get => _newpass;
        set
        {
            _newpass = value;
            NotifyPropertyChaged(nameof(NewPass));
        }
    }

    public string ConfurmPass
    {
        get => _confurmpass;
        set
        {
            _confurmpass = value;
            NotifyPropertyChaged(nameof(ConfurmPass));
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            NotifyPropertyChaged(nameof(ErrorMessage));
        }
    }

    public ICommand ChangePasswordCommand { get; }


    public event PropertyChangedEventHandler PropertyChanged;


    private bool CanExecuteLoginCommand(object obj)
    {
        bool validData;
        if (string.IsNullOrEmpty(LastPass) || LastPass.Length < 3 || string.IsNullOrEmpty(NewPass) ||
            NewPass.Length < 3 || string.IsNullOrEmpty(ConfurmPass) || ConfurmPass.Length < 3)
            validData = false;
        else
            validData = true;
        return validData;
    }

    private async void ExecutedLoginCommand(object obj)
    {
        if (NewPass == ConfurmPass)
        {
            AuthClient.ChangePassword(User.Username, LastPass, NewPass);
            ErrorMessage = Message;
        }
        else
        {
            ErrorMessage = "Пароли не совпадают";
        }
    }

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}