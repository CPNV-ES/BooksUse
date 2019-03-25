using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(YearsMetadata))]
    public partial class Years
    {
    }

    public class YearsMetadata
    {
        [Required(ErrorMessage = "Le champ Année est requis")]
        [Range(2000, 2100)]
        [DisplayName("Année")]
        public int Title { get; set; }

        [Required(ErrorMessage = "Le champ Status est requis")]
        [DisplayName("Status")]
        public bool Open { get; set; }
    }
}
