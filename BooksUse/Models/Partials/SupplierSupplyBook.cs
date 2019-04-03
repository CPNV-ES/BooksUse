using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(SupplierSupplyBookMetadata))]
    public partial class SupplierSupplyBook
    {
    }

    public class SupplierSupplyBookMetadata
    {
        [DisplayName("Fournisseur")]
        public int? SupplierId { get; set; }
        [DisplayName("Livre")]
        public int? BookId { get; set; }
        [DisplayName("Prix")]
        public decimal? Price { get; set; }
        [DisplayName("Delai")]
        public int? Deldelay { get; set; }

        [DisplayName("Livre")]
        public virtual Books Book { get; set; }
        [DisplayName("Fournisseur")]
        public virtual Suppliers Supplier { get; set; }
    }
}
