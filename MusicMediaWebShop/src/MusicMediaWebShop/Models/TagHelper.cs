using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class TagHelper
    {
        public int TagHelperID { get; set; }
        public Product Product { get; set; }
        public TagDetail TagDetail { get; set; }
    }
}
