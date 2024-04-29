using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using CodeGen.Entities;
using CodeGen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeGen.Controllers
{
    public class PageController : Controller
    {
        private readonly PelayoCoopContext _context;

        public PageController(PelayoCoopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        


    }
}