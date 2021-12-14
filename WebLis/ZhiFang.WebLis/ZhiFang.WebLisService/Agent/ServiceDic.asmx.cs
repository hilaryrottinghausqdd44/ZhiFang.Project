using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using ECDS.Common;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WSInvoke;


namespace ZhiFang.WebLisService.Agent
{
    /// <summary>
    /// ServiceDic 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServiceDic : System.Web.Services.WebService
    {

        public string wsfAddr = LIS.CacheConfig.Util.Readcfg.ReadINIConfig("ServiceDicAddr").ToString();

        /// <summary>
        /// 取字典信息, 最多取50行/次
        /// </summary>
        /// <param name="DicName">表名</param>
        /// <param name="PageStarts">起始页</param>
        /// <param name="RowsNeed">一页取多少条</param>
        /// <param name="strOrderByField">字典数据排序字段</param>
        /// <param name="startPageReturn">真正返回起始页</param>
        /// <param name="RowsNeedReturn">返回本页数据行</param>
        /// <param name="RowsCount">表中共有多少行</param>
        /// <param name="ReturnDescription"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetDicTable(
            string DicName,
            int PageStarts,
            int RowsNeed,
            string strOrderByField,
            out int startPageReturn,
            out int RowsNeedReturn,
            out int RowsCount,
            out string ReturnDescription)
        {

            startPageReturn = 0;
            RowsNeedReturn = 0;
            RowsCount = 0;
            ReturnDescription = "";

            object[] args = new object[8];
            args[0] = DicName;
            args[1] = PageStarts;
            args[2] = RowsNeed;
            args[3] = strOrderByField;
            args[4] = startPageReturn;
            args[5] = RowsNeedReturn;
            args[6] = RowsCount;
            args[7] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            string rs = (string)WebServiceHelper.InvokeWebService(wsfAddr, "GetDicTable", args);

            return rs;
        }
    }
}
