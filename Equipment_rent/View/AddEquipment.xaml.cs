using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;
namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddEquipment.xaml
    /// </summary>
    public partial class AddEquipment : Window
    {
        public AddEquipment()
        {
            InitializeComponent();
            DataContext = new AddEquipmentVM();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
