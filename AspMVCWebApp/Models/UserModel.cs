using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspMVCWebApp.Models
{
    public class UserModel
    {
        [Required]
        [Key]
        [MaxLength(50)]
        private string _id;
        [Required]
        [Display(Name = "Mã nhân viên")]
        public string UserID
        {
            get { return _id; }
            set { _id = value?.ToUpper(); }
        }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Tên nhân viên")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [MaxLength(50)]
        public string ConfirmedPassword { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [MaxLength(50)]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Số điện thoại")]
        public string Tel { get; set; }

        [Display(Name = "Thực hiện")]
        [Required]
        public byte Disabled { get; set; }
    }
}