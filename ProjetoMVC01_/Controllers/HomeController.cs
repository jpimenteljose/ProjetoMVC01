using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // método que abre a página Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
