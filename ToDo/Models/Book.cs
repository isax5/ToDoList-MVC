using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;
    }
}
