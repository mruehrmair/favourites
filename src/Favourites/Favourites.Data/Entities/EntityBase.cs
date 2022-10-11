using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities
{
    public abstract class EntityBase
    {
        [Required]
        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime ModificationDate { get; set; }

        public EntityBase(string name)
        {
            Name = name;
        }
    }
}
