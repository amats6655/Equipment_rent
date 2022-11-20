﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel
{
    internal class AddOrderVM : OrdersVM
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
        public User User { get; set; }
        public Equipment Equipment { get; set; }
        public int Amount { get; set; }
        public DateTime? DateIssue { get; set; }
        public DateTime? DateReturn { get; set; }
        public bool IsNewUser { get; set; }
        
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserPhone { get; set; }
        public User newUser { get; set; }


        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch(columnName)
                {
                    case "Amount":
                        if((Amount > cb_model))
                        {
                            error = "Превышает остаток";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Добавление заказа и пользователя
        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if(DateIssue == null) DateIssue = DateTime.Now;
                    if(DateReturn == null) DateReturn = DateTime.Now;

                    if(IsNewUser == false)
                    {
                        DataWorker.CreateOrder(User, Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn);
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
                            DataWorker.CreateOrder(DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone, true), Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn);
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

        public interface IDataErrorInfo
        {
            string Error { get; }
            string this[string colunnName] { get; }
        }
    }
}
