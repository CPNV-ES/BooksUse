using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class SchoolClasses
    {
        public SchoolClasses()
        {
            SchoolClassesRequests = new HashSet<SchoolClassesRequests>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Studentsnumber { get; set; }

        public virtual ICollection<SchoolClassesRequests> SchoolClassesRequests { get; set; }
    }
}
