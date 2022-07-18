using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SporSalonu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SporSalonu.Controllers
{
    public class BuyController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult KayitOl()
        {
            return View();
        }
       

        [HttpPost]
        [AllowAnonymous]
        public IActionResult KayitOl(kayitolmodel liste1)
        {
            if (ModelState.IsValid)
            {
                Context kayitdbisle1 = new Context();
                string kayitekledurum = kayitdbisle1.Kayitol(liste1);
                ViewData["sonucmesaj"] = kayitekledurum;
                ModelState.Clear();
            }
            return View();
        }
    }
}
