using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudiesManager.Common
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object @this)
        {
            return (@this is null);
        }

        public static bool IsNotNull(this object @this)
        {
            return !IsNull(@this);
        }
    }
}
