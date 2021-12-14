using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;
using System.Text;
using ECDS.Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// AutoUpdateFlag 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class AutoUpdateFlag : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet QueryBarCode(string clientno, string topNum, string StartDate, string EndDate)
        {
            Log.Info(String.Format("自动签收打标志开始clientno={0},topNum={1},StartDate={2},EndDate={3}", clientno, topNum, StartDate, EndDate));
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            if (topNum != "")
            {
                sb.Append(" top " + topNum + " ");
            }
            sb.Append(String.Format(" *  from nrequestform where nrequestformno in (select nrequestformno from nrequestitem where barcodeformno in (select b.barcodeformno from barcodeform b where WebLisSourceOrgId='{0}' and WebLisFlag=5)) ", clientno));

            if (StartDate.Length > 0)
            {
                sb.Append(" and OperDate>='" + StartDate + "'");
            }
            if (EndDate.Length > 0)
            {
                sb.Append(" and OperDate<='" + EndDate + "'");
            }

            SqlServerDB sqlDB = new SqlServerDB();
            DataSet ds = sqlDB.ExecDS(sb.ToString());
            Log.Info(String.Format("自动签收打标志:{0}", sb.ToString()));

            return ds;
        }

        [WebMethod]
        public int UpdateBarCode(string nrequestformno, int flag)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("update barcodeform set weblisflag = {0} where weblisflag=5 and barcodeformno in (select barcodeformno from nrequestitem where nrequestformno='{1}')",flag, nrequestformno));
            SqlServerDB sqlDB = new SqlServerDB();
            int r = sqlDB.ExecuteNonQuery(sb.ToString());
            Log.Info(String.Format("更新签收标志:{0}", sb.ToString()));
            return r;
        }
    }
}
