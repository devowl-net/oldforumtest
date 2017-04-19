using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Models
{
    public class LoginFormModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        //[Required]
        //[Display(Name = "Адрес электронной почты")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
    }
}