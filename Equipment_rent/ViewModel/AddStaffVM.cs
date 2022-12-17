using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Equipment_rent.ViewModel
{
    internal class AddStaffVM : StaffVM
    {
        #region Вывод списка должностей
        private List<Auth_role> _allroles = DataWorker.GetAllAuthRoles();
        public List<Auth_role> AllRoles
        {
            get { return _allroles; }
            set
            {
                _allroles = value;
                NotifyPropertyChaged(nameof(AllRoles));
            }
        }
        #endregion

        public static string UserId { get; set; }
        public string StaffUsername { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string StaffEmail { get; set; }
        public Auth_role Role { get; set; }
        public string Password { get; set; }

        #region Commands to add
        private RelayCommand addNewStaff;
        public RelayCommand AddNewStaff
        {
            get
            {
                return addNewStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    if (StaffFirstName == null || StaffFirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_lastname");
                        SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                    }
                    else if (StaffEmail == null)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_phone");
                    }
                    else
                    {
                        AuthClient.CreateNewUser(StaffUsername, Password);
                        if (UserId != null)
                        {
                            Guid guid = Guid.Parse(UserId);
                            DataWorker.CreateStaff(guid, StaffUsername, StaffFirstName, StaffLastName, StaffEmail, Role.Id);
                            UpdateAllStaffView();
                        }
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