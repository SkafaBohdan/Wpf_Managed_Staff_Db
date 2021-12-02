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
    /// Interaction logic for EditDepartment.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department department)
        {
            InitializeComponent();
            DataContext = new DataManagerVM();
            DataManagerVM.SelectedDepartment = department;
            DataManagerVM.DepartmentName = department.Name;
        }
    }
}
