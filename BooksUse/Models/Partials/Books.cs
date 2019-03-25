using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Le champ Titre est requis")]
        [StringLength(254)]
        [DisplayName("Titre")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Le champ ISBN est requis")]
        [StringLength(15)]
        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Le champ Auteur(s) est requis")]
        [StringLength(254)]
        [DisplayName("Auteur(s)")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Le champ Stock est requis")]
        [Range(0, 999)]
        [DisplayName("Stock")]
        public int? UnitsInStock { get; set; }

        [Required(ErrorMessage = "Le champ Prix est requis")]
        [Range(0, 999)]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Prix invalide")]
        [DisplayName("Prix")]
        public decimal? Price { get; set; }
    }
}
