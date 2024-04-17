using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeGen.Entities;

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
              return _context.ClientInfos != null ? 
                          View(await _context.ClientInfos.ToListAsync()) :
                          Problem("Entity set 'PelayoCoopContext.ClientInfos'  is null.");
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

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserType,FirstName,MiddleName,LastName,Address,ZipCode,Birthdate,Age,NameOfFather,NameOfmother,CivilStatus,Religion,Occupation")] ClientInfo clientInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientInfo);
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

            var clientInfo = await _context.ClientInfos.FindAsync(id);
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
