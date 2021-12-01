using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Managed_Staff_Db_fourth.Data;


namespace Wpf_Managed_Staff_Db_fourth.Models
{
    public static class DataWorker
    {
        // all departments
        public static List<Department> GetAllDepartments()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        // all position
        public static List<Position> GetAllPosition()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }

        // all users
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }


        //create department
        public static string CreateDepartment(string name)
        {
            string result = "Exists";
            using(ApplicationContext db = new ApplicationContext())
            {
                // if exists
                bool checkExist = db.Departments.Any(el => el.Name == name);
                if (!checkExist)
                {
                    Department newDepartment = new Department() { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Good add!";
                }
                return result;
            }
        }

        //create position
        public static string CreatePosition(string name, decimal salary,
            int maxNumber, Department department)
        {
            string result = "Exists";
            using (ApplicationContext db = new ApplicationContext())
            {
                // if exists position
                bool checkExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkExist)
                {
                    Position newPositions = new Position()
                    { 
                        Name = name, 
                        Salary = salary, 
                        MaxNumber = maxNumber, 
                        Department = department 
                    };
                    db.Positions.Add(newPositions);
                    db.SaveChanges();
                    result = "Good add!";
                }
                return result;
            }
        }

        //create user
        public static string CreateUser(string name, string surName, int phone, Position position)
        {
            string result = "Exists";
            using (ApplicationContext db = new ApplicationContext())
            {
                // if exists
                bool checkExist = db.Users.Any(el => el.Name == name && 
                el.SurName == surName && el.Position == position);
                if (!checkExist)
                {
                    User newUser = new User() 
                    { 
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Good add!";
                }
                return result;
            }
        }


        //edit department
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Not edit";
            using (ApplicationContext db = new ApplicationContext())
            {
                //  if exists

                Department editDepartment = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                editDepartment.Name = newName;
                db.SaveChanges();
                result = "Edit department " + editDepartment.Name;
            }
            return result;
        }

        //edit position
        public static string EditPosition(Position oldPosition, string newName, decimal newSalary, int newMaxNumber, Department department)
        {
            string result = "Not edit";
            using (ApplicationContext db = new ApplicationContext())
            {
                //  if exists

                Position editPosition = db.Positions.FirstOrDefault(d => d.Id == oldPosition.Id);
                editPosition.Name = newName;
                editPosition.Salary = newSalary;
                editPosition.MaxNumber = newMaxNumber;
                editPosition.DepartmentId = department.Id;
                db.SaveChanges();
                result = "Edit position " + editPosition.Name;
            }
            return result;
        }

        //edit user
        public static string EditUser(User oldUser, string newName, string newSurName, int newPhone, Position newPosition)
        {
            string result = "Not edit";
            using (ApplicationContext db = new ApplicationContext())
            {
                User editUser = db.Users.FirstOrDefault(d => d.Id == oldUser.Id);
                if (editUser != null)
                {
                    editUser.Name = newName;
                    editUser.SurName = newSurName;
                    editUser.Phone = newPhone;
                    editUser.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = "Edit user " + editUser.Name;
                }
               
            }
            return result;
        }


        //delete department
        public static string DeleteDepartment(Department department)
        {
            string result = "Not exists";
            using (ApplicationContext db = new ApplicationContext())
            {
                // if exists
                bool checkExist = db.Departments.Any(el => el.Id == department.Id);
                if (checkExist)
                {
                    db.Departments.Remove(department);
                    db.SaveChanges();
                    result = "Delete department " + department.Name;
                }
                return result;
            }
        }

        //delete position
        public static string DeletePosition(Position position)
        {
            string result = "Not position";
            using (ApplicationContext db = new ApplicationContext())
            {
                // if exists
                bool checkExist = db.Positions.Any(el => el.Id == position.Id);
                if (checkExist)
                {
                    db.Positions.Remove(position);
                    db.SaveChanges();
                    result = "Delete position: " + position.Name;
                }
                return result;
            }
        }

        //delete user
        public static string DeleteUser(User user)
        {
            string result = "Not user";
            using (ApplicationContext db = new ApplicationContext())
            {
                // if exists
                bool checkExist = db.Users.Any(el => el.Id == user.Id);
                if (checkExist)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    result = "Delete user: " + user.Name;
                }
                return result;
            }
        }
    }
}
