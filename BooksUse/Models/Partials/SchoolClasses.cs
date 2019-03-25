using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Nom")]
        public string Name { get; set; }
        [DisplayName("Nombre d'étudiants")]
        public int Studentsnumber { get; set; }
    }
}
