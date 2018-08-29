using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Book
    {
        public int id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime RelaseDate { get; set; }
        public ICollection<Author> Authors { get; set; }

    }
}
