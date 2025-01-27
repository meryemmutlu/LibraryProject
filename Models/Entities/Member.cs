using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string IdentityNo { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
