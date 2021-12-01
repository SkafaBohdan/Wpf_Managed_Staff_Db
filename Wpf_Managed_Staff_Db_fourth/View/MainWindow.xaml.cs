using System.Windows;
using System.Windows.Controls;
using Wpf_Managed_Staff_Db_fourth.ViewModel;


namespace Wpf_Managed_Staff_Db_fourth.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView;
        public static ListView AllPositionsView;
        public static ListView AllUsersView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManagerVM();
            AllDepartmentsView = ViewAllDepartments;
            AllPositionsView = ViewAllPositions;
            AllUsersView = ViewAllUsers;
        }
    }
}
