using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common
{
    public interface IConfigHelper
    {
        string GetConfigString(string key);
        bool GetConfigBool(string key);
        decimal GetConfigDecimal(string key);
        int? GetConfigInt(string key);

    }
}
