using mvc_project.Models.Entities;
using System.Collections.Generic;

namespace mvc_project.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }

    }
}