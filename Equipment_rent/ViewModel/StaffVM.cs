using System.Collections.Generic;
using System.ComponentModel;
using Equipment_rent.Model;
using Equipment_rent.Utilites;
using Equipment_rent.View;

namespace Equipment_rent.ViewModel;

internal class StaffVM : INotifyPropertyChanged
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

    private static List<Auth_user> allStaff = DataWorker.GetAllAuthUsers();
    private RelayCommand nextPage;
    private string pageInformation = numberOfRecPerPage + " из " + allStaff.Count;
    private RelayCommand previewPage;

    public string PageInformation
    {
        get => pageInformation;
        set
        {
            pageInformation = value;
            NotifyPropertyChaged(nameof(PageInformation));
        }
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

    private void NotifyPropertyChaged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Add Brush and Character for user

    private List<Auth_user> firstStaff = DataWorker.GetFirstStaff(numberOfRecPerPage);

    public List<Auth_user> FirstStaff
    {
        get
        {
            var users = new List<Auth_user>();
            foreach (var staff in firstStaff)
            {
                var Character = staff.LastName[0];
                var BgColor = GetBrush.getBrush(Character);
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


    #region Open Add new user window

    private RelayCommand addStaff;

    public RelayCommand AddStaff
    {
        get
        {
            return addStaff ?? new RelayCommand(obj => { Add_Button_Click(); }
            );
        }
    }

    private void Add_Button_Click()
    {
        var addStaff = new AddStaff();
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
        var editStaff = new EditStaff(staff);
        editStaff.ShowDialog();
    }

    #endregion

    #region Delete staff

    private RelayCommand removeStaff;

    public RelayCommand RemoveStaff
    {
        get { return removeStaff ?? new RelayCommand(obj => { Del_Button_Click(); }); }
    }

    private void Del_Button_Click()
    {
        var deleteWindow = new DeleteWindow();
        if (deleteWindow.ShowDialog() == true)
        {
            DataWorker.DeleteStaff((Auth_user)Staff.AllStaff.SelectedItem);
            UpdateAllStaffView();
        }
    }

    #endregion
}