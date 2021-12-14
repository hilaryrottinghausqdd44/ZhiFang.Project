using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReagentSys.Client.Common
{
    public class ZFPlatformHelp
    {
        /// <summary>
        /// 超时_毫秒数:TIME_OUT_MILLISECOND*5000
        /// </summary>
        public static readonly int TIME_OUT_MILLISECOND = 100;

        public static KeyValuePair<string, string> 订货总单 = new KeyValuePair<string, string>("orderDoc", "订货总单");
        public static KeyValuePair<string, string> 订货明细单 = new KeyValuePair<string, string>("orderDtlList", "订货明细单");

        public static KeyValuePair<string, string> 供货总单 = new KeyValuePair<string, string>("saleDoc", "供货总单");
        public static KeyValuePair<string, string> 供货明细单 = new KeyValuePair<string, string>("saleDtlList", "供货明细单");
        public static KeyValuePair<string, string> 供货条码 = new KeyValuePair<string, string>("barcodeOperationlList", "供货条码");

        public static KeyValuePair<string, string> 出库总单 = new KeyValuePair<string, string>("outDoc", "出库总单");
        public static KeyValuePair<string, string> 出库明细单 = new KeyValuePair<string, string>("outDtlList", "出库明细单");
    }
}
