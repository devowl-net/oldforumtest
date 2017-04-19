using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleForum.Entities
{
    [Flags]
    public enum ModedatorActionType
    {
        None,
        Deleted,
        InfoMark,
        WarningMark,
    }
}
