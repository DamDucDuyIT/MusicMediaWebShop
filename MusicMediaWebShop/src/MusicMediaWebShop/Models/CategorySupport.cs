using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class CategorySupport
    {
        public int CategorySupportID { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
    }
}
