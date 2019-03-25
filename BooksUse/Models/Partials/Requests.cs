using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksUse.Models
{
    [ModelMetadataType(typeof(RequestsMetadata))]
    public partial class Requests
    {
    }

    public class RequestsMetadata
    {
        [DisplayName("Approuvé")]
        public int Approved { get; set; }
        [DisplayName("Année")]
        public int FkYears { get; set; }
        [DisplayName("Demandeur")]
        public int FkUsers { get; set; }
        [DisplayName("Livre")]
        public int FkBooks { get; set; }

        [DisplayName("Livre")]
        public virtual Books FkBooksNavigation { get; set; }
        [DisplayName("Demandeur")]
        public virtual Users FkUsersNavigation { get; set; }
        [DisplayName("Année")]
        public virtual Years FkYearsNavigation { get; set; }
    }
}
