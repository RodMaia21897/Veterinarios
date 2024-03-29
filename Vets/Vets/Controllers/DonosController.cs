﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vets.Data;
using Vets.Models;

namespace Vets.Controllers
{
    public class DonosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donos.ToListAsync());
        }

        // GET: Donos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donos = await _context.Donos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donos == null)
            {
                return NotFound();
            }

            return View(donos);
        }

        // GET: Donos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NIF,Sexo")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donos);
        }

        // GET: Donos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donos = await _context.Donos.FindAsync(id);
            if (donos == null)
            {
                return NotFound();
            }
            return View(donos);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NIF,Sexo")] Donos donos)
        {
            if (id != donos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonosExists(donos.Id))
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
            return View(donos);
        }

        // GET: Donos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donos = await _context.Donos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donos == null)
            {
                return NotFound();
            }

            return View(donos);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donos = await _context.Donos.FindAsync(id);
            _context.Donos.Remove(donos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonosExists(int id)
        {
            return _context.Donos.Any(e => e.Id == id);
        }
    }
}
