using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Data;

namespace ProductCatalogue.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users.Select(u => new { u.UserName, u.Email, u.PhoneNumber });
        return Ok(users);
    }

    [HttpGet("/api/products")]
    public IActionResult GetProducts()
    {
        var products = _context.Products
            .Where(p => p.IsActive)
            .Include(i => i.Category)
            .Include(i => i.SubCategory)
            .Select(p => new { p.Name, p.Code, p.Price, p.Description, Category = p.Category.Name, SubCategory = p.SubCategory.Name });
        return Ok(products);
    }
}
