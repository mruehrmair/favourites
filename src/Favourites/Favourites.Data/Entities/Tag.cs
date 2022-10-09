using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Bookmark>? Bookmarks { get; set; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
