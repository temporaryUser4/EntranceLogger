using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntranceLogger
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public string Fullname { get {
                return Employee.Surname + " " + Employee.Name + " " + Employee.Patronymic ?? ""; } }

        public EmployeeViewModel(Employee employee)
        {
            Employee = employee;
        }

    }
}
