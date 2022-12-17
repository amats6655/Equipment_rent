using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Equipment_rent.ViewModel
{
    class UsersVM : INotifyPropertyChanged
    {
        public static int pageIndex = 1;
        private static int numberOfRecPerPage = 10;
        public enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 }
        public static int count;
        private string pageInformation = numberOfRecPerPage + " из " + allUsers.Count;
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

        private static List<User> allUsers = DataWorker.GetAllUsers();
        #region Add Brush and Character for user
        private List<User> firstUsers = DataWorker.GetFirstUsers(numberOfRecPerPage);
        public List<User> FirstUsers
        {
            get
            {
                List<User> users = new List<User>();
                foreach (User user in firstUsers)
                {
                    char Character = user.Name[0];
                    Brush BgColor = GetBrush.getBrush(Character);
                    user.BgColor = BgColor.ToString();
                    user.Character = Character;
                    users.Add(user);
                }
                return users;
            }
            set
            {
                allUsers = value;
                NotifyPropertyChaged("AllUsers");
            }
        }
        #endregion


        public void Navigate(int mode)
        {
            count = 0;
            switch (mode)
            {
                case (int)PagingMode.Previous:
                    firstUsers = DataWorker.GetPreviousPageUsers(pageIndex, numberOfRecPerPage);
                    Users.AllUsers.ItemsSource = null;
                    Users.AllUsers.Items.Clear();
                    Users.AllUsers.ItemsSource = FirstUsers;
                    Users.AllUsers.Items.Refresh();
                    PageInformation = count + " из " + allUsers.Count;
                    break;
                case (int)PagingMode.Next:
                    firstUsers = DataWorker.GetNextPageUsers(pageIndex, numberOfRecPerPage);
                    Users.AllUsers.ItemsSource = null;
                    Users.AllUsers.Items.Clear();
                    Users.AllUsers.ItemsSource = FirstUsers;
                    Users.AllUsers.Items.Refresh();
                    PageInformation = count + " из " + allUsers.Count;
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

        public void UpdateAllUsersView()
        {
            firstUsers = DataWorker.GetFirstUsers(numberOfRecPerPage);
            Users.AllUsers.ItemsSource = null;
            Users.AllUsers.Items.Clear();
            Users.AllUsers.ItemsSource = FirstUsers;
            Users.AllUsers.Items.Refresh();
        }



        #region Open Add new user window
        private RelayCommand addUser;
        public RelayCommand AddUser
        {
            get
            {
                return addUser ?? new RelayCommand(obj =>
                {
                    Add_Button_Click();
                }
                    );
            }
        }

        private void Add_Button_Click()
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
        }
        #endregion

        #region Open Edit user window
        private RelayCommand openEditUser;
        public RelayCommand OpenEditUser
        {
            get
            {
                return openEditUser ?? new RelayCommand(obj =>
                {
                    Edit_Button_Click((User)Users.AllUsers.SelectedItem);
                }
                    );
            }
        }

        private void Edit_Button_Click(User user)
        {
            EditUser editUser = new EditUser(user);
            editUser.ShowDialog();
        }
        #endregion

        #region Delete user
        private RelayCommand removeUser;
        public RelayCommand RemoveUser
        {
            get
            {
                return removeUser ?? new RelayCommand(obj =>
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
                DataWorker.DeleteUser((User)Users.AllUsers.SelectedItem);
                UpdateAllUsersView();
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
