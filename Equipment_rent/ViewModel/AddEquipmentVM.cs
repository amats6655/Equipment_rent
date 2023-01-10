using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Equipment_rent.Model;
using Equipment_rent.Utilites;

namespace Equipment_rent.ViewModel;

internal class AddEquipmentVM : EquipmentsVM
{
    public AddEquipmentVM()
    {
        newEquipmentCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
    }

    public ICommand newEquipmentCommand { get; }

    private bool CanExecuteAddCommand(object obj)
    {
        bool validData;
        if (EquipAmount <= 0 || string.IsNullOrEmpty(EquipModel.Replace(" ", "")) || EquipType == null)
            validData = false;
        else
            validData = true;
        return validData;
    }

    private void ExecuteAddCommand(object obj)
    {
        AddNewEquip.Execute(obj);
    }


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

    public Type EquipType { get; set; }
    public string EquipModel { get; set; }
    public int EquipAmount { get; set; }

    #endregion


    #region Commands to add

    private RelayCommand addNewEquip;

    public RelayCommand AddNewEquip
    {
        get
        {
            return addNewEquip ?? new RelayCommand(obj =>
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
                    DataWorker.CreateEquip(EquipType, EquipModel, EquipAmount);
                    UpdateAllEquipmentsView();
                    window.Close();
                }
            });
        }
    }

    #endregion
}