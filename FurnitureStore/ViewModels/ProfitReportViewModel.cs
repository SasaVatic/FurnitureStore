using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.ViewModels
{
    public class ProfitReportViewModel
    {
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Kategorija nameštaja")]
        public string ProductTypeName { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }

        // Datum je nulabilan da ne bi imali default datuma vrednost u polju za input
        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Od datuma")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Do datuma")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}