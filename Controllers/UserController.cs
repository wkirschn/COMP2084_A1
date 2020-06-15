using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084___A1.Models;

namespace COMP2084___A1.Controllers
{
    public class UserController : Controller
    {
        private readonly COMP2084A1Context _context;

        public UserController(COMP2084A1Context context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var cOMP2084A1Context = _context.UserIn.Include(u => u.Item);
            return View(await cOMP2084A1Context.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIn = await _context.UserIn
                .Include(u => u.Item)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userIn == null)
            {
                return NotFound();
            }

            return View(userIn);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.ProductIn, "ItemId", "Description");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,ItemId")] UserIn userIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.ProductIn, "ItemId", "Description", userIn.ItemId);
            return View(userIn);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIn = await _context.UserIn.FindAsync(id);
            if (userIn == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.ProductIn, "ItemId", "Description", userIn.ItemId);
            return View(userIn);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,ItemId")] UserIn userIn)
        {
            if (id != userIn.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInExists(userIn.UserId))
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
            ViewData["ItemId"] = new SelectList(_context.ProductIn, "ItemId", "Description", userIn.ItemId);
            return View(userIn);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIn = await _context.UserIn
                .Include(u => u.Item)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userIn == null)
            {
                return NotFound();
            }

            return View(userIn);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userIn = await _context.UserIn.FindAsync(id);
            _context.UserIn.Remove(userIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInExists(int id)
        {
            return _context.UserIn.Any(e => e.UserId == id);
        }
    }
}
