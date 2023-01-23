using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class Guest
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Sisesta nimi")]
        [Display(Name = "Nimi")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sisesta email")]
        [Display(Name = "E-post")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage ="Valesti sisestatud email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sisesta telefoni number")]
        [Display(Name = "Telefoni number")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Numbri algusel peab olema +372")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Sisesta oma valik")]
        [Display(Name = "Valik")]
        public bool? WillAttend { get; set; }
    }
}