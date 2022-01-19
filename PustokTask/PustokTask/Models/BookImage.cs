using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PustokTask.Models
{
    public class BookImage
    {
      public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        public int BookId { get; set; }
        public bool? BookStatus { get; set; }

        public Book Book { get; set; }
    }
}
