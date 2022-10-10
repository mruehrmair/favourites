using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities
{
    public class Bookmark : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Url]
        public Uri WebLink { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<Tag>? Tags  { get; set; }

        public Bookmark(string name, Uri webLink)
        {           
            Name = name;
            WebLink = webLink;         
        }
    }
}
