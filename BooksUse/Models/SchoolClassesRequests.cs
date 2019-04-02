using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class SchoolClassesRequests
    {
        public int Id { get; set; }
        public int FkSchoolClasses { get; set; }
        public int FkRequests { get; set; }

        public virtual Requests FkRequestsNavigation { get; set; }
        public virtual SchoolClasses FkSchoolClassesNavigation { get; set; }
    }
}
