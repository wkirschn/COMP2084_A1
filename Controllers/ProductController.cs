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
    public class ProductController : Controller
    {
        private readonly COMP2084A1Context _context;

        public ProductController(COMP2084A1Context context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductIn.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIn = await _context.ProductIn
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (productIn == null)
            {
                return NotFound();
            }

            return View(productIn);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Material,Description,Photo,EcoScore")] ProductIn productIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productIn);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIn = await _context.ProductIn.FindAsync(id);
            if (productIn == null)
            {
                return NotFound();
            }
            return View(productIn);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Material,Description,Photo,EcoScore")] ProductIn productIn)
        {
            if (id != productIn.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInExists(productIn.ItemId))
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
            return View(productIn);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIn = await _context.ProductIn
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (productIn == null)
            {
                return NotFound();
            }

            return View(productIn);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productIn = await _context.ProductIn.FindAsync(id);
            _context.ProductIn.Remove(productIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInExists(int id)
        {
            return _context.ProductIn.Any(e => e.ItemId == id);
        }
    }
}
