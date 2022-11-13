using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel
{
    internal class EditOrderVM : OrdersVM
    {
        #region Вывод списка пользователей и моделей
        private List<User> allUsers = DataWorker.GetAllUsers();
        public List<User> AllUsers
        {
            get { return allUsers; }
            set
            {
                allUsers = value;
                NotifyPropertyChaged("AllUsers");
            }
        }

        private List<Equipment> allEquipments = DataWorker.GetAllEquipments();
        public List<Equipment> AllEquipments { get { return allEquipments; } }
        #endregion

        #region Объявление переменных
        public static User User { get; set; }
        public static Equipment Equipment { get; set; }
        public static int Amount { get; set; }
        public static DateTime DateIssue { get; set; }
        public static DateTime DateReturn { get; set; }
        public static bool IsReturned { get; set; }
        public bool IsNewUser { get; set; }

        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserPhone { get; set; }
        public User newUser { get; set; }
        public static Order SelectedOrder { get; set; }
        #endregion

        #region Добавление заказа и пользователя
        private RelayCommand editOrder;
        public RelayCommand EditOrder
        {
            get
            {
                return editOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if(IsNewUser == false)
                    {
                        DataWorker.EditOrder(SelectedOrder, User, Equipment, Amount, DateIssue, DateReturn, IsReturned);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if(IsNewUser == true)
                    {
                        if (UserFirstName == null)
                        {
                            SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                        }
                        if (UserPhone == null)
                        {
                            SetRedBlockControl.RedBlockControl(window, "tb_phone");
                        }
                        else
                        {
                            DataWorker.EditOrder(SelectedOrder, DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone, true), Equipment, Amount, DateIssue, DateReturn, IsReturned);
                            DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                            UpdateAllOrdersView();
                            window.Close();
                        }
                    }
                });
            }
        }
        #endregion

        






        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChaged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
