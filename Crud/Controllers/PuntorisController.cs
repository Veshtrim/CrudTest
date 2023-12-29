using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crud.Controllers
{
    public class PuntorisController : Controller
    {
        private readonly CrudContext _context;

        public PuntorisController(CrudContext context)
        {
            _context = context;
        }

        // GET: Puntoris
        public async Task<IActionResult> Index(string departamentiFilter, string searchString)
        {
            if (_context.Puntori == null)
            {
                return Problem("Entity is null");
            }
            IQueryable<string> departamentQuery = from m in _context.Puntori
                                                  orderby m.Departament
                                                  select m.Departament;
            var puntoret = from m in _context.Puntori select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                puntoret = puntoret.Where(p => p.Name!.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(departamentiFilter))
            {
                puntoret = puntoret.Where(x => x.Departament == departamentiFilter);
            }
            var puntoriDepartamentiVM = new PuntoriDepartamentiViewModel
            {
                Departamenti = new SelectList(await departamentQuery.Distinct().ToListAsync()),
                Puntoret = await puntoret.ToListAsync()
            };
           
            return View(puntoriDepartamentiVM);
        }

        // GET: Puntoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntori = await _context.Puntori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puntori == null)
            {
                return NotFound();
            }

            return View(puntori);
        }

        // GET: Puntoris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Puntoris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Departament")] Puntori puntori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puntori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puntori);
        }

        // GET: Puntoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntori = await _context.Puntori.FindAsync(id);
            if (puntori == null)
            {
                return NotFound();
            }
            return View(puntori);
        }

        // POST: Puntoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Departament")] Puntori puntori)
        {
            if (id != puntori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puntori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuntoriExists(puntori.Id))
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
            return View(puntori);
        }

        // GET: Puntoris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntori = await _context.Puntori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puntori == null)
            {
                return NotFound();
            }

            return View(puntori);
        }

        // POST: Puntoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puntori = await _context.Puntori.FindAsync(id);
            if (puntori != null)
            {
                _context.Puntori.Remove(puntori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuntoriExists(int id)
        {
            return _context.Puntori.Any(e => e.Id == id);
        }
    }
}
