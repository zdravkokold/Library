using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), MinRating, MaxRating)]
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        public virtual List<ApplicationUserBook> ApplicationUsersBooks { get; set; }
            = new List<ApplicationUserBook>();

    }
}
