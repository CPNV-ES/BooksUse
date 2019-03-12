using System;
using System.Collections.Generic;

namespace BooksUse.Models
{
    public partial class Users
    {
        public Users()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        public int IntranetUserId { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FkRoles { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual Roles FkRolesNavigation { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }
    }
}
