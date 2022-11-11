using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Equipment_rent.ViewModel
{
    internal class AddUserVM : UsersVM
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }

        #region Commands to add
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    if (UserFirstName == null || UserFirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_lastname");
                        SetRedBlockControl.RedBlockControl(window, "tb_firstname");
                    }
                    else if (UserPhone == null)
                    {
                        SetRedBlockControl.RedBlockControl(window, "tb_phone");
                    }
                    else
                    {
                        DataWorker.CreateUser(UserFirstName + " " + UserLastName, UserPhone);
                        UpdateAllUsersView();
                        window.Close();
                    }
                });
            }
        }
        #endregion
    }
}