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

        public IActionResult GetClients()
        {
            try
            {
                var clients = _context.ClientInfos.ToList();
                throw new Exception("BURITTTT");

                if (clients.Count == 0)
                    return Ok("No record found");

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest($"Somethings error on the backend {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult SaveClients(ClientInfo c)
        {
            try
            {
                _context.ClientInfos.Add(c);
                var res = _context.SaveChanges();

                if (c.Id != 0 || c.Id != null)
                    return Ok("Saved Success");
                else
                    return Ok("Not Saved");
            }
            catch (Exception ex){
                return BadRequest($"Error happened: {ex.Message}");
            }
        }



    }
}