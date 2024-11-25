using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the list of Books
        public IActionResult Index() => View(_context.Books.Include(b => b.Author).ToList());

        #region Create

        // Displays the form to create a new Book item
        public IActionResult Create()
        {
            var authors = _context.Authors
                .Select(a => new { a.Id, a.Name })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            return View();
        }

        // Handles the HTTP POST request to create a new Book item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            var author = _context.Authors.First(a => a.Id == book.AuthorId);
            book.Author = author;

            ModelState.Clear();
            TryValidateModel(book);

            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            var authors = _context.Authors
                .Select(a => new { a.Id, a.Name })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            return View(book);
        }

        #endregion

        #region Details

        // Displays details of a specific Book item
        public IActionResult Details(int id)
        {
            var book = _context.Books.Include(b => b.Author).FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        #endregion

        #region Edit

        // Displays the form to edit a specific Book item
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            var authors = _context.Authors
                .Select(a => new { a.Id, a.Name })
                .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            return View(book);
        }

        // Handles the HTTP POST request to edit a specific Book item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            var author = _context.Authors.First(a => a.Id == book.AuthorId);
            book.Author = author;

            ModelState.Clear();
            TryValidateModel(book);

            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        // Displays a confirmation page for deleting a specific Book item
        public IActionResult Delete(int id)
        {
            var Book = _context.Books.Include(b => b.Author).FirstOrDefault(x => x.Id == id);
            if (Book == null)
                return NotFound();

            return View(Book);
        }

        // Handles the HTTP POST request to delete a specific Book item
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
