using System;
using System.Windows.Input;
using Equipment_rent.Properties;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

public class AuthVM : ViewModelBase
{
    public static string Message = "";
    private string _errorMessage;
    private bool _isViewVisible = true;

    private string _password;


    public AuthVM()
    {
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RecoverPasswodCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
    }

    public string Auth_username
    {
        get => Settings.Default.auth_username;
        set
        {
            Settings.Default.auth_username = value;
            Settings.Default.Save();
            OnPropertyChanged(nameof(Auth_username));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(_password));
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public bool IsViewVisible
    {
        get => _isViewVisible;
        set
        {
            _isViewVisible = value;
            OnPropertyChanged(nameof(IsViewVisible));
        }
    }


    public ICommand LoginCommand { get; }
    public ICommand RecoverPasswodCommand { get; }
    public ICommand ShowPasswordCommand { get; }
    public ICommand RememberPasswordCommand { get; }


    private bool CanExecuteLoginCommand(object obj)
    {
        bool validData;
        if (string.IsNullOrEmpty(Auth_username) || Auth_username.Length < 3 || Password == null || Password.Length < 3)
            validData = false;
        else
            validData = true;
        return validData;
    }

    private async void ExecuteLoginCommand(object obj)
    {
        AuthClient.AuthClient_Send(Auth_username, Password);

        ErrorMessage = Message;
    }

    private void ExecuteRecoverPassCommand(string username, string email)
    {
        throw new NotImplementedException();
    }
}