using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf_Managed_Staff_Db_fourth.Models;
using Wpf_Managed_Staff_Db_fourth.ViewModel;

namespace Wpf_Managed_Staff_Db_fourth.View
{
    /// <summary>
    /// Interaction logic for EditPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position position)
        {
            InitializeComponent();
            DataContext = new DataManagerVM();
            DataManagerVM.SelectedPosition = position;
            DataManagerVM.PositionName = position.Name;
            DataManagerVM.PositionSalary = position.Salary;
            DataManagerVM.PositionMaxNumber = position.MaxNumber;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
