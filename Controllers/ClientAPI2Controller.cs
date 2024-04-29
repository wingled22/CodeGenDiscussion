using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeGen.Entities;
using CodeGen.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CodeGen.Controllers
{
    public class ClientAPI2Controller : Controller
    {
        private readonly PelayoCoopContext _context;

        public ClientAPI2Controller(PelayoCoopContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> GetAllClients()
        {
            // var clientInfos = (
            //     from clientInfo in _context.ClientInfos
            //     join usertype in _context.UserTypes
            //     on clientInfo.UserType equals usertype.Id
            //     select new ClientInfoViewModel{
            //         Id = clientInfo.Id,
            //         UserType = clientInfo.UserType,
            //         UserTypeName = usertype.Name,
            //         FirstName = clientInfo.FirstName,
            //         MiddleName = clientInfo.MiddleName,
            //         LastName = clientInfo.LastName,
            //         Address = clientInfo.Address,
            //         ZipCode = clientInfo.ZipCode,
            //         Birthdate = clientInfo.Birthdate,
            //         Age = clientInfo.Age,
            //         NameOfFather = clientInfo.NameOfFather,
            //         NameOfmother = clientInfo.NameOfmother,
            //         CivilStatus = clientInfo.CivilStatus,
            //         Religion = clientInfo.Religion,
            //         Occupation = clientInfo.Occupation,
            //     }
            // ).ToList();

            return Ok(_context.ClientInfos.ToList());   
        }

        // GET: Client/Details/5
        public async Task<IActionResult> GetClientById(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return BadRequest("Requested data not found");
            }

            var clientInfo = await _context.ClientInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientInfo == null)
            {
                return BadRequest("Requested data not found");
            }

            return Ok(clientInfo);
        }

       

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientInfoViewModel clientInfo)
        {
            // if (ModelState.IsValid)
            // {
                ClientInfo c = new ClientInfo
                {
                    Id = clientInfo.Id,
                    UserType = clientInfo.UserType,
                    FirstName = clientInfo.FirstName,
                    MiddleName = clientInfo.MiddleName,
                    LastName = clientInfo.LastName,
                    Address = clientInfo.Address,
                    ZipCode = clientInfo.ZipCode,
                    Birthdate = clientInfo.Birthdate,
                    Age = clientInfo.Age,
                    NameOfFather = clientInfo.NameOfFather,
                    NameOfmother = clientInfo.NameOfmother,
                    CivilStatus = clientInfo.CivilStatus,
                    Religion = clientInfo.Religion,
                    Occupation = clientInfo.Occupation,
                };
                _context.ClientInfos.Add(c);
                await _context.SaveChangesAsync();

                return Ok();
            // }
            // return BadRequest("Data sent invalid..");
        }

        
       
        [HttpPost]
        public async Task<IActionResult> UpdateClient(int id,  ClientInfoViewModel clientInfo)
        {
            if (id != clientInfo.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var client  = await _context.ClientInfos.FirstOrDefaultAsync(x => x.Id == id);
                    client.UserType = clientInfo.UserType;
                    client.FirstName = clientInfo.FirstName;
                    client.MiddleName = clientInfo.MiddleName;
                    client.LastName = clientInfo.LastName;
                    client.Address = clientInfo.Address;
                    client.ZipCode = clientInfo.ZipCode;
                    client.Birthdate = clientInfo.Birthdate;
                    client.Age = clientInfo.Age;
                    client.NameOfFather = clientInfo.NameOfFather;
                    client.NameOfmother = clientInfo.NameOfmother;
                    client.CivilStatus = clientInfo.CivilStatus;
                    client.Religion = clientInfo.Religion;
                    client.Occupation = clientInfo.Occupation;

                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientInfoExists(clientInfo.Id))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(clientInfo);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientInfos == null)
            {
                return Problem("Entity set 'PelayoCoopContext.ClientInfos'  is null.");
            }
            var clientInfo = await _context.ClientInfos.FindAsync(id);
            if (clientInfo != null)
            {
                _context.ClientInfos.Remove(clientInfo);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ClientInfoExists(int id)
        {
            return (_context.ClientInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
