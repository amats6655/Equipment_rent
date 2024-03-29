﻿using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Equipment_rent.ViewModel
{

    internal class ProfileVM : INotifyPropertyChanged
    {
        private Auth_user _user = NavigationVM.AuthUser;
        public Auth_user User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyPropertyChaged(nameof(User));
            }
        }

        private List<Auth_role> all_Roles = DataWorker.GetAllAuthRoles();
        public List<Auth_role> AllRoles
        {
            get { return all_Roles; }
            set
            {
                all_Roles = value;
                NotifyPropertyChaged(nameof(AllRoles));
            }
        }

        private static string _username;
        private static string _firstname;
        private static string _lastname;
        private static string _email;
        private static Auth_role _role;

        public static string Username { get { return _username; } set { _username = value; } }
        public static string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
            }
        }
        public static string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
            }
        }
        public static string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }
        public static Auth_role Role
        {
            get { return _role; }
            set
            {
                _role = value;
            }
        }



        private string _lastpass;
        private string _newpass;
        private string _confurmpass;
        public string LastPass
        {
            get { return _lastpass; }
            set { _lastpass = value; NotifyPropertyChaged(nameof(LastPass)); }
        }
        public string NewPass
        {
            get { return _newpass; }
            set { _newpass = value; NotifyPropertyChaged(nameof(NewPass)); }
        }
        public string ConfurmPass
        {
            get { return _confurmpass; }
            set { _confurmpass = value; NotifyPropertyChaged(nameof(ConfurmPass)); }
        }


        #region Edit user
        private RelayCommand _editUser;
        public RelayCommand EditUser
        {
            get
            {
                return _editUser ?? new RelayCommand(obj =>
                {
                    Edit_Button_Click();
                });
            }
        }
        private void Edit_Button_Click()
        {
            DataWorker.EditAuthUser(User, User.Id, User.FirstName, User.LastName, User.Email, User.Role.Id);
        }
        #endregion

        //#region Change Password
        //private RelayCommand _chagePassword;
        //public RelayCommand ChangePassword
        //{
        //    get
        //    {
        //        return _chagePassword ?? new RelayCommand(obj =>
        //        {
        //            Change_Password_Click(User.Username, LastPass, NewPass, ConfurmPass);
        //        });
        //    }
        //}
        //private void Change_Password_Click(string username, string pass, string new_pass, string confurm_pass)
        //{
        //    if (pass != null)
        //    {
        //        if (new_pass.Equals(confurm_pass))
        //        {
        //            AuthClient.ChangePassword(username, pass, new_pass);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Пароли не совпадают");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Введи старый пароль");
        //    }

        //}
        //#endregion

        private string _errorMessage;
        public static string Message = "";
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                NotifyPropertyChaged(nameof(ErrorMessage));
            }
        }
        public ICommand ChangePasswordCommand { get; }
        public ProfileVM()
        {
            ChangePasswordCommand = new ViewModelCommand(ExecutedLoginCommand, CanExecuteLoginCommand);
        }


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



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChaged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
