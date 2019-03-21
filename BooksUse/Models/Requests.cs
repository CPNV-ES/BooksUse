using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class Requests
    {
        public Requests()
        {
            SchoolClassesRequests = new HashSet<SchoolClassesRequests>();
        }

        public int Id { get; set; }
        public int Approved { get; set; }
        public int FkYears { get; set; }
        public int FkUsers { get; set; }
        public int FkBooks { get; set; }

        public virtual Books FkBooksNavigation { get; set; }
        public virtual Users FkUsersNavigation { get; set; }
        public virtual Years FkYearsNavigation { get; set; }
        public virtual ICollection<SchoolClassesRequests> SchoolClassesRequests { get; set; }
    }
}
