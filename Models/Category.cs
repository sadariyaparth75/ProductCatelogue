using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogue.Models;
public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; } = true;
}
