using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TastyMVC.Data;
using TastyMVC.Models;
using TastyMVC.ViewModels;

namespace TastyMVC.Controllers
{
    public class HomeController : Controller
    {
        private Repository repository;
        public HomeController()
        {
            repository = new Repository();
        }
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!roleManager.RoleExists("IsAdmin"))
            {
                var role = new IdentityRole();
                role.Name = "IsAdmin";
                roleManager.Create(role);

                var _context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
                var user = UserManager.Users.Where(x => x.Email.ToLower() == "oclick021@gmail.com").FirstOrDefault();
                if (user != null)
                {
                    UserManager.AddToRole(user.Id, "IsAdmin");
                }
            }
            if (!roleManager.RoleExists("IsUser"))
            {
                var role = new IdentityRole();
                role.Name = "IsUser";
                roleManager.Create(role);
            }

            var homeViewModel = new HomePageViewModel() { 
                PublishedRecipes = repository.GetPublishedRecipes(),
                Categories = repository.GetAllCategories()};
            return View(homeViewModel);
        }
        [Route("About")]

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("Contact")]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}