using System.Collections.Generic;
using System.ComponentModel;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel;

internal class EquipmentsVM : INotifyPropertyChanged
{
    public enum PagingMode
    {
        First = 1,
        Next = 2,
        Previous = 3,
        Last = 4,
        PageCountChange = 5
    }

    public static int pageIndex = 1;
    private static readonly int numberOfRecPerPage = 10;
    public static int count;

    private static List<Equipment> firstEquipments = DataWorker.GetFirstEquipments(numberOfRecPerPage);
    private RelayCommand nextPage;
    private string pageInformation = numberOfRecPerPage + " из " + AllEquipments.Count;
    private RelayCommand prevPage;

    public string PageInformation
    {
        get => pageInformation;
        set
        {
            pageInformation = value;
            NotifyPropertyChaged("PageInformation");
        }
    }

    public static List<Equipment> AllEquipments { get; set; } = DataWorker.GetAllEquipments();

    public static List<Equipment> FirstEquipments
    {
        get => firstEquipments;
        set => AllEquipments = value;
    }

    public RelayCommand NextPage
    {
        get { return nextPage ?? new RelayCommand(obg => { BtnNext_Click(); }); }
    }

    public RelayCommand PrevPage
    {
        get { return prevPage ?? new RelayCommand(obj => { BtnPrev_Click(); }); }
    }


    public event PropertyChangedEventHandler PropertyChanged;

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
                PageInformation = count + " из " + AllEquipments.Count;
                break;
            case (int)PagingMode.Next:
                firstEquipments = DataWorker.GetNextPageEquipments(pageIndex, numberOfRecPerPage);
                Equipments.AllEquipments.ItemsSource = null;
                Equipments.AllEquipments.Items.Clear();
                Equipments.AllEquipments.ItemsSource = FirstEquipments;
                Equipments.AllEquipments.Items.Refresh();
                PageInformation = count + " из " + AllEquipments.Count;
                break;
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

    private void Edit_Button_Click(Equipment equipment)
    {
        var editEquipment = new EditEquipment(equipment);
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

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Open Window Add New Equipnent

    private RelayCommand addEquipment;

    public RelayCommand AddEquipment
    {
        get
        {
            return addEquipment ?? new RelayCommand(obj => { Add_Button_Click(); }
            );
        }
    }

    private void Add_Button_Click()
    {
        var addEquipment = new AddEquipment();
        addEquipment.ShowDialog();
    }

    #endregion

    #region Delete Equipment

    private RelayCommand removeEquipment;

    public RelayCommand RemoveEquipment
    {
        get { return removeEquipment ?? new RelayCommand(obj => { Del_Button_Click(); }); }
    }

    private void Del_Button_Click()
    {
        var deleteWindow = new DeleteWindow();
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
}