using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml;
using ECDS.Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class A : System.Web.Services.WebService
    {


        //社区医院BarcodeForm [WebLisFlag]: 0未上传，1上传, 2,修改中, 3删除,4(预留),5签收,6退回, 7核收，8正在检验,9 报告重审中, 10报告已发,11报告修订,12 部分报告

        //交换数据中心BarcodeForm [WebLisFlag]: 0未处理, 1(预留), 2修改中, 3删除,4(预留),5签收, 6退回, 7核收，8正在检验, 9 报告重审中,10报告已发, 11报告修订, 12 部分报告


        [WebMethod(Description = "下载申请")]
        public bool DownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //下载人的其他信息，下载人姓名，地点，时间等等扩展信息(本次先不开发)
            out XmlNode nodeBarCode,        //一个条码XML
            out XmlNode nodeNRequestItem,   //多少个项目
            out XmlNode nodeNRequestForm,   //多少个申请单
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            nodeBarCode = null;
            nodeNRequestForm = null;
            nodeNRequestItem = null;
            xmlWebLisOthers = null;
            ReturnDescription = "";

            ECDS.Common.Log.Info(String.Format("下载申请开始DestiOrgID={0},BarCodeNo={1}", DestiOrgID, BarCodeNo));
            ////---------------------------------------------------------------------------------------------------------
            //-----
            if (DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "外送单位,与标本条码号不能为空";
                ECDS.Common.Log.Error("外送单位,与标本条码号不能为空");
                return false;
            }
            DBUtility.IDBConnection sqlDB = LIS.DataConn.CreateLisDB();
            string strsql = "select top 1 * from barcodeForm where BarCode='"+ BarCodeNo + "'";
            DataSet dsBarCodeForm = sqlDB.ExecDS(strsql);
            //+ "' and WebLisOrgID='" + DestiOrgID + "'");
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            //WebLisSourceOrgID=SourceOrgID
            ECDS.Common.Log.Info("执行查询语句:" + strsql);

            if (!ECDS.Common.Security.FormatTools.CheckDataSet(dsBarCodeForm))
            {
                ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
                ECDS.Common.Log.Error("未找到条码号为[" + BarCodeNo + "]的数据");
                return false;
            }
            
            //if (dsBarCodeForm.Tables[0].Rows.Count == 0)
            //{
            //    ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
            //    return false;
            //}
            try
            {

                string PreviouseWebLisFlag = "0";
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                {
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                }
                if (PreviouseWebLisFlag == "5" || PreviouseWebLisFlag == "3"
                    || Convert.ToInt32(PreviouseWebLisFlag) > 6)
                //|| PreviouseWebLisFlag == "7"
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]";
                    Log.Error("数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]");
                    return false;
                }

                //这里要讨论决定, barcodeFormNo,NRequestFormNo等重新生成唯一号，用于NRequestItem关联

                string strBarCodeFormNo = dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"].ToString();
                if (strBarCodeFormNo == "")
                {
                    ReturnDescription = "BarCodeFormNo为空";
                    Log.Error("BarCodeFormNo为空,程序退出");
                    return false;
                }
                //-----
                XmlDocument docBarCode = new XmlDocument();     //条码信息
                docBarCode.LoadXml(dsBarCodeForm.GetXml());
                nodeBarCode = docBarCode.DocumentElement;
                //-----
                XmlDocument docNRequestItem = new XmlDocument();   //项目信息
                strsql = "select * from NRequestItem where BarCodeFormNo=" + strBarCodeFormNo;
                DataSet dsItem = sqlDB.ExecDS(strsql);

                if (!ECDS.Common.Security.FormatTools.CheckDataSet(dsItem))
                {
                    ReturnDescription = String.Format("未找到该条码所对应的项目，查询语句为:{0}", strsql);
                    Log.Error(String.Format("未找到该条码所对应的项目，查询语句为:{0}", strsql));
                    return false;
                }

                docNRequestItem.LoadXml(dsItem.GetXml());
                nodeNRequestItem = docNRequestItem.DocumentElement;
                //-----
                XmlDocument docNRequestForm = new XmlDocument();

                strsql = "select * from NRequestForm where NRequestFormNo='" + dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString() + "'";
                DataSet dsForm = sqlDB.ExecDS(strsql);

                docNRequestForm.LoadXml(dsForm.GetXml());
                nodeNRequestForm = docNRequestForm.DocumentElement;

            }
            catch (Exception ex)
            {
                ReturnDescription += "下载申请失败" + ex.Message;
                Log.Error("下载申请失败" + ex.Message);
                return false;
            }
            ////---------------------------------------------------------------------------------------------------------
            return true;
        }


        [WebMethod(Description = "取消下载")]
        public bool DownloadBarCodeCancel(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            int flag = 0;
            SqlServerDB sqlDB = new SqlServerDB();
            string strsql = String.Format("update barcodeform set WebLisFlag={0} where 2>1 and barcode = '{1}'", flag, BarCodeNo);
            sqlDB.ExecuteNonQuery(strsql);
            ReturnDescription = "取消签收标志";
            return true;
        }

        [WebMethod(Description = "签收标志")]
        public bool DownloadBarCodeFlag(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            int flag = 5;
            SqlServerDB sqlDB = new SqlServerDB();
            string strsql = String.Format("update barcodeform set WebLisFlag={0} where 2>1 and barcode = '{1}'", flag, BarCodeNo);
            sqlDB.ExecuteNonQuery(strsql);

            ReturnDescription = "打签收标志";
            return true;
        }

        [WebMethod(Description = "退回")]
        public bool RefuseDownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            int flag = 6;
            SqlServerDB sqlDB = new SqlServerDB();
            string strsql = String.Format("update barcodeform set WebLisFlag={0} where 2>1 and barcode = '{1}'", flag, BarCodeNo);
            sqlDB.ExecuteNonQuery(strsql);

            ReturnDescription = "取消签收标志";
            return true;
        }


        [WebMethod(Description = "下载申请")]
        public bool DownloadBarCode_yz(
            string SourceOrgID,             //送检单位的编号
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //下载人的其他信息，下载人姓名，地点，时间等等扩展信息(本次先不开发)
            out XmlNode nodeBarCode,        //一个条码XML
            out XmlNode nodeNRequestItem,   //多少个项目
            out XmlNode nodeNRequestForm,   //多少个申请单
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            nodeBarCode = null;
            nodeNRequestForm = null;
            nodeNRequestItem = null;
            xmlWebLisOthers = null;
            ReturnDescription = "";

            ////---------------------------------------------------------------------------------------------------------
            //-----
            if (DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "外送单位,与标本条码号不能为空";
                return false;
            }
            SqlServerDB sqlDB = new SqlServerDB();
            string strtemp = String.Format("select top 1 * from barcodeForm where BarCode='{0}' and WebLisOrgID='{1}' and WebLisSourceOrgID='{2}'", BarCodeNo, DestiOrgID, SourceOrgID);
            DataSet dsBarCodeForm = sqlDB.ExecDS(strtemp);
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            //WebLisSourceOrgID=SourceOrgID

            if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
            {
                ReturnDescription = "barcodeForm出错，请检查数据库结构[barcodeForm]表[WebLisOrgID][BarCode]字段";
                return false;
            }

            if (dsBarCodeForm.Tables[0].Rows.Count == 0)
            {
                ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
                return false;
            }

            string PreviouseWebLisFlag = "0";
            if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
            if (PreviouseWebLisFlag == "5"
                || Convert.ToInt32(PreviouseWebLisFlag) > 6)
            //|| PreviouseWebLisFlag == "7"
            //|| PreviouseWebLisFlag == "8"
            //|| PreviouseWebLisFlag == "10")
            {
                ReturnDescription = "数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]";
                return false;
            }

            //这里要讨论决定, barcodeFormNo,NRequestFormNo等重新生成唯一号，用于NRequestItem关联

            string strBarCodeFormNo = dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"].ToString();

            //-----
            XmlDocument docBarCode = new XmlDocument();     //条码信息
            docBarCode.LoadXml(dsBarCodeForm.GetXml());
            nodeBarCode = docBarCode.DocumentElement;
            //-----
            XmlDocument docNRequestItem = new XmlDocument();   //项目信息
            DataSet dsItem = sqlDB.ExecDS("select * from NRequestItem where BarCodeFormNo=" + strBarCodeFormNo);
            docNRequestItem.LoadXml(dsItem.GetXml());
            nodeNRequestItem = docNRequestItem.DocumentElement;
            //-----
            XmlDocument docNRequestForm = new XmlDocument();
            DataSet dsForm = sqlDB.ExecDS("select * from NRequestForm where NRequestFormNo='" + dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString() + "'");
            docNRequestForm.LoadXml(dsForm.GetXml());
            nodeNRequestForm = docNRequestForm.DocumentElement;
            ////---------------------------------------------------------------------------------------------------------
            return true;
        }
    }
}
