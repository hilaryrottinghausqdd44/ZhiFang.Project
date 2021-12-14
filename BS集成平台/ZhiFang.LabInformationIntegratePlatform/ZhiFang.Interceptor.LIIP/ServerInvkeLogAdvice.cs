using AopAlliance.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Interceptor.LIIP
{
    public class ServerInvokeLogAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            //string MethodName = invocation.Method.Name;
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("BParameter", "参数字典");
            //dic.Add("ClassDic", "基础字典");
            //dic.Add("BAntiUse", "抗生素用药字典");
            //dic.Add("AgeUnit", "年龄单位字典");           
            //dic.Add("BTestItemCon", "中心项目组套关系字典");
            //dic.Add("BTestItemControl", "项目对照字典");
            //dic.Add("BLabTestItem", "实验室项目字典");
            //dic.Add("BLabItemCon", "实验室项目组套关系字典");
            //dic.Add("BLabTestItemDeliveryRule", "实验室项目外送规则字典");
            //dic.Add("BLabTestItemDeliveryRuleInfo", "实验室项目外送规则明细字典");
            //dic.Add("NrequestForm", "申请单业务");
            //dic.Add("ReportForm", "申请单业务");
            //dic.Add("Delivery", "物流系统");
            //dic.Add("BSampleType", "样本类型字典");
            //dic.Add("BSickType", "就诊类型字典");
            //dic.Add("BMicroAntiRange", "微生物字典");
            //dic.Add("BMicroAntiClass", "微生物抗生素字典");
            //dic.Add("BTestItem", "中心项目字典");
            //dic.Add("BAnti", "抗生素字典");
            //dic.Add("BMicro", "微生物字典");
            //dic.Add("Color", "城市字典");
            //foreach (var a in dic)
            //{
            //    if (MethodName.ToUpper().IndexOf(a.Key.ToUpper()) >= 0)
            //    {
            //        string context = a.Value;

            //        string ip=ZhiFang.WebLis.PlatForm.Common.IPHelper.GetClientIP();

            //        string EmpID = ZhiFang.Common.Public.Cookie.CookieHelper.Read("000200");
            //        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("000201");



            //        if (MethodName.ToUpper().IndexOf("SEARCH") >= 0 || MethodName.ToUpper().IndexOf("GET") >= 0)
            //        {
            //            context = context + "查询操作";
            //        }
            //        else if (MethodName.ToUpper().IndexOf("SAVE") >= 0 || MethodName.ToUpper().IndexOf("ADD") >= 0)
            //        {
            //            context = context + "新增操作";
            //        }
            //        else if (MethodName.ToUpper().IndexOf("UPDATE") >= 0 || MethodName.ToUpper().IndexOf("SET") >= 0)
            //        {
            //            context = context + "修改操作";
            //        }
            //        else if (MethodName.IndexOf("DEL") >= 0)
            //        {
            //            context = context + "删除操作";
            //        }
            //        else
            //        {
            //            context = context + "操作";
            //        }
            //        string logstr = ZhiFang.WebLis.PlatForm.Common.JsonSerializer.JsonDotNetSerializer(new SLog()
            //        {
            //            IP = ip,
            //            OperateName = "系统日志",
            //            Comment=context,
            //            OperateType = "10000008",
            //            EmpID = long.Parse(EmpID),
            //            EmpName = EmpName
            //        });
            //        ZhiFang.WebLis.PlatForm.Common.LIIPHelp.LIIPAddSLog("{\"entity\":" + logstr + "}");
            //        break;
            //    }
            //}
            return invocation.Proceed();
        }
    }

   
}
