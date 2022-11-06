using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogue.ViewModels;
public class CreateSubCategoryVM
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    [Required]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
}
