using Equipment_rent.Model;
using Equipment_rent.Utilites;
using System;
using System.Windows;

namespace Equipment_rent.ViewModel
{
    internal class EditStaffVM : StaffVM
    {
        public static string StaffFirstname { get; set; }
        public static string StaffLastname { get; set; }
        public static string StaffEmail { get; set; }
        public static Guid StaffRole_Id { get; set; }
        public static Auth_user SelectedStaff { get; set; }
        public static string StaffUsername { get; set; }

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
                    else
                    {
                        DataWorker.EditStaff(SelectedStaff, StaffFirstname, StaffLastname, StaffEmail, StaffRole_Id);
                        UpdateAllStaffView();
                        window.Close();
                    }
                });
            }
        }
        #endregion
    }
}