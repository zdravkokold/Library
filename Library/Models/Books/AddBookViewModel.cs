using Library.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Models.Books
{
    public class AddBookViewModel
    {
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

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
