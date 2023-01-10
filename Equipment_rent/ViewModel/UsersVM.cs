using System.Collections.Generic;
using System.ComponentModel;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel;

internal class UsersVM : INotifyPropertyChanged
{
    public enum PagingMode
    {
        Next = 2,
        Previous = 3
    }

    public static int pageIndex = 1;
    private static readonly int numberOfRecPerPage = 10;
    public static int count;

    private static List<User> allUsers = DataWorker.GetAllUsers();
    private RelayCommand nextPage;
    private string pageInformation = numberOfRecPerPage + " из " + allUsers.Count;
    private RelayCommand previewPage;

    public string PageInformation
    {
        get => pageInformation;
        set
        {
            pageInformation = value;
            NotifyPropertyChanged(nameof(PageInformation));
        }
    }

    public static List<User> AllUsers
    {
        get
        {
            var users = new List<User>();
            foreach (var user in allUsers)
            {
                var Character = user.Name[0];
                var BgColor = GetBrush.getBrush(Character);
                user.BgColor = BgColor.ToString();
                user.Character = Character;
                users.Add(user);
            }

            return users;
        }
        set => allUsers = value;
    }

    public RelayCommand NextPage
    {
        get { return nextPage ?? new RelayCommand(obj => { BtnNext_Click(); }); }
    }

    public RelayCommand PreviewPage
    {
        get { return previewPage ?? new RelayCommand(obj => { BtnPreview_Click(); }); }
    }

    public event PropertyChangedEventHandler PropertyChanged;


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

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Add Brush and Character for user

    private static List<User> firstUsers = DataWorker.GetFirstUsers(numberOfRecPerPage);

    public static List<User> FirstUsers
    {
        get
        {
            var users = new List<User>();
            foreach (var user in firstUsers)
            {
                var Character = user.Name[0];
                var BgColor = GetBrush.getBrush(Character);
                user.BgColor = BgColor.ToString();
                user.Character = Character;
                users.Add(user);
            }

            return users;
        }
        set => allUsers = value;
    }

    #endregion


    #region Open Add new user window

    private RelayCommand addUser;

    public RelayCommand AddUser
    {
        get
        {
            return addUser ?? new RelayCommand(obj => { Add_Button_Click(); }
            );
        }
    }

    private void Add_Button_Click()
    {
        var addUser = new AddUser();
        addUser.ShowDialog();
    }

    #endregion

    #region Open Edit user window

    private readonly RelayCommand openEditUser;

    public RelayCommand OpenEditUser
    {
        get
        {
            return openEditUser ?? new RelayCommand(obj => { Edit_Button_Click((User)Users.AllUsers.SelectedItem); }
            );
        }
    }

    private void Edit_Button_Click(User user)
    {
        var editUser = new EditUser(user);
        editUser.ShowDialog();
    }

    #endregion

    #region Delete user

    private RelayCommand removeUser;

    public RelayCommand RemoveUser
    {
        get { return removeUser ?? new RelayCommand(obj => { Del_Button_Click(); }); }
    }

    private void Del_Button_Click()
    {
        var deleteWindow = new DeleteWindow();
        if (deleteWindow.ShowDialog() == true)
        {
            DataWorker.DeleteUser((User)Users.AllUsers.SelectedItem);
            UpdateAllUsersView();
        }
    }

    #endregion
}