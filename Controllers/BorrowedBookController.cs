using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Controllers
{
    [AllowAnonymous]
    public class BorrowedBookController : Controller
    {
        LibraryContext db = new LibraryContext();

        public IActionResult Index()
        {
            var items = db.BorrowedBooks
                .Include(p => p.Book)
                .Include(p => p.Member)
                .ToList();
            return View(items);
        }


        [HttpGet]
        public IActionResult BorrowBook()
        {
            List<SelectListItem> bookValue = (from x in db.Books.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.BookTitle,
                                                  Value = x.BookId.ToString()
                                              }).ToList();

            ViewBag.getBook = bookValue;

            List<SelectListItem> memberValue = (from x in db.Members.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MemberName + " " + x.MemberSurname,
                                                    Value = x.MemberId.ToString()
                                                }).ToList();

            ViewBag.getMember = memberValue;



            return View();
        }

        [HttpPost]
        public IActionResult BorrowBook(BorrowedBook borrowed)
        {
            var item = db.Books.Find(borrowed.BookId);

            if (item != null && item.BookStatus == true)
            {
                borrowed.BorrowStatus = false;
                item.BookStatus = false;
                db.BorrowedBooks.Add(borrowed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var value = db.BorrowedBooks.Find(id);
            var value1 = db.Books.Find(value.BookId);

            if (value.BorrowStatus == false)
            {
                value1.BookStatus = true;
            }

            db.BorrowedBooks.Remove(value);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ReturnBook(int id)
        {
            var value = db.BorrowedBooks.Find(id);

            List<SelectListItem> bookValue = (from x in db.Books.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.BookTitle,
                                                  Value = x.BookId.ToString()
                                              }).ToList();

            ViewBag.getBooks = bookValue;

            List<SelectListItem> memberValue = (from x in db.Members.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.MemberName + " " + x.MemberSurname,
                                                    Value = x.MemberId.ToString()
                                                }).ToList();

            ViewBag.getMembers = memberValue;

            return View(value);
        }

        [HttpPost]
        public IActionResult ReturnBook(BorrowedBook borrowed)
        {

            var item = db.BorrowedBooks.Find(borrowed.BorrowId);

            item.BorrowStatus = true;
            item.BorrowDate = borrowed.BorrowDate;
            item.ReturnDate = borrowed.ReturnDate;
            item.BookId = borrowed.BookId;
            item.MemberId = borrowed.MemberId;

            var item1 = db.Books.Find(item.BookId);
            if (item1 != null)
            {
                item1.BookStatus = true;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PageOfBorrow()  //Sadece ödünç alınan kitapları listeler
        {
            var items = db.BorrowedBooks.Where(p => p.BorrowStatus == false)
                                        .Include(p => p.Book)
                                        .Include(p => p.Member)
                                        .ToList();

            return View(items);
        }

        public IActionResult ReturnPage() //Sadece iade edilen kitapları listeler
        {
            var items = db.BorrowedBooks.Where(p => p.BorrowStatus == true)
                                        .Include(p => p.Book)
                                        .Include(p => p.Member)
                                        .ToList();
            return View(items);
        }
        public IActionResult MemberBooks(int id) //Üyelere ait kitap alma işlemlerini gösterir.
        {
            var value = db.Members.FirstOrDefault(p => p.MemberId == id);

            ViewBag.member = value.MemberName + " " + value.MemberSurname;

            var values = db.BorrowedBooks.Where(p => p.MemberId == id)
                                         .Include(p => p.Book)
                                         .ToList();
            return View(values);
        }
    }
}
