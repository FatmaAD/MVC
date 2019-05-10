using mvc_project.Models.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.ViewModels;
using System.Threading.Tasks;

namespace mvc_project.Controllers
{
    public class DepartmentController : Controller
    {
        static Model1 context = new Model1();
        // GET: Department
        public ActionResult Index()
        {
            var deps = context.departments.ToList();
            return View(deps); 
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = context.departments.FirstOrDefault(d => d.Id == id);
            ViewBag.Action = "Edit";
            return View("DepartmentForm",department);
        }

        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            if (ModelState.IsValid)
            {
                Department department = context.departments.FirstOrDefault(d => d.Id == dep.Id);
                if (department != null)
                {
                    department.Name = dep.Name;
                }
                return View("Index");
            }

            return RedirectToAction("DepartmentForm", dep);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("DepartmentForm");
        }
        [HttpPost]
        public ActionResult Add(Department dep)
        {
            if (ModelState.IsValid)
            {
                context.departments.Add(dep);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("DepartmentForm");
        }

        public async Task<bool> Delete(int id)
        {
            var emp = context.departments.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.departments.Remove(emp);
                await context.SaveChangesAsync();
                return true;
            }
                return false;

        }

        public ActionResult Details(int id)
        {
            var department = context.departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
            return View( department);
        }
    }
}