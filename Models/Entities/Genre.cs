using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
    }
}
