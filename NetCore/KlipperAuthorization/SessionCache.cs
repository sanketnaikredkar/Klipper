using Models.Core.Employment;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlipperAuthorization
{
    public static class SessionCache
    {
        static public Employee CurrentEmployee { get; set; } = null;
    }
}
