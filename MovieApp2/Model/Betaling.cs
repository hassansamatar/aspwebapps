using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MovieApp.Model
{
    public class Betaling
    {

        public int id { get; set; }

        [Required(ErrorMessage = "Kortnummer må oppgis")]
        [RegularExpression(@"[0-9 .\- ]{16}", ErrorMessage = "Vennligst skriv inn ditt riktig kortnummer, 16 siffer")]
        public string kortnummer { get; set; }

        [Required(ErrorMessage = "Utløpsdato må oppgis")]
        [RegularExpression(@"[0-9]{2}\/[0-9]{2}", ErrorMessage = "Vennligst skriv inn kortets utløpsdato, mm/åå")]
        public string utløpsdato { get; set; }

        [Required(ErrorMessage = "CVC må oppgis")]
        [RegularExpression(@"[0-9 .\- ]{3}", ErrorMessage = "Vennligst skriv inn CVC, 3 siffer")]
        public string cvc { get; set; }

        public string ErrorMessage { get; set; }

    }

}