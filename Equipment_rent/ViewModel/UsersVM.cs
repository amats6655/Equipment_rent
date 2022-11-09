﻿using System;
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
                    }
                });
            }
        }
        #endregion


        private void UpdateAllUsersView()
        {
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

        //string connectionString;
        //SqlDataAdapter adapter;
        //public string getUsers;


        //public UsersVM()
        //{
        //    connectionString = ConfigurationManager.ConnectionStrings["equipmentDbConnect"].ConnectionString;
        //    getUsers = "SELECT * " +
        //               "FROM users " +
        //               "ORDER BY id " +
        //               "OFFSET 0 ROWS " +
        //               "FETCH NEXT 999 ROWS ONLY;";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        adapter = new SqlDataAdapter(getUsers, connection);
        //        DataTable usersDataTable = new DataTable("users");
        //        adapter.Fill(usersDataTable);
        //        usersDataTable.Columns.Add("BgColor", typeof(Brush));
        //        usersDataTable.Columns.Add("Character", typeof(string));

        //        foreach (DataRowView dt in usersDataTable.DefaultView)
        //        {
        //            string name = dt.Row[1].ToString().ToUpper();
        //            char character = name[0];
        //            Brush BgColor = GetBrush.getBrush(character);
        //            dt.Row[5] = character;
        //            dt.Row[4] = BgColor;
        //        }
        //        UsersDataGrid.ItemsSource = usersDataTable.DefaultView;
        //    }
        //}
    }
}
