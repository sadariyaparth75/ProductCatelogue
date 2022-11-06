using System.ComponentModel;
using Microsoft.Build.Framework;

namespace ProductCatalogue.Models;
public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    [DisplayName("Is Active?")]
    public bool IsActive { get; set; } = true;

    [Required]
    /// <summary>
    /// Foreign Key for 1-to-1 relationship
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Navigation Property
    /// </summary>
    public Category Category { get; set; } = default!;

    [Required]
    /// <summary>
    /// Foreign Key for 1-to-1 relationship
    /// </summary>
    public int SubCategoryId { get; set; }

    [DisplayName("Sub Category")]
    /// <summary>
    /// Navigation Property
    /// </summary>
    public SubCategory SubCategory { get; set; } = default!;
}
