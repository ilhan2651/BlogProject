using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDemo1.Models
{
    public class BlogAddViewModel
    {
        public string BlogTitle { get; set; }

        public string BlogImage { get; set; } 

        public string BlogThumbnailImage { get; set; } 

        [Required(ErrorMessage = "Lütfen bir blog görseli seçin.")]
        public IFormFile BlogImageFile { get; set; }


        [Required(ErrorMessage = "Lütfen bir blog küçük görseli seçin.")]
        public IFormFile BlogThumbnailImageFile { get; set; } 

        [Required(ErrorMessage = "Lütfen bir kategori seçin.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Blog içeriği boş bırakılamaz.")]
        public string BlogContent { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

}
