using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [AllowAnonymous]
    public class GenreController : Controller
    {
        LibraryContext db = new LibraryContext();
        public IActionResult Index()
        {
            var items = db.Genres.ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(Genre g)
        {
            db.Genres.Add(g);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveGenre(int id)
        {
            var item = db.Genres.Find(id);
            db.Genres.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateGenre(int id)
        {
            var item = db.Genres.Find(id);

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateGenre(Genre g)
        {
            var item = db.Genres.Find(g.GenreId);
            item.GenreName = g.GenreName;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
