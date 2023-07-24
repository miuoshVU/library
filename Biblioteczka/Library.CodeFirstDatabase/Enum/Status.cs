using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.CodeFirstDatabase.Enum
{
    public enum Status
    {
        Available       = 0,
        Borrowed        = 1,
        Being_Delivered = 2,
        Lost            = 3,
        Returned        = 4,
        Hold            = 5,
        Archived        = 6

    }
}
