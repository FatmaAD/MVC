using mvc_project_auth.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project_auth.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }

    }
}