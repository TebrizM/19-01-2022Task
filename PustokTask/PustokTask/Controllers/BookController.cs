using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokTask.Models;
using PustokTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokTask.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokContext _context;

        public BookController(PustokContext context)
        {
            _context = context;
        }
        public IActionResult GetBook(int id)
        {
            Book book = _context.Books.Include(x => x.Genre).Include(x=>x.Author).Include(x=>x.BookImages).FirstOrDefault(x => x.Id == id);


            return PartialView("_ModalPartialView", book);
        }

        public IActionResult Index(int? genreId, int page = 1)
        {
            var books = _context.Books.Include(x => x.Author).Include(x => x.BookImages).Where(x => !x.IsDeleted);
            ViewBag.GenreId = genreId;
            ViewBag.PageIndex = page;

            if (genreId != null)
                books = books.Where(x => x.GenreId == genreId);

            ViewBag.TotalPages = (int)Math.Ceiling(books.Count() / 2d);

            BookViewModel bookVM = new BookViewModel
            {
                Genres = _context.Genres.Include(x => x.Books).Where(x => !x.IsDeleted).ToList(),
                Books = books.Skip((page - 1) * 2).Take(2).ToList()
            };

            return View(bookVM);
        }
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("name", "Tabriz Mammadov");
            return RedirectToAction("index");
        }
        public IActionResult GetSession(string key)
        {
            var value = HttpContext.Session.GetString(key);

            return Content(value);
        }
        public IActionResult SetCookie()
        {
            HttpContext.Response.Cookies.Append("name", "Test");
            return RedirectToAction("index");
        }
        public IActionResult GetCookie(string key)
        {
            var value = HttpContext.Request.Cookies[key];
            return Content(value);
        }

        public IActionResult AddBasket(int id)
        {
            if (!_context.Books.Any(x => x.Id == id && !x.IsDeleted))
                return NotFound();

            string basketItemsStr = HttpContext.Request.Cookies["basket"];
            List<BasketItemViewModel> items = new List<BasketItemViewModel>();

            if (!string.IsNullOrWhiteSpace(basketItemsStr))
                items = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

            BasketItemViewModel item = items.FirstOrDefault(x => x.BookId == id);

            if (item == null)
            {
                item = new BasketItemViewModel { BookId = id, Count = 1 };
                items.Add(item);
            }
            else
                item.Count++;

            basketItemsStr = JsonConvert.SerializeObject(items);

            HttpContext.Response.Cookies.Append("basket", basketItemsStr);

            return PartialView("_BasketPartialView", _getBasket(items));
        }


        private BaketViewModel _getBasket(List<BasketItemViewModel> basketItems)
        {
            BaketViewModel basketVM = new BaketViewModel
            {
                BasketItems = new List<BookBasketItemViewModel>(),
                TotalPrice = 0
            };

            foreach (var item in basketItems)
            {
                Book book = _context.Books.Include(x => x.BookImages).FirstOrDefault(x => x.Id == item.BookId);
                BookBasketItemViewModel bookBasketItem = new BookBasketItemViewModel
                {
                    Book = book,
                    Count = item.Count
                };

                basketVM.BasketItems.Add(bookBasketItem);
                decimal totalPrice = book.DiscountPercent > 0 ? (book.SalePrice * (1 - book.DiscountPercent / 100)) : book.SalePrice;
                basketVM.TotalPrice += totalPrice * item.Count;

            }

            return basketVM;
        }
    }
}
