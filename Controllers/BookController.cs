using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace LibraryProject.Controllers
{
    [AllowAnonymous]
    public class BookController : Controller
    {
        LibraryContext db = new LibraryContext();

        public IActionResult Index(string q, int page = 1)
        {
            var items = db.Books.Include(p => p.Genre)
                                .Include(p => p.Author)
                                .ToList();


            if (!string.IsNullOrEmpty(q))    //Arama İşlemi - Search
            {
                items = db.Books.Include(p => p.Genre)
                                .Include(p => p.Author)
                                .Where(p => p.BookTitle.ToLower().Contains(q.ToLower()))
                                .ToList();

            }

            return View(items.ToPagedList(page, 6));
        }


        [HttpGet]
        public IActionResult AddBook()
        {

            List<SelectListItem> genreItems = (from x in db.Genres.ToList()  //Açılır liste - dropdownlist
                                               select new SelectListItem
                                               {
                                                   Text = x.GenreName,
                                                   Value = x.GenreId.ToString()
                                               }).ToList();
            ViewBag.genres = genreItems;

            List<SelectListItem> authorItems = (from x in db.Authors.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.AuthorName + " " + x.AuthorSurname,
                                                    Value = x.AuthorId.ToString()
                                                }).ToList();
            ViewBag.authors = authorItems;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book b, IFormFile file)
        {
            b.BookStatus = true;

            if (file != null)             //Dosyadan Resim yükleme - image upload
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = string.Format($"{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                b.BookImage = fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            db.Books.Add(b);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult RemoveBook(int id)
        {
            var item = db.Books.Find(id);

            db.Books.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBook(int? id)
        {
            var item = db.Books.Find(id);

            List<SelectListItem> authorItems = (from x in db.Authors.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.AuthorName + " " + x.AuthorSurname,
                                                    Value = x.AuthorId.ToString()
                                                }).ToList();
            ViewBag.authors = authorItems;

            List<SelectListItem> genreItems = (from x in db.Genres.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.GenreName,
                                                   Value = x.GenreId.ToString()
                                               }).ToList();
            ViewBag.genres = genreItems;

            return View("UpdateBook", item);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBook(Book b, IFormFile file)
        {
            var item = db.Books.Find(b.BookId);


            item.BookTitle = b.BookTitle;
            item.AuthorId = b.AuthorId;
            item.GenreId = b.GenreId;


            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = string.Format($"{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                item.BookImage = fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AuthorsBooks(int id)  //Yazara Ait Kitapları getirme
        {
            var item = db.Authors.FirstOrDefault(p => p.AuthorId == id);   //Yazarın ismini başlık olarak getirme
            ViewBag.author = item.AuthorName + " " + item.AuthorSurname;

            var items = db.Books.Where(p => p.AuthorId == id)
                .Include(p => p.Author)
                .Include(p => p.Genre)
                .ToList();

            return View(items);
        }

        public IActionResult GenresBooks(int id) //Türlere ait kitapları getirme
        {
            var item = db.Genres.FirstOrDefault(p => p.GenreId == id);  //Türün ismini başlık olarak getirme
            ViewBag.genre = item.GenreName;

            var items = db.Books.Where(p => p.GenreId == id)
                .Include(p => p.Author)
                .Include(p => p.Genre)
                .ToList();

            return View(items);
        }
    }
}
