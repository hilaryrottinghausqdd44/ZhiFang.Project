using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.BloodTransfusion.Common
{
    public class BarcodeRules
    {
        protected static string _prefix = "Z";
        /// <summary>
        /// 一维条码的前缀
        /// </summary>
        public static string Prefix { get { return _prefix; } set { _prefix = value; } }
        /// <summary>
        /// 一维条码是否添加当次的供货数/库存数
        /// </summary>
        public static bool ISAddGoodsQty = false;
        /// <summary>
        /// 一维条码是否添加当前明细序号
        /// </summary>
        public static bool ISAddCurNo =true;
        /// <summary>
        /// 生成一维批条码
        /// </summary>
        /// <param name="prefix">条码前缀</param>
        /// <param name="goodsQty">总数</param>
        /// <param name="curNo">当前序号</param>
        /// <returns></returns>
        public static string GetCreateBarcode(string prefix, double goodsQty, int curNo)
        {
            string serialNo = "";
            StringBuilder strb = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix)) strb.Append(prefix + "|");
            strb.Append(DateTime.Now.ToString("yyMMddHHmmssff"));//ff
            if (BarcodeRules.ISAddGoodsQty == true && goodsQty >= 0)
            {
                strb.Append("|" + goodsQty);
            }
            if (BarcodeRules.ISAddCurNo == true && curNo >= 0)
            {
                if (curNo >= 0) strb.Append("|" + curNo);
            }
            serialNo = strb.ToString();
            return serialNo;
        }
    }
}
