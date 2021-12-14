using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using ECDS.Common;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ZhiFang.WebLisService.WebService
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
            ECDS.Common.Log.Info(String.Format("下载字典开始DicName={0},PageStarts={1},RowsNeed={2},strOrderByField={3}",DicName,PageStarts,RowsNeed,strOrderByField));
            if (RowsNeed > 50 || RowsNeed < 1)
                RowsNeed = 50;
            if (PageStarts < 0)
                PageStarts = 1;

            startPageReturn = 0;
            RowsNeedReturn = 0;
            RowsCount = 0;
            ReturnDescription = "";
            try
            {
                SqlServerDB sqldb = new SqlServerDB();
                string sql = "select * from [" + DicName + "]";
                if (strOrderByField != null && strOrderByField.Trim() != "")
                    sql += " order by " + strOrderByField;
                DataSet dsAll = sqldb.ExecDSPages(sql, PageStarts, RowsNeed, out startPageReturn, out RowsCount);
                if (dsAll != null && dsAll.Tables.Count > 0)
                {
                    RowsNeed = dsAll.Tables[0].Rows.Count;
                    return dsAll.GetXml();
                }
                else
                {
                    ReturnDescription = "传入表名[]有误，或数据库服务器无法连接";
                    ECDS.Common.Log.Error("传入表名[]有误，或数据库服务器无法连接");
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                ECDS.Common.Log.Error(ReturnDescription);
                return null;
            }
            return null;
        }
    }
}
