using Equipment_rent.Utilites;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace Equipment_rent.ViewModel
{

    public class AuthVM : ViewModelBase
    {

        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public static string Message = "";

        public string Auth_username
        {
            get => Properties.Settings.Default.auth_username;
            set
            {
                Properties.Settings.Default.auth_username = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(Auth_username));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(_password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
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


        public AuthVM()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswodCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }




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
}

