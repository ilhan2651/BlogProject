using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDemo1.Models
{
    public class BlogUpdateViewModel
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogImage { get; set; }
        public string BlogThumbnailImage { get; set; }
        public string BlogContent { get; set; }
        public int CategoryID { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
