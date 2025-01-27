using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models.Entities
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool BorrowStatus { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
