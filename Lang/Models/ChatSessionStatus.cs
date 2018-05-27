using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public enum ChatSessionStatus
    {
        Pending = 1,
        Rejected = 2,
        InProgress = 3,
        Complete = 4
    }
}
