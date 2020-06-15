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
    public class ItemInfoesController : Controller
    {
        private readonly COMP2084A1Context _context;

        public ItemInfoesController(COMP2084A1Context context)
        {
            _context = context;
        }

        // GET: ItemInfoes
        public async Task<IActionResult> Index()
        {
            var cOMP2084A1Context = _context.ItemInfo.Include(i => i.EcoScore);
            return View(await cOMP2084A1Context.ToListAsync());
        }

        // GET: ItemInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInfo = await _context.ItemInfo
                .Include(i => i.EcoScore)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (itemInfo == null)
            {
                return NotFound();
            }

            return View(itemInfo);
        }

        // GET: ItemInfoes/Create
        public IActionResult Create()
        {
            ViewData["EcoScoreId"] = new SelectList(_context.EcoScoreTally, "EcoScoreId", "Material");
            return View();
        }

        // POST: ItemInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,UserName,Description,Photo,EcoScoreId")] ItemInfo itemInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EcoScoreId"] = new SelectList(_context.EcoScoreTally, "EcoScoreId", "Material", itemInfo.EcoScoreId);
            return View(itemInfo);
        }

        // GET: ItemInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInfo = await _context.ItemInfo.FindAsync(id);
            if (itemInfo == null)
            {
                return NotFound();
            }
            ViewData["EcoScoreId"] = new SelectList(_context.EcoScoreTally, "EcoScoreId", "Material", itemInfo.EcoScoreId);
            return View(itemInfo);
        }

        // POST: ItemInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,UserName,Description,Photo,EcoScoreId")] ItemInfo itemInfo)
        {
            if (id != itemInfo.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInfoExists(itemInfo.ItemId))
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
            ViewData["EcoScoreId"] = new SelectList(_context.EcoScoreTally, "EcoScoreId", "Material", itemInfo.EcoScoreId);
            return View(itemInfo);
        }

        // GET: ItemInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInfo = await _context.ItemInfo
                .Include(i => i.EcoScore)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (itemInfo == null)
            {
                return NotFound();
            }

            return View(itemInfo);
        }

        // POST: ItemInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemInfo = await _context.ItemInfo.FindAsync(id);
            _context.ItemInfo.Remove(itemInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInfoExists(int id)
        {
            return _context.ItemInfo.Any(e => e.ItemId == id);
        }
    }
}
