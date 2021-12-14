using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.JScript;
using Microsoft.Vsa;

namespace ZhiFang.ReportFormQueryPrint.Common
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
    }
}
