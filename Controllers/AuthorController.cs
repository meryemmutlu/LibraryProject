using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [AllowAnonymous] 
    public class AuthorController : Controller
    {
        LibraryContext db = new LibraryContext();
        public IActionResult Index()
        {
            var items = db.Authors.ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(Author a)
        {
            db.Authors.Add(a);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveAuthor(int id)
        {
            var item = db.Authors.Find(id);
            db.Authors.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateAuthor(int id)
        {
            var item = db.Authors.Find(id);

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateAuthor(Author a)
        {
            var item = db.Authors.Find(a.AuthorId);
            item.AuthorName = a.AuthorName;
            item.AuthorSurname = a.AuthorSurname;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
