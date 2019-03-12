﻿using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class Books
    {
        public Books()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime? Year { get; set; }
        public string Author { get; set; }
        public int? UnitsInStock { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
