using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGen.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeGen.Controllers
{

    public class ClientAPIController : ControllerBase
    {
        private readonly PelayoCoopContext _context;

        public ClientAPIController(PelayoCoopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            try
            {
                var clients = _context.ClientInfos.ToList();

                if (clients.Any())
                {
                    return Ok(clients);
                }

                else
                {
                    return Ok("No records found");
                }
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }


    }
}