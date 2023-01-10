using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

internal class EditEquipmentVM : EquipmentsVM
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Вывод списка типов

    private List<Type> allTypes = DataWorker.GetAllTypes();

    public List<Type> AllTypes
    {
        get => allTypes;
        set
        {
            allTypes = value;
            NotifyPropertyChaged("AllTypes");
        }
    }

    #endregion

    #region Объявление переменных

    public static Type EquipType { get; set; }
    public static string EquipModel { get; set; }
    public static int EquipAmount { get; set; }
    public static Equipment SelectedEquipment { get; set; }
    public static int EquipBalance { get; set; }

    #endregion


    #region Commands to add

    private RelayCommand editEquip;

    public RelayCommand EditEquip
    {
        get
        {
            return editEquip ?? new RelayCommand(obj =>
            {
                var window = obj as Window;

                if (EquipModel == null || EquipModel.Replace(" ", "").Length == 0)
                {
                    SetRedBlockControl.RedBlockControl(window, "tb_model");
                }
                else if (EquipAmount == 0)
                {
                    SetRedBlockControl.RedBlockControl(window, "tb_amount");
                }
                else
                {
                    DataWorker.EditEquipment(SelectedEquipment, EquipType, EquipModel, EquipAmount, EquipBalance);
                    UpdateAllEquipmentsView();
                    window.Close();
                }
            });
        }
    }

    #endregion
}