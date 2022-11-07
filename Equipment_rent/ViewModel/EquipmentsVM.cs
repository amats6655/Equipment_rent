using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment_rent.ViewModel
{
    internal class EquipmentsVM : INotifyPropertyChanged
    {
        private List<Equipment> allEquipments = DataWorker.GetAllEquipments();

        public List<Equipment> AllEquipments
        {
            get { return allEquipments; }
            set
            {
                allEquipments = value;
                NotifyPropertyChaged("AllEquipments");
            }
        }

        #region Open Window Add New Equipnent
        private RelayCommand addEquipment;
        public RelayCommand AddEquipment
        {
            get
            {
                return addEquipment ?? new RelayCommand(obj =>
                {
                    Add_Button_Click();
                }
                    );
            }
        }

        private void Add_Button_Click()
        {
            AddEquipment addEquipment = new AddEquipment();
            addEquipment.ShowDialog();
        }
        #endregion



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
