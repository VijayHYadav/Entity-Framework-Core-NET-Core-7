using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Category obj = new();
            if (id == null || id == 0)
            {
                // create
                return View(obj);
            }
            // edit
            obj = _db.Categories.FirstOrDefault(u=>u.CategoryId== id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.CategoryId== 0)
                {
                    // create
                    await _db.Categories.AddAsync(obj);
                } else
                {
                    // update
                    _db.Categories.Update(obj);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category obj = new();
            obj = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            
            // edit
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {
            for (int i = 0; i <= 2; i++)
            {
                _db.Categories.Add(new Category { CategoryName = Guid.NewGuid().ToString() });
            }
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<Category> Catlist = new List<Category>();
            for (int i = 0; i <= 5; i++)
            {
                Catlist.Add(new Category { CategoryName = Guid.NewGuid().ToString() });
            }
            _db.Categories.AddRange(Catlist);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {
            List<Category> categories = _db.Categories.OrderByDescending(c => c.CategoryId).Take(2).ToList();
            _db.Categories.RemoveRange(categories);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            List<Category> categories = _db.Categories.OrderByDescending(c => c.CategoryId).Take(5).ToList();
            _db.Categories.RemoveRange(categories);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
