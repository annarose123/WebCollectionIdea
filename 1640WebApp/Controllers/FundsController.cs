﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebApp.Data;

namespace _1640WebApp.Controllers
{
    public class FundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funds
        public async Task<IActionResult> Index()
        {
              return _context.Funds != null ? 
                          View(await _context.Funds.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Funds'  is null.");
        }

        // GET: Funds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funds == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funds == null)
            {
                return NotFound();
            }

            return View(funds);
        }

        // GET: Funds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameFund,Content,Datetime")] Funds funds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funds);
        }

        // GET: Funds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funds == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds.FindAsync(id);
            if (funds == null)
            {
                return NotFound();
            }
            return View(funds);
        }

        // POST: Funds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameFund,Content,Datetime")] Funds funds)
        {
            if (id != funds.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundsExists(funds.Id))
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
            return View(funds);
        }

        // GET: Funds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funds == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funds == null)
            {
                return NotFound();
            }

            return View(funds);
        }

        // POST: Funds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Funds'  is null.");
            }
            var funds = await _context.Funds.FindAsync(id);
            if (funds != null)
            {
                _context.Funds.Remove(funds);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundsExists(int id)
        {
          return (_context.Funds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
