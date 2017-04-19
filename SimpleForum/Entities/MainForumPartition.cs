using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleForum.Entities
{
    public class MainForumPartition : EntityBase
    {
        [Required]
        public string PartitionName { get; set; }
        public virtual ICollection<ForumPartition> ForumPartitions { get; set; }
    }
}