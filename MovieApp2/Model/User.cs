using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MovieApp.Model
{
    public class User
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[a-zA-ZøæåØÆÅ .\- ]{2,15}", ErrorMessage = "Vennligst skriv inn ditt Fornavn med bare tegn fra det latinske alfabetet (inkludert æøå) og det må være 2 tegn eller over, men 15 tegn eller mindre.")]
        public string fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[a-zA-ZøæåØÆÅ .\- ]{2,30}", ErrorMessage = "Vennligst skriv inn ditt Etternavn med bare tegn fra det latinske alfabetet (inkludert æøå) og det må være 2 tegn eller over, men 20 tegn eller mindre.")]
        public string etternavn { get; set; }

        [Required(ErrorMessage = "Adresse må oppgis")]
        [RegularExpression(@"[a-zA-Z]{1}[0-9a-zA-ZøæåØÆÅ .\- ]{4,25}", ErrorMessage = "Vennligst skriv inn din adresse som du skal opphold deg her Norge under VM med bare tegn fra det latinske alfabetet (inkludert æøå) og det må være 5 tegn eller over, men 25 tegn eller mindre.")]
        public string adresse { get; set; }

        [Required(ErrorMessage = "Epost må oppgis")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Ugyldig epost")]
        public string epost { get; set; }

        [Compare("epost", ErrorMessage = "Bekreftet epost matcher ikke")]
        public string epostBekreftelse { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }

        [Compare("passord", ErrorMessage = "Bekreftet passord matcher ikke")]
        public string passordBekreftelse { get; set; }

        [Required(ErrorMessage = "Postnummer må oppgis")]
        public string postnr { get; set; }

        public string poststed { get; set; }

        public string errorMessage { get; set; }

        public string accessLevel { get; set; }

        }
}