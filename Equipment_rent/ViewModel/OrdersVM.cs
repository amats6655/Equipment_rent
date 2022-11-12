﻿using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Equipment_rent.ViewModel
{
    internal class OrdersVM : INotifyPropertyChanged
    {
        private List<Order> allOrders = DataWorker.GetAllOrders();

        public List<Order> AllOrders
        {
            get 
            { 
                List<Order> orders = new List<Order>();
                foreach (Order order in allOrders)
                {
                    char Character;
                    if (order.IsReturned == true) Character = 'R';
                    else Character = 'D';
                          
                    Brush BgColor = GetBrush.getBrush(Character);
                    order.BgColor = BgColor.ToString();
                    orders.Add(order);
                }
                return orders; 
            }
            set
            {
                allOrders = value;
                NotifyPropertyChaged("AllOrders");
            }
        }

        #region Open Window Add New Order
        private RelayCommand addOrder;
        public RelayCommand AddOrder
        {
            get
            {
                return addOrder ?? new RelayCommand(obj =>
                {
                    Add_Button_Click();
                }
                    );
            }
        }

        private void Add_Button_Click()
        {
            AddOrder addOrder = new AddOrder();
            addOrder.ShowDialog();
        }
        #endregion

        #region Delete order
        private RelayCommand removeOrder;
        public RelayCommand RemoveOrder
        {
            get
            {
                return removeOrder ?? new RelayCommand(obj =>
                {
                    Del_Button_Click();
                });
            }
        }
        private void Del_Button_Click()
        {
            ConfirmWindow confirmWindow = new ConfirmWindow();
            if (confirmWindow.ShowDialog() == true)
            {
                DataWorker.DeleteOrder((Order)Orders.AllOrders.SelectedItem);
                UpdateAllOrdersView();
            }    
        }
        #endregion

        #region Open Edit order window
        private RelayCommand openEditOrder;
        public RelayCommand OpenEditOrder
        {
            get
            {
                return openEditOrder ?? new RelayCommand(obj =>
                {
                    Edit_Button_Click((Order)Orders.AllOrders.SelectedItem);
                }
                    );
            }
        }

        private void Edit_Button_Click(Order order)
        {
            EditOrder editOrder = new EditOrder(order);
            editOrder.ShowDialog();
        }
        #endregion

        public void UpdateAllOrdersView()
        {
            allOrders = DataWorker.GetAllOrders();
            Orders.AllOrders.ItemsSource = null;
            Orders.AllOrders.Items.Clear();
            Orders.AllOrders.ItemsSource = AllOrders;
            Orders.AllOrders.Items.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChaged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
