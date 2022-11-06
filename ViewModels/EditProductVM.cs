using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogue.ViewModels;
public class EditProductVM
{
    [Required]
    public int Id { get; set; } = default!;

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
    [DisplayName("Category")]
    public int CategoryId { get; set; }

    [Required]
    [DisplayName("Sub Category")]
    public int SubCategoryId { get; set; }
}
