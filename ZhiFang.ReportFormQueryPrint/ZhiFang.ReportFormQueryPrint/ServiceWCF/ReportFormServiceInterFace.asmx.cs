using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using ZhiFang.ReportFormQueryPrint.BLL;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    /// <summary>
    /// ReportFormServiceInterFace 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.ReportFormQueryPrint")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ReportFormServiceInterFace : System.Web.Services.WebService
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestForm brqf = new BLL.BRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRFPReportFormPrintOperation BRFRFPO = new BLL.BRFPReportFormPrintOperation();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestForm bnrf = new BLL.BNRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestItem bnri = new BLL.BNRequestItem();
        [WebMethod(Description ="中日医院自助打单机服务，先调用HIS视图")]
        public bool ZhongRiYiYuanZiZhuDaDanReportFormService(string CardNo,out string[] ReportFormPdfPath,out string ErrorInfo,int searchMonth)
        {
            #region 新改版
            /*try
            {
                ZhiFang.Common.Log.Log.Debug("调用服务开始.ZhongRiYiYuanZiZhuDaDanReportFormService");
                ErrorInfo = "";
                ReportFormPdfPath = null;
                #region 初始化CardNo
                if (CardNo == null || CardNo.Trim() == "")
                {
                    ErrorInfo = "调用服务异常！CardNo为空！";
                    ZhiFang.Common.Log.Log.Debug("ZhongRiYiYuanZiZhuDaDanReportFormService.调用服务异常！CardNo为空！");
                    ReportFormPdfPath = null;
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug("ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo："+ CardNo);
                string[] CardNoArr = CardNo.Split(';');
                string strWhere = " 1=2 ";
                for (var i = 0 ; i < CardNoArr.Length ; i++) {
                    strWhere += " or zdy9 = '" + CardNoArr[i] + "' ";
                } 
                strWhere += " and (printtimes=0) and receivedate>='" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss") + "' and (SickTypeName = '门诊' or SickTypeName = '国门')";
                //string CardValue = (CardNo.Trim().Length == 12) ? CardNo.Trim().Substring(0, 9) : CardNo;//卡号等于12取前9位，否则不变。
                //ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo:" + CardNo+ ",CardValue:" + CardValue);
                #endregion

                #region 调用HIS视图
                //if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString") == null || ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim() == "")
                //{
                //    ErrorInfo = "调用服务异常！HIS数据库连接未配置！";
                //    ReportFormPdfPath = null;
                //    return false;
                //}
                //string HISOrcaleConnectionString = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim();
                //OracleConnection oc = new OracleConnection(HISOrcaleConnectionString);
                //ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HISOrcaleConnectionString:"+ HISOrcaleConnectionString);
                //string hissql = " select newzrhis.v_lis_cardno.card_no from newzrhis.v_lis_cardno where markno='" + CardValue + "' ";
                //ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.hissql:" + hissql);

                //DataTable dt = ExecuteDataTable(hissql,oc);
                //if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                //{
                //    ErrorInfo = "调用服务异常！HIS数据库未查到数据！";
                //    ReportFormPdfPath = null;
                //    return false;
                //}
                //if (dt.Rows[0]["card_no"] == null || dt.Rows[0]["card_no"].ToString().Trim() == "")
                //{
                //    ErrorInfo = "调用服务异常！HIS返回的卡号为空！";
                //    ReportFormPdfPath = null;
                //    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HIS返回的卡号为空!");
                //    return false;
                //}
                //string card_no = dt.Rows[0]["card_no"].ToString().Trim();
                //ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HIS返回的卡号card_no:"+ card_no);
                #endregion

                #region 查询报告库
                DataSet dsreportform = new DataSet();
                dsreportform = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo",strWhere);
                if (dsreportform == null || dsreportform.Tables.Count<=0 || dsreportform.Tables[0].Rows.Count <= 0)
                {
                    ErrorInfo = "暂无卡号为:'"+CardNo+"'的报告单！";
                    ZhiFang.Common.Log.Log.Debug("ZhongRiYiYuanZiZhuDaDanReportFormService.暂无卡号为:" + CardNo + "'的报告单！");
                    ReportFormPdfPath = null;
                    return false;
                }
                ReportFormPdfPath = new string[dsreportform.Tables[0].Rows.Count];
                List<string> reportformidlist = new List<string>();
                List<string> reportformidlist66 = new List<string>();
                for (int i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                {
                    var listreportformfile = bprf.CreatReportFormFiles(new List<string>() { dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim() }, ReportFormTitle.center, ReportFormFileType.PDF, dsreportform.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0);
                    if (listreportformfile.Count > 0)
                    {
                        ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[0].PDFPath.Replace(@"\", "/");
                        reportformidlist.Add(dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                        reportformidlist66.Add(dsreportform.Tables[0].Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SampleNo"].ToString().Trim());
                    }
                }
                //ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo:" + CardNo+ ";CardValue:" + CardValue + ";card_no:" + dt.Rows[0]["card_no"].ToString().Trim() + ";ReportFormPdfPath:" + string.Join(",", ReportFormPdfPath));
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo:" + CardNo   + ";ReportFormPdfPath:" + string.Join(",", ReportFormPdfPath));
                #endregion
                #region 打印次数累计
                brf.UpdatePrintTimes(reportformidlist.ToArray(),"");//报告库打标记
                IDReportForm dal = DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66",ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                ZhiFang.Common.Log.Log.Debug("调用服务结束.ZhongRiYiYuanZiZhuDaDanReportFormService");
                #endregion
                return true;
            }
            catch (Exception e)
            {
                ErrorInfo = "调用服务异常！请重试或联系管理员！";
                ReportFormPdfPath = null;
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.异常:" + e.ToString());
                return false;
            }*/
            #endregion
            try
            {
                ErrorInfo = "";
                ReportFormPdfPath = null;
                #region 初始化CardNo
                if (CardNo == null || CardNo.Trim() == "")
                {
                    ErrorInfo = "调用服务异常！CardNo为空！";
                    ReportFormPdfPath = null;
                    return false;
                }
                string CardValue = CardNo;
                //卡号等于12并且不以0开头取前9位，否则不变。
                if (CardNo.Trim().Length == 12 && CardNo.Trim().Substring(0, 1) != "0" ) {
                    CardValue = CardNo.Trim().Substring(0, 9);
                }
                

                //string CardValue = (CardNo.Trim().Length == 12) ? CardNo.Trim().Substring(0, 9) : CardNo;//卡号等于12取前9位，否则不变。
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo:" + CardNo + ",CardValue:" + CardValue);
                #endregion
                #region 调用HIS视图
                if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString") == null || ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim() == "")
                {
                    ErrorInfo = "调用服务异常！HIS数据库连接未配置！";
                    ReportFormPdfPath = null;
                    return false;
                }
                string HISOrcaleConnectionString = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim();
                OracleConnection oc = new OracleConnection(HISOrcaleConnectionString);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HISOrcaleConnectionString:" + HISOrcaleConnectionString);
                string hissql = " select newzrhis.v_lis_cardno.card_no from newzrhis.v_lis_cardno where markno='" + CardValue + "' ";
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.hissql:" + hissql);

                DataTable dt = ExecuteDataTable(hissql, oc);
                if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                {
                    ErrorInfo = "调用服务异常！HIS数据库未查到数据！";
                    ReportFormPdfPath = null;
                    return false;
                }
                if (dt.Rows[0]["card_no"] == null || dt.Rows[0]["card_no"].ToString().Trim() == "")
                {
                    ErrorInfo = "调用服务异常！HIS返回的卡号为空！";
                    ReportFormPdfPath = null;
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HIS返回的卡号为空!");
                    return false;
                }
                string card_no = dt.Rows[0]["card_no"].ToString().Trim();
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.HIS返回的卡号card_no:" + card_no);
                #endregion
                #region 查询报告库
                DataSet dsreportform = new DataSet();
                //dsreportform = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", " zdy9='" + card_no + "' and (printtimes=0) and receivedate>='" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss") + "' and (SickTypeName = '门诊' or SickTypeName = '国门')");
                if (searchMonth>0)
                {
                    searchMonth = -1;
                }
                dsreportform = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", " zdy9='" + card_no + "' and receivedate>='" + DateTime.Now.AddMonths(searchMonth).ToString("yyyy-MM-dd HH:mm:ss") + "' and (SickTypeName = '门诊' or SickTypeName = '国门') and ISNULL(PrintTimes,0) <1");
                if (dsreportform == null || dsreportform.Tables.Count <= 0 || dsreportform.Tables[0].Rows.Count <= 0)
                {
                    ErrorInfo = "暂无卡号为:'" + CardNo + "'的报告单！";
                    ReportFormPdfPath = null;
                    return false;
                }
                ReportFormPdfPath = new string[dsreportform.Tables[0].Rows.Count];
                List<string> reportformidlist = new List<string>();
                List<string> reportformidlist66 = new List<string>();
                for (int i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                {
                    List<ReportFormFilesVO> listreportformfile = new List<ReportFormFilesVO>();
                    //判断是否糖耐量等特殊报告，如果是，多份没全部检验完成则返回提示信息，都检验完成则合并到一份报告
                    if (!ZhongRiBloodGlucoseTesting(dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim(), dsreportform.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0, ref ErrorInfo,ref listreportformfile))
                    {
                        listreportformfile = bprf.CreatReportFormFiles(new List<string>() { dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim() }, ReportFormTitle.center, ReportFormFileType.PDF, dsreportform.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0);
                    }
                    if (listreportformfile.Count > 0)
                    {
                        ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[0].PDFPath.Replace(@"\", "/");
                        reportformidlist.Add(dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                        reportformidlist66.Add(dsreportform.Tables[0].Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SampleNo"].ToString().Trim());
                    }
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.CardNo:" + CardNo + ";CardValue:" + CardValue + ";card_no:" + dt.Rows[0]["card_no"].ToString().Trim() + ";ReportFormPdfPath:" + string.Join(",", ReportFormPdfPath));
                #endregion
                #region 打印次数累计
                brf.UpdatePrintTimes(reportformidlist.ToArray(),"");//报告库打标记
                IDReportForm dal = DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");

                #endregion
                return true;
            }
            catch (Exception e)
            {
                ErrorInfo = "调用服务异常！请重试或联系管理员！";
                ReportFormPdfPath = null;
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService.异常:" + e.ToString());
                return false;
            }
        }
        /// <summary>
        /// 中日医院糖耐量报告定制
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="SectionType"></param>
        /// <param name="flag"></param>
        /// <param name="errorInfo"></param>
        /// <param name="listreportformfile"></param>
        /// <returns></returns>
        public bool ZhongRiBloodGlucoseTesting(string ReportFormID, string SectionType, int flag, ref string errorInfo,ref List<ReportFormFilesVO> listreportformfile)
        {
            bool matchFlag = false;
            #region 中日定制-糖耐量
            string BloodGlucoseTestingConfiguration = ConfigHelper.GetConfigString("BloodGlucoseTestingConfiguration");
            if (!string.IsNullOrWhiteSpace(BloodGlucoseTestingConfiguration) && BloodGlucoseTestingConfiguration == "1")
            {
                BRequestForm bqf = new BLL.BRequestForm();
                DataTable reportform = brf.GetListByDataSource(ReportFormID);

                if (reportform != null && reportform.Rows.Count > 0)
                {
                    if (reportform.Rows[0]["zdy4"] == null || reportform.Rows[0]["zdy4"].ToString().Trim() == "")
                    {
                        errorInfo = "zdy4为空";
                        return false;
                    }
                    DateTime ReceiveDate = DateTime.Parse(reportform.Rows[0]["zdy4"].ToString().Trim());
                    string receiveDate = ReceiveDate.ToString("yyyy/MM/d");
                    string where = "zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and zdy4>='" + receiveDate + "' and zdy4<'" + receiveDate + " 23:59:59.999'";
                    //查NRequestForm表，对应病人当天数据
                    List<NRequestForm> NRequestFormList = bnrf.GetModelList_FormFull("", where);
                    if (NRequestFormList != null && NRequestFormList.Count > 0)
                    {
                        List<string> serialNos = new List<string>();
                        for (int i = 0; i < NRequestFormList.Count; i++)
                        {
                            serialNos.Add(NRequestFormList[i].SerialNo);
                        }
                        #region 先判断本分报告是不是特殊报告，如果不是直接不走此流程
                        List<Model.NRequestItem> NRequestItemList1 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (101010,101111,101112,101113,101114,101115)");//糖耐量
                        List<Model.NRequestItem> NRequestItemList2 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (701036,701043,701039,701041,701045,701110,701035,701037)");//胰岛素
                        List<Model.NRequestItem> NRequestItemList3 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (406117,406129,406130,406131,701100)");//C肽
                        List<Model.NRequestItem> NRequestItem1 = NRequestItemList1.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem2 = NRequestItemList2.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem3 = NRequestItemList3.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        bool isSpecial = false;
                        //对应NRequestItem表,特殊项目号，并且两项以上
                        if (NRequestItemList1 != null && NRequestItemList1.Count >= 2 && NRequestItem1.Count > 0)
                        {
                            errorInfo = "各时间点血糖报告尚未全部完成，请稍后再试，全部完成后系统将把各时点血糖合并打印。";
                            isSpecial = true;
                        }
                        else if (NRequestItemList2 != null && NRequestItemList2.Count >= 2 && NRequestItem2.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList2;
                            errorInfo = "各时间点胰岛素报告尚未全部完成，请稍后再试，全部完成后系统将把各时点胰岛素合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.胰岛素检测");
                        }
                        else if (NRequestItemList3 != null && NRequestItemList3.Count >= 2 && NRequestItem3.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList3;
                            errorInfo = "各时间点C肽报告尚未全部完成，请稍后再试，全部完成后系统将把各时点C肽合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.C肽检测");
                        }
                        else
                        {
                            return false;
                        }
                        #endregion

                        if (isSpecial)
                        {
                            ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.特殊项目2项及以上");
                            //项目可能开重复，重复的项目如果有一个对应的检验单签收或核收，那么其他项目则作废
                            var itemGroups = NRequestItemList1.GroupBy(a => a.ParItemNo);//按项目号分组
                            List<string> itemSerialNoList = new List<string>();//核收的项目申请单号集合
                            foreach (var group in itemGroups)
                            {
                                foreach (var item in group)
                                {
                                    if (item.RECEIVEFLAG == 1)
                                    {
                                        itemSerialNoList.Add(item.SerialNo);
                                    }
                                }
                            }
                            //判断所有特殊项目是否全部核收
                            if (itemSerialNoList.Count != itemGroups.Count())
                            {
                                return true;
                            }
                            //特殊项目全部核收再查看报告单是否有在检验中
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.特殊项目全部核收");
                            int requestFormCount = bqf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (requestFormCount > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.报告单有" + requestFormCount + "份正在检验中");
                                return true;
                            }
                            //均完成后，判断是否全部二审
                            int finalSendCount = barf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (finalSendCount != itemSerialNoList.Count)
                            {
                                ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.报告单没有全部二审");
                                return true;
                            }
                            //所有报告检验完成，开始合成
                            errorInfo = "";
                            string SerialNos = string.Join(",", itemSerialNoList);
                            ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.所有报告检验完成，开始合成：" + SerialNos);
                            //如果都检验完成则合并所有item到一张pdf里
                            listreportformfile = bprf.CreatReportFormFilesGlucoseToleranceZhongRi(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, SerialNos, flag);
                            return true;
                        }
                    }
                }
            }
            #endregion
            return matchFlag;
        }
        [WebMethod(Description = "中日医院自助打单机服务不限制打印次数，先调用HIS视图")]
        public bool ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes(string CardNo, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            
            try
            {
                ErrorInfo = "";
                ReportFormPdfPath = null;
                #region 初始化CardNo
                if (CardNo == null || CardNo.Trim() == "")
                {
                    ErrorInfo = "调用服务异常！CardNo为空！";
                    ReportFormPdfPath = null;
                    return false;
                }
                string CardValue = CardNo;
                //卡号等于12并且不以0开头取前9位，否则不变。
                if (CardNo.Trim().Length == 12 && CardNo.Trim().Substring(0, 1) != "0")
                {
                    CardValue = CardNo.Trim().Substring(0, 9);
                }


                //string CardValue = (CardNo.Trim().Length == 12) ? CardNo.Trim().Substring(0, 9) : CardNo;//卡号等于12取前9位，否则不变。
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.CardNo:" + CardNo + ",CardValue:" + CardValue);
                #endregion
                #region 调用HIS视图
                if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString") == null || ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim() == "")
                {
                    ErrorInfo = "调用服务异常！HIS数据库连接未配置！";
                    ReportFormPdfPath = null;
                    return false;
                }
                string HISOrcaleConnectionString = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HISOrcaleConnectionString").Trim();
                OracleConnection oc = new OracleConnection(HISOrcaleConnectionString);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.HISOrcaleConnectionString:" + HISOrcaleConnectionString);
                string hissql = " select newzrhis.v_lis_cardno.card_no from newzrhis.v_lis_cardno where markno='" + CardValue + "' ";
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.hissql:" + hissql);

                DataTable dt = ExecuteDataTable(hissql, oc);
                if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                {
                    ErrorInfo = "调用服务异常！HIS数据库未查到数据！";
                    ReportFormPdfPath = null;
                    return false;
                }
                if (dt.Rows[0]["card_no"] == null || dt.Rows[0]["card_no"].ToString().Trim() == "")
                {
                    ErrorInfo = "调用服务异常！HIS返回的卡号为空！";
                    ReportFormPdfPath = null;
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.HIS返回的卡号为空!");
                    return false;
                }
                string card_no = dt.Rows[0]["card_no"].ToString().Trim();
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.HIS返回的卡号card_no:" + card_no);
                #endregion
                #region 查询报告库
                DataSet dsreportform = new DataSet();
                dsreportform = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", " zdy9='" + card_no + "' and receivedate>='" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss") + "' and (SickTypeName = '门诊' or SickTypeName = '国门')");
                if (dsreportform == null || dsreportform.Tables.Count <= 0 || dsreportform.Tables[0].Rows.Count <= 0)
                {
                    ErrorInfo = "暂无卡号为:'" + CardNo + "'的报告单！";
                    ReportFormPdfPath = null;
                    return false;
                }
                ReportFormPdfPath = new string[dsreportform.Tables[0].Rows.Count];
                List<string> reportformidlist = new List<string>();
                List<string> reportformidlist66 = new List<string>();
                for (int i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                {
                    List<ReportFormFilesVO> listreportformfile = new List<ReportFormFilesVO>();
                    //判断是否糖耐量等特殊报告，如果是，多份没全部检验完成则返回提示信息，都检验完成则合并到一份报告
                    if (!ZhongRiBloodGlucoseTesting(dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim(), dsreportform.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0, ref ErrorInfo, ref listreportformfile))
                    {
                        listreportformfile = bprf.CreatReportFormFiles(new List<string>() { dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim() }, ReportFormTitle.center, ReportFormFileType.PDF, dsreportform.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0);
                    }
                    if (listreportformfile.Count > 0)
                    {
                        ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[0].PDFPath.Replace(@"\", "/");
                        reportformidlist.Add(dsreportform.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                        reportformidlist66.Add(dsreportform.Tables[0].Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Tables[0].Rows[i]["SampleNo"].ToString().Trim());
                    }
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.CardNo:" + CardNo + ";CardValue:" + CardValue + ";card_no:" + dt.Rows[0]["card_no"].ToString().Trim() + ";ReportFormPdfPath:" + string.Join(",", ReportFormPdfPath));
                #endregion
                #region 打印次数累计
                brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                IDReportForm dal = DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");

                #endregion
                return true;
            }
            catch (Exception e)
            {
                ErrorInfo = "调用服务异常！请重试或联系管理员！";
                ReportFormPdfPath = null;
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + "调用服务ZhongRiYiYuanZiZhuDaDanReportFormService_NoPrintTimes.异常:" + e.ToString());
                return false;
            }
        }
        [WebMethod(Description = "根据病历号生成PDF报告并返回报告URL（返回结果包含正在打印的报告单数和正在检验的报告单数）")]
        public bool GetReportFormPDFByPatNo(string PatNo, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = ""; 
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and PRINTTIMES <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool isPdf =  ReportFormWebService.GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        public static DataTable ExecuteDataTable(string sql, OracleConnection oc)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter oda = new OracleDataAdapter(sql, oc);
            oda.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("ExecuteDataTable未查到数据.sql:" + sql+ ".ConnectionString:" + oc.ConnectionString);
                return null;
            }
        }

        [WebMethod(Description = "北医三院报告服务:根据FormNo(四个关键字Receivedate;Section;TestTypeno;Sampleno)生成PDF报告并返回报告FTP路径")]
        public bool GetReportFormPDFByFormNo_FTP(string FormNo, string FormNoFormat, string SplitChar, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            double d = DateTime.Now.Ticks / 10000;
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (FormNo == null || FormNo.Trim().Length <= 0)
                {
                    ErrorInfo = "报告单号为空!";
                    return false;
                }
                if (SplitChar == null || SplitChar.Trim().Length <= 0)
                {
                    SplitChar = ";";
                }
                if (FormNoFormat == null || FormNoFormat.Trim().Length <= 0)
                {
                    FormNoFormat = "ReceiveDate" + SplitChar + "SectionNo" + SplitChar + "TestTypeNo" + SplitChar + "SampleNo";
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                string[] formnop = FormNo.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnop.Length != 4)
                {
                    ErrorInfo = "报告单号或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.报告单号或分割符出错。FormNo:" + FormNo + ", SplitChar:" + SplitChar);
                    return false;
                }
                string[] formnoformatp = FormNoFormat.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnoformatp.Length != 4)
                {
                    ErrorInfo = "报告单号格式或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.报告单号或分割符出错。FormNoFormat:" + FormNoFormat);
                    return false;
                }
                string urlWhere = " " + formnoformatp[0] + "='" + formnop[0] + "' and " + formnoformatp[1] + "='" + formnop[1] + "' and " + formnoformatp[2] + "='" + formnop[2] + "' and " + formnoformatp[3] + "='" + formnop[3] + "' ";

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";

                double k = DateTime.Now.Ticks / 10000;
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                double j = DateTime.Now.Ticks / 10000;
                double t = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP查询报告数据时间：" + t.ToString());
                double a = DateTime.Now.Ticks / 10000;
                bool IsDs = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
                double b = DateTime.Now.Ticks / 10000;
                double c = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP生成报告并返回报告路径时间：" + c.ToString());
                if (ReportFormPdfPath != null)
                {
                    List<string> rfids = new List<string>();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                        }
                    }
                    for (int i = 0; i < ReportFormPdfPath.Length; i++)
                    {
                        ReportFormPdfPath[i] = ReportFormPdfPath[i].Replace("http://", "ftp://");
                        string aa = System.AppDomain.CurrentDomain.BaseDirectory + ReportFormPdfPath[i].Substring(ReportFormPdfPath[i].IndexOf("/ReportFormFiles"));
                        System.IO.File.Copy(aa, aa = aa.Replace(rfids[i], FormNo), true);
                        System.IO.File.Delete(aa = aa.Replace(FormNo ,rfids[i] ));
                       ReportFormPdfPath[i] = ReportFormPdfPath[i].Replace(rfids[i], FormNo );
                    }
                }
                double e = DateTime.Now.Ticks / 10000;
                double f = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP服务总体执行时间：" + f.ToString());

                return IsDs;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.异常:" + e.ToString() + ".FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        public static bool GetReportFormPDFByDS(DataSet ds, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.没找到报告,请检查报告日期及查询条件。");
                ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                ReportFormPdfPath = null;
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.rfids:" + string.Join(",", rfids));
            var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
            if (listreportformfile.Count > 0)
            {

                ReportFormPdfPath = new string[listreportformfile.Count];
                for (int i = 0; i < listreportformfile.Count; i++)
                {
                    ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[i].PDFPath.Replace(@"\", "/");
                }
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.返回pdf路径:" + string.Join(",", ReportFormPdfPath));
                ErrorInfo = "";
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.未查找到报告单");
                ErrorInfo = "未查找到报告单";
                ReportFormPdfPath = null;
                return false;
            }
        }

        [WebMethod(Description = "秦皇岛第一医院报告服务:统一入口及公共接口")]
        public string zzjRequest(String req)
        {

            ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院服务.zzjRequest");

            string BusinessCode = "";//调用方法名称

            Dictionary<string, string> dictionarys = new Dictionary<string, string>();

            try
            {
                if (req == null || req == "")
                {
                    ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院服务.zzjRequest.传入参数为空req!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>req为空!</ErrorMsg><ReportData></ReportData></Response>";
                }

                //解析XML字符串开始
                var doc = new XmlDocument();
                doc.LoadXml(req);
                var rowNoteList = doc.SelectNodes("Request");
                if (rowNoteList != null)
                {
                    foreach (XmlNode rowNode in rowNoteList)
                    {
                        var fieldNodeList = rowNode.ChildNodes;
                        foreach (XmlNode courseNode in fieldNodeList)
                        {
                            dictionarys.Add(courseNode.Name.ToString(), courseNode.InnerText.Trim());
                        }
                    }
                }
                //解析XML字符串结束

                BusinessCode = dictionarys["BusinessCode"];

                string aa = "";
                if (BusinessCode == null || BusinessCode == "")
                {
                    ZhiFang.Common.Log.Log.Debug("BusinessCode为空!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>BusinessCode为空!</ErrorMsg><ReportData></ReportData></Response>";
                }
                else if (BusinessCode.Equals("GetLisReportBase64"))
                {
                    aa = this.GetLisReportBase64(req);
                } 
                else if (BusinessCode.Equals("SetLisReportPrinted")) 
                {//增加打印次数
                    var ReportId = dictionarys["ReportId"];
                    if (ReportId == null || ReportId == "")
                    {
                        ZhiFang.Common.Log.Log.Debug("BusinessCode为空!");
                        aa = "<Response><ResultCode>-1</ResultCode><ErrorMsg>ReportId为空!</ErrorMsg><ReportData></ReportData></Response>";
                    }
                    else {
                        string ip = HttpContext.Current.Request.UserHostAddress;
                        string[] reportformlist = ReportId.Split(',');
                        bool Ms66Flag = true;
                        bool flag = true;
                        #region 向lis加入打印次数
                        if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                        {
                            DataTable dsreportform = new DataTable();
                            dsreportform = brf.GetReportFormList(reportformlist);
                            if (dsreportform == null || dsreportform.Rows.Count <= 0)
                            {
                                aa = "<Response><ResultCode>-1</ResultCode><ErrorMsg>LIS中没有找到报告单!</ErrorMsg><ReportData></ReportData></Response>";
                            }
                            List<string> reportformidlist66 = new List<string>();
                            for (int i = 0; i < dsreportform.Rows.Count; i++)
                            {
                                reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                            }
                            IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                            Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                            ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院增加打印次数.IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                            Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                            ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院增加打印次数.IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                        }
                        #endregion
                        string operCode = dictionarys["OperCode"];
                        flag = brf.UpdatePrintTimes(reportformlist, "");
                        ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院增加打印次数.IP:" + ip + "添加打印次数:reportformID:" + ReportId + ";自助机机器号：" + operCode);
                        flag = brf.UpdateClientPrintTimes(reportformlist);
                        ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院增加打印次数.IP:" + ip + "添加外围打印次数:reportformID:" + ReportId + ";自助机机器号：" + operCode);
                        if (Ms66Flag && flag)
                        {
                            aa = "<Response><ResultCode>0</ResultCode><ErrorMsg>增加打印次数成功!</ErrorMsg><ReportData></ReportData></Response>";
                        }
                        else
                        {
                            aa = "<Response><ResultCode>-1</ResultCode><ErrorMsg>增加打印次数失败!</ErrorMsg><ReportData></ReportData></Response>";
                        }
                        //向打印记录表增加数据开始
                        RFPReportFormPrintOperation brfEntity = new RFPReportFormPrintOperation();
                        var EmpName = operCode;
                        foreach (string a in reportformlist)
                        {
                            string[] aaa  =  a.Split(';');
                            if (aaa.Count() > 1)
                            {
                                brfEntity.ReceiveDate = Convert.ToDateTime(aaa[0]);
                                brfEntity.SectionNo = int.Parse(aaa[1]);
                                brfEntity.TestTypeNo = int.Parse(aaa[2]);
                                brfEntity.SampleNo = aaa[3];
                            }
                            else {
                                brfEntity.BobjectID = long.Parse(a);
                            }
                            
                            if (EmpName != null && EmpName != "")
                            {
                                brfEntity.EmpName = EmpName;
                            }
                            BRFRFPO.Add(brfEntity);
                        }
                        //向打印记录表增加数据结束
                    }
                }
                return aa;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".zzjRequest.BusinessCode:" + BusinessCode );
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".zzjRequest:" + e.ToString());
                return "<Response><ResultCode>-1</ResultCode><ErrorMsg>" + this.GetType().FullName + ".zzjRequest:" + e.ToString() + "</ErrorMsg><ReportData></ReportData></Response>";
            }
        }

        [WebMethod(Description = "秦皇岛第一医院报告服务:根据传入字段和值生成PDF报告并返回XML文件格式字符串(报告PDF为Base64字符串)")]
        public string GetLisReportBase64(string batchXMLRequest)
        {
            
            ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院报告服务查询报告单开始.GetLisReportBase64");

            string ReportData = "";//接收返回值用
            string CardType = "";//卡类型
            string CardNo = "";//卡号
            string BeginDate ="";//开始时间
            string EndDate = "";//结束时间
            string OperCode = "";//自助机机器号
            Dictionary<string, string> dictionarys = new Dictionary<string, string>();
            
            try
            {
                if (batchXMLRequest == null || batchXMLRequest == "")
                {
                    ZhiFang.Common.Log.Log.Debug("batchXMLRequest为空!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>batchXMLRequest为空!</ErrorMsg><ReportData></ReportData></Response>";
                }

                //解析XML字符串开始
                var doc = new XmlDocument();
                doc.LoadXml(batchXMLRequest);
                var rowNoteList = doc.SelectNodes("Request");
                if (rowNoteList != null)
                {
                    foreach (XmlNode rowNode in rowNoteList)
                    {
                        var fieldNodeList = rowNode.ChildNodes;
                        foreach (XmlNode courseNode in fieldNodeList)
                        {
                            dictionarys.Add(courseNode.Name.ToString(),courseNode.InnerText.Trim());
                        }
                    }
                }
                //解析XML字符串结束

                CardType = dictionarys["CardType"];
                CardNo = dictionarys["CardNo"];
                BeginDate = dictionarys["BeginDate"];
                EndDate = dictionarys["EndDate"];
                OperCode = dictionarys["OperCode"];
                if (CardNo == null || CardNo == "" )
                {
                    ZhiFang.Common.Log.Log.Debug("CardNo为空!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>CardNo为空!</ErrorMsg><ReportData></ReportData></Response>";
                }
                if (BeginDate == null || BeginDate == "")
                {
                    ZhiFang.Common.Log.Log.Debug("BeginDate为空!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>BeginDate为空!</ErrorMsg><ReportData></ReportData></Response>";
                }

                if (EndDate == null || EndDate == "")
                {
                    ZhiFang.Common.Log.Log.Debug("EndDate为空!");
                    return "<Response><ResultCode>-1</ResultCode><ErrorMsg>EndDate为空!</ErrorMsg><ReportData></ReportData></Response>";
                }
                if (OperCode == null || OperCode == "")
                {
                    ZhiFang.Common.Log.Log.Debug("OperCode为空!");
                    
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetLisReportBase64.CardType:" + CardType + " CardNo:" + CardNo + " BeginDate: " + BeginDate+ ", EndDate:" + EndDate + ", OperCode:" + OperCode);

                string urlWhere = " ";
                //2021-4-22修改查询条件，bFinalSend = 1代表二审完毕，一审二审的报告单都要查询，一审的报告归为检验中
                urlWhere += "( zdy1 = '"+ CardNo + "' or SerialNo = '" + CardNo+"' or zdy2 = '"+ CardNo + "')   and (ReceiveDate >='" + BeginDate + "' and ReceiveDate <='" + EndDate + "')";
                urlWhere+= " and SickTypeName != '住院' and ItemName not in('细胞形态图文成像分析','骨髓象检查','骨髓象检查（复检）') order by ReceiveDate desc";
                string requestWhere = "( zdy1 = '" + CardNo + "' or SerialNo = '" + CardNo + "' or zdy2 = '" + CardNo + "')   and (ReceiveDate >='" + BeginDate + "' and ReceiveDate <='" + EndDate + "')";
                string reportFields = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,SampleTypeNo,ClientPrint,TestTypeName,Doctor,Sender2";
                string requestFields = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,SampleTypeNo,ClientPrint,TestType,Doctor";
                //ds---包含一审二审的报告单
                DataSet ds = barf.GetList_FormFull(reportFields, urlWhere);
                DataSet requestds = brqf.GetList_FormFull(requestFields, requestWhere);//检验中的报告单
                //对检验完成的报告单和检验中的报告单做处理，一审的归为检验中，二审的生成base64字符串
                bool isPdf = GetReportFormPDFBitsByDSXML(ds, requestds, 0, out ReportData);
                
                ZhiFang.Common.Log.Log.Debug("秦皇岛第一医院报告服务查询报告单结束.GetLisReportBase64");
                if (isPdf) {                    
                    return ReportData.Replace("qinhuangdaodiyiPdfXmlVO", "Item");
                }
                else{
                    return ReportData;
                }
               
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetLisReportBase64.CardType:" + CardType + " CardNo:" + CardNo + " BeginDate: " + BeginDate + ", EndDate:" + EndDate);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetLisReportBase64:" + e.ToString());
                return "<Response><ResultCode>-1</ResultCode><ErrorMsg>"+ this.GetType().FullName + ".GetLisReportBase64:" + e.ToString() + "</ErrorMsg><ReportData></ReportData></Response>";
            }
        }

        [WebMethod(Description = "秦皇岛第一医院定制服务:生成PDF报告并返回报告二进制流XML")]
        public static bool GetReportFormPDFBitsByDSXML(DataSet ds, DataSet requestds, int flag, out string ReportData)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            List<int> reportIndexs = new List<int>();//二审过的报告单在reportPdfXml.Response集合中的位置
            ReportPdfXml reportPdfXml = new ReportPdfXml();
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
            int requestCount = 0;
            int reportCount = 0;
            //将检验中的报告添加到返回结果
            if (requestds != null && requestds.Tables[0].Rows.Count > 0)
            {
                requestCount = requestds.Tables[0].Rows.Count;
                for (int i = 0; i < requestds.Tables[0].Rows.Count; i++)
                {
                    qinhuangdaodiyiPdfXmlVO rpxv = new qinhuangdaodiyiPdfXmlVO();
                    rpxv.ReportId = requestds.Tables[0].Rows[i]["ReportFormID"].ToString();
                    rpxv.reportType = requestds.Tables[0].Rows[i]["SampleTypeNo"].ToString();
                    rpxv.Status = "2";
                    rpxv.Type = requestds.Tables[0].Rows[i]["TestType"].ToString();
                    rpxv.JYDate = DateTime.Parse(requestds.Tables[0].Rows[i]["Receivedate"].ToString()).ToString("yyyyMMdd");
                    rpxv.Doctor = requestds.Tables[0].Rows[i]["Doctor"].ToString();
                    if (reportPdfXml.Response == null)
                    {
                        reportPdfXml.Response = new List<qinhuangdaodiyiPdfXmlVO>();
                        reportPdfXml.Response.Add(rpxv);
                    }
                    else
                    {
                        reportPdfXml.Response.Add(rpxv);
                    }
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDSXML.没找到检验中报告,请检查报告日期及查询条件。");
            }
            //将检验完成的报告添加到返回结果
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                reportCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Sender2"] == null || ds.Tables[0].Rows[i]["Sender2"].ToString() == "")
                    {
                        qinhuangdaodiyiPdfXmlVO rpxv = new qinhuangdaodiyiPdfXmlVO();
                        rpxv.ReportId = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        rpxv.reportType = ds.Tables[0].Rows[i]["SampleTypeNo"].ToString();
                        rpxv.Status = "2";//未二审的报告Sender2为空，归为检验中
                        rpxv.Type = ds.Tables[0].Rows[i]["TestTypeName"].ToString();
                        rpxv.JYDate = DateTime.Parse(ds.Tables[0].Rows[i]["Receivedate"].ToString()).ToString("yyyyMMdd");
                        rpxv.Doctor = ds.Tables[0].Rows[i]["Doctor"].ToString();
                        if (reportPdfXml.Response == null)
                        {
                            reportPdfXml.Response = new List<qinhuangdaodiyiPdfXmlVO>();
                            reportPdfXml.Response.Add(rpxv);
                        }
                        else
                        {
                            reportPdfXml.Response.Add(rpxv);
                        }
                        requestCount++;
                    }

                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Sender2"] != null && ds.Tables[0].Rows[i]["Sender2"].ToString() != "")
                    {
                        qinhuangdaodiyiPdfXmlVO rpxv = new qinhuangdaodiyiPdfXmlVO();
                        rpxv.ReportId = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        rpxv.reportType = ds.Tables[0].Rows[i]["SampleTypeNo"].ToString();
                        var cp = ds.Tables[0].Rows[i]["ClientPrint"];
                        if ( cp == null || cp.ToString() == "" || cp.ToString() == "0")
                        {
                            rpxv.Status = "0";
                        }
                        else if(cp != null && cp.ToString() != "" && cp.ToString() != "0") {
                            rpxv.Status = "1";
                        }
                        rpxv.Type = ds.Tables[0].Rows[i]["TestTypeName"].ToString();
                        rpxv.JYDate = DateTime.Parse(ds.Tables[0].Rows[i]["Receivedate"].ToString()).ToString("yyyyMMdd");
                        rpxv.Doctor = ds.Tables[0].Rows[i]["Doctor"].ToString();
                        if (reportPdfXml.Response == null)
                        {
                            reportPdfXml.Response = new List<qinhuangdaodiyiPdfXmlVO>();
                            reportPdfXml.Response.Add(rpxv);
                        }
                        else {
                            reportPdfXml.Response.Add(rpxv);
                        }
                        rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                }
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDSXML.rfids:" + string.Join(",", rfids));
                var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
                if (listreportformfile.Count > 0)
                {
                    for (int i = 0; i < listreportformfile.Count ; i++)
                    {
                        try
                        {
                            byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                            string JarContent = Convert.ToBase64String(byteArray);//将二进制转成Base64字符串
                            reportPdfXml.Response[i + requestCount].ReportData = JarContent;
                        }
                        catch (Exception ex)
                        {
                            ZhiFang.Common.Log.Log.Debug(ex.Message);
                        }

                        if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                        {
                            File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                        }
                    }
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDSXML.没找到检验完成报告,请检查报告日期及查询条件。");
            }
            
            if (reportCount > 0 || requestCount > 0)
            {
                
                //转化XML
                using (System.IO.StringWriter sw = new StringWriter())
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(reportPdfXml.GetType());
                    serializer.Serialize(sw, reportPdfXml);
                    sw.Close();
                    string xmlreportdata = sw.ToString().Replace("ReportPdfXmlVO", "Item");

                    string result = string.Empty;
                    int startindex, endindex;
                    startindex = xmlreportdata.IndexOf("<Response>");
                    string tmpstr = xmlreportdata.Substring(startindex + "<Response>".Length);
                    endindex = tmpstr.IndexOf("</Response>");
                    result = tmpstr.Remove(endindex);
                   
                    ReportData = "<Response><ResultCode>0</ResultCode><ErrorMsg>成功</ErrorMsg>" + result + "</Response>";
                }
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDSXML.未查找到报告单");
                ReportData = "<Response><ResultCode>-1</ResultCode><ErrorMsg>未查找到任何报告单</ErrorMsg></Response>"; ;
                return false;
            }
        }

        public static bool GetReportFormPDFBitsByDS(DataSet ds, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.没找到报告,请检查报告日期及查询条件。");
                ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                ReportFormPdfPath = null;
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.rfids:" + string.Join(",", rfids));
            var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
            if (listreportformfile.Count > 0)
            {

                ReportFormPdfPath = new string[listreportformfile.Count];
                for (int i = 0; i < listreportformfile.Count; i++)
                {
                    try
                    {
                        byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                        string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型

                        ReportFormPdfPath[i] = JarContent;
                    }
                    catch (Exception ex)
                    {
                        ErrorInfo = ex.Message;
                    }

                    if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                    {
                        File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                    }
                }
                //ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.pdf物理路径:" + string.Join(",", ReportFormPdfPath));
                ErrorInfo = "";
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.未查找到报告单");
                ErrorInfo = "未查找到报告单";
                ReportFormPdfPath = null;
                return false;
            }
        }

        [WebMethod(Description = "根据传入字段和值生成PDF报告并返回报告Base64字符串")]
        public bool GetReportFormPDFByFields_Base64String(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPathBtis, out string ErrorInfo)
        {
            ReportFormPdfPathBtis = null;
            ErrorInfo = "";
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                string urlOrder = " order by ";

                if (order == null || order.Count <= 0 || order[0].Length <= 2)
                {
                    urlOrder = " order by ReceiveDate desc";
                }
                else
                {
                    foreach (var item in order)
                    {
                        urlOrder += item + ", ";
                    }
                    urlOrder = urlOrder.Substring(0, urlOrder.Length - 2);
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ClientPrint <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFBitsByDS(ds, flag, out ReportFormPdfPathBtis, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:IsPrintTimes=" + IsPrintTimes);
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:开始记录打印次数");
                        if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                        {
                            DataTable dsreportform = new DataTable();
                            dsreportform = brf.GetReportFormList(reportformidlist.ToArray());
                            if (dsreportform == null || dsreportform.Rows.Count <= 0)
                            {
                                ErrorInfo = "LIS中没有找到报告单";
                                return false;
                            }
                            List<string> reportformidlist66 = new List<string>();
                            for (int i = 0; i < dsreportform.Rows.Count; i++)
                            {
                                reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                            }
                            IDAL.IDReportForm dal = Factory.DalFactory<IDAL.IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                            bool Ms66Flag = false;
                            Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                            ZhiFang.Common.Log.Log.Debug("向Lis添加打印次数:" + Ms66Flag);
                            Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                            ZhiFang.Common.Log.Log.Debug("向Lis添加外围打印次数:" + Ms66Flag);
                        }
                        //获得的客户端ip
                        string ip = HttpContext.Current.Request.UserHostAddress;

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:不记录打印次数");
                    }
                    #endregion


                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "内江东区人民医院:查询报告列表返回xml格式数据")]
        public bool NeiJiangGetReportFormPDFListByUserId(string userId, string cardType, string cardNo, string patientId, string type, out string ReportData)
        {
            ReportData = "";
            ZhiFang.Common.Log.Log.Debug("内江东区人民医院:查询报告列表返回xml格式数据.NeiJiangGetReportFormPDFListByUserId.开始");
            try
            {
                if (patientId == null || patientId == "")
                {
                    ZhiFang.Common.Log.Log.Debug("传入参数patientId为空!");
                    return false;
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".NeiJiangGetReportFormPDFListByUserId.userId:" + userId + " ,cardType:" + cardType + " ,cardNo: " + cardNo + ", patientId:" + patientId + ",type:" + type);

                string urlWhere = " zdy6 = '" + patientId + "'";
                if (type != null && type != "") {
                   var types = type.Split('|');
                     urlWhere += " and (ReceiveDate >= '" + types[0] + "' and ReceiveDate<='" + types[1] +"')" ;
                }
                string files = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy6,CName,Age,Gender,Bed,paritemname,Dept,Doctor,zdy8,SampletypeName,FormComment,CollectTime,CheckTime,PrintTimes,CheckDate";

                //检查sql注入
                //bool isSqlInject = SqlInjectHelper.CheckKeyWord(urlWhere);
                //if (isSqlInject)
                //{
                //    ZhiFang.Common.Log.Log.Debug("存在SQL注入，请注意传入条件!");
                //    return false;
                //}
                DataSet reportds = barf.GetList_FormFull(files, urlWhere);
                DataSet requestds= brqf.GetList_FormFull(files, urlWhere);

                ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
                List<neijiangPDFXmlVo> xmlVoList = new List<neijiangPDFXmlVo>();
                var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
                if ((reportds != null && reportds.Tables[0].Rows.Count > 0) || (requestds != null && requestds.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < reportds.Tables[0].Rows.Count; i++)
                    {
                        neijiangPDFXmlVo XmlVo = new neijiangPDFXmlVo();
                        XmlVo.reportId = reportds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        XmlVo.patientId = reportds.Tables[0].Rows[i]["zdy6"].ToString();
                        XmlVo.patientName = reportds.Tables[0].Rows[i]["CName"].ToString();
                        XmlVo.age = reportds.Tables[0].Rows[i]["Age"].ToString();
                        XmlVo.sex = reportds.Tables[0].Rows[i]["Gender"].ToString();
                        XmlVo.bedNo = reportds.Tables[0].Rows[i]["Bed"].ToString();
                        XmlVo.examType = reportds.Tables[0].Rows[i]["paritemname"].ToString();
                        XmlVo.checkPart = reportds.Tables[0].Rows[i]["Dept"].ToString();
                        XmlVo.checkDoc = reportds.Tables[0].Rows[i]["Doctor"].ToString();
                        XmlVo.examResult = reportds.Tables[0].Rows[i]["zdy8"].ToString();
                        XmlVo.sampleType = reportds.Tables[0].Rows[i]["SampletypeName"].ToString();
                        XmlVo.sampleMark = reportds.Tables[0].Rows[i]["FormComment"].ToString();
                        XmlVo.receiveTime = reportds.Tables[0].Rows[i]["CollectTime"].ToString().Replace('/', '-');
                        XmlVo.resultTime = reportds.Tables[0].Rows[i]["CheckTime"].ToString().Replace('/','-');
                        XmlVo.printTimes = reportds.Tables[0].Rows[i]["PrintTimes"].ToString();
                        XmlVo.auditTime = reportds.Tables[0].Rows[i]["CheckDate"].ToString().Replace('/', '-');
                        XmlVo.Status = "检验完成";
                        xmlVoList.Add(XmlVo);
                    }
                    for (int i = 0; i < requestds.Tables[0].Rows.Count; i++)
                    {
                        neijiangPDFXmlVo XmlVo = new neijiangPDFXmlVo();
                        XmlVo.reportId = reportds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        XmlVo.patientId = reportds.Tables[0].Rows[i]["zdy6"].ToString();
                        XmlVo.patientName = reportds.Tables[0].Rows[i]["CName"].ToString();
                        XmlVo.age = reportds.Tables[0].Rows[i]["Age"].ToString();
                        XmlVo.sex = reportds.Tables[0].Rows[i]["Gender"].ToString();
                        XmlVo.bedNo = reportds.Tables[0].Rows[i]["Bed"].ToString();
                        XmlVo.examType = reportds.Tables[0].Rows[i]["paritemname"].ToString();
                        XmlVo.checkPart = reportds.Tables[0].Rows[i]["Dept"].ToString();
                        XmlVo.checkDoc = reportds.Tables[0].Rows[i]["Doctor"].ToString();
                        XmlVo.examResult = reportds.Tables[0].Rows[i]["zdy8"].ToString();
                        XmlVo.sampleType = reportds.Tables[0].Rows[i]["SampletypeName"].ToString();
                        XmlVo.sampleMark = reportds.Tables[0].Rows[i]["FormComment"].ToString();
                        XmlVo.receiveTime = reportds.Tables[0].Rows[i]["CollectTime"].ToString().Replace('/', '-');
                        XmlVo.resultTime = reportds.Tables[0].Rows[i]["CheckTime"].ToString().Replace('/', '-');
                        XmlVo.printTimes = reportds.Tables[0].Rows[i]["PrintTimes"].ToString();
                        XmlVo.auditTime = reportds.Tables[0].Rows[i]["CheckDate"].ToString().Replace('/', '-');
                        XmlVo.Status = "检验中";
                        xmlVoList.Add(XmlVo);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListByUserId.没找到报告,请检查报告日期及查询条件。");
                    ReportData = "";
                    return false;
                }

                //转化XML
                using (System.IO.StringWriter sw = new StringWriter())
                {
                   
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( xmlVoList.GetType());
                    serializer.Serialize(sw, xmlVoList);
                    sw.Close();
                    string xmlreportdata = sw.ToString().Replace("neijiangPDFXmlVo", "RecordInfo");

                    string result = string.Empty;
                    int startindex, endindex;
                    startindex = xmlreportdata.IndexOf("<RecordInfo>");
                    string tmpstr = xmlreportdata.Substring(startindex + "<RecordInfo>".Length);
                    endindex = tmpstr.LastIndexOf("</RecordInfo>");
                    result = tmpstr.Remove(endindex);

                    ReportData = "<Response><ResultCode>0</ResultCode><ErrorMsg>交易成功</ErrorMsg><ResultData><RecordList><RecordInfo>" + result + "</RecordInfo></RecordList></ResultData></Response>";
                }

                ZhiFang.Common.Log.Log.Debug("内江东区人民医院:查询报告列表返回xml格式数据.NeiJiangGetReportFormPDFListByUserId.结束");
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".NeiJiangGetReportFormPDFListByUserId:" + e.ToString());
                return false;
            }
        }

        [WebMethod(Description = "内江东区人民医院:查询并生成PDf返回xml格式数据")]
        public  bool NeiJiangGetReportFormPDFListBitsByDSXML(string userId, string cardType, string cardNo, string patientId, string reportId, out string ReportData)
        {
            ReportData = "";
            ZhiFang.Common.Log.Log.Debug("内江东区人民医院:查询并生成PDf返回xml格式数据.NeiJiangGetReportFormPDFListBitsByDSXML.开始");
            try
            {
                if (patientId == null || patientId == "")
                {
                    ZhiFang.Common.Log.Log.Debug("传入参数patientId为空!");
                    return false;
                }
                if (reportId == null || reportId == "")
                {
                    ZhiFang.Common.Log.Log.Debug("传入参数reportId为空!");
                    return false;
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".NeiJiangGetReportFormPDFListBitsByDSXML.userId:" + userId + " cardType:" + cardType + " cardNo: " + cardNo + ", patientId:" + patientId + ",reportId" + reportId);

               string[] reportIds = reportId.Split(';');

                string urlWhere = " zdy6 = '" + patientId + "' and Receivedate = '" + reportIds[0] + "' and SectionNo = '" + reportIds[1] + "' and  TestTypeNo = '" + reportIds[2] + "' and SampleNo = '" + reportIds[3] + "'";
                string files = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy6,CName,Age,Gender,Bed,paritemname,Dept,Doctor,zdy8,SampletypeName,FormComment,CollectTime,CheckTime,PrintTimes,CheckDate";
                //检查sql注入
                //bool isSqlInject = SqlInjectHelper.CheckKeyWord(urlWhere);
                //if (isSqlInject)
                //{
                //    ZhiFang.Common.Log.Log.Debug("存在SQL注入，请注意传入条件!");
                //    return false;
                //}
                DataSet reportds = barf.GetList_FormFull(files, urlWhere);


                ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
                List<string> rfids = new List<string>();
                var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
                if (reportds != null && reportds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < reportds.Tables[0].Rows.Count; i++)
                    {
                        rfids.Add(reportds.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.没找到报告,请检查报告日期及查询条件。");
                    ReportData = null;
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.rfids:" + string.Join(",", rfids));
                var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", 0);
                string ReportFormPdfPath = "";
                if (listreportformfile.Count > 0)
                {

                    for (int i = 0; i < listreportformfile.Count; i++)
                    {
                        try
                        {
                            byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                            string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型

                            ReportFormPdfPath = JarContent;
                        }
                        catch (Exception ex)
                        {
                            ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.异常:"+ ex.Message);
                        }

                        if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                        {
                            File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.未查找到报告单");
                    ReportData = null;
                    return false;
                }
                ReportData = "<Response><ResultCode>0</ResultCode><ErrorMsg>交易成功</ErrorMsg><ResultData><reportPicture>" + ReportFormPdfPath + "</reportPicture></ResultData></Response>";
                ZhiFang.Common.Log.Log.Debug("内江东区人民医院:查询报告列表返回xml格式数据.NeiJiangGetReportFormPDFListBitsByDSXML.结束");
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".NeiJiangGetReportFormPDFListBitsByDSXML:" + e.ToString());
                return false;
            }
        }

        [WebMethod(Description = "内江东区人民医院:根据传入patientId增加打印次数")]
        public bool NeiJiangUpdateReportPrintTimes(string userId, string cardType, string cardNo, string patientId, string reportId, out string ReportData)
        {

            string ip = HttpContext.Current.Request.UserHostAddress;
            bool success = false;
            ReportData = null;
            try
            {
                ZhiFang.Common.Log.Log.Debug("内江东区人民医院:根据传入patientId增加打印次数.NeiJiangUpdateReportPrintTimes.开始");
                if (patientId == null || patientId == "")
                {
                    ZhiFang.Common.Log.Log.Debug("传入参数patientId为空!");
                    return false;
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".NeiJiangUpdateReportPrintTimes.userId:" + userId + " cardType:" + cardType + " cardNo: " + cardNo + ", patientId:" + patientId + ",reportId" + reportId);

                string urlWhere = " zdy6 = '" + patientId + "'";
                string files = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy6,CName,Age,Gender,Bed,paritemname,Dept,Doctor,zdy8,SampletypeName,FormComment,CollectTime,CheckTime,PrintTimes,CheckDate";
                //检查sql注入
                //bool isSqlInject = SqlInjectHelper.CheckKeyWord(urlWhere);
                //if (isSqlInject)
                //{
                //    ZhiFang.Common.Log.Log.Debug("存在SQL注入，请注意传入条件!");
                //    return false;
                //}
                DataSet reportds = barf.GetList_FormFull(files, urlWhere);
                List<string> rfids = new List<string>();
                if (reportds != null && reportds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < reportds.Tables[0].Rows.Count; i++)
                    {
                        rfids.Add(reportds.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.没找到报告,请检查查询条件。");
                    ReportData = null;
                    return false;
                }
                string[] reportformlist = rfids.ToArray(); ;
                bool Ms66Flag = true;
                bool flag = true;
                #region 向lis加入打印次数
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        success = false;
                        ZhiFang.Common.Log.Log.Debug("NeiJiangGetReportFormPDFListBitsByDSXML.LIS中没有找到报告单。"); 
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }
                #endregion

                flag = brf.UpdatePrintTimes(reportformlist, "");
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimes.IP:" + ip + "添加打印次数:reportformID:" + reportformlist.ToString());
                flag = brf.UpdateClientPrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimes.IP:" + ip + "添加外围打印次数:reportformID:" + reportformlist.ToString());
                if (Ms66Flag && flag)
                {
                    success = true;
                    ReportData = "<Response><ResultCode>0</ResultCode><ErrorMsg>交易成功</ErrorMsg><ResultData><success>true</success></ResultData></Response>";
                }
                else
                {
                    success = false;
                }
                ZhiFang.Common.Log.Log.Debug("内江东区人民医院:根据传入patientId增加打印次数.NeiJiangUpdateReportPrintTimes.结束");
            }            
            catch (Exception e)
            {
                success = false;
                ZhiFang.Common.Log.Log.Debug("内江东区人民医院:根据传入patientId增加打印次数.NeiJiangUpdateReportPrintTimes.异常:"+ e.ToString());
            }
            return success;
        }

        [WebMethod(Description = "天津血研究所根据FormNo(四个关键字Receivedate;Section;TestTypeno;Sampleno)生成PDF报告并返回报告URL")]
        public bool TianJinGetReportFormPDFByFormNo(string FormNo, string FormNoFormat, string SplitChar, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (FormNo == null || FormNo.Trim().Length <= 0)
                {
                    ErrorInfo = "报告单号为空!";
                    return false;
                }
                if (SplitChar == null || SplitChar.Trim().Length <= 0)
                {
                    SplitChar = ";";
                }
                if (FormNoFormat == null || FormNoFormat.Trim().Length <= 0)
                {
                    FormNoFormat = "ReceiveDate" + SplitChar + "SectionNo" + SplitChar + "TestTypeNo" + SplitChar + "SampleNo";
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                string[] formnop = FormNo.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnop.Length != 4)
                {
                    ErrorInfo = "报告单号或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.报告单号或分割符出错。FormNo:" + FormNo + ", SplitChar:" + SplitChar);
                    return false;
                }
                string[] formnoformatp = FormNoFormat.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnoformatp.Length != 4)
                {
                    ErrorInfo = "报告单号格式或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.报告单号或分割符出错。FormNoFormat:" + FormNoFormat);
                    return false;
                }
                string urlWhere = " " + formnoformatp[0] + "='" + formnop[0] + "' and " + formnoformatp[1] + "='" + formnop[1] + "' and " + formnoformatp[2] + "='" + formnop[2] + "' and " + formnoformatp[3] + "='" + formnop[3] + "' ";

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }

                DateTime enddate = DateTime.Today.AddDays(2);
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                return TianJinXueYanSuoGetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.异常:" + e.ToString() + ".FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        public static bool TianJinXueYanSuoGetReportFormPDFByDS(DataSet ds, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.没找到报告,请检查报告日期及查询条件。");
                ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                ReportFormPdfPath = null;
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.rfids:" + string.Join(",", rfids));
            var listreportformfile = bprf.TianJinXueYanSuoCreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
            if (listreportformfile.Count > 0)
            {

                ReportFormPdfPath = new string[listreportformfile.Count];
                for (int i = 0; i < listreportformfile.Count; i++)
                {
                    ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[i].PDFPath.Replace(@"\", "/");
                }
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.返回pdf路径:" + string.Join(",", ReportFormPdfPath));
                ErrorInfo = "";
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.未查找到报告单");
                ErrorInfo = "未查找到报告单";
                ReportFormPdfPath = null;
                return false;
            }
        }


        [WebMethod(Description = "根据传入字段和值生成PDF报告并返回报告Base64字符串，不限制日期且不更新打印次数")]
        public bool GetReportFormPDFByFields_Base64String_shaocan(List<string> fields, List<string> values, List<string> order, int flag, out string[] ReportFormPdfPathBtis, out string ErrorInfo)
        {
            ReportFormPdfPathBtis = null;
            ErrorInfo = "";
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                string urlOrder = " order by ";

                if (order == null || order.Count <= 0 || order[0].Length <= 2)
                {
                    urlOrder = " order by ReceiveDate desc";
                }
                else
                {
                    foreach (var item in order)
                    {
                        urlOrder += item + ", ";
                    }
                    urlOrder = urlOrder.Substring(0, urlOrder.Length - 2);
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", flag:" + flag);

               
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);


               
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFBitsByDS(ds, flag, out ReportFormPdfPathBtis, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString()  + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "查询报告列表返回xml格式数据")]
        public bool GetReportFormPDFListByfileds(List<string> fields, List<string> values, int RangeDay, out string ReportData,out string ErrorInfo)
        {
            ReportData = "";
            ErrorInfo = "";
            ZhiFang.Common.Log.Log.Debug("查询报告列表返回xml格式数据.GetReportFormPDFListByfileds.开始");
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFListByfileds.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + ", RangeDay:" + RangeDay );

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFListByfileds.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFListByfileds.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFListByfileds.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                string files = "ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy4,CName,Age,Gender,Bed,paritemname,Dept,Doctor,zdy8,SampletypeName,FormComment,CollectTime,CheckTime,PrintTimes,CheckDate";

                DataSet reportds = barf.GetList_FormFull(files, urlWhere);
                DataSet requestds = brqf.GetList_FormFull(files, urlWhere);

                ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
                List<feixiVo> xmlVoList = new List<feixiVo>();
                var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
                if ((reportds != null && reportds.Tables[0].Rows.Count > 0) || (requestds != null && requestds.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < reportds.Tables[0].Rows.Count; i++)
                    {
                        feixiVo XmlVo = new feixiVo();
                        XmlVo.ReportFormID = reportds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        XmlVo.ZDY4 = reportds.Tables[0].Rows[i]["zdy4"].ToString();
                        XmlVo.CNAME = reportds.Tables[0].Rows[i]["CName"].ToString();
                        XmlVo.ParItemName = reportds.Tables[0].Rows[i]["paritemname"].ToString();
                        XmlVo.CHECKTIME= DateTime.Parse(reportds.Tables[0].Rows[i]["CheckTime"].ToString().Replace('/', '-'));
                        XmlVo.PRINTTIMES = long.Parse(reportds.Tables[0].Rows[i]["PrintTimes"].ToString());
                        XmlVo.CHECKDATE = DateTime.Parse(reportds.Tables[0].Rows[i]["CheckDate"].ToString().Replace('/', '-'));
                        XmlVo.ReportStatus = "检验完成";
                        xmlVoList.Add(XmlVo);
                    }
                    for (int i = 0; i < requestds.Tables[0].Rows.Count; i++)
                    {
                        feixiVo XmlVo = new feixiVo();
                        XmlVo.ReportFormID = reportds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        XmlVo.ZDY4 = reportds.Tables[0].Rows[i]["zdy4"].ToString();
                        XmlVo.CNAME = reportds.Tables[0].Rows[i]["CName"].ToString();
                        XmlVo.ParItemName = reportds.Tables[0].Rows[i]["paritemname"].ToString();
                        XmlVo.CHECKTIME = DateTime.Parse(reportds.Tables[0].Rows[i]["CheckTime"].ToString().Replace('/', '-'));
                        XmlVo.PRINTTIMES = long.Parse(reportds.Tables[0].Rows[i]["PrintTimes"].ToString());
                        XmlVo.CHECKDATE = DateTime.Parse(reportds.Tables[0].Rows[i]["CheckDate"].ToString().Replace('/', '-'));
                        XmlVo.ReportStatus = "检验中";
                        xmlVoList.Add(XmlVo);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportFormPDFListByfileds.没找到报告,请检查报告日期及查询条件。");
                    ReportData = "";
                    return false;
                }

                //转化XML
                using (System.IO.StringWriter sw = new StringWriter())
                {

                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(xmlVoList.GetType());
                    serializer.Serialize(sw, xmlVoList);
                    sw.Close();
                    string xmlreportdata = sw.ToString().Replace("feixiVo", "RecordInfo");

                    string result = string.Empty;
                    int startindex, endindex;
                    startindex = xmlreportdata.IndexOf("<RecordInfo>");
                    string tmpstr = xmlreportdata.Substring(startindex + "<RecordInfo>".Length);
                    endindex = tmpstr.LastIndexOf("</RecordInfo>");
                    result = tmpstr.Remove(endindex);

                    ReportData = "<Response><ResultCode>0</ResultCode><ErrorMsg>交易成功</ErrorMsg><ResultData><RecordList><RecordInfo>" + result + "</RecordInfo></RecordList></ResultData></Response>";
                }

                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFListByfileds:查询报告列表返回xml格式数据结束");
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFListByfileds:" + e.ToString());
                return false;
            }
        }

        [WebMethod(Description = "马鞍山市中心医院自助定制:根据传入字段和值生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByFields_MaAnShanZiZhu(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                string urlOrder = " order by ";

                if (order == null || order.Count <= 0 || order[0].Length <= 2)
                {
                    urlOrder = " order by ReceiveDate desc";
                }
                else
                {
                    foreach (var item in order)
                    {
                        urlOrder += item + ", ";
                    }
                    urlOrder = urlOrder.Substring(0, urlOrder.Length - 2);
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ";
                string codeNameWhere = "";
                string otherWhere = "";
                for (int i = 0; i < fields.Count; i++)
                {
                    if (fields[i] == "CodeName")
                    {
                        string codeName=ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("CodeName");
                        if (!string.IsNullOrWhiteSpace(codeName))
                        {
                            string[] codeNamelist=codeName.Split(',');
                            for (int j=0;j<codeNamelist.Length;j++)
                            {
                                codeNameWhere += codeNamelist[j] + "='" + values[i] + "' or ";
                            }
                        }
                        
                    }
                    else {
                        otherWhere += fields[i] + "='" + values[i] + "' and ";
                    }
                    
                }
                codeNameWhere= "("+codeNameWhere.Substring(0, codeNameWhere.Length - 4)+") and ";
                
                string sicktypeno = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("sicktypeno");
                if (!string.IsNullOrWhiteSpace(sicktypeno))
                {
                    otherWhere += " sicktypeno in (" + sicktypeno + ")";
                }
                urlWhere = codeNameWhere+ otherWhere;

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ISNULL(ClientPrint,0) <1)";
                ErrorInfo = urlWhere;
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields:IsPrintTimes=" + IsPrintTimes);
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields:开始记录打印次数");
                        if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                        {
                            DataTable dsreportform = new DataTable();
                            dsreportform = brf.GetReportFormList(reportformidlist.ToArray());
                            if (dsreportform == null || dsreportform.Rows.Count <= 0)
                            {
                                ErrorInfo = "LIS中没有找到报告单";
                                return false;
                            }
                            List<string> reportformidlist66 = new List<string>();
                            for (int i = 0; i < dsreportform.Rows.Count; i++)
                            {
                                reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                            }
                            IDAL.IDReportForm dal = Factory.DalFactory<IDAL.IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                            bool Ms66Flag = false;
                            Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                            ZhiFang.Common.Log.Log.Debug("向Lis添加打印次数:" + Ms66Flag);
                            Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                            ZhiFang.Common.Log.Log.Debug("向Lis添加外围打印次数:" + Ms66Flag);
                        }
                        //获得的客户端ip
                        string ip = HttpContext.Current.Request.UserHostAddress;

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_MaAnShanZiZhu:不记录打印次数");
                    }
                    #endregion


                }
                return isPdf;
                
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_MaAnShanZiZhu:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

    }
}
