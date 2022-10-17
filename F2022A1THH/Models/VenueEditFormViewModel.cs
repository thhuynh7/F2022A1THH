using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A1THH.Models
{
    public class VenueEditFormViewModel : VenueEditViewModel
    {
        // extra
        [StringLength(24, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Sign-up Password")]
        public string Password { get; set; }

        // extra        
        [StringLength(5, MinimumLength = 5)]
        [RegularExpression("[0-9][0-9][0-9][A-Z][A-Z]", ErrorMessage = "Promo Code must contain 3 numbers followed by 2 capital letters")]
        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }

        // extra
        [Range(1, 40000, ErrorMessage = "Capacity must be between 1 and 40,000")]
        public int? Capacity { get; set; }
    }
}