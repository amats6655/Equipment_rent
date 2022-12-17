using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Equipment_rent.ViewModel
{
    internal class EditStaffVM : StaffVM
    {

        // Вывод списка должностей
        private List<Auth_role> _allRoles = DataWorker.GetAllAuthRoles();
        public List<Auth_role> AllRoles
        {
            get { return _allRoles; }
            set
            {
                _allRoles = value;
                NotifyPropertyChaged(nameof(AllRoles));
            }
        }

        public static string StaffFirstname { get; set; }
        public static string StaffLastname { get; set; }
        public static string StaffEmail { get; set; }
        public Auth_role StaffRole { get; set; }
        public static Auth_user SelectedStaff { get; set; }
        public static string StaffUsername { get; set; }
        public static string Password { get; set; }

        #region Commands to Edit
        private RelayCommand editStaff;
        public RelayCommand EditStaff
        {
            get
            {
                return editStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    if (StaffFirstname == null || StaffFirstname.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_lastname");
                        SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                    }
                    else if (StaffEmail == null)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_phone");
                    }
                    else if (Password != null)
                    {
                        DataWorker.EditStaff(SelectedStaff, StaffFirstname, StaffLastname, StaffEmail, StaffRole.Id);
                        MessageBox.Show("Функция смены пароля здесь пока не доступна");
                        UpdateAllStaffView();
                        window.Close();
                    }
                    else
                    {
                        DataWorker.EditStaff(SelectedStaff, StaffFirstname, StaffLastname, StaffEmail, StaffRole.Id);
                        UpdateAllStaffView();
                        window.Close();
                    }
                });
            }
        }
        #endregion




        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChaged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}