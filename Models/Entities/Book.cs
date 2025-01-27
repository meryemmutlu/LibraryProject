using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public Boolean BookStatus { get; set; }
        public string? BookImage { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
