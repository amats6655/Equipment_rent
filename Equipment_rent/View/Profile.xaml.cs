﻿using Equipment_rent.Model;
using Equipment_rent.ViewModel;
using System.Windows.Controls;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        private readonly Auth_user User = NavigationVM.AuthUser;
        public Profile()
        {
            InitializeComponent();
            DataContext = new ProfileVM();
            ProfileVM.Username = User.Username;
            ProfileVM.Firstname = User.FirstName;
            ProfileVM.Lastname = User.LastName;
            ProfileVM.Email = User.Email;
            ProfileVM.Role = User.Role;
            cb_role.Text = User.Role.Role;
            cb_role.SelectedValue = User.Role;
            cb_role.SelectedValuePath = User.Role.Role;


        }
    }
}
