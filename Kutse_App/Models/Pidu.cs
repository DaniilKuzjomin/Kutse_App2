using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class Pidu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sisesta nimi")]
        [Display(Name = "Pidu nimi")]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "Sisesta kuupaev")]
        [Display(Name = "Kuupäev")]
        public DateTime Date { get; set; }

    }
}