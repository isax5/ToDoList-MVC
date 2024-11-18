using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        public ICollection<Book> Books { get; } = [];
    }
}
