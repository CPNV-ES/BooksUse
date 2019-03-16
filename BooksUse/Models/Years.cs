using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class Years
    {
        public Years()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        public int Title { get; set; }
        public bool Open { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
