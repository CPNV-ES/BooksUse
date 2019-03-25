using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
    }

    public class UsersMetadata
    {
        [DisplayName("Id intranet")]
        public int IntranetUserId { get; set; }
        [DisplayName("Initials")]
        public string Initials { get; set; }
        [DisplayName("Prénom")]
        public string FirstName { get; set; }
        [DisplayName("Nom")]
        public string LastName { get; set; }
        [DisplayName("Role")]
        public int FkRoles { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Téléphone")]
        public string Phone { get; set; }
        [DisplayName("Mot de passe")]
        public string Password { get; set; }

        [DisplayName("Role")]
        public virtual Roles FkRolesNavigation { get; set; }
    }
}
