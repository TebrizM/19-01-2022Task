using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokTask.ViewModels
{
    public class BaketViewModel
    {
        public List<BookBasketItemViewModel> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
        
    }
}
