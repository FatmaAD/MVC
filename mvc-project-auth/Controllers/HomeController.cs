using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using mvc_project_auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project_auth.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        ApplicationRoleManager roleManager;
        ApplicationUserManager userManager;

        public ApplicationDbContext Context
        {
            get
            {
                return context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
        }
        public ApplicationRoleManager RoleManager {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }
        public ApplicationUserManager UserManager {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
        }



        public ActionResult Index()
        {
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole manager = new IdentityRole("Manager");
            IdentityRole employee = new IdentityRole("Employee");

            //to save the roles in the db
            RoleManager.Create(admin);
            RoleManager.Create(manager);
            RoleManager.Create(employee);
            //to assign the roles to users
            ApplicationUser adminUser = UserManager.Users.FirstOrDefault(e => e.Email.StartsWith("admin"));
            ApplicationUser managerUser = UserManager.Users.FirstOrDefault(e => e.Email.StartsWith("manager"));
            ApplicationUser empUser = UserManager.Users.FirstOrDefault(e => e.Email.StartsWith("employee" ));

        
            UserManager.AddToRole(adminUser.Id, admin.Name);
            UserManager.AddToRole(managerUser.Id, manager.Name);
            UserManager.AddToRole(empUser.Id, employee.Name);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}