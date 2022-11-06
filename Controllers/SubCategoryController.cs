using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Data;
using ProductCatalogue.Models;
using ProductCatalogue.ViewModels;

namespace ProductCatalogue.Controllers;

[Authorize]
public class SubCategoryController : Controller
{
    private readonly AppDbContext _context;

    public SubCategoryController(AppDbContext context)
    {
        _context = context;
    }

    // GET: GetActiveSubCategories
    [Route("GetActive/{categoryId}")]
    public IActionResult GetActive(int categoryId)
    {
        var categories = _context.SubCategories.Where(c => c.CategoryId == categoryId && c.IsActive);
        return new JsonResult(categories);
    }

    // GET: SubCategory
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.SubCategories.Include(s => s.Category);
        return View(await appDbContext.ToListAsync());
    }

    // GET: SubCategory/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.SubCategories == null)
        {
            return NotFound();
        }

        var subCategory = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subCategory == null)
        {
            return NotFound();
        }

        return View(subCategory);
    }

    // GET: SubCategory/Create
    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name");
        return View();
    }

    // POST: SubCategory/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSubCategoryVM vm)
    {
        if (ModelState.IsValid)
        {
            if (await _context.SubCategories.AnyAsync(c => c.Name == vm.Name))
            {
                ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
                ModelState.AddModelError("Name", "Sub Category name already exists.");
                return View(vm);
            }

            _context.Add(new SubCategory { Name = vm.Name, IsActive = vm.IsActive, CategoryId = vm.CategoryId });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
        return View(vm);
    }

    // GET: SubCategory/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.SubCategories == null)
        {
            return NotFound();
        }

        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", subCategory.CategoryId);
        return View(new EditSubCategoryVM { Id = subCategory.Id, Name = subCategory.Name, IsActive = subCategory.IsActive, CategoryId = subCategory.CategoryId });
    }

    // POST: SubCategory/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditSubCategoryVM vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (await _context.SubCategories.AnyAsync(c => c.Name == vm.Name && c.Id != vm.Id))
            {
                ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
                ModelState.AddModelError("Name", "Category name already exists.");
                return View(vm);
            }

            try
            {
                _context.Update(new SubCategory { Id = vm.Id, Name = vm.Name, IsActive = vm.IsActive, CategoryId = vm.CategoryId });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!SubCategoryExists(vm.Id))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
        return View(vm);
    }

    // GET: SubCategory/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.SubCategories == null)
        {
            return NotFound();
        }

        var subCategory = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subCategory == null)
        {
            return NotFound();
        }

        return View(subCategory);
    }

    // POST: SubCategory/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.SubCategories == null)
        {
            return Problem("Entity set 'AppDbContext.SubCategories'  is null.");
        }
        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory != null)
        {
            _context.SubCategories.Remove(subCategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubCategoryExists(int id)
    {
        return (_context.SubCategories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
