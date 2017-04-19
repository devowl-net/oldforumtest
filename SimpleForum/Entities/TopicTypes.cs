using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleForum.Entities
{
    [Flags]
    public enum TopicTypes
    {
        Common,
        Important
    }
}
