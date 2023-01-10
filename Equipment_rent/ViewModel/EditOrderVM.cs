using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

internal class EditOrderVM : OrdersVM
{
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
            NotifyPropertyChaged("AllUsers");
        }
    }

    public List<Equipment> AllEquipments { get; } = DataWorker.GetAllEquipments();

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


    public bool newIsReturned { get; set; }

    #endregion

    #region Добавление заказа и пользователя

    private RelayCommand editOrder;

    public RelayCommand EditOrder
    {
        get
        {
            return editOrder ?? new RelayCommand(obj =>
            {
                var window = obj as Window;
                if (IsNewUser == false)
                {
                    if (IsReturned == newIsReturned)
                    {
                        DataWorker.EditOrder(SelectedOrder, User, Equipment, Amount, DateIssue, DateReturn,
                            newIsReturned, SelectedOrder.WhoTake);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if (IsReturned != newIsReturned && newIsReturned)
                    {
                        if (User.UserOrders.Count == 1) DataWorker.EditUser(User, User.Name, User.Phone, false);
                        DataWorker.EditOrder(SelectedOrder, User, Equipment, Amount, DateIssue, DateReturn,
                            newIsReturned, NavigationVM.AuthUser.Id);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance + Amount);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if (IsReturned != newIsReturned && newIsReturned == false)
                    {
                        DataWorker.EditOrder(SelectedOrder, User, Equipment, Amount, DateIssue, DateReturn,
                            newIsReturned, Guid.Empty);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance - Amount);
                        DataWorker.EditUser(User, User.Name, User.Phone, true);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                }
                else if (IsNewUser)
                {
                    if (IsReturned == newIsReturned)
                    {
                        DataWorker.EditOrder(SelectedOrder,
                            DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone, true), Equipment,
                            Amount, DateIssue, DateReturn, newIsReturned, SelectedOrder.WhoTake);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if (IsReturned != newIsReturned && newIsReturned)
                    {
                        if (User.UserOrders.Count == 1) DataWorker.EditUser(User, User.Name, User.Phone, false);
                        DataWorker.EditOrder(SelectedOrder,
                            DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone, true), Equipment,
                            Amount, DateIssue, DateReturn, newIsReturned, NavigationVM.AuthUser.Id);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance + Amount);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                    else if (IsReturned != newIsReturned && newIsReturned == false)
                    {
                        DataWorker.EditOrder(SelectedOrder,
                            DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone, true), Equipment,
                            Amount, DateIssue, DateReturn, newIsReturned, Guid.Empty);
                        DataWorker.EditEquipment(Equipment, DataWorker.GetTypeById(Equipment.TypeId), Equipment.Model,
                            Equipment.Amount, Equipment.Balance - Amount);
                        DataWorker.EditUser(User, User.Name, User.Phone, true);
                        UpdateAllOrdersView();
                        window.Close();
                    }
                }
            });
        }
    }

    #endregion
}