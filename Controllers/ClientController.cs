using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeGen.Entities;
using CodeGen.ViewModels;

namespace CodeGen.Controllers
{
    public class ClientController : Controller
    {
        private readonly PelayoCoopContext _context;

        public ClientController(PelayoCoopContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            var clientInfos = (
                from clientInfo in _context.ClientInfos
                join usertype in _context.UserTypes
                on clientInfo.UserType equals usertype.Id
                select new ClientInfoViewModel{
                    Id = clientInfo.Id,
                    UserType = clientInfo.UserType,
                    UserTypeName = usertype.Name,
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
                }
            ).ToList();

            return View(clientInfos);   
            
            // return _context.ClientInfos != null ?
            //             View(await _context.ClientInfos.ToListAsync()) :
            //             Problem("Entity set 'PelayoCoopContext.ClientInfos'  is null.");
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return NotFound();
            }

            var clientInfo = await _context.ClientInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientInfo == null)
            {
                return NotFound();
            }

            return View(clientInfo);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientInfoViewModel clientInfo)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(clientInfo);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return NotFound();
            }

            var clientInfo = await _context.ClientInfos.Where(q => q.Id == id)
                                        .Select(q => new ClientInfoViewModel{
                                            Id = q.Id,
                                            UserType = q.UserType,
                                            FirstName = q.FirstName,
                                            MiddleName = q.MiddleName,
                                            LastName = q.LastName,
                                            Address = q.Address,
                                            ZipCode = q.ZipCode,
                                            Birthdate = q.Birthdate,
                                            Age = q.Age,
                                            NameOfFather = q.NameOfFather,
                                            NameOfmother = q.NameOfmother,
                                            CivilStatus = q.CivilStatus,
                                            Religion = q.Religion,
                                            Occupation = q.Occupation,
                                        })
                                        .FirstOrDefaultAsync();
            if (clientInfo == null)
            {
                return NotFound();
            }
            return View(clientInfo);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ClientInfoViewModel clientInfo)
        {
            if (id != clientInfo.Id)
            {
                return NotFound();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clientInfo);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return NotFound();
            }

            var clientInfo = await _context.ClientInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientInfo == null)
            {
                return NotFound();
            }

            return View(clientInfo);
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
            return RedirectToAction(nameof(Index));
        }

        private bool ClientInfoExists(int id)
        {
            return (_context.ClientInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
