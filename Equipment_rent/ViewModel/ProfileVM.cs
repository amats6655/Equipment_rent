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
        
        public string LastPass { set { LastPass = value; } }
        public string NewPass { set { NewPass = value; } }
        public string ConfurmPass { set { ConfurmPass = value; } }

        



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
