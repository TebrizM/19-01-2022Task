using PustokTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PustokTask.ViewModels
{
    public class BookBasketItemViewModel
    {
      public Book Book { get; set; }
        public int Count { get; set; }
    }
}
