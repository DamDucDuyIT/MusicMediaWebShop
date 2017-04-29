using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models.ViewModels
{
    public class OrderIndexViewModel
    {
        public IOrder Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
