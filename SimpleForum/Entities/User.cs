using SimpleForum.AppSource.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleForum.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            Type = UserType.Common;
            RegistrationDate = DateTime.Today;
        }
        public UserType Type { get; set; }

        [Required]
        [Display(Name="Ник на форме")]
        public string NickName { get; set; }

        /// <summary>
        /// Подпись в сообщениях
        /// </summary>
        [Display(Name="Подпись на форуме")]
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage="Подпись в сообщениях на форуме. Не может превышать 100 символов")]

        public string Postscript { get; set; }
        [Display(Name="День рождения")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Откуда Вы родом")]
        [DataType(DataType.Text)]
        public string Location { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int MessageCount { get; set; }

        public int Reputation { get; set; }

        [Display(Name = "Фото для профиля")]
        [DataType(DataType.Upload)]
        public byte[] ProfilePhoto { get; set; }
        
        public string ProfilePhotoType { get; set; }

        [Display(Name = "Картинка для форума")]
        [DataType(DataType.Upload)]
        public byte[] ForumAvatar { get; set; }

        public string ForumAvatarType { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        #region Passwords
        
        
        public string Password { get; set; }

        #endregion
    }
}