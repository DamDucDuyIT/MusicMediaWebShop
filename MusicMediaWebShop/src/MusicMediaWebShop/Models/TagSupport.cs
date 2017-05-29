using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class TagSupport
    {
        public int TagSupportID { get; set; }
        public Tag Tag { get; set; }
        
        public TagDetail TagDetail { get; set; }
    }
}
