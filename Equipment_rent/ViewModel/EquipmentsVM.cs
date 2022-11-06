using Equipment_rent.Model;
using Equipment_rent.Utilites;
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
