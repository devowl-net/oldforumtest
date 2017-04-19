using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SimpleForum.Entities
{
    public class Message : EntityBase
    {
        [Required]
        public virtual User MessageOwner { get; set; }
        public string Text { get; set; }
        public ModedatorActionType ModeratorAction { get; set; }
        public string ModeratorRemark { get; set; }
        public virtual User Moderator { get; set; }
    }
}
