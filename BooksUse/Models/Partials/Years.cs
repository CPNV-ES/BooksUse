using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Année")]
        public int Title { get; set; }
        [DisplayName("Status")]
        public bool Open { get; set; }
    }
}
