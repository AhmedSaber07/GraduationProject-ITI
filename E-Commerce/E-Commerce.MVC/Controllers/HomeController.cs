﻿using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}