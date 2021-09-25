using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Entities.Dtos
{
   public class UserPasswordChangeDto
    {
        [DisplayName("Şu Anki Şifre")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Yeni şifre ile yeni şifrenizin tekrarı aynı değil.")]
        public string RepeatPassword { get; set; }
    }
}
