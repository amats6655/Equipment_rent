using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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
                NotifyPropertyChaged(nameof(AllUsers));
            }
        }

        private List<Equipment> _allEquipments = DataWorker.GetAllEquipments();
        public List<Equipment> AllEquipments
        {
            get => _allEquipments;
            set
            {
                _allEquipments = value;
            }
        }

        private List<Model.Type> allTypes = DataWorker.GetAllTypes();
        public List<Model.Type> AllTypes
        {
            get { return allTypes; }
            set
            {
                allTypes = value;
                NotifyPropertyChaged(nameof(AllTypes));
            }
        }
        #endregion

        #region Объявление переменных
        public User User { get; set; }
        private Model.Type _type;
        public Model.Type Types
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if (_type != null)
                {

                    View.AddOrder.Models.ItemsSource = DataWorker.GetAllEquipmentsByIdType(_type.TypeId);
                    View.AddOrder.Models.IsEnabled = true;
                    View.AddOrder.Models.SelectedIndex = 0;
                }
            }
        }
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
        public DateTime? DateIssue { get; set; }
        private DateTime? _dateReturn;
        public DateTime? DateReturn
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

        #endregion

        public ICommand AddNewOrder { get; }
        public AddOrderVM()
        {
            AddNewOrder = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private bool CanExecuteAddCommand(object obj)
        {
            bool validData;
            if (Amount <= 0 || (User == null && string.IsNullOrEmpty(UserFirstName)) || Equipment == null)
                validData = false;
            else
                validData = true;
            return validData;
        }
        private void ExecuteAddCommand(object obj)
        {
            OrderAdd.Execute(obj);
        }
    


        #region Добавление заказа и пользователя
        private RelayCommand orderAdd;
        public RelayCommand OrderAdd
        {
            get
            {
                return orderAdd ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (DateIssue == null) DateIssue = DateTime.Now;
                    if (DateReturn == null) DateReturn = DateTime.Now;

                    if (IsNewUser == false)
                    {
                        DataWorker.CreateOrder(User, Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn, NavigationVM.AuthUser.Id);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if (IsNewUser == true)
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
                            User OldUser = DataWorker.GetUserByName(UserLastName + " " + UserFirstName);
                            if (OldUser == null)
                            {
                                DataWorker.CreateOrder(DataWorker.CreateUser(UserLastName + " " + UserFirstName, UserPhone, true), Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn, NavigationVM.AuthUser.Id);
                                DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                                UpdateAllOrdersView();
                                window.Close();
                            }
                            else
                            {
                                DataWorker.CreateOrder(OldUser, Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn, NavigationVM.AuthUser.Id);
                                DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                                UpdateAllOrdersView();
                                window.Close();
                            }
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
