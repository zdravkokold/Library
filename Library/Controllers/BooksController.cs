using Library.Data;
using Library.Data.Entities;
using Library.Models.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryDbContext context;

        public BooksController(LibraryDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<BooksViewModel> books = await context.Books
                .Select(x => new BooksViewModel()
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    Author = x.Author,
                    Rating = x.Rating,
                    Category = x.Category.Name
                })
                .ToListAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBookViewModel()
            {
                Categories = await context.Categories.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID");
            }

            if (user.ApplicationUsersBooks.Any(m => m.BookId == bookId))
            {
                return RedirectToAction(nameof(All));
            }

            user.ApplicationUsersBooks.Add(new ApplicationUserBook()
            {
                BookId = bookId,
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                Book = book
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var user = context.Users.Find(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var books = user.ApplicationUsersBooks
                .Select(m => new BooksViewModel()
                {
                    Id = m.Book.Id,
                    ImageUrl = m.Book.ImageUrl,
                    Title = m.Book.Title,
                    Author = m.Book.Author,
                    Description = m.Book.Description,
                    Rating = m.Book.Rating,
                    Category = m.Book.Category.Name
                });

            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var movie = user.ApplicationUsersBooks.FirstOrDefault(m => m.BookId == bookId);

            if (movie != null)
            {
                user.ApplicationUsersBooks.Remove(movie);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBook(int bookId)
        {
            var book = await context.Books.FindAsync(bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            if (book != null)
            {
                context.Books.Remove(book);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
