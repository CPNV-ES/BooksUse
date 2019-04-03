using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(SuppliersMetadata))]
    public partial class Suppliers
    {
    }

    public class SuppliersMetadata
    {
        [DisplayName("Fournisseur")]
        [Required(ErrorMessage = "Le champ Fournisseur est requis")]
        public string Suppliername { get; set; }
    }
}
