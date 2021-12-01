using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Managed_Staff_Db_fourth.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Position> Positions { get; set; }
    }
}
