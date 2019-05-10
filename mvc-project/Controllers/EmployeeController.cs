using mvc_project.Models;
using mvc_project.Models.Entities;
using mvc_project.ViewModels;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace mvc_project.Controllers
{
    public class EmployeeController : Controller
    {
        static Model1 context = new Model1(); 

        // GET: Home
        public ActionResult Index()
        {
            EmployeeViewModel empvm = new EmployeeViewModel();
            empvm.Employees = context.employees.Include(e => e.Department).ToList();
            empvm.Departments = context.departments.ToList();
            return View(empvm);
        }

        //[HttpGet]
        //public ActionResult Add()
        //{
        //    EmployeeViewModel employeeVM = new EmployeeViewModel();
        //    employeeVM.Departments = context.departments.ToList();
        //    ViewBag.Action = "Add";
        //    return View("EmployeeForm", employeeVM);
        //}

        [HttpPost]
        public async Task<ActionResult> Add(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                context.employees.Add(emp.Employee);
                await context.SaveChangesAsync();
                return PartialView("_parcialEmpRow", emp.Employee);
            }
            emp.Departments = context.departments.ToList();
            return PartialView("_parcialAddEmp", emp);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Employee emp = context.employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                ViewBag.Action = "Edit";
                EmployeeViewModel employeeVM = new EmployeeViewModel();
                employeeVM.Employee = emp;
                employeeVM.Departments = context.departments.ToList();
                return View("EmployeeForm", employeeVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var oldEmp = context.employees.FirstOrDefault(e => e.Id == emp.Employee.Id);
                if (oldEmp != null)
                {
                    oldEmp.Name = emp.Employee.Name;
                    oldEmp.Age = emp.Employee.Age;
                    oldEmp.Gender = emp.Employee.Gender;
                    oldEmp.Salary = emp.Employee.Salary;
                    oldEmp.Fk_DepartmentId = emp.Employee.Fk_DepartmentId;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
           
            emp.Departments = context.departments.ToList();

            return RedirectToAction("EmployeeForm", emp);
        }

        public async Task<bool> Delete(int id)
        {
            Employee emp = context.employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.employees.Remove(emp);
                await context.SaveChangesAsync();
                return true;
            }
                return false;
        }

        public ActionResult Details(int id)
        {
            EmployeeViewModel empVM = new EmployeeViewModel();
            empVM.Employee = context.employees.FirstOrDefault(e => e.Id == id);
            return View(empVM);
        }
    }
}

