using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(RolesMetadata))]
    public partial class Roles
    {
    }

    public class RolesMetadata
    {
        [DisplayName("Nom")]
        public string Name { get; set; }
    }
}
