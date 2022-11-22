using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

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
        public Equipment? Equipment { get; set; }
        private int _amount;
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value > Equipment.Balance)
                {
                    throw new ArgumentException("Такого количества нет в наличии");
                }
                if (value <= 0)
                {
                    throw new ArgumentException("Количество должно быть больше нуля");
                }
                _amount = value;
            }
        }
        public DateTime DateIssue { get; set; }
        private DateTime _dateReturn;
        public DateTime DateReturn
        {
            get
            {
                return _dateReturn;
            }
            set
            {
                if (value < DateIssue)
                {
                    throw new ArgumentException("Дата возарата не может быть раньше даты выдачи");
                }
                _dateReturn = value;
            }
        }
        public bool IsNewUser { get; set; }
        
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserPhone { get; set; }
        public User newUser { get; set; }


        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string error = string.Empty;
        //        switch(columnName)
        //        {
        //            case "Amount":
        //                if(Amount > Equipment?.Balance)
        //                {
        //                    error = "Превышает остаток";
        //                }
        //                break;
        //        }
        //        return error;
        //    }
        //}
        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}
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

    }
}
