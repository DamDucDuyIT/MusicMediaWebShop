using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class ShippingInfo
    {
        [BindNever]
        public int ShippingInfoID { get; set; }

        [BindNever]
        public ICollection<OrderDetail> LineItems { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your house number")]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a street name")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a ward name")]
        public string Ward { get; set; }

        [Required(ErrorMessage = "Please enter a district name")]
        public string District { get; set; }
        public bool GiftWrap { get; set; }
        public bool Paid { get; set; }
        public bool Sent { get; set; }
    }
}
