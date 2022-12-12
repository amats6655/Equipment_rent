using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment_rent.ViewModel
{

    internal class ProfileVM:INotifyPropertyChanged
    {
        public static Guid User_id = NavigationVM.UserID;

        private Auth_user _user = DataWorker.GetAuthUserById(User_id);
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


        private Auth_role _role = User.Role;
        private string _username = User.Username;
        private string _firstname = User.FirstName;
        private string _lastname = User.LastName;
        private string _email = User.Email;

        public Auth_role Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                NotifyPropertyChaged(nameof(Role));
            }
        }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                NotifyPropertyChaged(nameof(Username));
            }
        }
        public string Firstname
        {
            get { return _firstname;}
            set { _firstname = value; NotifyPropertyChaged(nameof(Firstname)); }
        }
        public string Lastname
        {
            get { return _lastname;}
            set { _lastname = value; NotifyPropertyChaged(nameof(Lastname)); }
        }
        public string Email
        {
            get { return _email;}
            set { _email = value; NotifyPropertyChaged(nameof(Email)); }
        }
        private string _lastpass;
        private string _newpass;
        private string _confurmpass;
        public string LastPass
        {
            get { return _lastpass; }
            set { LastPass = value; NotifyPropertyChaged(nameof(LastPass)); }
        }
        public string NewPass 
        { 
            get { return _newpass; }
            set { NewPass = value; NotifyPropertyChaged(nameof(NewPass)); } 
        }
        public string ConfurmPass
        {
            get { return _confurmpass; }
            set { ConfurmPass = value; NotifyPropertyChaged(nameof(ConfurmPass)); } 
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
            DataWorker.EditAuthUser(User, User.Id, Firstname, Lastname, Email, Role.Id);
        }
        #endregion

        #region Change Password
        private RelayCommand _chagePassword;
        public RelayCommand ChangePassword
        {
            get
            {
                return _chagePassword ?? new RelayCommand(obj =>
                {
                    Change_Password_Click(Username, LastPass, NewPass, ConfurmPass);
                });
            }
        }
        private void Change_Password_Click(string username, string pass, string new_pass, string confurm_pass )
        {
            if(new_pass.Equals(confurm_pass))
            {
                AuthClient.ChangePassword(username, pass, new_pass);
            }
        }
        #endregion


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
