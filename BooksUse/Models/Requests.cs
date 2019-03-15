using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BooksUse.Models
{
    public partial class Requests
    {
        public int Id { get; set; }
        [DisplayName("Année")]
        public int ForYear { get; set; }
        [DisplayName("Approuvé")]
        public int Approved { get; set; }
        public int FkUsers { get; set; }
        public int FkBooks { get; set; }

        [DisplayName("Livre")]
        public virtual Books FkBooksNavigation { get; set; }
        [DisplayName("Responsable")]
        public virtual Users FkUsersNavigation { get; set; }
    }
}
