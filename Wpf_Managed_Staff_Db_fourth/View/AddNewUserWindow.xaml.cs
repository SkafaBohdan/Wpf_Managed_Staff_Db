using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Wpf_Managed_Staff_Db_fourth.ViewModel;


namespace Wpf_Managed_Staff_Db_fourth.View
{
    /// <summary>
    /// Interaction logic for AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        public AddNewUserWindow()
        {
            InitializeComponent();
            DataContext = new DataManagerVM();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
