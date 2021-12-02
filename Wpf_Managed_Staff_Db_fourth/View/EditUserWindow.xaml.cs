using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf_Managed_Staff_Db_fourth.Models;
using Wpf_Managed_Staff_Db_fourth.ViewModel;

namespace Wpf_Managed_Staff_Db_fourth.View
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(User userToEdit)
        {
            InitializeComponent();
            DataContext = new DataManagerVM();
            DataManagerVM.SelectedUser = userToEdit;
            DataManagerVM.UserName = userToEdit.Name;
            DataManagerVM.UserSurName = userToEdit.SurName;
            DataManagerVM.UserPhone = userToEdit.Phone;
            DataManagerVM.UserPosition = userToEdit.UserPosition;
        }
    }
}
