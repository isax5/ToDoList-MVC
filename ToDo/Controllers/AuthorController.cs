using Microsoft.AspNetCore.Mvc;
using ToDo.Data;

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

        // Displays the form to create a new to-do item
        public IActionResult Create()
        {
            return View();
        }

        // Handles the HTTP POST request to create a new to-do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo.Models.ToDo todo)
        {
            if (ModelState.IsValid)
            {
                _context.ToDos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        #endregion

        #region Details

        // Displays details of a specific to-do item
        public IActionResult Details(int id)
        {
            if (_context.ToDos == null)
            {
                return RedirectToAction("Index");
            }

            var toDos = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDos == null)
            {
                return NotFound();
            }

            return View(toDos);
        }

        #endregion

        #region Edit

        // Displays the form to edit a specific to-do item
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.ToDos == null)
            {
                return RedirectToAction("Index");
            }

            var toDos = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDos == null)
            {
                return NotFound();
            }
            return View(toDos);
        }

        // Handles the HTTP POST request to edit a specific to-do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, ToDo.Models.ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.ToDos.Update(toDo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        // Displays a confirmation page for deleting a specific to-do item
        public IActionResult Delete(int id)
        {
            if (_context.ToDos == null)
            {
                return RedirectToAction("Index");
            }

            var toDos = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDos == null)
            {
                return NotFound();
            }

            return View(toDos);
        }

        // Handles the HTTP POST request to delete a specific to-do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            if (_context.ToDos == null)
            {
                return NotFound();
            }

            var toDos = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDos != null)
            {
                _context.ToDos.Remove(toDos);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
