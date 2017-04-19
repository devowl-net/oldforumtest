using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SimpleForum.Entities
{
    public class Topic : EntityBase, IViewBase
    {
        [Required]
        public string TopicName { get; set; }
        public TopicTypes TopicType { get; set; }
        public virtual User TopicOwner { get; set; }
        public int Views { get; set; }
        public int TopicIcon { get; set; }
        [Required]
        public virtual Message FirstMessage { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
