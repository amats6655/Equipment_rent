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




        public Auth_role Role
        {
            get
            {
                return User.Role;
            }
            set
            {
                Role = value;
            }
        }
        public string Username
        {
            get
            {
                return User.Username;
            }
            set
            {
                User.Username = value;
            }
        }
        public string Firstname
        {
            get { return User.FirstName;}
            set { User.FirstName = value;}
        }
        public string Lastname
        {
            get { return User.LastName;}
            set { User.LastName = value;}
        }
        public string Email
        {
            get { return User.Email;}
            set { User.Email = value;}
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
