using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(BooksMetadata))]
    public partial class Books
    {
    }

    public class BooksMetadata
    {
        [DisplayName("Titre")]
        public string Title { get; set; }
        [DisplayName("ISBN")]
        public string Isbn { get; set; }
        [DisplayName("Auteur(s)")]
        public string Author { get; set; }
        [DisplayName("Stock")]
        public int? UnitsInStock { get; set; }
        [DisplayName("Prix")]
        public decimal? Price { get; set; }
    }
}
