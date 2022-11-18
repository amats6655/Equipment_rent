using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Equipment_rent.ViewModel
{
    internal class HomeVM : ViewModelBase
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
            }
        }
    }
}
