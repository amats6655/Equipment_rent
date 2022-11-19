using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Equipment_rent.ViewModel
{
    internal class HomeVM : INotifyPropertyChanged
    {
        private List<Order> allActiveOrders = DataWorker.GetAllOrdersByStatus();
        public List<Order> AllActiveOrders
        {
            get
            {
                return allActiveOrders;
            }
            set
            {
                allActiveOrders = value;
                NotifyPropertyChaged("AllActivityOrders");
            }
        }
        public Order SelectedOrder { get; set;}


        #region Return order
        private RelayCommand returnOrder;
        public RelayCommand ReturnOrder
        {
            get
            {
                return returnOrder ?? new RelayCommand(obj =>
                {
                    Return_Button_Click();
                });
            }
        }
        private void Return_Button_Click()
        {
            Order thisSelectedOrder = SelectedOrder;
            ConfirmWindow confirmWindow = new ConfirmWindow();
            if(confirmWindow.ShowDialog() == true)
            {
                DataWorker.EditOrder(thisSelectedOrder, thisSelectedOrder.OrdersUser, thisSelectedOrder.OrdersEquipment, thisSelectedOrder.Amount, thisSelectedOrder.DateIssue, DateTime.Now, true);
                UpdateAllOrdersView();
            }
        }
        #endregion


        public void UpdateAllOrdersView()
        {
            allActiveOrders = DataWorker.GetAllOrdersByStatus();
            Home.AllOrdersByID.ItemsSource = null;
            Home.AllOrdersByID.Items.Clear();
            Home.AllOrdersByID.ItemsSource = AllActiveOrders;
            Home.AllOrdersByID.Items.Refresh();
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

