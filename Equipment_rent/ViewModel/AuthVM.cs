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

        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public static string Message = "";
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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
            if (string.IsNullOrEmpty(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }
        private async void ExecuteLoginCommand(object obj)
        {

            AuthClient.AuthClient_Send(Username, Password);
            // считываем данные авторизации из файла
            string path = "./Settings/auth.dat";   // путь к файлу

            // запись в файл
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                var strWithoutSpaces = Username.Replace(" ", "");
                byte[] buffer = Encoding.UTF8.GetBytes(strWithoutSpaces);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
                fstream.Close();
            }


            ErrorMessage = Message;

        }
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

    }
}

