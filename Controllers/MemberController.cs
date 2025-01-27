using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [AllowAnonymous]
    public class MemberController : Controller
    {
        LibraryContext db = new LibraryContext();
        public IActionResult Index()
        {
            var items = db.Members.ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMember(Member m)
        {
            db.Members.Add(m);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveMember(int id)
        {
            var item = db.Members.Find(id);
            db.Members.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateMember(int id)
        {
            var item = db.Members.Find(id);

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateMember(Member m)
        {
            var item = db.Members.Find(m.MemberId);
            item.MemberName = m.MemberName;
            item.MemberSurname = m.MemberSurname;
            item.PhoneNumber = m.PhoneNumber;
            item.Email = m.Email;
            item.Gender = m.Gender;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
