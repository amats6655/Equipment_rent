using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Type = Equipment_rent.Model.Type;

namespace Equipment_rent.ViewModel;

internal class AddOrderVM : OrdersVM
{
    public AddOrderVM()
    {
        AddNewOrder = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
    }

    public ICommand AddNewOrder { get; }

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


    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Вывод списка пользователей и моделей

    private List<User> allUsers = DataWorker.GetAllUsers();

    public List<User> AllUsers
    {
        get => allUsers;
        set
        {
            allUsers = value;
            NotifyPropertyChaged(nameof(AllUsers));
        }
    }

    public List<Equipment> AllEquipments { get; set; } = DataWorker.GetAllEquipments();

    private List<Type> allTypes = DataWorker.GetAllTypes();

    public List<Type> AllTypes
    {
        get => allTypes;
        set
        {
            allTypes = value;
            NotifyPropertyChaged(nameof(AllTypes));
        }
    }

    #endregion

    #region Объявление переменных

    public User User { get; set; }
    private Type _type;

    public Type Types
    {
        get => _type;
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
        get => _amount;
        set
        {
            if (value > Equipment.Balance) throw new ArgumentException("Такого количества нет в наличии");
            if (value <= 0) throw new ArgumentException("Количество должно быть больше нуля");
            _amount = value;
        }
    }

    public DateTime? DateIssue { get; set; }
    private DateTime? _dateReturn;

    public DateTime? DateReturn
    {
        get => _dateReturn;
        set
        {
            if (value < DateIssue) throw new ArgumentException("Дата возарата не может быть раньше даты выдачи");
            _dateReturn = value;
        }
    }

    public bool IsNewUser { get; set; }

    public string UserLastName { get; set; }
    public string UserFirstName { get; set; }
    public string UserPhone { get; set; }
    public User newUser { get; set; }

    #endregion


    #region Добавление заказа и пользователя

    private RelayCommand orderAdd;

    public RelayCommand OrderAdd
    {
        get
        {
            return orderAdd ?? new RelayCommand(obj =>
            {
                var window = obj as Window;
                if (DateIssue == null) DateIssue = DateTime.Now;
                if (DateReturn == null) DateReturn = DateTime.Now;

                if (IsNewUser == false)
                {
                    DataWorker.CreateOrder(User, Equipment, Amount, (DateTime)DateIssue, (DateTime)DateReturn,
                        NavigationVM.AuthUser.Id);
                    DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                        Equipment.Amount, Equipment.Balance - Amount);
                    UpdateAllOrdersView();
                    window.Close();
                }
                else if (IsNewUser)
                {
                    if (UserFirstName == null) SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                    if (UserPhone == null)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_phone");
                    }
                    else
                    {
                        var OldUser = DataWorker.GetUserByName(UserLastName + " " + UserFirstName);
                        if (OldUser == null)
                        {
                            DataWorker.CreateOrder(
                                DataWorker.CreateUser(UserLastName + " " + UserFirstName, UserPhone, true), Equipment,
                                Amount, (DateTime)DateIssue, (DateTime)DateReturn, NavigationVM.AuthUser.Id);
                            DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId),
                                Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                            UpdateAllOrdersView();
                            window.Close();
                        }
                        else
                        {
                            DataWorker.CreateOrder(OldUser, Equipment, Amount, (DateTime)DateIssue,
                                (DateTime)DateReturn, NavigationVM.AuthUser.Id);
                            DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId),
                                Equipment.Model, Equipment.Amount, Equipment.Balance - Amount);
                            UpdateAllOrdersView();
                            window.Close();
                        }
                    }
                }
            });
        }
    }

    #endregion
}