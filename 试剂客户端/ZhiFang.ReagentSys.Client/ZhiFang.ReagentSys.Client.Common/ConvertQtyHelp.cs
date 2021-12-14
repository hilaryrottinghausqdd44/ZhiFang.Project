using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReagentSys.Client.Common
{
    public static class ConvertQtyHelp
    {
        public static double ConvertQty(double value, int digits)
        {
            //先按指定的小数点取值,如果取值后为0,取最小整数值
            var value1 = Math.Round(value, digits);
            if (value1 <= 0)
                value1 = Math.Ceiling(value);
            ZhiFang.Common.Log.Log.Info("ConvertQtyHelp转换前值为:" + value + ",转换后值为:" + value1);
            return value1;
        }
    }
}
