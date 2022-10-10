using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
