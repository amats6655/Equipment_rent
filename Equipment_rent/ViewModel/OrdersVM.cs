using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Equipment_rent.ViewModel
{
    internal class OrdersVM : INotifyPropertyChanged
    {
        public static int pageIndex = 1;
        private static int numberOfRecPerPage = 10;
        public enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 }

        public static int count;

        private string pageInformation = numberOfRecPerPage + " из " + allOrders.Count;
        public string PageInformation
        {
            get
            {
                return pageInformation;
            }
            set
            {
                pageInformation = value;
                NotifyPropertyChaged(nameof(PageInformation));
            }
        }
        private static List<Order> allOrders = DataWorker.GetAllOrders();


        private List<Order> firstOrders = DataWorker.GetFirstOrders(numberOfRecPerPage);

        public List<Order> FirstOrders
        {
            get
            {
                List<Order> orders = new List<Order>();
                foreach (Order order in firstOrders)
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
                firstOrders = value;
                NotifyPropertyChaged("AllOrders");
            }
        }



        public void Navigate(int mode)
        {
            count = 0;
            switch (mode)
            {
                case (int)PagingMode.Previous:
                    firstOrders = DataWorker.GetPreviousPageOrders(pageIndex, numberOfRecPerPage);
                    Orders.AllOrders.ItemsSource = null;
                    Orders.AllOrders.Items.Clear();
                    Orders.AllOrders.ItemsSource = FirstOrders;
                    Orders.AllOrders.Items.Refresh();
                    PageInformation = count + " из " + allOrders.Count;
                    break;

                case (int)PagingMode.Next:
                    firstOrders = DataWorker.GetNextPageOrders(pageIndex, numberOfRecPerPage);
                    Orders.AllOrders.ItemsSource = null;
                    Orders.AllOrders.Items.Clear();
                    Orders.AllOrders.ItemsSource = FirstOrders;
                    Orders.AllOrders.Items.Refresh();
                    PageInformation = count + " из " + allOrders.Count;
                    break;
            }
        }
        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ?? new RelayCommand(obj =>
                {
                    BtnNext_Click();
                });
            }
        }
        private void BtnNext_Click()
        {
            Navigate((int)PagingMode.Next);
        }
        private RelayCommand previewPage;
        public RelayCommand PreviewPage
        {
            get
            {
                return previewPage ?? new RelayCommand(obj =>
                {
                    BtnPreview_Click();
                });
            }
        }
        private void BtnPreview_Click()
        {
            Navigate((int)PagingMode.Previous);
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
            DeleteWindow deleteWindow = new DeleteWindow();
            if (deleteWindow.ShowDialog() == true)
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
            firstOrders = DataWorker.GetFirstOrders(numberOfRecPerPage);
            Orders.AllOrders.ItemsSource = null;
            Orders.AllOrders.Items.Clear();
            Orders.AllOrders.ItemsSource = FirstOrders;
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
