using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogue.Models;
public class SubCategory
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; } = true;

    [Required]
    /// <summary>
    /// Foreign Key for 1-to-1 relationship
    /// </summary>
    public int CategoryId { get; set; } = default!;
    /// <summary>
    /// Navigation Property
    /// </summary>
    public Category Category { get; set; } = default!;
}
