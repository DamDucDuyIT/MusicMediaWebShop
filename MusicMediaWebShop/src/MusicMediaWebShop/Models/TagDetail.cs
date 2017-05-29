using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class TagDetail
    {
        public int TagDetailID { get; set; }
        public string TagDetailName { get; set; }
        public ICollection<TagHelper> TagHelper { get; set; }
        public ICollection<TagSupport> TagSupports { get; set; }
    }
}
