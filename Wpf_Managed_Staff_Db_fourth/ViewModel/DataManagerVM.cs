using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf_Managed_Staff_Db_fourth.Models;
using Wpf_Managed_Staff_Db_fourth.View;


namespace Wpf_Managed_Staff_Db_fourth.ViewModel
{
    public class DataManagerVM : INotifyPropertyChanged
    {
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments 
        {
            get { return allDepartments; } 
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            } 
        }

        private List<User> allUsers = DataWorker.GetAllUsers();
        public List<User> AllUsers
        {
            get { return allUsers; }
            set
            {
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }

        private List<Position> allPosition = DataWorker.GetAllPosition();
        public List<Position> AllPosition
        {
            get { return allPosition; }
            set
            {
                allPosition = value;
                NotifyPropertyChanged("AllPosition");
            }
        }

        #region view model property
        //depart
        public static string DepartmentName { get; set; }
        //position
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; } 
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartmnet { get; set; }
        //users
        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static int UserPhone { get; set; }
        public static Position UserPosition { get; set; }
        //property for selected item
        public TabItem SelectedTabItem { get; set; }
        public static User SelectedUser { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #endregion

        #region COMMAND TO ADD
        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ??
                    new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "";
                        if(DepartmentName == null || DepartmentName.Trim().Length == 0)
                        {
                            SetRedBlockControl(window, "DepartNameBlock");
                        }
                        else
                        {
                            resultStr = DataWorker.CreateDepartment(DepartmentName);
                            UpdateAllDataViews();
                            ShowMessageToUser(resultStr);
                            SetNullPropertyValues();
                            window.Close();
                        }
                    });
            }
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ??
                    new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "";
                        if (PositionName == null || PositionName.Trim().Length == 0)
                        {
                            SetRedBlockControl(window, "NameBlock");
                        }
                        if(PositionSalary == 0)
                        {
                            SetRedBlockControl(window, "SalaryBlock");
                        }
                        if (PositionMaxNumber == 0)
                        {
                            SetRedBlockControl(window, "MaxBlock");
                        }
                        if (PositionDepartmnet == null)
                        {
                            MessageBox.Show("Choose Department");
                        }
                        else
                        {
                            resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartmnet);
                            updateAllPositionView();
                            ShowMessageToUser(resultStr);
                            SetNullPropertyValues();
                            window.Close();
                        }
                    });
            }
        }


        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ??
                    new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "";
                        if (UserName == null || UserName.Trim().Length == 0)
                        {
                            SetRedBlockControl(window, "NameBlock");
                        }
                        if (UserSurName == null || UserSurName.Trim().Length == 0)
                        {
                            SetRedBlockControl(window, "SurNameBlock");
                        }
                        //if (UserPhone.ToString() == null)
                        //{
                        //    SetRedBlockControl(window, "PhoneBlock");
                        //}
                        if (UserPosition == null)
                        {
                            MessageBox.Show("Choose Position");
                        }
                        else
                        {
                            resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                            updateAllUsersView();
                            ShowMessageToUser(resultStr);
                            SetNullPropertyValues();
                            window.Close();
                        }
                    });
            }
        }

        #endregion

        #region COMMAND TO DELETE
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ??
                    new RelayCommand(obj =>
                    {
                        string resultStr = "Not Selected";
                        //user
                        if(SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                        {
                            resultStr = DataWorker.DeleteUser(SelectedUser);
                            UpdateAllDataViews();
                        }

                        //position
                        if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                        {
                            resultStr = DataWorker.DeletePosition(SelectedPosition);
                            UpdateAllDataViews();
                        }

                        //department
                        if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                        {
                            resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                            UpdateAllDataViews();
                        }

                        //update view
                        SetNullPropertyValues();
                        ShowMessageToUser(resultStr);
                    });
            }
        }
        #endregion

        #region COMMAND TO EDIT
        private RelayCommand openEditItemWindow;
        public RelayCommand OpenEditItemWindow
        {
            get
            {
                return openEditItemWindow ??
                    new RelayCommand(obj =>
                    {
                        string resultStr = "Not Selected";
                        //user
                        if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                        {
                            OpenEditUserWindowMethod(SelectedUser);
                        }

                        //position
                        if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                        {
                            OpenEditPositionWindowMethod(SelectedPosition);
                        }

                        //department
                        if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                        {
                            OpenEditDepartmentsWindowMethod(SelectedDepartment);
                        }
                    });
            }
        }


        private RelayCommand editUser;
        public RelayCommand EditUser
        {
            get
            {
                return editUser ??
                    new RelayCommand(obj => 
                    {
                        Window window = obj as Window;
                        string resultStr = "Not Selected employee";
                        string notSelectPosition = "Not selected position";
                        if (SelectedUser != null)
                        {
                            if(UserPosition != null)
                            {
                                resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition);
                                UpdateAllDataViews();
                                SetNullPropertyValues();
                                ShowMessageToUser(resultStr);
                                window.Close();
                            }
                            else
                                ShowMessageToUser(notSelectPosition);
                        }
                        else
                            ShowMessageToUser(resultStr);
                    });
            }
        }

        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ??
                    new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "Not Selected position";
                        string notSelectDepartments= "Not selected departments";
                        if (SelectedPosition != null)
                        {
                            if (PositionDepartmnet != null)
                            {
                                resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionSalary, PositionMaxNumber, PositionDepartmnet);

                                UpdateAllDataViews();
                                SetNullPropertyValues();
                                ShowMessageToUser(resultStr);
                                window.Close();
                            }
                            else
                                ShowMessageToUser(notSelectDepartments);
                        }
                        else
                            ShowMessageToUser(resultStr);
                    });
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ??
                    new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "Not Selected department";
                        if (SelectedDepartment != null)
                        {

                            resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);
                            UpdateAllDataViews();
                            SetNullPropertyValues();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                            ShowMessageToUser(resultStr);
                    });
            }
        }
        #endregion

        #region COMMANDS TO OPEN METHOD
        //Commands open window
        private RelayCommand openAddNewDepartmentWindow;
        public RelayCommand OpenAddNewDepartmentWindow
        {
            get
            {
                return openAddNewDepartmentWindow ??
                    new RelayCommand(obj =>
                    {
                        OpenAddDepartmentsWindowMethod();
                    });
            }
        }

        private RelayCommand openAddNewPositiontWindow;
        public RelayCommand OpenAddNewPositiontWindow
        {
            get
            {
                return openAddNewPositiontWindow ??
                    new RelayCommand(obj =>
                    {
                        OpenAddPositionWindowMethod();
                    });
            }
        }

        private RelayCommand openAddNewUserWindow;
        public RelayCommand OpenAddNewUserWindow
        {
            get
            {
                return openAddNewUserWindow ??
                    new RelayCommand(obj =>
                    {
                        OpenAddUserWindowMethod();
                    });
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOW
        //enter open add window
        private void OpenAddDepartmentsWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        private void OpenAddUserWindowMethod()
        {
            AddNewUserWindow newUserWindow = new AddNewUserWindow();
            SetCenterPositionAndOpen(newUserWindow);
        }

        //enter open edit window
        private void OpenEditDepartmentsWindowMethod(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }
        private void OpenEditUserWindowMethod(User user)
        {
            EditUserWindow editUserWindow = new EditUserWindow(user);
            SetCenterPositionAndOpen(editUserWindow);
        }
        #endregion

        #region UPDATE VIEWS
        private void SetNullPropertyValues()
        {
            //users
            UserName = null;
            UserSurName = null; 
            UserPhone = 0; 
            UserPosition = null;
            //department
            DepartmentName = null;

            //position
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartmnet = null;
        }
        private void UpdateAllDataViews()
        {
            updateAllDepartmentsView();
            updateAllPositionView();
            updateAllUsersView();
        }
        private void updateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void updateAllPositionView()
        {
            AllPosition = DataWorker.GetAllPosition();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPosition;
            MainWindow.AllPositionsView.Items.Refresh();
        }
        private void updateAllUsersView()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }
        #endregion

        private void SetRedBlockControl(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
