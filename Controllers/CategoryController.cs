using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Data;
using ProductCatalogue.Models;

namespace ProductCatalogue.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        return _context.Categories != null ? View(await _context.Categories.ToListAsync()) : Problem("Entity set 'AppDbContext.Category' is null.");
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Category name already exists.");
                return View(category);
            }

            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // POST: Category/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == category.Name && c.Id != category.Id))
            {
                ModelState.AddModelError("Name", "Category name already exists.");
                return View(category);
            }

            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CategoryExists(category.Id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Categories == null)
        {
            return Problem("Entity set 'AppDbContext.Category'  is null.");
        }
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
