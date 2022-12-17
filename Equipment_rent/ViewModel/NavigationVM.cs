﻿using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System.Windows.Input;

namespace Equipment_rent.ViewModel
{
    internal class NavigationVM : Utilites.ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand EquipmentsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand StaffCommand { get; set; }
        public ICommand ProfileCommand { get; set; }

        //!! 1 раз создаем объект
        private void Home(object obj) => CurrentView = new HomeVM();
        private void Users(object obj) => CurrentView = new UsersVM();
        private void Equipments(object obj) => CurrentView = new EquipmentsVM();
        private void Orders(object obj) => CurrentView = new OrdersVM();
        private void Staff(object obj) => CurrentView = new StaffVM();
        private void Profile(object obj) => CurrentView = new ProfileVM();
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            UsersCommand = new RelayCommand(Users);
            EquipmentsCommand = new RelayCommand(Equipments);
            OrdersCommand = new RelayCommand(Orders);
            StaffCommand = new RelayCommand(Staff);
            ProfileCommand = new RelayCommand(Profile);

            //startup Page
            CurrentView = new HomeVM();
        }

        private static Auth_user auth_User;
        public static Auth_user AuthUser
        {
            get
            {
                return auth_User;
            }
            set
            {
                auth_User = value;
            }
        }
    }
}
