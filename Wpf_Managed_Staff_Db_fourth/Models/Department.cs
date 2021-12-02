using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public List<Position> DepartmentPosition
        {
            get
            {
                return DataWorker.GetAllPositionsByDepartmentId(Id);
            }
        }
    }
}
