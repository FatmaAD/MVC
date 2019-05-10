using mvc_project_auth.Models;
using mvc_project_auth.ViewModels;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using mvc_project_auth.Models.Entities;

namespace mvc_project_auth.Controllers
{
    public class EmployeesController : Controller
    {
        [Authorize]
        public class EmployeeController : Controller
        {
            static ApplicationDbContext context = new ApplicationDbContext();

            public ActionResult Index()
            {
                EmployeeViewModel empvm = new EmployeeViewModel();
                empvm.Employees = context.Employees.Include(e => e.Department).ToList();
                empvm.Departments = context.Departments.ToList();
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
            [Authorize(Roles ="Admin, Manager")]
            public async Task<ActionResult> Add(EmployeeViewModel emp)
            {
                if (ModelState.IsValid)
                {
                    context.Employees.Add(emp.Employee);
                    await context.SaveChangesAsync();
                    return PartialView("_parcialEmpRow", emp.Employee);
                }
                emp.Departments = context.Departments.ToList();
                return PartialView("_parcialAddEmp", emp);
            }

            [Authorize(Roles = "Admin, Manager")]
            public ActionResult Edit(int id)
            {
                ViewBag.Action = "Edit";
                Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    ViewBag.Action = "Edit";
                    EmployeeViewModel employeeVM = new EmployeeViewModel();
                    employeeVM.Employee = emp;
                    employeeVM.Departments = context.Departments.ToList();
                    return View("EmployeeForm", employeeVM);
                }
                return RedirectToAction("Index");
            }

            [HttpPost]
            [Authorize(Roles = "Admin, Manager")]
            public ActionResult Edit(EmployeeViewModel emp)
            {
                if (ModelState.IsValid)
                {
                    var oldEmp = context.Employees.FirstOrDefault(e => e.Id == emp.Employee.Id);
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

                emp.Departments = context.Departments.ToList();

                return RedirectToAction("EmployeeForm", emp);
            }

            [Authorize(Roles = "Admin, Manager")]
            public async Task<bool> Delete(int id)
            {
                Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    context.Employees.Remove(emp);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }

            public ActionResult Details(int id)
            {
                EmployeeViewModel empVM = new EmployeeViewModel();
                empVM.Employee = context.Employees.FirstOrDefault(e => e.Id == id);
                return View(empVM);
            }
        }
    }
}