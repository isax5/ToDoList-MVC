using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the list of Authors
        public IActionResult Index() => View(_context.Authors.ToList());

        #region Create

        // Displays the form to create a new Author item
        public IActionResult Create() => View();

        // Handles the HTTP POST request to create a new Author item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        #endregion

        #region Details

        // Displays details of a specific Author item
        public IActionResult Details(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        #endregion

        #region Edit

        // Displays the form to edit a specific Author item
        public IActionResult Edit(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // Handles the HTTP POST request to edit a specific Author item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Update(author);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        // Displays a confirmation page for deleting a specific Author item
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // Handles the HTTP POST request to delete a specific Author item
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
