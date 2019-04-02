using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(SchoolClassesMetadata))]
    public partial class SchoolClasses
    {
    }

    public class SchoolClassesMetadata
    {
        [Required(ErrorMessage = "Le champ Nom est requis")]
        [StringLength(20, ErrorMessage = "Le nom de classe doit contenir au maximum 20 caractères")]
        [DisplayName("Nom")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le champ Nombre d'étudiants est requis")]
        [Range(1, 100, ErrorMessage = "Le nombre d'étudiants doit être compris entre 1 et 100")]
        [DisplayName("Nombre d'étudiants")]
        public int Studentsnumber { get; set; }
    }
}
