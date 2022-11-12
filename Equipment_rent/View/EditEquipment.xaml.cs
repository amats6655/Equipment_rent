using Equipment_rent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            EditEquipmentVM.EquipType = EquipmentToEdit.Type;
            EditEquipmentVM.EquipModel = EquipmentToEdit.Model;
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
