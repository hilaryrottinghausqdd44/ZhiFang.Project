using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.WeiXin.Common
{
    public static class NextRuleNumber
    {

        /// <summary>
        /// 医生医嘱单号(特征码)
        /// </summary>
        /// <returns></returns>
        public static string GetFeatureCode()
        {
            //ZhiFang.Common.Log.Log.Debug("用户订单编号:" + GetNextNumber());
            return GetNextNumber();
        }
        /// <summary>
        /// 用户订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetUOFCode()
        {
            //ZhiFang.Common.Log.Log.Debug("用户订单编号:" + GetNextNumber());
            return GetNextNumber();
        }
        /// <summary>
        /// 医生奖金结算单号
        /// </summary>
        /// <returns></returns>
        public static string GetOSBonusFormCode()
        {
            string strSubNumber = "";
            strSubNumber = DateTime.Now.ToString("yyMMddHHmmssfff");
            return strSubNumber;
        }
        private static string GetNextNumber()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));

            Random ran = new Random();
            int startNum = 0; //顺序号区间开始值
            int endNum = 9999;   //顺序号区间结束值
            int randKey = ran.Next(startNum, endNum);
            strb.Append(randKey.ToString().PadLeft(4, '0'));//补零

            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            //ZhiFang.Common.Log.Log.Debug("用户订单编号:" + strb.ToString());
            return strb.ToString();
        }
    }
}
