using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Data;
using ProductCatalogue.Models;
using ProductCatalogue.ViewModels;

namespace ProductCatalogue.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Product
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Products.Include(p => p.Category).Include(p => p.SubCategory);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Product/Create
    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name");
        return View();
    }

    // POST: Product/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductVM vm)
    {
        if (ModelState.IsValid)
        {
            if (await _context.Products.AnyAsync(p => p.Name == vm.Name || p.Code == vm.Code))
            {
                ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
                ModelState.AddModelError(string.Empty, "Name or code already exists.");
                return View(vm);
            }

            _context.Add(new Product
            {
                Name = vm.Name,
                Code = vm.Code,
                Description = vm.Description,
                Price = vm.Price,
                IsActive = vm.IsActive,
                CategoryId = vm.CategoryId,
                SubCategoryId = vm.SubCategoryId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
        return View(vm);
    }

    // GET: Product/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", product.CategoryId);
        ViewData["SubCategories"] = new SelectList(_context.SubCategories.Where(c => c.IsActive && c.CategoryId == product.CategoryId), "Id", "Name", product.SubCategoryId);
        return View(new EditProductVM
        {
            Id = product.Id,
            Name = product.Name,
            Code = product.Code,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            SubCategoryId = product.SubCategoryId
        });
    }

    // POST: Product/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditProductVM vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (await _context.Products.AnyAsync(p => (p.Name == vm.Name || p.Code == vm.Code) && p.Id != vm.Id))
            {
                ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
                ViewData["SubCategories"] = new SelectList(_context.SubCategories.Where(c => c.IsActive && c.CategoryId == vm.CategoryId), "Id", "Name", vm.SubCategoryId);
                ModelState.AddModelError(string.Empty, "Name or code already exists.");
                return View(vm);
            }

            try
            {
                _context.Update(new Product
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Code = vm.Code,
                    Description = vm.Description,
                    Price = vm.Price,
                    IsActive = vm.IsActive,
                    CategoryId = vm.CategoryId,
                    SubCategoryId = vm.SubCategoryId
                });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductExists(vm.Id))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["Categories"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", vm.CategoryId);
        ViewData["SubCategories"] = new SelectList(_context.SubCategories.Where(c => c.IsActive && c.CategoryId == vm.CategoryId), "Id", "Name", vm.SubCategoryId);

        return View(vm);
    }

    // GET: Product/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Products == null)
        {
            return Problem("Entity set 'AppDbContext.Products' is null.");
        }
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
