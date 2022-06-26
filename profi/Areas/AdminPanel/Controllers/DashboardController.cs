using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profi.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = nameof(Role.RoleType.Admin))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
