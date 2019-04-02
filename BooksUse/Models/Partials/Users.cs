using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [DisplayName("Id intranet")]
        public int IntranetUserId { get; set; }

        [Required]
        [DisplayName("Initials")]
        public string Initials { get; set; }

        [Required]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Nom")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Role")]
        public int FkRoles { get; set; }

        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Téléphone")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Mot de passe")]
        public string Password { get; set; }

        [DisplayName("Role")]
        public virtual Roles FkRolesNavigation { get; set; }
    }
}
