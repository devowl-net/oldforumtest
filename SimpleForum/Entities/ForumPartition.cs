using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SimpleForum.Entities
{
    public class ForumPartition : EntityBase
    {
        [Required]
        public string PartitionName { get; set; }
        public string PartitionDescription { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
