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
    public class EcoScoreTalliesController : Controller
    {
        private readonly COMP2084A1Context _context;

        public EcoScoreTalliesController(COMP2084A1Context context)
        {
            _context = context;
        }

        // GET: EcoScoreTallies
        public async Task<IActionResult> Index()
        {
            return View(await _context.EcoScoreTally.ToListAsync());
        }

        // GET: EcoScoreTallies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScoreTally = await _context.EcoScoreTally
                .FirstOrDefaultAsync(m => m.EcoScoreId == id);
            if (ecoScoreTally == null)
            {
                return NotFound();
            }

            return View(ecoScoreTally);
        }

        // GET: EcoScoreTallies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EcoScoreTallies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EcoScoreId,Material,Removal,Reuse")] EcoScoreTally ecoScoreTally)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ecoScoreTally);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ecoScoreTally);
        }

        // GET: EcoScoreTallies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScoreTally = await _context.EcoScoreTally.FindAsync(id);
            if (ecoScoreTally == null)
            {
                return NotFound();
            }
            return View(ecoScoreTally);
        }

        // POST: EcoScoreTallies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EcoScoreId,Material,Removal,Reuse")] EcoScoreTally ecoScoreTally)
        {
            if (id != ecoScoreTally.EcoScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ecoScoreTally);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EcoScoreTallyExists(ecoScoreTally.EcoScoreId))
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
            return View(ecoScoreTally);
        }

        // GET: EcoScoreTallies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScoreTally = await _context.EcoScoreTally
                .FirstOrDefaultAsync(m => m.EcoScoreId == id);
            if (ecoScoreTally == null)
            {
                return NotFound();
            }

            return View(ecoScoreTally);
        }

        // POST: EcoScoreTallies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ecoScoreTally = await _context.EcoScoreTally.FindAsync(id);
            _context.EcoScoreTally.Remove(ecoScoreTally);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EcoScoreTallyExists(int id)
        {
            return _context.EcoScoreTally.Any(e => e.EcoScoreId == id);
        }
    }
}
