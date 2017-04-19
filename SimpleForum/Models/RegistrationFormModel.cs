using SimpleForum.AppSource.Helpers;
using SimpleForum.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleForum.Models
{
    [NotMapped]
    public class RegistrationFormModel : User
    {
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string PasswordNotEnscripted { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("PasswordNotEnscripted", ErrorMessage = "пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Error { get; set; }
    }
}