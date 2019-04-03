using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class SupplierSupplyBook
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public int? BookId { get; set; }
        public decimal? Price { get; set; }
        public int? Deldelay { get; set; }

        public virtual Books Book { get; set; }
        public virtual Suppliers Supplier { get; set; }
    }
}
