using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.JScript;
using Microsoft.Vsa;

namespace ZhiFang.Common.Public
{
    public class MathExpHelp
    {
        /// <summary>
        /// 计算字符串形式的数学表达式
        /// </summary>
        /// <param name="Exp">字符串数学表达式</param>
        /// <returns>Object</returns>
        public static object ExecStringByMSJScript(string Exp)
        {
            return Microsoft.JScript.Eval.JScriptEvaluate(Exp, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
        }
        ///// <summary>
        ///// 计算字符串形式的数学表达式
        ///// </summary>
        ///// <param name="Exp">字符串数学表达式</param>
        ///// <returns>Object</returns>
        //public static object ExecStringByMSScriptControlOCX(string Exp)
        //{
        //    MSScriptControl.ScriptControlClass sc = new MSScriptControl.ScriptControlClass();
        //    sc.Language = "javascript";
        //    object obj = sc.Eval(Exp);
        //    return obj;
        //}
        /// <summary>
        /// 小数位数格式化
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="digit">保留的位数</param>
        /// <param name="mode">保留方式：0四舍五入；1直接截取</param>
        /// <param name="isFillZero">位数不足时是否补零</param>
        /// <returns></returns>
        public static double? DigitFormat(double? value, int digit, int mode, bool isFillZero)
        {
            if (value != null)
            {
                if (mode == 0)
                    value = Math.Round((double)value, digit, MidpointRounding.AwayFromZero);
                else if (mode == 1)
                    value = Math.Round((double)value, digit);
            }
            return value;
        }
    }
}
