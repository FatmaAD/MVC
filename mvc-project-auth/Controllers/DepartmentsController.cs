using mvc_project_auth.Models;
using mvc_project_auth.Models.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace mvc_project_auth.Controllers
{
    [Authorize(Roles="Admin, Manager")]
    public class DepartmentsController : Controller
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var deps = context.Departments.ToList();
            return View(deps);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var department = context.Departments.FirstOrDefault(d => d.Id == id);
            ViewBag.Action = "Edit";
            return View("DepartmentForm", department);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Department dep)
        {
            if (ModelState.IsValid)
            {
                Department department = context.Departments.FirstOrDefault(d => d.Id == dep.Id);
                if (department != null)
                {
                    department.Name = dep.Name;
                }
                return View("Index");
            }

            return RedirectToAction("DepartmentForm", dep);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("DepartmentForm");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Department dep)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(dep);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("DepartmentForm");
        }

        [Authorize(Roles = "Admin")]
        public async Task<bool> Delete(int id)
        {
            var emp = context.Departments.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Departments.Remove(emp);
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public ActionResult Details(int id)
        {
            var department = context.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
            return View(department);
        }

    }
}