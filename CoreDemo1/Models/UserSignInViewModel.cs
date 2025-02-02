using System.ComponentModel.DataAnnotations;

namespace CoreDemo1.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Girin")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Girin")]
        public string Password { get; set; }
    }
}
