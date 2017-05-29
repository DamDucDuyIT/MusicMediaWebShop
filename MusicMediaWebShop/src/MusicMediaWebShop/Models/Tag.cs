using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<TagSupport> TagSupports { get; set; }
        public ICollection<CategorySupport> CategorySupports { get; set; }
    }
}
