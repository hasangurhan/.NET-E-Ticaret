using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.Mvc.WebUI.Models
{
    public class ShippingDetails
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen adres gir")]
        public string AdresBasligi { get; set; }
        [Required(ErrorMessage = "Lütfen bir adres gir")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Lütfen sehir gir")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Lütfen semt gir")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen mahalle gir")]
        public string Mahalle { get; set; }
        public int PostaKodu{ get; set; }
    }
}