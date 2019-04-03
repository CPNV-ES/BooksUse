using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            SupplierSupplyBook = new HashSet<SupplierSupplyBook>();
        }

        public int Id { get; set; }
        public string Suppliername { get; set; }

        public virtual ICollection<SupplierSupplyBook> SupplierSupplyBook { get; set; }
    }
}
