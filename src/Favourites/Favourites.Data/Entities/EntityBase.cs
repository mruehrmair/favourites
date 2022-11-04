using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities;

public abstract class EntityBase
{
    [Required]
    [Key]
    [MaxLength(50)]
    public string Name { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public DateTime ModificationDate { get; set; }

    protected EntityBase(string name)
    {
        Name = name;
    }
}