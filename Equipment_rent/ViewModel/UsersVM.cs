using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel
{
    class UsersVM : INotifyPropertyChanged
    {
        private List<User> allUsers = DataWorker.GetAllUsers();

        #region Add Brush and Character for user
        public List<User> AllUsers
        {
            get 
            { 
                List<User> users = new List<User>();
                foreach (User user in allUsers)
                {
                    char Character = user.Name[0];
                    Brush BgColor = GetBrush.getBrush(Character);
                    user.BgColor = BgColor.ToString();
                    user.Character = Character;
                    users.Add(user);
                }
                return users;
            }
            set 
            { 
                allUsers = value;
                NotifyPropertyChaged("AllUsers");
            }
        }
        #endregion

        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string UserPhone { get; set; }
        #region Commands to add
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";
                    if (FirstName == null || FirstName.Replace(" ", "").Length == 0)
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
                        resultStr = DataWorker.CreateUser(FirstName+" "+LastName, UserPhone);
                        UpdateAllUsersView();
                        window.Close();
                    }
                });
            }
        }
        #endregion


        private void UpdateAllUsersView()
        {
            allUsers = DataWorker.GetAllUsers();
            Users.AllUsers.ItemsSource = null;
            Users.AllUsers.Items.Clear();
            Users.AllUsers.ItemsSource = AllUsers;
            Users.AllUsers.Items.Refresh();
        }



        #region Open Add new user wingow
        private RelayCommand addUser;
        public RelayCommand AddUser
        {
            get
            {
                return addUser ?? new RelayCommand(obj =>
                {
                    Add_Button_Click();
                }
                    );
            }
        }

        private void Add_Button_Click()
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
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
