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

        #region shitcode
        //depart
        public string DepartmentName { get; set; }
        //position
        public string PositionName { get; set; }
        public decimal PositionSalary { get; set; } 
        public int PositionMaxNumber { get; set; }
        public Department PositionDepartmnet { get; set; }
        //users
        public string UserName { get; set; }
        public string UserSurName { get; set; }
        public int UserPhone { get; set; }
        public Position UserPosition { get; set; }
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
        private void OpenEditDepartmentsWindowMethod()
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow();
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditPositionWindowMethod()
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow();
            SetCenterPositionAndOpen(editPositionWindow);
        }
        private void OpenEditUserWindowMethod()
        {
            EditUserWindow editUserWindow = new EditUserWindow();
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
