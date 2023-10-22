using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class InstitutesController : Controller
    {
        private readonly InstituteDbContext db;

        public InstitutesController(InstituteDbContext db)
        {
            this.db = db;
        }

       
        public async Task<IActionResult> Index()
        {
              return db.Institutes != null ? 
                          View(await db.Institutes.ToListAsync()) :
                          Problem("Entity set 'InstituteDbContext.Institutes'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Institutes == null)
            {
                return NotFound();
            }

            var institute = await db.Institutes
                .FirstOrDefaultAsync(m => m.InstituteId == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }
       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteId,InstituteName,Established")] Institute institute)
        {
            if (ModelState.IsValid)
            {
                db.Add(institute);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institute);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Institutes == null)
            {
                return NotFound();
            }

            var institute = await db.Institutes.FindAsync(id);
            if (institute == null)
            {
                return NotFound();
            }
            return View(institute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteId,InstituteName,Established")] Institute institute)
        {
            if (id != institute.InstituteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(institute);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(institute.InstituteId))
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
            return View(institute);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Institutes == null)
            {
                return NotFound();
            }

            var institute = await db.Institutes
                .FirstOrDefaultAsync(m => m.InstituteId == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Institutes == null)
            {
                return Problem("Entity set 'InstituteDbContext.Institutes'  is null.");
            }
            var institute = await db.Institutes.FindAsync(id);
            if (institute != null)
            {
                db.Institutes.Remove(institute);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(int id)
        {
          return (db.Institutes?.Any(e => e.InstituteId == id)).GetValueOrDefault();
        }
    }
}
