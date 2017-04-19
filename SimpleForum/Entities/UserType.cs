using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleForum.Entities
{
    [Flags]
	public enum UserType
    {
        Guest,
        Common,
        Moderator,
        Administrator
    }
}
