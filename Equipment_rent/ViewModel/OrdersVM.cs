using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel;

internal class OrdersVM : INotifyPropertyChanged
{
    public enum PagingMode
    {
        Next = 2,
        Previous = 3,
    }

    public static int PageIndex = 1;
    private const int NumberOfRecPerPage = 10;

    public static int Count;
    private static List<Order> allOrders = DataWorker.GetAllOrders();

    private static List<Order> firstOrders = DataWorker.GetFirstOrders(NumberOfRecPerPage);
    private RelayCommand nextPage;

    private string pageInformation = NumberOfRecPerPage + " из " + allOrders.Count;
    private RelayCommand previewPage;

    public string PageInformation
    {
        get => pageInformation;
        set
        {
            pageInformation = value;
            NotifyPropertyChaged(nameof(PageInformation));
        }
    }

    public static List<Order> AllOrders
    {
        get
        {
            foreach (var order in allOrders)
            {
                var character = order.IsReturned ? 'R' : 'D';

                var bgColor = GetBrush.getBrush(character);
                order.BgColor = bgColor.ToString();
            }

            return allOrders;
        }
        set => allOrders = value;
    }

    public static string? Filter { get; set; }

    public static List<Order> FirstOrders
    {
        get
        {
            var orders = new List<Order>();
            foreach (var order in firstOrders)
            {
                char Character;
                if (order.IsReturned) Character = 'R';
                else Character = 'D';

                var BgColor = GetBrush.getBrush(Character);
                order.BgColor = BgColor.ToString();
                orders.Add(order);
            }

            return orders;
        }
        set => firstOrders = value;
    }

    public RelayCommand NextPage
    {
        get { return nextPage ?? new RelayCommand(obj => { BtnNext_Click(); }); }
    }

    public RelayCommand PreviewPage
    {
        get { return previewPage ?? new RelayCommand(obj => { BtnPreview_Click(); }); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public static void Search(object sender, KeyEventArgs e)
    {
        if (Filter != "" && Filter != " ")
        {
            var filtered = AllOrders.Where(u => u.OrdersUser.Name.ToLower().Contains(Filter) ||
                                               u.OrdersUser.Phone.Contains(Filter) ||
                                               u.OrdersEquipment.Model.ToLower().Contains(Filter) ||
                                               u.OrdersEquipment.EquipType.Name.ToLower().Contains(Filter));

            Orders.AllOrders.ItemsSource = null;
            Orders.AllOrders.Items.Clear();
            Orders.AllOrders.ItemsSource = filtered;
            Orders.AllOrders.Items.Refresh();
        }
        else
        {
            Orders.AllOrders.ItemsSource = null;
            Orders.AllOrders.Items.Clear();
            Orders.AllOrders.ItemsSource = FirstOrders;
            Orders.AllOrders.Items.Refresh();
        }
    }


    public void Navigate(int mode)
    {
        Count = 0;
        switch (mode)
        {
            case (int)PagingMode.Previous:
                firstOrders = DataWorker.GetPreviousPageOrders(PageIndex, NumberOfRecPerPage);
                Orders.AllOrders.ItemsSource = null;
                Orders.AllOrders.Items.Clear();
                Orders.AllOrders.ItemsSource = FirstOrders;
                Orders.AllOrders.Items.Refresh();
                PageInformation = Count + " из " + allOrders.Count;
                break;

            case (int)PagingMode.Next:
                firstOrders = DataWorker.GetNextPageOrders(PageIndex, NumberOfRecPerPage);
                Orders.AllOrders.ItemsSource = null;
                Orders.AllOrders.Items.Clear();
                Orders.AllOrders.ItemsSource = FirstOrders;
                Orders.AllOrders.Items.Refresh();
                PageInformation = Count + " из " + allOrders.Count;
                break;
        }
    }

    private void BtnNext_Click()
    {
        Navigate((int)PagingMode.Next);
    }

    private void BtnPreview_Click()
    {
        Navigate((int)PagingMode.Previous);
    }

    public void UpdateAllOrdersView()
    {
        firstOrders = DataWorker.GetFirstOrders(NumberOfRecPerPage);
        Orders.AllOrders.ItemsSource = null;
        Orders.AllOrders.Items.Clear();
        Orders.AllOrders.ItemsSource = FirstOrders;
        Orders.AllOrders.Items.Refresh();
    }

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }


    #region Open Window Add New Order

    private RelayCommand addOrder;

    public RelayCommand AddOrder
    {
        get
        {
            return addOrder ?? new RelayCommand(obj => { Add_Button_Click(); }
            );
        }
    }

    private void Add_Button_Click()
    {
        var addOrder = new AddOrder();
        addOrder.ShowDialog();
    }

    #endregion

    #region Delete order

    private RelayCommand removeOrder;

    public RelayCommand RemoveOrder
    {
        get { return removeOrder ?? new RelayCommand(obj => { Del_Button_Click(); }); }
    }


    private void Del_Button_Click()
    {
        var deleteWindow = new DeleteWindow();
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
            return openEditOrder ?? new RelayCommand(obj => { Edit_Button_Click((Order)Orders.AllOrders.SelectedItem); }
            );
        }
    }

    private void Edit_Button_Click(Order order)
    {
        var editOrder = new EditOrder(order);
        editOrder.ShowDialog();
    }

    #endregion
}