using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksUse.Models
{
    public partial class Books
    {
        public Books()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        [DisplayName("Nom")]
        public string Title { get; set; }
        public string Isbn { get; set; }
        [DisplayName("Année")]
        [DataType(DataType.Date)]
        public DateTime? Year { get; set; }
        [DisplayName("Auteur(s)")]
        public string Author { get; set; }
        [DisplayName("Stock")]
        public int? UnitsInStock { get; set; }
        [DisplayName("Prix")]
        public decimal? Price { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }

    }
}
