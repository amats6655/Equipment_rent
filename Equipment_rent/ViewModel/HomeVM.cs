﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel;

internal class HomeVM : INotifyPropertyChanged
{
    private List<Order> allActiveOrders = DataWorker.GetAllOrdersByStatus();

    public List<Order> AllActiveOrders
    {
        get => allActiveOrders;
        set
        {
            allActiveOrders = value;
            NotifyPropertyChaged("AllActivityOrders");
        }
    }

    public Order SelectedOrder { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;


    public void UpdateAllOrdersView()
    {
        allActiveOrders = DataWorker.GetAllOrdersByStatus();
        Home.AllOrdersByID.ItemsSource = null;
        Home.AllOrdersByID.Items.Clear();
        Home.AllOrdersByID.ItemsSource = AllActiveOrders;
        Home.AllOrdersByID.Items.Refresh();
    }

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }


    #region Return order

    private RelayCommand returnOrder;

    public RelayCommand ReturnOrder
    {
        get { return returnOrder ?? new RelayCommand(obj => { Return_Button_Click(); }); }
    }

    private void Return_Button_Click()
    {
        if (NavigationVM.AuthUser.Role.Role.Equals("test"))
        {
            MessageBox.Show("Недостаточно прав =(");
        }
        else
        {
            var thisSelectedOrder = SelectedOrder;
            var confirmWindow = new ConfirmWindow();
            if (confirmWindow.ShowDialog() == true)
            {
                if (thisSelectedOrder.OrdersUser.UserOrders.Count <= 1)
                    DataWorker.EditUser(thisSelectedOrder.OrdersUser, thisSelectedOrder.OrdersUser.Name,
                        thisSelectedOrder.OrdersUser.Phone, false);
                DataWorker.EditOrder(thisSelectedOrder, thisSelectedOrder.OrdersUser, thisSelectedOrder.OrdersEquipment,
                    thisSelectedOrder.Amount, thisSelectedOrder.DateIssue, DateTime.Now, true,
                    NavigationVM.AuthUser.Id);
                DataWorker.EditEquipment(thisSelectedOrder.OrdersEquipment, thisSelectedOrder.OrdersEquipment.EquipType,
                    thisSelectedOrder.OrdersEquipment.Model, thisSelectedOrder.OrdersEquipment.Amount,
                    thisSelectedOrder.OrdersEquipment.Balance + thisSelectedOrder.Amount);
                UpdateAllOrdersView();
            }
        }
    }

    #endregion
}