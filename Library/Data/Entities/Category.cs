using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Category;

namespace Library.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
            = new List<Book>();
    }
}
