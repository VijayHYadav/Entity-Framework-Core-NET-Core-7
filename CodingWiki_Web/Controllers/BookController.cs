using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

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
            List<Book> objList = _db.Books.Include(u=>u.Publisher).ToList();
            //foreach (var obj in objList)
            //{
            //    // obj.Publisher = _db.Publishers.Find(obj.Publisher_Id); non-effecitve way
            //    _db.Entry(obj).Reference(u => u.Publisher).Load(); // effective way So this method basically avoids the duplicate calls to the database for the same publisher ID.
            //    // since there will be only one publisher, we will be using Reference here.
            //    // If there were more than one publisher, then we will be using collection
            //}
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM obj = new();

            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });
            // SELECT [p].[Name] AS [Text], CONVERT(varchar(11), [p].[Publisher_Id]) AS [Value] FROM[Publishers] AS[p]

            if (id == null || id == 0)
            {
                // create
                return View(obj);
            }
            // edit
            obj.Book = _db.Books.FirstOrDefault(u => u.BookId == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookVM obj)
        {
                if (obj.Book.BookId == 0)
                {
                    // create
                    await _db.Books.AddAsync(obj.Book);
                }
                else
                {
                    // update
                    _db.Books.Update(obj.Book);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            BookDetail obj = new();

            // edit
            obj = _db.BookDetails.Include(u=>u.Book).FirstOrDefault(v => v.Book_Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(BookDetail obj)
        {
            if (obj.BookDetail_Id == 0)
            {
                // create
                await _db.BookDetails.AddAsync(obj);
            }
            else
            {
                // update
                _db.BookDetails.Update(obj);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            Book obj = new();
            obj = _db.Books.FirstOrDefault(u => u.BookId == id);

            // edit
            if (obj == null)
            {
                return NotFound();
            }

            _db.Books.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlayGround(int? id)
        {
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 50).ToList();

            IQueryable<Book> BookList2 = _db.Books;
            var fileredBook2 = BookList2.Where(b => b.Price > 50).ToList();

            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //decimal totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();

            return RedirectToAction(nameof(Index));
        }
    }
}

/*
 *  83. Projections in EF Core
 *  
 * Projections in EF Core: projection is a way of translating a full entity into a C-sharp class with a subset of those properties.
 * It is used to create a query that will select from a complete entity, like all the properties in your model, but the result that it will return will be of a different type and it typically has less properties.
 * Now, projection query improves the efficiency of your application because you select only fields that are needed and not all the properties. We will be using Select LINQ statement for projection.
 * 
 */