using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities;

public abstract class EntityBase(string name)
{
    [Required]
    [Key]
    [MaxLength(50)]
    public string Name { get; set; } = name;

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DateTime ModificationDate { get; set; }
}