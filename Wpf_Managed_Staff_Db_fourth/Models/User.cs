using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Managed_Staff_Db_fourth.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Phone { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
