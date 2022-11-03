using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class ApplicationUserBook
    {
        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }


    }
}
