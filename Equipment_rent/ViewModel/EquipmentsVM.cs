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
        public static int pageIndex = 1;
        private static int numberOfRecPerPage = 10;
        public enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 }
        public static int count;
        private string pageInformation = numberOfRecPerPage + " из " + allEquipments.Count;
        public string PageInformation
        {
            get
            {
                return pageInformation;
            }
            set
            {
                pageInformation = value;
                NotifyPropertyChaged("PageInformation");
            }
        }

        private static List<Equipment> allEquipments = DataWorker.GetAllEquipments();

        private List<Equipment> firstEquipments = DataWorker.GetFirstEquipments(numberOfRecPerPage);
        public List<Equipment> FirstEquipments
        {
            get { return firstEquipments; }
            set
            {
                allEquipments = value;
                NotifyPropertyChaged("AllEquipments");
            }
        }

        public void Navigate(int mode)
        {
            count = 0;
            switch (mode)
            {
                case (int)PagingMode.Previous:
                    firstEquipments = DataWorker.GetPreviousPageEquipments(pageIndex, numberOfRecPerPage);
                    Equipments.AllEquipments.ItemsSource = null;
                    Equipments.AllEquipments.Items.Clear();
                    Equipments.AllEquipments.ItemsSource = FirstEquipments;
                    Equipments.AllEquipments.Items.Refresh();
                    PageInformation = count + " из " + allEquipments.Count;
                    break;
                case (int)PagingMode.Next:
                    firstEquipments = DataWorker.GetNextPageEquipments(pageIndex, numberOfRecPerPage);
                    Equipments.AllEquipments.ItemsSource = null;
                    Equipments.AllEquipments.Items.Clear();
                    Equipments.AllEquipments.ItemsSource = FirstEquipments;
                    Equipments.AllEquipments.Items.Refresh();
                    PageInformation = count + " из " + allEquipments.Count;
                    break;
            }
        }
        private RelayCommand nextPage;
        private RelayCommand prevPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ?? new RelayCommand(obg =>
                {
                    BtnNext_Click();
                });
            }
        }
        public RelayCommand PrevPage
        {
            get
            {
                return prevPage ?? new RelayCommand(obj =>
                {
                    BtnPrev_Click();
                });
            }
        }
        private void BtnNext_Click()
        {
            Navigate((int)PagingMode.Next);
        }
        private void BtnPrev_Click()
        {
            Navigate((int)PagingMode.Previous);
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
            if (deleteWindow.ShowDialog() == true)
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
            firstEquipments = DataWorker.GetFirstEquipments(count);
            Equipments.AllEquipments.ItemsSource = null;
            Equipments.AllEquipments.Items.Clear();
            Equipments.AllEquipments.ItemsSource = FirstEquipments;
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
