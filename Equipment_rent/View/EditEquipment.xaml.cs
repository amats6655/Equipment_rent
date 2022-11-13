using Equipment_rent.ViewModel;
using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;

namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AddEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {
        public EditEquipment(Equipment EquipmentToEdit)
        {
            InitializeComponent();
            DataContext = new EditEquipmentVM();
            EditEquipmentVM.SelectedEquipment = EquipmentToEdit;
            EditEquipmentVM.EquipType = EquipmentToEdit.EquipType;
            EditEquipmentVM.EquipModel = EquipmentToEdit.Model;
            cb_type.Text = EquipmentToEdit.EquipType.Name;
            cb_type.SelectedValue = EquipmentToEdit.EquipType;
            cb_type.SelectedValuePath = EquipmentToEdit.EquipType.Name;
            EditEquipmentVM.EquipAmount = EquipmentToEdit.Amount;
            EditEquipmentVM.EquipBalance = EquipmentToEdit.Balance;
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
