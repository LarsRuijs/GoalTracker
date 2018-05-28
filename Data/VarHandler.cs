using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class VarHandler
    {
        public static DateTime? NullableDateTimeHandler(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            else
                return obj as DateTime?;
        }
    }
}
