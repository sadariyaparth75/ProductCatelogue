using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogue.ViewModels;
public class EditSubCategoryVM
{
    [Required]
    public int Id { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
