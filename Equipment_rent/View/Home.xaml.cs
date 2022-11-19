using System.Windows.Controls;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public static ListBox AllOrdersByID;
        public Home()
        {
            InitializeComponent();
            AllOrdersByID = lv_orders;
        }
    }
}
