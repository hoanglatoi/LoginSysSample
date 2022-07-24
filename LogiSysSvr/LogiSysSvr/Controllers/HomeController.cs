using LogiSysSvr.Data;
using LogiSysSvr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LogiSysSvr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MyIdentifyUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(UserManager<MyIdentifyUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.Name != null)
            {
                // loginユーザー
                var user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                var userRoles = _userManager.GetRolesAsync(user).Result;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
