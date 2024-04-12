using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeGen.Entities;
using CodeGen.ViewModel;

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
            var client = (
                from clientInfo in _context.ClientInfos
                join usertype in _context.UserTypes
                on clientInfo.UserType equals usertype.Id

                select new ClientInfoViewModel
                {
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
            ).AsAsyncEnumerable();


            if (client != null)
                return View(client);
            else
                return Problem("walay clients");

            //   return _context.ClientInfos != null ? 
            //               View(await _context.ClientInfos.ToListAsync()) :
            //               Problem("Entity set 'PelayoCoopContext.ClientInfos'  is null.");
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
        public async Task<IActionResult> Create(ClientInfo clientInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientInfo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return NotFound();
            }

            var clientInfo = await _context.ClientInfos.Where(x => x.Id == id)
                                            .Select(x => new ClientInfoViewModel
                                            {
                                                Id = x.Id,
                                                UserType = x.UserType,
                                                FirstName = x.FirstName,
                                                MiddleName = x.MiddleName,
                                                LastName = x.LastName,
                                                Address = x.Address,
                                                ZipCode = x.ZipCode,
                                                Birthdate = x.Birthdate,
                                                Age = x.Age,
                                                NameOfFather = x.NameOfFather,
                                                NameOfmother = x.NameOfmother,
                                                CivilStatus = x.CivilStatus,
                                                Religion = x.Religion,
                                                Occupation = x.Occupation,
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserType,FirstName,MiddleName,LastName,Address,ZipCode,Birthdate,Age,NameOfFather,NameOfmother,CivilStatus,Religion,Occupation")] ClientInfo clientInfo)
        {
            if (id != clientInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientInfo);
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
