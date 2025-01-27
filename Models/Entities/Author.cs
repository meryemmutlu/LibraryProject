using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public List<Book> Books { get; set; }
    }
}
