using System.Windows.Controls;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : UserControl
    {
        public static DataGrid AllStaff;
        public Staff()
        {
            InitializeComponent();
            AllStaff = StaffDataGrid;
        }
    }
}
