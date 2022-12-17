using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using Microsoft.VisualBasic;

namespace Equipment_rent.ViewModel
{
    class StaffVM : INotifyPropertyChanged
    {
        public static int pageIndex = 1;
        private static int numberOfRecPerPage = 10;
        public enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 }
        public static int count;
        private string pageInformation = numberOfRecPerPage + " из " + allStaff.Count;
        public string PageInformation
        {
            get
            {
                return pageInformation;
            }
            set
            {
                pageInformation = value;
                NotifyPropertyChaged(nameof(PageInformation));
            }
        }

        private static List<Auth_user> allStaff = DataWorker.GetAllAuthUsers();
        #region Add Brush and Character for user
        private List<Auth_user> firstStaff = DataWorker.GetFirstStaff(numberOfRecPerPage);
        public List<Auth_user> FirstStaff
        {
            get 
            { 
                List<Auth_user> users = new List<Auth_user>();
                foreach (Auth_user staff in firstStaff)
                {
                    char Character = staff.LastName[0];
                    Brush BgColor = GetBrush.getBrush(Character);
                    staff.BgColor = BgColor.ToString();
                    staff.Character = Character;
                    users.Add(staff);
                }
                return users;
            }
            set 
            { 
                allStaff = value;
                NotifyPropertyChaged("AllStaff");
            }
        }
        #endregion


        public void Navigate(int mode)
        {
            count = 0;
            switch (mode)
            {
                case (int)PagingMode.Previous:
                    firstStaff = DataWorker.GetPreviousPageStaff(pageIndex, numberOfRecPerPage);
                    Staff.AllStaff.ItemsSource = null;
                    Staff.AllStaff.Items.Clear();
                    Staff.AllStaff.ItemsSource = FirstStaff;
                    Staff.AllStaff.Items.Refresh();
                    PageInformation = count + " из " + allStaff.Count;
                    break;
                case (int)PagingMode.Next:
                    firstStaff = DataWorker.GetNextPageStaff(pageIndex, numberOfRecPerPage);
                    Staff.AllStaff.ItemsSource = null;
                    Staff.AllStaff.Items.Clear();
                    Staff.AllStaff.ItemsSource = FirstStaff;
                    Staff.AllStaff.Items.Refresh();
                    PageInformation = count + " из " + allStaff.Count;
                    break;
            }
        }
        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ?? new RelayCommand(obj =>
                {
                    BtnNext_Click();
                });
            }
        }
        private RelayCommand previewPage;
        public RelayCommand PreviewPage
        {
            get
            {
                return previewPage ?? new RelayCommand(obj =>
                {
                    BtnPreview_Click();
                });
            }
        }
        private void BtnNext_Click()
        {
            Navigate((int)PagingMode.Next);
        }
        private void BtnPreview_Click()
        {
            Navigate((int)PagingMode.Previous);
        }

        public void UpdateAllStaffView()
        {
            firstStaff = DataWorker.GetFirstStaff(numberOfRecPerPage);
            Staff.AllStaff.ItemsSource = null;
            Staff.AllStaff.Items.Clear();
            Staff.AllStaff.ItemsSource = FirstStaff;
            Staff.AllStaff.Items.Refresh();
        }



        #region Open Add new user window
        private RelayCommand addStaff;
        public RelayCommand AddStaff
        {
            get
            {
                return addStaff ?? new RelayCommand(obj =>
                {
                    Add_Button_Click();
                }
                    );
            }
        }

        private void Add_Button_Click()
        {
            AddStaff addStaff = new AddStaff();
            addStaff.ShowDialog();
        }
        #endregion

        #region Open Edit user window
        private RelayCommand openEditStaff;
        public RelayCommand OpenEditStaff
        {
            get
            {
                return openEditStaff ?? new RelayCommand(obj =>
                {
                    Edit_Button_Click((Auth_user)Staff.AllStaff.SelectedItem);
                }
                    );
            }
        }

        private void Edit_Button_Click(Auth_user staff)
        {
            EditStaff editStaff = new EditStaff(staff);
            editStaff.ShowDialog();
        }
        #endregion

        #region Delete staff
        private RelayCommand removeStaff;
        public RelayCommand RemoveStaff
        {
            get
            {
                return removeStaff ?? new RelayCommand(obj =>
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
                DataWorker.DeleteStaff((Auth_user)Staff.AllStaff.SelectedItem);
                UpdateAllStaffView();
            }

        }
        #endregion

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
