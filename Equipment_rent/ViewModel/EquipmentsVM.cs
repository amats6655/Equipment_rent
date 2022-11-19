using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        #region Delete Equipment
        private RelayCommand removeEquipment;
        public RelayCommand RemoveEquipment
        {
            get
            {
                return removeEquipment ?? new RelayCommand(obj =>
                {
                    Del_Button_Click();
                });
            }
        }
        private void Del_Button_Click()
        {
            DeleteWindow deleteWindow = new DeleteWindow();
            if(deleteWindow.ShowDialog() == true)
            {
                DataWorker.DeleteEquipment((Equipment)Equipments.AllEquipments.SelectedItem);
                UpdateAllEquipmentsView();
            }

        }
        #endregion

        #region Open Edit Equipment window
        private RelayCommand openEditEquipment;
        public RelayCommand OpenEditEquipment
        {
            get
            {
                return openEditEquipment ?? new RelayCommand(obj =>
                {
                    Edit_Button_Click((Equipment)Equipments.AllEquipments.SelectedItem);
                }
                    );
            }
        }
        #endregion

        private void Edit_Button_Click(Equipment equipment)
        {
            EditEquipment editEquipment = new EditEquipment(equipment);
            editEquipment.ShowDialog();
        }
        public void UpdateAllEquipmentsView()
        {
            allEquipments = DataWorker.GetAllEquipments();
            Equipments.AllEquipments.ItemsSource = null;
            Equipments.AllEquipments.Items.Clear();
            Equipments.AllEquipments.ItemsSource = AllEquipments;
            Equipments.AllEquipments.Items.Refresh();
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
