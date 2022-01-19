using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokTask.Models;
using PustokTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokTask.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public LayoutService(PustokContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.Where(x => x.IsDeleted).ToList();
        }

        public BaketViewModel GetBasket()
        {
            BaketViewModel basketVM = new BaketViewModel
            {
                BasketItems = new List<BookBasketItemViewModel>(),
                TotalPrice = 0
            };

            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

            var baksetStr = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

            if (!string.IsNullOrWhiteSpace(baksetStr))
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(baksetStr);

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
