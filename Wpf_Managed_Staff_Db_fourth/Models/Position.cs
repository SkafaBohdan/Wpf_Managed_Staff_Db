﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Managed_Staff_Db_fourth.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxNumber { get; set; }

        public List<User> Users { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);
            }
        }

        [NotMapped]
        public List<User> PositionUsers
        {
            get
            {
                return DataWorker.GetAllUsersByPositionId(Id);
            }
        }
    }
}
