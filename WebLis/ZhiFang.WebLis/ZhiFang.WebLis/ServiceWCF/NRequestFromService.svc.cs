using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.Model.UiModel;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLL.Common;
using ZhiFang.Common.Dictionary;
using Common;
using System.IO;
using FastReport;
using FastReport.Export.Mht;
using System.Web;
using ZhiFang.Model.WeiXinDic;
using ZhiFang.WebLis.ServerContract;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NRequestFromService : ZhiFang.WebLis.ServerContract.INRequestFromService
    {
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
        IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBOSConsumerUserOrderForm iboscuof = BLLFactory<IBOSConsumerUserOrderForm>.GetBLL("OSConsumerUserOrderForm");
        IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();

        #region 申请单列表

        public BaseResultDataValue GetNRequestFromListByRBACFull(int page, int rows, string fields, string jsonentity, string sort)
        {
            BaseResultDataValue result = new BaseResultDataValue();

            List<Model.NRequestFormResult> nrequestformlist = new List<Model.NRequestFormResult>();

            try
            {
                NRequestForm nrequestform = JsonConvert.DeserializeObject<NRequestForm>(jsonentity);
                NRequestForm nrf_m = new NRequestForm();
                if (nrequestform.ClientNo.Trim() != "")
                {
                    nrf_m.ClientNo = nrequestform.ClientNo;
                }
                if (nrequestform.WebLisSourceOrgID.Trim() != "")
                {
                    nrf_m.WebLisSourceOrgID = nrequestform.WebLisSourceOrgID;
                }
                if (nrequestform.DoctorName.Trim() != "")
                {
                    nrf_m.DoctorName = nrequestform.DoctorName;
                }
                if (nrequestform.CName.Trim() != "")
                {
                    nrf_m.CName = nrequestform.CName;
                }
                if (nrequestform.PatNo.Trim() != "")
                {
                    nrf_m.PatNo = nrequestform.PatNo;
                }
                if (nrequestform.OperDateStart != "")
                {
                    nrf_m.OperDateStart = nrequestform.OperDateStart;
                }
                if (nrequestform.OperDateEnd != null && nrequestform.OperDateEnd.Trim() != "")
                {
                    if (nrequestform.OperDateEnd.Trim().Length > 11)
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim() + " 23:59:59";
                    }
                }

                if (nrequestform.CollectDateStart.Trim() != "")
                {
                    nrf_m.CollectDateStart = nrequestform.CollectDateStart.Trim();
                }
                if (nrequestform.CollectDateEnd != null && nrequestform.CollectDateEnd.Trim() != "")
                {
                    if (nrequestform.CollectDateEnd.Trim().Length > 11)
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim() + " 23:59:59";
                    }
                }
                nrf_m.IsOnlyNoBar = nrequestform.IsOnlyNoBar;
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetPostList();
                if (user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                {

                }
                else
                {
                    if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                    {
                        DataSet ds = user.GetClientListByPost("", -1); ;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            nrf_m.ClientList += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        }
                        nrf_m.ClientList = nrf_m.ClientList.Remove(nrf_m.ClientList.Length - 1);
                    }
                    else
                    {
                        nrf_m.ClientList += " -1 ";
                    }
                }
                DataTable dt = new DataTable();
                int iCount, intPageCount;
                dt = rfb.GetNRequstFormList(nrf_m, page, rows, out intPageCount, out iCount);
                if (dt != null)
                {
                    nrequestformlist = NRFDataTableConvertList(dt);
                    EntityList<Model.NRequestFormResult> entitylist_nrf = new EntityList<NRequestFormResult>();
                    entitylist_nrf.list = nrequestformlist;
                    entitylist_nrf.count = iCount;
                    result.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(entitylist_nrf);
                    result.success = true;
                }
                else
                {
                    result.ResultDataValue = "";
                    result.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                result.ErrorInfo = e.ToString();
                result.ResultDataValue = "";
                result.success = false;
            }
            return result;
        }

        public EntityListEasyUI<Model.NRequestFormResult> GetNRequestFromListByRBAC(int page, int rows, string fields, string jsonentity, string sort)
        {
            EntityListEasyUI<Model.NRequestFormResult> result = new EntityListEasyUI<Model.NRequestFormResult>();
            List<Model.NRequestFormResult> nrequestformlist = new List<Model.NRequestFormResult>();
            try
            {
                NRequestForm nrequestform = JsonConvert.DeserializeObject<NRequestForm>(jsonentity);
                NRequestForm nrf_m = new NRequestForm();
                List<string> ClientNoList = new List<string>();
                if (nrequestform.ClientNo != null && nrequestform.ClientNo.Trim() != "")
                {
                    if (nrequestform.ClientNo.Split(',').Length > 1)
                        nrequestform.ClientNoList = nrequestform.ClientNo.Split(',').ToList();
                    else
                        nrf_m.ClientNo = nrequestform.ClientNo;
                }

                if (nrequestform.WebLisSourceOrgID != null && nrequestform.WebLisSourceOrgID.Trim() != "")
                {
                    nrf_m.WebLisSourceOrgID = nrequestform.WebLisSourceOrgID;
                }
                if (nrequestform.DoctorName != null && nrequestform.DoctorName.Trim() != "")
                {
                    nrf_m.DoctorName = nrequestform.DoctorName;
                }
                if (nrequestform.DoctorNameList != null && nrequestform.DoctorNameList.Trim() != "")
                {
                    nrf_m.DoctorNameList = nrequestform.DoctorNameList;
                }
                if (nrequestform.CName != null && nrequestform.CName.Trim() != "")
                {
                    nrf_m.CName = nrequestform.CName;
                }
                if (nrequestform.BarCode != null && nrequestform.BarCode.Trim() != "")
                {
                    nrf_m.BarCode = nrequestform.BarCode;
                }
                if (nrequestform.PatNo != null && nrequestform.PatNo.Trim() != "")
                {
                    nrf_m.PatNo = nrequestform.PatNo;
                }
                if (nrequestform.PersonID != null && nrequestform.PersonID.Trim() != "")
                {
                    nrf_m.PersonID = nrequestform.PersonID;
                }
                if (nrequestform.OperDateStart != null && nrequestform.OperDateStart != "")
                {
                    nrf_m.OperDateStart = nrequestform.OperDateStart;
                }
                if (nrequestform.OperDateEnd != null && nrequestform.OperDateEnd.Trim() != "")
                {
                    if (nrequestform.OperDateEnd.Trim().Length > 11)
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim() + " 23:59:59";
                    }
                }
                if (nrequestform.CollectDateStart != null && nrequestform.CollectDateStart.Trim() != "")
                {
                    nrf_m.CollectDateStart = nrequestform.CollectDateStart.Trim();
                }
                if (nrequestform.CollectDateEnd != null && nrequestform.CollectDateEnd.Trim() != "")
                {
                    if (nrequestform.CollectDateEnd.Trim().Length > 11)
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim() + " 23:59:59";
                    }
                }
                if (nrequestform.jztype != null)
                {
                    nrf_m.jztype = nrequestform.jztype;
                }

                if (nrequestform.SickTypeList != null)
                {
                    nrf_m.SickTypeList = nrequestform.SickTypeList;
                }
                nrf_m.IsOnlyNoBar = nrequestform.IsOnlyNoBar;
                nrf_m.CombiItemNo = nrequestform.CombiItemNo;
                nrf_m.SampleSendNo = nrequestform.SampleSendNo;

                if (nrequestform.WeblisflagList != null&& nrequestform.WeblisflagList != "-1")
                {
                    nrf_m.WeblisflagList = nrequestform.WeblisflagList;
                }

                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetPostList();
                if (user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                {

                }
                else
                {
                    if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                    {
                        DataSet ds = user.GetClientListByPost("", -1); ;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            nrf_m.ClientList += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        }
                        nrf_m.ClientList = nrf_m.ClientList.Remove(nrf_m.ClientList.Length - 1);
                    }
                    else
                    {
                        nrf_m.ClientList += " -1 ";
                    }
                }
                DataTable dt = new DataTable();
                int iCount, intPageCount;
                if (page > 0)
                {
                    page = page - 1;
                }
                if (nrf_m.CombiItemNo != null && nrf_m.CombiItemNo.Trim() != "")
                {
                    dt = rfb.GetNRequstFormList_CombiItemNo(nrf_m, page, rows, out intPageCount, out iCount);
                }
                else if (nrf_m.SampleSendNo != null && nrf_m.SampleSendNo.Trim() != "")
                {
                    dt = rfb.GetNRequstFormList_SampleSendNo(nrf_m, page, rows, out intPageCount, out iCount);
                }
                else
                {
                    dt = rfb.GetNRequstFormList(nrf_m, page, rows, out intPageCount, out iCount);
                }
                if (dt != null)
                {
                    nrequestformlist = NRFDataTableConvertList(dt);
                    result.rows = nrequestformlist;
                    result.total = iCount;
                }
                else
                {
                    result.rows = null;
                    result.total = 0;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                result.rows = nrequestformlist;
                result.total = 0;
            }
            return result;
        }

        public BaseResultDataValue GetNRequestFromListByRBACToExcel(int page, int rows, string fields, string jsonentity, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.NRequestFormResult> result = GetNRequestFromListByRBAC(page, rows, fields, jsonentity, sort);
            try
            {
                if (result.rows != null && result.rows.Count > 0)
                {
                    NRequestForm nrequestform = JsonConvert.DeserializeObject<NRequestForm>(jsonentity);

                    DataTable resultdt = JsonHelp.JsonToDatatable(JsonHelp.JsonDotNetSerializer(result.rows));
                    FastReport.Report report = new FastReport.Report();
                    string url = "";
                    if (nrequestform.CollectDateStart != null && nrequestform.CollectDateStart.Trim() != "" && nrequestform.CollectDateEnd != null && nrequestform.CollectDateEnd.Trim() != "")
                    {
                        url = nrequestform.CollectDateStart.Replace(":", "") + "_" + nrequestform.CollectDateEnd.Replace(":", "") + "_" + GUIDHelp.GetGUIDLong() + ".xlsx";
                    }

                    if (nrequestform.OperDateStart != null && nrequestform.OperDateStart.Trim() != "" && nrequestform.OperDateEnd != null && nrequestform.OperDateEnd.Trim() != "")
                    {
                        url = nrequestform.OperDateStart.Replace(":", "") + "_" + nrequestform.OperDateEnd.Replace(":", "") + "_" + GUIDHelp.GetGUIDLong() + ".xlsx";
                    }

                    string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\GetNRequestFromListByRBAC\\" + url;
                    ZhiFang.Common.Log.Log.Debug("GetNRequestFromListByRBAC_Excel.Url:" + strPath);

                    if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\GetNRequestFromListByRBAC\\"))
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\GetNRequestFromListByRBAC\\");
                    report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\GetNRequestFromListByRBAC.frx");
                    TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                    if (Text_DateRange != null)
                    {
                        Text_DateRange.Text = nrequestform.CollectDateStart + "_" + nrequestform.CollectDateEnd;
                    }
                    DataSet ds = new DataSet();
                    resultdt.TableName = "Table";
                    ds.Tables.Add(resultdt);
                    //report.RegisterData(resultdt, "Table");
                    report.RegisterData(ds);
                    report.Prepare();
                    FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                    report.Export(export, strPath);
                    report.Dispose();
                    string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/GetNRequestFromListByRBAC/" + url;
                    brdv.ResultDataValue = strTable;
                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("GetNRequestFromListByRBACToExcel.未查找到数据！");
                    brdv.success = false;
                    brdv.ErrorInfo = "未查找到数据！请重新登陆后重试！";
                    return brdv;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("GetNRequestFromListByRBACToExcel.异常:" + e.ToString());
                brdv.success = false;
                return brdv;
            }
        }

        public EntityListEasyUI<Model.NRequestFormResult> GetNRequestFromStatisticsList(int page, int rows, string fields, string jsonentity, string sort)
        {
            EntityListEasyUI<Model.NRequestFormResult> result = new EntityListEasyUI<Model.NRequestFormResult>();

            List<Model.NRequestFormResult> nrequestformlist = new List<Model.NRequestFormResult>();

            try
            {
                NRequestForm nrequestform = JsonConvert.DeserializeObject<NRequestForm>(jsonentity);
                NRequestForm nrf_m = new NRequestForm();
                if (nrequestform.ClientNo != null && nrequestform.ClientNo.Trim() != "")
                {
                    nrf_m.ClientNo = nrequestform.ClientNo;
                }
                if (nrequestform.NRequestFormNo != null && nrequestform.NRequestFormNo != 0)
                {
                    nrf_m.NRequestFormNo = nrequestform.NRequestFormNo;
                }
                if (nrequestform.DoctorName != null && nrequestform.DoctorName.Trim() != "")
                {
                    nrf_m.DoctorName = nrequestform.DoctorName;
                }
                if (nrequestform.CName != null && nrequestform.CName.Trim() != "")
                {
                    nrf_m.CName = nrequestform.CName;
                }
                if (nrequestform.PatNo != null && nrequestform.PatNo.Trim() != "")
                {
                    nrf_m.PatNo = nrequestform.PatNo;
                }
                if (nrequestform.BarCode != null && nrequestform.BarCode.Trim() != "")
                {
                    nrf_m.BarCode = nrequestform.BarCode;
                }
                if (nrequestform.Weblisflag != null && nrequestform.Weblisflag.Trim() != "")
                {
                    nrf_m.Weblisflag = nrequestform.Weblisflag;
                }
                if (nrequestform.OperDateStart != null && nrequestform.OperDateStart != "")
                {
                    nrf_m.OperDateStart = nrequestform.OperDateStart;
                }
                if (nrequestform.OperDateEnd != null && nrequestform.OperDateEnd.Trim() != "")
                {
                    if (nrequestform.OperDateEnd.Trim().Length > 11)
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim() + " 23:59:59";
                    }
                }
                if (nrequestform.CollectDateStart != null && nrequestform.CollectDateStart.Trim() != "")
                {
                    nrf_m.CollectDateStart = nrequestform.CollectDateStart.Trim();
                }
                if (nrequestform.CollectDateEnd != null && nrequestform.CollectDateEnd.Trim() != "")
                {
                    if (nrequestform.CollectDateEnd.Trim().Length > 11)
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim() + " 23:59:59";
                    }
                }
                nrf_m.IsOnlyNoBar = nrequestform.IsOnlyNoBar;
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetPostList();
                if (user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                {

                }
                else
                {
                    if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                    {
                        DataSet ds = user.GetClientListByPost("", -1); ;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            nrf_m.ClientList += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        }
                        nrf_m.ClientList = nrf_m.ClientList.Remove(nrf_m.ClientList.Length - 1);
                    }
                    else
                    {
                        nrf_m.ClientList += " -1 ";
                    }
                }
                DataTable dt = new DataTable();
                int iCount, intPageCount;
                if (page > 0)
                {
                    page = page - 1;
                }

                dt = rfb.GetNRequstFormList2(nrf_m, page, rows, out intPageCount, out iCount);
                if (dt != null)
                {

                    nrequestformlist = NRFDataTableConvertList_NRequestFromStatistics(dt);
                    result.rows = nrequestformlist;
                    result.total = iCount;

                }
                else
                {
                    result.rows = null;
                    result.total = 0;

                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                result.rows = nrequestformlist;
                result.total = 0;
            }
            return result;
        }

        /// <summary>
        /// 打印清单-根据条码展示信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public List<NRequestFormResult> BarCodeFormFillList(DataTable dt, string barCode)
        {
            List<NRequestFormResult> nrequesrformresultlist = new List<NRequestFormResult>();
            foreach (DataRow dr in dt.Rows)
            {
                NRequestFormResult nrfr = new NRequestFormResult();
                //var barcode = "";
                string barcodeformno;
                string colorname = "";
                string itemname = "";
                string itemno = "";
                string samplytypename = "";
                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim(), barCode, out barcodeformno, out colorname, out itemname, out itemno, out samplytypename);
                //foreach (var a in b.Split(','))
                //{
                //    barcode += a + "</br>";
                //}
                nrfr.BarcodeList = barCode;

                nrfr.ColorName = colorname;

                nrfr.ItemList = itemname;

                nrfr.CName = dr["CName"].ToString().Trim();

                nrfr.GenderName = dr["Sex"].ToString().Trim();

                nrfr.Age = decimal.Parse(dr["Age"].ToString().Trim());

                nrfr.AgeUnitName = dr["AgeUnitName"].ToString().Trim();

                nrfr.SampleTypeName = samplytypename;//dr["SampleName"].ToString().Trim();

                nrfr.SickTypeName = dr["jztypeName"].ToString().Trim();

                nrfr.PatNo = dr["PatNo"].ToString().Trim();

                nrfr.DeptName = dr["DEPTNAME"].ToString().Trim();

                nrfr.DoctorName = dr["DoctorName"].ToString().Trim();

                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    nrfr.OperTime = dr["OperTime"].ToString().Trim();
                }

                if (dr["CollectTime"].ToString().Trim().Length > 0)
                {
                    nrfr.CollectTime = dr["CollectTime"].ToString().Trim();
                }
                nrfr.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString().Trim();

                string Diag = dr["Diag"].ToString().Trim();

                nrfr.Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                nrfr.NRequestFormNo = dr["NRequestFormNo"].ToString().Trim();
                nrequesrformresultlist.Add(nrfr);
            }
            return nrequesrformresultlist;
        }

        #endregion

        #region 申请单数据转换
        /// <summary>
        /// 申请单列表转换(DT->List)
        /// </summary>
        /// <param name="dt"></param>
        List<NRequestFormResult> NRFDataTableConvertList(DataTable dt)
        {
            List<NRequestFormResult> nrequesrformresultlist = new List<NRequestFormResult>();
            foreach (DataRow dr in dt.Rows)
            {
                NRequestFormResult nrfr = new NRequestFormResult();
                var barcode = "";
                string barcodeformno;
                string colorname = "";
                string itemname = "";
                string itemno = "";
                string samplytypename = "";
                string NRFStatus = "0";
                //string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim(), out barcodeformno, out colorname, out itemname, out itemno, out samplytypename);
                List<Model.BarCodeFormResult> barcodelist = rfb.GetBarCodeListByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                foreach (var a in barcodelist)
                {
                    barcode += a.BarCode + ",";
                    colorname += a.Color + ",";
                    itemname += a.ItemName + ",";
                    itemno += a.ItemNo + ",";
                    samplytypename += a.SampleTypeName + ",";
                }
                if (barcodelist.Count(a => a.WebLisFlag == "5" || a.WebLisFlag == "10") > 0)//一个条码签收即为整个申请单已签收，暂时逻辑。
                {
                    NRFStatus = "5";
                }
                nrfr.BarCodeFormList = barcodelist;

                nrfr.BarcodeList = (barcode.Length > 0) ? barcode.Substring(0, barcode.Length - 1) : "";

                nrfr.ColorName = (colorname.Length > 0) ? colorname.Substring(0, colorname.Length - 1) : "";

                nrfr.ItemList = (itemname.Length > 0) ? itemname.Substring(0, itemname.Length - 1) : "";

                nrfr.SampleTypeName = (samplytypename.Length > 0) ? samplytypename.Substring(0, samplytypename.Length - 1) : "";

                nrfr.WebLisFlag = (dr["WebLisFlag"]!=null && dr["WebLisFlag"].ToString().Trim()!="")? dr["WebLisFlag"].ToString().Trim():NRFStatus;

                nrfr.CName = dr["CName"].ToString().Trim();

                nrfr.GenderName = dr["Sex"].ToString().Trim();
                if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                    nrfr.Age = decimal.Parse(dr["Age"].ToString().Trim());

                nrfr.AgeUnitName = dr["AgeUnitName"].ToString().Trim();

                nrfr.SampleTypeName = dr["SampleName"].ToString().Trim();

                nrfr.SickTypeName = dr["jztypeName"].ToString().Trim();

                nrfr.PatNo = dr["PatNo"].ToString().Trim();

                nrfr.DeptName = dr["DEPTNAME"].ToString().Trim();

                nrfr.DoctorName = dr["DoctorName"].ToString().Trim();

                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    nrfr.OperTime = dr["OperTime"].ToString().Trim();
                }

                if (dr["CollectTime"].ToString().Trim().Length > 0)
                {
                    nrfr.CollectTime = dr["CollectTime"].ToString().Trim();
                }
                if (dr["CollectTime"].ToString().Trim().Length > 0)
                {
                    nrfr.CollectDate = dr["CollectTime"].ToString().Trim().Substring(0, 10);
                }
                nrfr.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString().Trim();

                if (dr.Table.Columns.Contains("PersonID") && dr["PersonID"].ToString().Trim().Length > 0)
                {
                    nrfr.PersonID = dr["PersonID"].ToString().Trim();
                }
                if (dr.Table.Columns.Contains("Charge") && dr["Charge"].ToString().Trim().Length > 0)
                {
                    nrfr.Charge = dr["Charge"].ToString().Trim().Substring(0, dr["Charge"].ToString().Trim().IndexOf('.') + 3);
                }
                if (dr.Table.Columns.Contains("Diag") && dr["Diag"].ToString().Trim().Length > 0)
                {
                    nrfr.Diag = dr["Diag"].ToString().Trim();
                }
                if (dr.Table.Columns.Contains("TestTypeName") && dr["TestTypeName"].ToString().Trim().Length > 0)
                {
                    nrfr.TestTypeName = dr["TestTypeName"].ToString().Trim();
                }
                if (dr.Table.Columns.Contains("Receivedate") && dr["Receivedate"].ToString().Trim().Length > 0)
                {
                    nrfr.Receivedate = dr["Receivedate"].ToString().Trim().Substring(0, 10);
                }
                #region zdy
                if (dr.Table.Columns.Contains("zdy1") && dr["zdy1"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy1 = dr["zdy1"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy2") && dr["zdy2"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy2 = dr["zdy2"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy3") && dr["zdy3"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy3 = dr["zdy3"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy4") && dr["zdy4"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy4 = dr["zdy4"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy5") && dr["zdy5"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy5 = dr["zdy5"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy6") && dr["zdy6"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy6 = dr["zdy6"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy7") && dr["zdy7"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy7 = dr["zdy7"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy8") && dr["zdy8"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy8 = dr["zdy8"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy9") && dr["zdy9"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy9 = dr["zdy9"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("zdy10") && dr["zdy10"].ToString().Trim().Length > 0)
                {
                    nrfr.zdy10 = dr["zdy10"].ToString().Trim();
                }
                #endregion
                if (dr.Table.Columns.Contains("SampleSendNo") && dr["SampleSendNo"].ToString().Trim().Length > 0)
                {
                    nrfr.SampleSendNo = dr["SampleSendNo"].ToString().Trim();
                }

                if (dr.Table.Columns.Contains("RejectionReason") && dr["RejectionReason"].ToString().Trim().Length > 0)
                {
                    nrfr.RejectionReason = dr["RejectionReason"].ToString().Trim();
                }
                string Diag = dr["Diag"].ToString().Trim();

                nrfr.Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                nrfr.NRequestFormNo = dr["NRequestFormNo"].ToString().Trim();
                nrequesrformresultlist.Add(nrfr);
            }
            return nrequesrformresultlist;
        }

        /// <summary>
        /// 微信采样申请单列表转换(DT->List)
        /// </summary>
        /// <param name="dt"></param>
        List<NRequestFormResultOfConsume> NRFDataTableConvertByDetailsList(DataTable dt)
        {
            List<NRequestFormResultOfConsume> nrequesrformresultlist = new List<NRequestFormResultOfConsume>();
            foreach (DataRow dr in dt.Rows)
            {
                NRequestFormResultOfConsume nrfr = new NRequestFormResultOfConsume();
                var barcode = "";
                string barcodeformno;
                string colorname = "";
                string itemname = "";
                string itemno = "";
                string samplytypename = "";
                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim(), out barcodeformno, out colorname, out itemname, out itemno, out samplytypename);
                foreach (var a in b.Split(','))
                {
                    barcode += a + "</br>";
                }
                nrfr.BarcodeList = b;

                nrfr.ColorName = colorname;

                nrfr.ItemList = itemname;

                nrfr.CName = dr["CName"].ToString().Trim();

                nrfr.GenderName = dr["Sex"].ToString().Trim();

                nrfr.Age = decimal.Parse(dr["Age"].ToString().Trim());

                nrfr.AgeUnitName = dr["AgeUnitName"].ToString().Trim();

                nrfr.SampleTypeName = dr["SampleName"].ToString().Trim();

                nrfr.SickTypeName = dr["jztypeName"].ToString().Trim();

                nrfr.PatNo = dr["PatNo"].ToString().Trim();

                nrfr.DeptName = dr["DEPTNAME"].ToString().Trim();

                nrfr.DoctorName = dr["DoctorName"].ToString().Trim();

                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    nrfr.OperTime = dr["OperTime"].ToString().Trim();
                }

                if (dr["CollectTime"].ToString().Trim().Length > 0)
                {
                    nrfr.CollectTime = dr["CollectTime"].ToString().Trim();
                }
                nrfr.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString().Trim();

                string Diag = dr["Diag"].ToString().Trim();

                nrfr.Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                nrfr.NRequestFormNo = dr["NRequestFormNo"].ToString().Trim();
                nrfr.OldSerialNo = dr["OldSerialNo"].ToString().Trim();
                nrfr.ZDY10 = dr["ZDY10"].ToString().Trim();
                nrequesrformresultlist.Add(nrfr);
            }
            return nrequesrformresultlist;
        }
        /// <summary>
        /// 申请单列表转换(DT->List)
        /// </summary>
        /// <param name="dt"></param>
        List<NRequestFormResult> NRFDataTableConvertList_NRequestFromStatistics(DataTable dt)
        {
            List<NRequestFormResult> nrequesrformresultlist = new List<NRequestFormResult>();
            foreach (DataRow dr in dt.Rows)
            {
                NRequestFormResult nrfr = new NRequestFormResult();
                nrfr.BarcodeList = dr["BarCode"].ToString().Trim();
                nrfr.ItemList = dr["ItemName"].ToString().Trim();
                nrfr.CName = dr["CName"].ToString().Trim();
                nrfr.GenderName = dr["Sex"].ToString().Trim();
                nrfr.Age = decimal.Parse(dr["Age"].ToString().Trim());
                nrfr.SampleTypeName = dr["SampleTypeName"].ToString().Trim();
                nrfr.PatNo = dr["PatNo"].ToString().Trim();
                nrfr.DoctorName = dr["DoctorName"].ToString().Trim();
                nrfr.OperDate = dr["OperDate"].ToString().Trim();
                nrfr.CollectDate = dr["CollectDate"].ToString().Trim();
                nrfr.NRequestFormNo = dr["NRequestFormNo"].ToString().Trim();
                nrfr.WebLisSourceOrgName = dr["ClientName"].ToString().Trim();
                nrfr.WebLisFlag = dr["WebLisFlag"].ToString().Trim();
                nrequesrformresultlist.Add(nrfr);
            }
            return nrequesrformresultlist;
        }
        #endregion

        #region 申请单新增修改
        /// <summary>
        ///申请录入增加和修改服务
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResult NrequestFormAddOrUpdate(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResult br = new BaseResult();

            try
            {
                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;

                bool b = jsonentity.BarCodeList.GroupBy(l => l.BarCode).Where(g => g.Count() > 1).Count() > 0;

                var barcodelist = jsonentity.BarCodeList.GroupBy(l => l.BarCode);
                foreach (var bg in barcodelist)
                {
                    if (bg.GroupBy(barcode => barcode.ColorName).Count() > 1)
                    {
                        br.success = false;
                        br.ErrorInfo = "输入的条码号有重复！BarCode:" + bg.Key;
                        ZhiFang.Common.Log.Log.Debug("Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "输入的条码号有重复！BarCode:" + bg.Key);
                        return br;
                    }
                }

                //if (b)
                //{
                //    br.success = false;
                //    br.ErrorInfo = "输入的条码号有重复！";
                //    return br;
                //}
                //数据库中条码为唯一值
                string repeatbarcodestr;
                if (ibbcf.IsExistBarCode(jsonentity.flag, jsonentity.BarCodeList, out repeatbarcodestr))
                {
                    br.success = false;
                    br.ErrorInfo = "条码号:'" + repeatbarcodestr + "'已存在！";
                    return br;
                }
                #endregion

                Model.NRequestForm nrf_m = null;
                Model.NRequestItem nri_m = new Model.NRequestItem();
                Model.BarCodeForm bcf_m = new Model.BarCodeForm();

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion

                //申请单号
                long nRequestFormNo;

                if ((long)jsonentity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;


                #region 对象赋值

                #region 组合项目
                IBTestItem ibTest = BLLFactory<IBTestItem>.GetBLL();
                List<NRequestItem> nri_List = new List<NRequestItem>();
                foreach (UiCombiItem uicombiItem in jsonentity.CombiItems)
                {
                    //if (nri_List.Count(a => a.CombiItemNo == uicombiItem.CombiItemNo) > 0)
                    //{
                    //    continue;
                    //}
                    //明细
                    foreach (UiCombiItemDetail uicombiItemDetail in uicombiItem.CombiItemDetailList)
                    {
                        nri_m = new NRequestItem();

                        ////假组套,组套中只包含自己
                        //if (uicombiItem.CombiItemDetailList.Count == 1 && uicombiItem.CombiItemNo == uicombiItemDetail.CombiItemDetailNo)
                        //{

                        //}
                        //else
                        nri_m.CombiItemNo = uicombiItem.CombiItemNo;//uicombiItemDetail.CombiItemDetailNo;
                        nri_m.ParItemNo = uicombiItemDetail.CombiItemDetailNo.ToString();
                        nri_m.NRequestFormNo = nRequestFormNo;

                        nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nri_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nri_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nri_m.ClientName = jsonentity.NrequestForm.ClientName;
                        if (nri_List.Count(a => a.ParItemNo == nri_m.ParItemNo) > 0)
                        {
                            continue;
                        }
                        nri_List.Add(nri_m);
                    }
                }
                #endregion

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.NRequestFormNo = nRequestFormNo;
                //nrf_m.WebLisSourceOrgID = ClientNo;
                //nrf_m.WebLisSourceOrgName = txtClientNo;
                nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                nrf_m.Collecter = user.NameL + user.NameF;
                #endregion

                #region 条码

                List<Model.BarCodeForm> bcf_List = new List<BarCodeForm>();

                foreach (UiBarCode uibc in jsonentity.BarCodeList)
                {
                    bcf_m = new BarCodeForm();

                    bcf_m.BarCode = uibc.BarCode;
                    bcf_m.Color = uibc.ColorName;
                    int sampleTypeNo;
                    int.TryParse(uibc.SampleType, out sampleTypeNo);
                    bcf_m.SampleTypeNo = sampleTypeNo;

                    bcf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                    bcf_m.WebLisSourceOrgId = nrf_m.ClientNo;
                    bcf_m.WebLisSourceOrgName = nrf_m.ClientName;
                    bcf_m.ClientNo = nrf_m.ClientNo;
                    bcf_m.ClientName = nrf_m.ClientName;
                    bcf_m.CollectDate = nrf_m.CollectDate;
                    bcf_m.CollectTime = nrf_m.CollectTime;
                    bcf_m.Collecter = user.NameL + user.NameF;
                    bcf_m.CollecterID = user.EmplID;
                    bool flag = false;
                    if (jsonentity.flag == "1")
                    {
                        bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();
                        //1条码对应多个子项目                       
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }

                    }
                    else if (jsonentity.flag == "0")
                    {
                        DataSet ds = ibbcf.GetList(new BarCodeForm() { BarCode = uibc.BarCode });
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            long barCodeFormNo;
                            long.TryParse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString(), out barCodeFormNo);
                            bcf_m.BarCodeFormNo = barCodeFormNo;

                        }
                        else
                            bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();

                        //1条码对应多个子项目                     
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }
                    }
                    if (bcf_m.ItemName != null && bcf_m.ItemName.Length > 0)
                    {
                        bcf_m.ItemName = bcf_m.ItemName.Remove(bcf_m.ItemName.LastIndexOf(','));
                    }
                    if (bcf_m.ItemNo != null && bcf_m.ItemNo.Length > 0)
                    {
                        bcf_m.ItemNo = bcf_m.ItemNo.Remove(bcf_m.ItemNo.LastIndexOf(','));
                    }
                    bcf_List.Add(bcf_m);
                }

                #endregion

                #endregion

                if (jsonentity.flag.Trim().ToString() == "0")
                {
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormEidtType") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormEidtType").Trim() == "0")//只修改申请单信息
                    {
                        #region NRequestItem
                        if (nri_List != null)
                        {
                            //先删除
                            rib.DeleteList_ByNRequestFormNo(nRequestFormNo);

                            foreach (NRequestItem nri in nri_List)
                            {
                                nri.NRequestFormNo = nRequestFormNo;
                                nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                                nri.CombiItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString());
                                if (nri.ParItemNo.Trim() == "0")
                                {
                                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.出现ParItemNo为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity));
                                    continue;
                                }
                                if (!nri.BarCodeFormNo.HasValue || nri.BarCodeFormNo.Value == 0)
                                {
                                    ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.出现BarCodeFormNo为空或者为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity) + ",nri.NRequestFormNo=" + nri.NRequestFormNo + ",nri.ParItemNo=" + nri.ParItemNo + ",nri.CombiItemNo=" + nri.CombiItemNo);
                                    continue;
                                }
                                int i = rib.Add(nri);
                                if (i > 0)
                                {
                                    bNRequestItemRusult = true;
                                }
                                else
                                    bNRequestItemRusult = false;
                            }

                        }

                        #endregion

                        #region BarCodeForm
                        if (bcf_List != null && bNRequestItemRusult == true)
                        {

                            foreach (BarCodeForm bcf in bcf_List)
                            {

                                //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                                bcf.WebLisSourceOrgId = bcf.ClientNo;
                                bcf.WebLisSourceOrgName = bcf.ClientName;
                                bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                                bcf.Collecter = (user.NameL + user.NameF);
                                bcf.PrintCount = 0;
                                bcf.IsAffirm = 1;
                                int i = 0;
                                if (ibbcf.Exists((long)bcf.BarCodeFormNo))
                                {
                                    i = ibbcf.Update(bcf);
                                }
                                else
                                    i = ibbcf.Add(bcf);

                                if (i > 0)
                                    bBarCodeFormRusult = true;
                                else
                                    bBarCodeFormRusult = false;
                            }

                        }
                        #endregion
                    }
                    else
                    {
                        //不修改条码和项目信息，直接结果赋值为true
                        bNRequestItemRusult = true;
                        bBarCodeFormRusult = true;
                    }

                    #region NRequestForm
                    if (nrf_m != null && nRequestFormNo > 0 && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Update(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;

                        }
                    }

                    #endregion
                }
                else if (jsonentity.flag.Trim().ToString() == "1")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {


                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                            int result = 0;
                            if (int.TryParse(tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString()), out result))
                            {
                                nri.CombiItemNo = result.ToString();
                            }
                            if (nri.ParItemNo.Trim() == "0")
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.flag=1.出现ParItemNo为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity));
                                continue;
                            }
                            if (!nri.BarCodeFormNo.HasValue || nri.BarCodeFormNo.Value == 0)
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.flag=1.出现BarCodeFormNo为空或者为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity) + ",nri.NRequestFormNo=" + nri.NRequestFormNo + ",nri.ParItemNo=" + nri.ParItemNo + ",nri.CombiItemNo=" + nri.CombiItemNo);
                                continue;
                            }
                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;

                        }

                    }

                    #endregion

                    #region BarCodeForm

                    if (bcf_List != null && bNRequestItemRusult == true)
                    {
                        //ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
                        foreach (BarCodeForm bcf in bcf_List)
                        {
                            //ZhiFang.Common.Log.Log.Debug("bcf.BarCode:" + bcf.BarCode);
                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = (user.NameL + user.NameF);
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            int i = ibbcf.Add(bcf);
                            if (i > 0)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.SerialNo = nRequestFormNo.ToString();
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Add(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;
                        }
                    }

                    #endregion
                }

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    br.success = true;
                else
                    br.success = false;

            }
            catch (Exception e)
            {
                br.ErrorInfo = "程序异常！请重试！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate.异常:" + e.ToString() + ".IP地址:" + IPHelper.GetClientIP());
            }
            return br;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue requestFormAdd_BarCodePrint(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;
                #endregion

                #region 定义变量
                //申请单号
                long nRequestFormNo;//= GUIDHelp.GetGUIDLong();
                if (jsonentity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;
                string m_WebLisOrgId = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                int m_collecterId = user.EmplID;
                string m_collecter = user.NameL + user.NameF;

                Model.NRequestForm nrf_m = null;

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion
                #endregion

                #region 对象赋值

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.NRequestFormNo = nRequestFormNo;
                nrf_m.SerialNo = nRequestFormNo.ToString();
                //nrf_m.ClientNo = m_WebLisOrgId;
                //nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                nrf_m.WebLisSourceOrgID = nrf_m.ClientNo;
                nrf_m.WebLisSourceOrgName = nrf_m.ClientName;
                nrf_m.WebLisOrgID = m_WebLisOrgId;
                nrf_m.Collecter = user.NameL + user.NameF;
                #region 根据前台选择的组套项目,计算一个申请单中组套的总价
                foreach (var item in jsonentity.CombiItems)
                {
                    Model.Lab_TestItem lbmodel = ltic.GetModel(nrf_m.AreaNo, item.CombiItemNo.ToString());
                    if (lbmodel != null)
                        nrf_m.Price += lbmodel.Price == null ? 0 : (decimal)lbmodel.Price;
                }
                #endregion
                //nrf_m.Price=ltic.getmo
                #endregion

                #region 组合项目/条码
                List<Model.BarCodeForm> bcf_List = null;
                List<NRequestItem> nri_List = rib.SetNrequestItemAndBarCodeForm(jsonentity.BarCodeList, nrf_m, m_collecter, m_collecterId, out bcf_List);
                #endregion

                #endregion

                #region NRequestItem

                bNRequestItemRusult = rib.AddNrequestItem(nri_List);

                #endregion

                #region BarCodeForm

                bBarCodeFormRusult = ibbcf.AddBarCodeForm(bcf_List);
                #endregion

                #region NRequestForm
                bNRequestFormRusult = rfb.AddNrequest(nrf_m, bcf_List);

                #endregion

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                {
                    List<UiBarCode> BarCodeList = ibbcf.ConvertBarCodeFormToUiBarCode(bcf_List, nrf_m);

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(BarCodeList);
                    brdv.success = true;
                }
                else
                    brdv.success = false;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = "程序异常！请重试！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("requestFormAdd_BarCodePrint.异常:" + e.ToString() + ".IP地址:" + IPHelper.GetClientIP());
            }
            return brdv;
        }

        /// <summary>
        /// 批量增加申请单并返回条码列表
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue RequestFormAdd_Batch_BarCodePrint(List<NrequestCombiItemBarCodeEntity> jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (jsonentity == null || jsonentity.Count <= 0)
                {
                    brdv.ErrorInfo = "参数错误！请重试！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Error("RequestFormAdd_Batch_BarCodePrint.jsonentity为空！IP地址:" + IPHelper.GetClientIP());
                    return brdv;
                }
                ZhiFang.Common.Log.Log.Debug("RequestFormAdd_Batch_BarCodePrint.jsonentity:" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(jsonentity));

                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;
                #endregion

                List<UiBarCode> BarCodeList = new List<UiBarCode>();
                foreach (var nrf in jsonentity)
                {
                    #region 定义变量
                    //申请单号
                    long nRequestFormNo = GUIDHelp.GetGUIDLong();

                    string m_WebLisOrgId = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                    int m_collecterId = user.EmplID;
                    string m_collecter = user.NameL + user.NameF;
                    Model.NRequestForm nrf_m = null;
                    #region 定义三个bool类型的变量,判断是否都成功
                    bool bNRequestFormRusult = false;
                    bool bNRequestItemRusult = false;
                    bool bBarCodeFormRusult = false;
                    #endregion
                    #endregion

                    #region 对象赋值
                    #region 表单对象
                    nrf_m = new Model.NRequestForm();
                    nrf_m = nrf.NrequestForm;
                    nrf_m.NRequestFormNo = nRequestFormNo;
                    nrf_m.SerialNo = nRequestFormNo.ToString();
                    //nrf_m.ClientNo = m_WebLisOrgId;
                    //nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                    nrf_m.WebLisSourceOrgID = nrf_m.ClientNo;
                    nrf_m.WebLisSourceOrgName = nrf_m.ClientName;
                    nrf_m.WebLisOrgID = m_WebLisOrgId;
                    nrf_m.Collecter = user.NameL + user.NameF;
                    #region 根据前台选择的组套项目,计算一个申请单中组套的总价
                    foreach (var item in nrf.CombiItems)
                    {
                        Model.Lab_TestItem lbmodel = ltic.GetModel(nrf_m.AreaNo, item.CombiItemNo.ToString());
                        if (lbmodel != null)
                            nrf_m.Price += lbmodel.Price == null ? 0 : (decimal)lbmodel.Price;
                    }
                    #endregion
                    //nrf_m.Price=ltic.getmo
                    #endregion

                    #region 组合项目/条码
                    List<Model.BarCodeForm> bcf_List = null;
                    List<NRequestItem> nri_List = rib.SetNrequestItemAndBarCodeForm(nrf.BarCodeList, nrf_m, m_collecter, m_collecterId, out bcf_List);
                    #endregion
                    #endregion

                    #region 保存
                    #region NRequestItem

                    bNRequestItemRusult = rib.AddNrequestItem(nri_List);

                    #endregion

                    #region BarCodeForm

                    bBarCodeFormRusult = ibbcf.AddBarCodeForm(bcf_List);
                    #endregion

                    #region NRequestForm
                    bNRequestFormRusult = rfb.AddNrequest(nrf_m, bcf_List);

                    #endregion

                    #endregion

                    #region 获取条码
                    if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    {
                        var barcodelist = ibbcf.ConvertBarCodeFormToUiBarCode(bcf_List, nrf_m);
                        BarCodeList.AddRange(barcodelist);
                        brdv.success = true;
                    }
                    else
                        brdv.success = false;
                    #endregion
                }
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(BarCodeList);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = "程序异常！请重试！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("RequestFormAdd_Batch_BarCodePrint.异常:" + e.ToString() + ".IP地址:" + IPHelper.GetClientIP());
                return brdv;
            }
        }

        /// <summary>
        /// 组套项目不需要对照  
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue requestFormAdd_BarCodePrintTaiHe(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;
                #endregion

                #region 定义变量
                //申请单号
                long nRequestFormNo = 0;//= GUIDHelp.GetGUIDLong();
                if (jsonentity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;
                string m_WebLisOrgId = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                int m_collecterId = user.EmplID;
                string m_collecter = user.NameL + user.NameF;

                Model.NRequestForm nrf_m = null;

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion
                #endregion

                #region 对象赋值

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.NRequestFormNo = nRequestFormNo;
                nrf_m.SerialNo = nRequestFormNo.ToString();
                //nrf_m.ClientNo = m_WebLisOrgId;
                //nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                nrf_m.WebLisSourceOrgID = nrf_m.ClientNo;
                nrf_m.WebLisSourceOrgName = nrf_m.ClientName;
                nrf_m.WebLisOrgID = m_WebLisOrgId;
                nrf_m.Collecter = user.NameL + user.NameF;
                #region 根据前台选择的组套项目,计算一个申请单中组套的总价
                foreach (var item in jsonentity.CombiItems)
                {
                    Model.Lab_TestItem lbmodel = ltic.GetModel(nrf_m.AreaNo, item.CombiItemNo.ToString());
                    if (lbmodel != null)
                        nrf_m.Price += lbmodel.Price == null ? 0 : (decimal)lbmodel.Price;
                }
                #endregion
                //nrf_m.Price=ltic.getmo
                #endregion

                #region 组合项目/条码
                List<Model.BarCodeForm> bcf_List = null;
                List<NRequestItem> nri_List = rib.SetNrequestItemAndBarCodeForm_TaiHe(jsonentity.BarCodeList, nrf_m, m_collecter, m_collecterId, out bcf_List);
                #endregion

                #endregion

                #region NRequestItem

                bNRequestItemRusult = rib.AddNrequestItem_TaiHe(nri_List);

                #endregion

                #region BarCodeForm

                bBarCodeFormRusult = ibbcf.AddBarCodeForm_TaiHe(bcf_List);
                #endregion

                #region NRequestForm
                bNRequestFormRusult = rfb.AddNrequest(nrf_m, bcf_List);

                #endregion

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                {
                    List<UiBarCode> BarCodeList = ibbcf.ConvertBarCodeFormToUiBarCode_TaiHe(bcf_List, nrf_m);

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(BarCodeList);
                    brdv.success = true;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("requestFormAdd_BarCodePrintTaiHe.bNRequestFormRusult：" + bNRequestFormRusult + ";bNRequestItemRusult:" + bNRequestItemRusult + ";bBarCodeFormRusult:" + bBarCodeFormRusult);
                    brdv.success = false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("requestFormAdd_BarCodePrintTaiHe.异常：" + e.ToString());
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
            }
            return brdv;
        }

        /// <summary>
        /// 修改weblis生成条码并打印
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue requestFormUpdate_BarCodePrint(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;
                //申请单号
                long nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;

                //判断Weblisflag==5,说明条码是迁出状态,不让更新。
                if (!rfb.CheckNReportFormStatus(nRequestFormNo))
                {
                    return null;
                }
                #endregion

                #region 定义变量
                string m_WebLisOrgId = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                int m_collecterId = user.EmplID;
                string m_collecter = user.NameL + user.NameF;

                Model.NRequestForm nrf_m = null;

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion
                #endregion

                #region 对象赋值

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.SerialNo = nRequestFormNo.ToString();
                //nrf_m.ClientNo = m_WebLisOrgId;
                //nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                nrf_m.WebLisSourceOrgID = nrf_m.ClientNo;
                nrf_m.WebLisSourceOrgName = nrf_m.ClientName;
                nrf_m.WebLisOrgID = m_WebLisOrgId;
                nrf_m.Collecter = user.NameL + user.NameF;
                #region 根据前台选择的组套项目,计算一个申请单中组套的总价
                foreach (var item in jsonentity.CombiItems)
                {
                    Model.Lab_TestItem lbmodel = ltic.GetModel(nrf_m.ClientNo, item.CombiItemNo.ToString());
                    if (lbmodel != null)
                        nrf_m.Price += lbmodel.Price == null ? 0 : (decimal)lbmodel.Price;
                }
                #endregion
                #endregion

                #region 组合项目/条码

                List<Model.BarCodeForm> bcf_List = null;
                List<NRequestItem> nri_List = rib.SetNrequestItemAndBarCodeForm(jsonentity.BarCodeList, nrf_m, m_collecter, m_collecterId, out bcf_List);
                #endregion

                #endregion

                #region NRequestItem

                bNRequestItemRusult = rib.UpdateNrequestItem(nri_List, nRequestFormNo);

                #endregion

                #region BarCodeForm

                bBarCodeFormRusult = ibbcf.UpdateBarCodeForm(bcf_List);
                #endregion

                #region NRequestForm
                bNRequestFormRusult = rfb.UpdateNrequest(nrf_m, bcf_List);

                #endregion

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                {

                    brdv.success = true;
                }
                else
                    brdv.success = false;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
            }
            return brdv;
        }

        /// <summary>
        /// 组套项目不需要对照  
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue requestFormUpdate_BarCodePrintTaiHe(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                #region 验证
                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;
                //申请单号
                long nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;

                //判断Weblisflag==5,说明条码是迁出状态,不让更新。
                if (!rfb.CheckNReportFormStatus(nRequestFormNo))
                {
                    return null;
                }
                #endregion

                #region 定义变量
                string m_WebLisOrgId = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                int m_collecterId = user.EmplID;
                string m_collecter = user.NameL + user.NameF;

                Model.NRequestForm nrf_m = null;

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion
                #endregion

                #region 对象赋值

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.SerialNo = nRequestFormNo.ToString();
                //nrf_m.ClientNo = m_WebLisOrgId;
                //nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                nrf_m.WebLisSourceOrgID = nrf_m.ClientNo;
                nrf_m.WebLisSourceOrgName = nrf_m.ClientName;
                nrf_m.WebLisOrgID = m_WebLisOrgId;
                nrf_m.Collecter = user.NameL + user.NameF;
                #region 根据前台选择的组套项目,计算一个申请单中组套的总价
                foreach (var item in jsonentity.CombiItems)
                {
                    Model.Lab_TestItem lbmodel = ltic.GetModel(nrf_m.ClientNo, item.CombiItemNo.ToString());
                    if (lbmodel != null)
                        nrf_m.Price += lbmodel.Price == null ? 0 : (decimal)lbmodel.Price;
                }
                #endregion
                #endregion

                #region 组合项目/条码

                List<Model.BarCodeForm> bcf_List = null;
                List<NRequestItem> nri_List = rib.SetNrequestItemAndBarCodeForm_TaiHe(jsonentity.BarCodeList, nrf_m, m_collecter, m_collecterId, out bcf_List);
                #endregion

                #endregion

                #region NRequestItem

                bNRequestItemRusult = rib.UpdateNrequestItem_TaiHe(nri_List, nRequestFormNo);

                #endregion

                #region BarCodeForm

                bBarCodeFormRusult = ibbcf.UpdateBarCodeForm_TaiHe(bcf_List);
                #endregion

                #region NRequestForm
                bNRequestFormRusult = rfb.UpdateNrequest(nrf_m, bcf_List);

                #endregion

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                {

                    brdv.success = true;
                }
                else
                    brdv.success = false;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
            }
            return brdv;
        }

        /// <summary>
        /// 根据申请单号，更新成功打印的次数
        /// </summary>
        /// <param name="nrequestformnos"></param>
        /// <returns></returns>
        public BaseResultDataValue UpdateNrequestPrintTimesByFormNo(string nrequestformnos)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            NrequestCombiItemBarCodeEntity ncibe = new NrequestCombiItemBarCodeEntity();
            try
            {
                bool isSucess = false;
                foreach (var nrequestformno in nrequestformnos.Split(','))
                {
                    ncibe = new NrequestCombiItemBarCodeEntity();
                    long NrequestFormNo;
                    long.TryParse(nrequestformno, out NrequestFormNo);
                    isSucess = rfb.UpdatePrintTimesByNrequestNo(NrequestFormNo);
                }

                if (isSucess)
                    brdv.success = true;
                else
                    brdv.success = false;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }


        #endregion

        #region 申请单删除
        /// <summary>
        /// 根据申请单号删除记录
        /// </summary>
        /// <param name="NRequestFromNo"></param>
        /// <returns></returns>
        public BaseResultBool DeleteNRequestFromByNRequestFromNo(string nrequestFromNo)
        {
            BaseResultBool baseresultbool = new BaseResultBool();
            baseresultbool.BoolFlag = false;
            try
            {
                //检查记录是否存在
                if (rfb.CheckNReportFormStatus(long.Parse(nrequestFromNo)))
                {
                    baseresultbool.BoolFlag = (rfb.Delete(long.Parse(nrequestFromNo)) > 0) ? true : false;
                    baseresultbool.success = true;
                }
                else
                {
                    baseresultbool.BoolFlag = false;
                    baseresultbool.success = false;
                }
            }
            catch (Exception e)
            {
                baseresultbool.BoolFlag = false;
                baseresultbool.success = true;
                baseresultbool.ErrorInfo = e.ToString();
            }
            return baseresultbool;
        }
        #endregion

        #region 查询统计
        /// <summary>
        /// 根据申请单号加载信息
        /// </summary>
        /// <param name="nrequestformno"></param>
        /// <returns></returns>
        public BaseResultDataValue GetNrequestFormByFormNo(string nrequestformno)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            NrequestCombiItemBarCodeEntity ncibe = new NrequestCombiItemBarCodeEntity();

            try
            {

                long nRequestFormNo;
                long.TryParse(nrequestformno, out nRequestFormNo);
                #region 查询



                #region NRequestForm
                Model.NRequestForm nrf_m = rfb.GetModel(nRequestFormNo);
                ncibe.NrequestForm = nrf_m;
                #endregion

                #region NrequestItem
                List<UiCombiItem> uiCombiItemList = null;
                GetNrequestItemByRequsetNo(ref nrf_m, out uiCombiItemList);
                ncibe.CombiItems = uiCombiItemList;
                #endregion

                #region BarCodeForm
                List<UiBarCode> uibarCodeList = new List<UiBarCode>();
                UiBarCode uiBarCode = new UiBarCode();

                List<long> barCodeFormNoList = new List<long>();
                DataSet dsTemp = rib.GetNrequestItemByNrequestNo(nRequestFormNo);
                if (dsTemp != null)
                {
                    DataRowCollection drs = dsTemp.Tables[0].Rows;
                    foreach (DataRow dr in drs)
                    {

                        long barCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString());

                        if (!barCodeFormNoList.Contains(barCodeFormNo))
                        {
                            barCodeFormNoList.Add(barCodeFormNo);

                            ZhiFang.Common.Log.Log.Info("barCodeFormNo:" + barCodeFormNo);
                            List<string> itemList = new List<string>();
                            DataRow[] drsItem = dsTemp.Tables[0].Select("BarCodeFormNo=" + barCodeFormNo);
                            if (drsItem.Length > 0)
                            {
                                ZhiFang.Common.Log.Log.Info("drsItem.Length:" + drsItem.Length);
                                string barCode = dr["BarCode"].ToString();
                                string sampleTypeNo = dr["SampleTypeNo"].ToString();

                                string colorName = string.Empty;
                                string colorValue = string.Empty;
                                List<SampleTypeDetail> sampleTypeDetailList = new List<SampleTypeDetail>();
                                SampleTypeDetail sampleTypeDetail = new SampleTypeDetail();

                                DataSet dsLabTestItem = ltic.GetLabTestItemByItemNo(nrf_m.AreaNo, drsItem[0]["ParItemNo"].ToString());
                                if (dsLabTestItem != null && dsLabTestItem.Tables[0].Rows.Count > 0)
                                {
                                    colorName = dsLabTestItem.Tables[0].Rows[0]["Color"].ToString();
                                    if (colorName != null && colorName != "")
                                    {
                                        colorValue = icd.GetModelByColorName(colorName).ColorValue;//ZhiFang.BLL.Common.Lib.ItemColor()[colorName].ColorValue;
                                        foreach (var sampleType in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(colorName))//ZhiFang.BLL.Common.Lib.ItemColor()[colorName].SampleType)
                                        {
                                            sampleTypeDetail = new SampleTypeDetail();
                                            sampleTypeDetail.CName = sampleType.CName;
                                            sampleTypeDetail.SampleTypeID = sampleType.SampleTypeID.ToString();
                                            sampleTypeDetailList.Add(sampleTypeDetail);
                                        }
                                    }
                                }
                                foreach (var item in drsItem)
                                {
                                    DataSet dsItem = ltic.GetLabTestItemByItemNo(nrf_m.AreaNo, item["ParItemNo"].ToString());
                                    if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                                    {
                                        itemList.Add(dsItem.Tables[0].Rows[0]["ItemNo"].ToString());
                                    }
                                }

                                //给对象赋值
                                uiBarCode = new UiBarCode();
                                uiBarCode.BarCode = barCode;
                                uiBarCode.ColorName = colorName;
                                uiBarCode.ColorValue = colorValue;
                                uiBarCode.ItemList = itemList;
                                uiBarCode.SampleType = sampleTypeNo;
                                uiBarCode.SampleTypeDetailList = sampleTypeDetailList;

                                uibarCodeList.Add(uiBarCode);
                            }

                        }
                    }
                }
                ncibe.BarCodeList = uibarCodeList;
                #endregion

                #endregion

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ncibe);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("GetNrequestFormByFormNo.异常:" + e.ToString() + ".IP地址:" + IPHelper.GetClientIP());
            }

            return brdv;
        }

        /// <summary>
        /// 组合为契约格式
        /// </summary>
        /// <param name="nRequestFormNo"></param>
        /// <param name="uicombiItemList"></param>
        private void GetNrequestItemByRequsetNo(ref NRequestForm nrf_m, out List<UiCombiItem> uicombiItemList)
        {
            UiCombiItem uiCombiItem = new UiCombiItem();
            List<UiCombiItem> uiCombiItemList = new List<UiCombiItem>();
            List<UiCombiItemDetail> uiCombiItemDetailList = new List<UiCombiItemDetail>();
            List<Model.NRequestItem> nrequestItemList = rib.GetModelList(new NRequestItem() { NRequestFormNo = nrf_m.NRequestFormNo });
            IBCLIENTELE bclientele = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL("BaseDictionary.CLIENTELE");
            IBClientEleArea bclientelearea = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBClientEleArea>.GetBLL("BaseDictionary.ClientEleArea");
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            long dicclientno = -1;
            CLIENTELE client = null;
            client = bclientele.GetModel(long.Parse(nrf_m.ClientNo));
            if (ltic.GetTotalCount(new Lab_TestItem() { LabCode = nrf_m.ClientNo }) > 0)
            {
                dicclientno = long.Parse(nrf_m.ClientNo);
            }
            else
            {
                dicclientno = bclientelearea.GetModel(client.AreaID.Value).ClientNo.Value;
            }
            nrf_m.AreaNo = dicclientno.ToString();
            DataSet dsitemmap = tic.GetLabItemCodeMapListByNRequestLabCodeAndFormNo(dicclientno.ToString(), nrf_m.NRequestFormNo.ToString());

            //通过nRequestFormNo,获取NrequestItem的值


            #region NRequestItem中包含多个combiItemNo

            Dictionary<int, string> combiItemNoTempList = new Dictionary<int, string>();
            foreach (var requestItem1 in nrequestItemList)
            {
                int combiItemNo = -1;
                string combiItemName = "";
                if (dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").Count() > 0)
                {
                    combiItemNo = int.Parse(dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["ItemNo"].ToString());
                    combiItemName = dsitemmap.Tables[0].Select(" ItemNo='" + requestItem1.CombiItemNo + "' ").ElementAt(0)["CName"].ToString();
                }

                //用于记录nrequestItemList的combiItemNo
                if (!combiItemNoTempList.Keys.Contains(combiItemNo))
                {
                    combiItemNoTempList.Add(combiItemNo, combiItemName);
                }
            }
            #endregion

            #region 拼接契约所需格式
            for (int i = 0; i < combiItemNoTempList.Count; i++)
            {
                //List<Model.NRequestItem> nrequestItemListTemp = nrequestItemList.FindAll(p => p.CombiItemNo == combiItemNoTempList[i]);
                uiCombiItem = new UiCombiItem();
                uiCombiItem.CombiItemNo = combiItemNoTempList.ElementAt(i).Key.ToString();
                uiCombiItem.CombiItemName = combiItemNoTempList.ElementAt(i).Value;

                //获取项目名称


                //获取子项
                var subnrequestItemList = nrequestItemList.Where(a => a.CombiItemNo == combiItemNoTempList.ElementAt(i).Key.ToString());


                if (subnrequestItemList != null && subnrequestItemList.Count() > 0)
                {
                    uiCombiItemDetailList = new List<UiCombiItemDetail>();
                    foreach (var subitem in subnrequestItemList)
                    {
                        UiCombiItemDetail uiCombiItemDetail = new UiCombiItemDetail();
                        if (dsitemmap.Tables[0].Select(" ItemNo='" + subitem.ParItemNo.ToString() + "' ").Count() > 0)
                        {
                            uiCombiItemDetail.CombiItemDetailNo = dsitemmap.Tables[0].Select(" ItemNo='" + subitem.ParItemNo.ToString() + "' ").ElementAt(0)["ItemNo"].ToString(); ;
                            uiCombiItemDetail.CombiItemDetailName = dsitemmap.Tables[0].Select(" ItemNo='" + subitem.ParItemNo.ToString() + "' ").ElementAt(0)["CName"].ToString();
                        }

                        uiCombiItemDetailList.Add(uiCombiItemDetail);

                    }
                    uiCombiItem.CombiItemDetailList = uiCombiItemDetailList;

                }
                uiCombiItemList.Add(uiCombiItem);

            }
            #endregion

            uicombiItemList = uiCombiItemList;
        }

        /// <summary>
        /// 根据申请单号查询信息
        /// </summary>
        /// <param name="nrequestformno"></param>
        /// <returns></returns>
        public BaseResultDataValue GetNrequestFormByFormNo_BarCodePrint(string nrequestformno)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            NrequestCombiItemBarCodeEntity ncibe = new NrequestCombiItemBarCodeEntity();
            try
            {
                long nRequestFormNo;
                long.TryParse(nrequestformno, out nRequestFormNo);

                #region NRequestForm
                Model.NRequestForm nrf_m = rfb.GetModel(nRequestFormNo);
                ncibe.NrequestForm = nrf_m;
                #endregion

                #region NRequestItem
                List<UiCombiItem> uiCombiItemList = rib.GetUiCombiItemByNrequestForm(nrf_m);
                ncibe.CombiItems = uiCombiItemList;
                #endregion

                #region BarCodeForm
                List<UiBarCode> uibarCodeList = ibbcf.GetUiBarCodeListByNrequestFormNo(nRequestFormNo);
                ncibe.BarCodeList = uibarCodeList;
                #endregion

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ncibe);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        /// <summary>
        ///  组套项目不需要对照
        /// </summary>
        /// <param name="nrequestformno"></param>
        /// <returns></returns>
        public BaseResultDataValue GetNrequestFormByFormNo_BarCodePrintTaiHe(string nrequestformno)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            NrequestCombiItemBarCodeEntity ncibe = new NrequestCombiItemBarCodeEntity();
            try
            {
                long nRequestFormNo;
                long.TryParse(nrequestformno, out nRequestFormNo);

                #region NRequestForm
                Model.NRequestForm nrf_m = rfb.GetModel(nRequestFormNo);
                ncibe.NrequestForm = nrf_m;
                #endregion

                #region NRequestItem
                List<UiCombiItem> uiCombiItemList = rib.GetUiCombiItemByNrequestForm_TaiHe(nrf_m);
                ncibe.CombiItems = uiCombiItemList;
                #endregion

                #region BarCodeForm
                List<UiBarCode> uibarCodeList = ibbcf.GetUiBarCodeListByNrequestFormNo_TaiHe(nRequestFormNo);
                ncibe.BarCodeList = uibarCodeList;
                #endregion

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ncibe);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        /// <summary>
        /// 根据申请单号,获取批量打印的条码信息
        /// </summary>
        /// <param name="nrequestformnos"></param>
        /// <returns></returns>
        public BaseResultDataValue GetBatchCodeListByFormNo_BarCodePrint(string nrequestformnos)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            NrequestCombiItemBarCodeEntity ncibe = new NrequestCombiItemBarCodeEntity();
            List<NrequestCombiItemBarCodeEntity> ncibes = new List<NrequestCombiItemBarCodeEntity>();
            try
            {
                foreach (var nrequestformno in nrequestformnos.Split(','))
                {
                    ncibe = new NrequestCombiItemBarCodeEntity();
                    //申请单号
                    long nRequestFormNo;
                    long.TryParse(nrequestformno, out nRequestFormNo);

                    #region NRequestForm
                    Model.NRequestForm nrf_m = rfb.GetModel(nRequestFormNo);
                    ncibe.NrequestForm = nrf_m;
                    #endregion

                    #region BarCodeForm
                    List<UiBarCode> uibarCodeList = ibbcf.GetBatchUiBarCodeListByNrequestFormNo(nRequestFormNo);
                    ncibe.BarCodeList = uibarCodeList;
                    #endregion

                    ncibes.Add(ncibe);
                }
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ncibes);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetNRequestFormList(string ClientNo, string txtStartDate, string txtEndDate, string txtCollectStartDate, string txtCollectEndDate, string SelectDoctor, string txtPatientID, string txtPatientName, string SickTypeNo, string filetype)
        {
            int PageSize = 25;  //单页显示最大申请单数：25
            string loginID = "";  //登录用户编号
            string loginName = "";  //登录用户姓名     
            Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
            IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
            IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();


            BaseResultDataValue brdv = new BaseResultDataValue();
            Class.User user = new ZhiFang.WebLis.Class.User();
            string _ClientNo = "";
            string _ClientName = "";
            string _txtStartDate = "";
            string _txtEndDate = "";
            string _txtCollectStartDate = "";
            string _txtCollectEndDate = "";
            string _SelectDoctor = "";
            string _txtPatientID = "";
            string _txtPatientName = "";
            string _SickTypeNo = "";

            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
            {
                brdv.ErrorInfo = "未登录，请登陆后继续！";
                brdv.success = false;
                return brdv;
            }
            else
            {
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetOrganizationsList();
                user.GetPostList();
                try
                {
                    #region 客户编码
                    if (!user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                    {
                        if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                        {
                            if (user.OrganizationsList.Count > 0)
                            {
                                if (user.OrganizationsList.ElementAt(0).Value != null && user.OrganizationsList.ElementAt(0).Value.Count() > 0)
                                {
                                    if (_ClientNo != null)
                                    {
                                        _ClientNo = ClientNo.Trim();
                                    }
                                    else
                                    {
                                        foreach (var c in user.OrganizationsList)
                                        {
                                            _ClientNo += "'" + c.Value + "',";
                                        }
                                        DataSet ds = user.GetClientListByPost("", -1); ;
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            _ClientNo += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                                        }
                                        _ClientNo = _ClientNo.Substring(0, _ClientNo.LastIndexOf(','));
                                    }
                                }
                            }
                            else
                            {
                                _ClientNo = "-1";
                                brdv.ErrorInfo = "客户编码输入错误！";
                                brdv.success = false;
                                return brdv;
                            }
                        }
                        else
                        {
                            _ClientNo = " -1 ";
                            brdv.ErrorInfo = "客户编码输入错误！";
                            brdv.success = false;
                            return brdv;
                        }
                    }
                    else
                    {
                        if (ClientNo != null)
                        {
                            _ClientNo = ClientNo.Trim();
                            _ClientName = ibc.GetModel(long.Parse(_ClientNo)).CNAME + "标本外送清单";

                        }
                        else
                        {
                            _ClientName = "标本外送清单";
                        }
                    }
                    #endregion
                    if (ClientNo != null)
                        _ClientName = ibc.GetModel(long.Parse(_ClientNo)).CNAME;

                    #region 开单时间
                    if (_txtStartDate != null)
                    {
                        _txtStartDate = txtStartDate;
                        //this.Label3.Text = _txtStartDate;
                    }
                    if (txtEndDate != null)
                    {
                        _txtEndDate = txtEndDate;
                        //this.Label3.Text += "--" + _txtEndDate;
                    }
                    #endregion
                    #region 采样时间
                    if (txtCollectStartDate != null)
                    {
                        _txtCollectStartDate = txtCollectStartDate;
                    }
                    if (txtCollectEndDate != null)
                    {
                        _txtCollectEndDate = txtCollectEndDate;
                    }
                    #endregion
                    #region 医生
                    if (SelectDoctor != null)
                    {
                        _SelectDoctor = SelectDoctor;
                    }
                    #endregion
                    #region 病历号
                    if (txtPatientID != null)
                    {
                        _txtPatientID = txtPatientID;
                    }
                    #endregion
                    #region 姓名
                    if (txtPatientName != null)
                    {
                        _txtPatientName = txtPatientName;
                    }
                    #endregion
                    #region 就诊类型
                    if (SickTypeNo != null)
                    {
                        _SickTypeNo = SickTypeNo;
                    }
                    #endregion
                    DataTable dt = PrintNRequestForm(_txtPatientName, _txtCollectStartDate, _txtCollectEndDate, _txtStartDate, _txtEndDate, _SelectDoctor, _ClientNo, _txtPatientID, "", _SickTypeNo);
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        brdv.ErrorInfo = "未能查找到数据！";
                        brdv.success = false;
                        return brdv;
                    }
                    brdv = CreatNRequestFormFile(dt, filetype);

                    return brdv;
                }
                catch (Exception eee)
                {
                    ZhiFang.Common.Log.Log.Error("GetNRequestFormList.异常：" + eee.ToString());
                    brdv.ErrorInfo = "异常:" + eee.Message;
                    brdv.success = false;
                    return brdv;
                }
            }
        }

        private BaseResultDataValue CreatNRequestFormFile(DataTable dt, string filetype)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            IBSickType ibst = BLLFactory<IBSickType>.GetBLL();
            int k = 0;
            List<string> columnsnamelist = new List<string>() { "BarCode", "ClientNo", "ClientName", "WebLisOrgID", "WebLisOrgName", "WebLisSourceOrgID", "WebLisSourceOrgName", "Age", "ItemName", "ItemNo", "color", "SampleTypeName", "CName", "AgeUnitName", "GenderName", "PatNo", "GenderNo", "AgeUnitNo", "OperDate", "OperTime", "jztype", "jztypeName", "Count" };
            foreach (string c in columnsnamelist)
            {
                if (!dt.Columns.Contains(c))
                    dt.Columns.Add(c);
            }
            foreach (DataRow dr in dt.Rows)
            {

                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                barcode = string.Join(",\r\n", b.Split(','));
                k += b.Split(',').Length;
                dr["barcode"] = barcode;


                if (dr["jztype"].ToString().Trim().Length > 0)
                {
                    SickType st = ibst.GetModel(int.Parse(dr["jztype"].ToString().Trim()));
                    if (st != null)
                    {
                        dr["jztypeName"] = st.CName;
                    }
                    else
                    {
                        dr["jztypeName"] = "";
                    }
                }
                else
                {
                    dr["jztypeName"] = "";
                }
                dr["GenderName"] = dr["Sex"].ToString();
                DataSet dsri = new DataSet();
                string htmltmp = "";
                try
                {
                    dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = long.Parse(dr["NRequestFormNo"].ToString().Trim()) });
                    List<string> itemname = new List<string>();
                    List<string> itemno = new List<string>();
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsri.Tables[0].Rows.Count; i++)
                        {
                            if (!itemname.Contains(dsri.Tables[0].Rows[i]["CName"].ToString().Trim()))
                                itemname.Add(dsri.Tables[0].Rows[i]["CName"].ToString().Trim());
                            if (!itemno.Contains(dsri.Tables[0].Rows[i]["CombiItemNo"].ToString().Trim()))
                                itemno.Add(dsri.Tables[0].Rows[i]["CombiItemNo"].ToString().Trim());
                        }
                        dr["ItemName"] = string.Join(",\r\n", itemname);
                        dr["ItemNo"] = string.Join(",", itemno);
                    }


                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error("CreatNRequestFormFile.异常：" + e.ToString());
                }
                dr["Count"] = dt.Rows.Count;
            }
            FastReport.Report report = new FastReport.Report();
            if (filetype.Trim().ToUpper() == "XLS")
            {
                string url = dt.Rows[0]["WebLisSourceOrgID"] + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\");

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\NRequestFormList.frx");

                dt.TableName = "tab_nrf";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
                return brdv;
            }

            if (filetype.Trim().ToUpper() == "PDF")
            {
                string url = dt.Rows[0]["WebLisSourceOrgID"] + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".PDF";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\");

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\NRequestFormList.frx");

                dt.TableName = "tab_nrf";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport pdfexport = new FastReport.Export.Pdf.PDFExport();
                report.Export(pdfexport, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
                return brdv;
            }
            brdv.success = false;
            return brdv;
        }

        public DataTable PrintNRequestForm(string PatientName, string CollectStartDate, string CollectEndDate, string AddStartDate, string AddEndDate, string Doctor, string WebLisSourceOrgID, string PatNo, string SampleTypeNo, string SickTypeNo)
        {
            ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
            if (WebLisSourceOrgID.Trim() != "")
            {
                nrf_m.ClientList = WebLisSourceOrgID;
            }
            if (Doctor.Trim() != "")
            {
                nrf_m.DoctorNameList = Doctor;
            }
            if (PatientName.Trim() != "")
            {
                nrf_m.CName = PatientName.Trim();
            }
            if (PatNo.Trim() != "")
            {
                nrf_m.PatNo = PatNo.Trim();
            }
            if (AddStartDate.Trim() != "")
            {
                nrf_m.OperDateStart = AddStartDate.Trim();
            }
            if (AddEndDate.Trim() != "")
            {
                if (AddEndDate.Trim().Length > 11)
                {
                    nrf_m.OperDateEnd = AddEndDate.Trim();
                }
                else
                {
                    nrf_m.OperDateEnd = AddEndDate.Trim() + " 23:59:59";
                }
            }
            if (CollectStartDate.Trim() != "")
            {
                nrf_m.CollectDateStart = CollectStartDate.Trim();
            }
            if (CollectEndDate.Trim() != "")
            {
                if (CollectEndDate.Trim().Length > 11)
                {
                    nrf_m.CollectDateEnd = CollectEndDate.Trim();
                }
                else
                {
                    nrf_m.CollectDateEnd = CollectEndDate.Trim() + " 23:59:59";
                }
            }
            //if (SickTypeNo.Trim() != "")
            //{
            //    nrf_m.jztype = Convert.ToInt32(SickTypeNo.Trim());
            //}

            if (SickTypeNo.Trim() != "")
            {
                nrf_m.SickTypeList = SickTypeNo.Trim();
            }
            //nrf_m.IsOnlyNoBar = false;
            //if (chkOnlyNoPrintBarCode.Checked)
            //{
            //    nrf_m.IsOnlyNoBar = true;
            //}



            DataTable dt = new DataTable();
            int iCount, intPageCount;
            dt = rfb.GetNRequstFormList(nrf_m, 0, 10000, out intPageCount, out iCount);
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        public BaseResultBool CheckNRequestFromStatusByNRequestFromNo(string NRequestFromNo)
        {
            BaseResultBool baseresultbool = new BaseResultBool();
            baseresultbool.BoolFlag = false;
            try
            {
                baseresultbool.BoolFlag = rfb.CheckNReportFormStatus(long.Parse(NRequestFromNo));
                baseresultbool.success = true;
            }
            catch (Exception e)
            {
                baseresultbool.BoolFlag = false;
                baseresultbool.success = true;
                baseresultbool.ErrorInfo = e.ToString();
            }
            return baseresultbool;
        }

        #endregion     

        #region 订单管理

        IBLL.Common.BaseDictionary.IBSendOrder ibSendOrder = BLLFactory<IBSendOrder>.GetBLL();

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue GetOrderList(Model.SendOrder jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SendOrder> Orderlist = new EntityListEasyUI<Model.SendOrder>();
            DataSet ds = ibSendOrder.GetList(jsonentity);

            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Orderlist.rows = ibSendOrder.DataTableToList(ds.Tables[0]);
                    Orderlist.total = Orderlist.rows.Count;
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(Orderlist);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("查询字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "查询字典无数据！";
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
            return brdv;
        }

        /// <summary>
        /// 根据单条订单号,修改备注信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="Note"></param>
        /// <returns></returns>
        public BaseResultDataValue UpdateOrderNote(string OrderNo, string Note)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (ibSendOrder.IsExist(OrderNo))
            {
                if (ibSendOrder.UpdateNoteByOrderNo(OrderNo, Note) > 0)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                }
            }
            else
            {
                brdv.ErrorInfo = "OrderNo不存在";
                brdv.success = false;
            }
            return brdv;
        }

        /// <summary>
        /// 根据订单号查询条码信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public BaseResultDataValue GetBarCodeByOrderNo(string OrderNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.UiModel.SampleTypeBarCodeInfo> Orderlist = new EntityListEasyUI<Model.UiModel.SampleTypeBarCodeInfo>();
            DataSet ds = ibbcf.GetBarCodeByOrderNo(OrderNo);
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    Orderlist.rows = ibSendOrder.DataTableToSampleTypeBarCode(ds.Tables[0]);
                    Orderlist.total = Orderlist.rows.Count;
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(Orderlist);
                }
                else
                {
                    brdv.success = true;
                    brdv.ErrorInfo = "没有记录";
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
            return brdv;
        }

        /// <summary>
        /// 根据条码主键号获取项目
        /// </summary>
        /// <param name="BarCodeFormNo"></param>
        /// <returns></returns>
        public BaseResultDataValue GetNrequestItemByBarCodeFormNo(string BarCodeFormNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.UiModel.SampleItemInfo> Orderlist = new EntityListEasyUI<Model.UiModel.SampleItemInfo>();
            DataSet ds = rib.GetNrequestItemByBarCodeFormNo(BarCodeFormNo);
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    Orderlist.rows = ibSendOrder.DataTableToSampleItemInfo(ds.Tables[0]);
                    Orderlist.total = Orderlist.rows.Count;
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(Orderlist);
                }
                else
                {
                    brdv.success = true;
                    brdv.ErrorInfo = "没有记录";
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
            return brdv;

        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public BaseResultDataValue DelOrder(string OrderNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 验证
                Model.SendOrder sendOrder = ibSendOrder.GetModel(OrderNo);
                if (sendOrder != null)
                {
                    //订单为已打印状态
                    if (sendOrder.Status == 2)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "订单:" + OrderNo + "已打印,不能删除";
                        return brdv;
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "没有记录";
                    return brdv;
                }
                #endregion

                if (ibSendOrder.Delete(OrderNo) > 0)
                {
                    if (ibbcf.UpdateByOrderNo(new BarCodeForm() { OrderNo = OrderNo }) > 0)
                    {
                        brdv.success = true;
                    }
                }


            }
            catch (Exception e)
            {

                brdv.ErrorInfo = e.ToString();
                brdv.success = false;
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }

            return brdv;
        }

        /// <summary>
        /// 订单打印,根据订单号,查询多个申请单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="OrderNo">订单号</param>
        /// <returns></returns>
        public BaseResultDataValue GetNRequestFromListByOrderNo(int page, int rows, string OrderNo)
        {

            //EntityListEasyUI<Model.UiModel.OrderPrint> OrderPrintResult = new EntityListEasyUI<Model.UiModel.OrderPrint>();
            DataSet dsTemp = new DataSet();

            BaseResultDataValue OrderPrintResult = new BaseResultDataValue();
            List<Model.NRequestFormResult> nrequestformlist = new List<Model.NRequestFormResult>();
            //List<List<Model.NRequestFormResult>> nrequestformlists = new List<List<NRequestFormResult>>();
            Model.UiModel.OrderPrint orderPrint = new OrderPrint();
            try
            {
                orderPrint.OrderNo = OrderNo;
                Model.SendOrder senderModel = ibSendOrder.GetModelByOrderPrint(OrderNo);
                if (senderModel != null)
                {
                    orderPrint.SendLabName = senderModel.SendLabCodeName;
                }

                DataSet dsBarCode = ibbcf.GetBarCodeByOrderNo(OrderNo);
                if (dsBarCode != null && dsBarCode.Tables[0].Rows.Count > 0)
                {

                    orderPrint.BarcodeSum = dsBarCode.Tables[0].Rows.Count;
                    DataRowCollection drs = dsBarCode.Tables[0].Rows;
                    foreach (DataRow dr in drs)
                    {
                        long NrequestFormNo = long.Parse(dr["NRequestFormNo"].ToString());
                        //NRequestForm nrequestform = rfb.GetModel(NrequestFormNo); //JsonConvert.DeserializeObject<NRequestForm>(jsonentity);
                        NRequestForm nrf_m = new NRequestForm() { NRequestFormNo = NrequestFormNo };//nrequestform;//new NRequestForm();

                        DataTable dt = new DataTable();
                        int iCount = 0, intPageCount;
                        if (page > 0)
                        {
                            page = page - 1;
                        }
                        dt = rfb.GetNRequstFormList(nrf_m, page, rows, out intPageCount, out iCount);
                        if (dt != null)
                        {
                            nrequestformlist.AddRange(BarCodeFormFillList(dt, dr["BarCode"].ToString()));
                        }

                    }
                }
                //查询开单时间范围
                if (nrequestformlist.Count > 0)
                {
                    nrequestformlist.OrderByDescending(t => t.OperTime);
                    orderPrint.OperEndDate = nrequestformlist[0].OperTime;
                    orderPrint.OperSartDate = nrequestformlist[nrequestformlist.Count - 1].OperTime;
                }
                orderPrint.ReciveCompany = ConfigHelper.GetConfigString("CompanyName");
                orderPrint.nrequestFormResultList = nrequestformlist;
                dsTemp.Tables.Add(ibSendOrder.NrequestFormToDataTable(nrequestformlist));
                dsTemp.Tables.Add(ibSendOrder.OrderPrintToDataTable(orderPrint));
                //OrderPrintResult.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(orderPrint);
                //OrderPrintResult.success = true;


                ZhiFang.BLL.Report.Print.PrintReportFormCommon prfc = new BLL.Report.Print.PrintReportFormCommon();
                string pdfurl = prfc.GetOrderPrintHtmlContext(dsTemp, OrderNo);
                OrderPrintResult.ResultDataValue = pdfurl;//ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(pdfurl);
                OrderPrintResult.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                OrderPrintResult.ErrorInfo = e.ToString();
                OrderPrintResult.success = false;
            }
            return OrderPrintResult;
        }

        /// <summary>
        /// 根据申请单的查询条件，获取条码列表,过滤掉其它订单使用的条码
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue GetBarCodeListByWhere(NRequestForm jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                int page = 1, rows = 1000;
                EntityListEasyUI<Model.UiModel.SampleTypeBarCodeInfo> result = new EntityListEasyUI<Model.UiModel.SampleTypeBarCodeInfo>();
                List<SampleTypeBarCodeInfo> sampleTypeBarCodeInfoList = new List<SampleTypeBarCodeInfo>();
                SampleTypeBarCodeInfo sampleTypeBarCodeInfo = new SampleTypeBarCodeInfo();
                NRequestForm nrf_m = jsonentity;
                nrf_m.ClientName = null;
                nrf_m.OperDateStart = jsonentity.OperDateStart;
                if (jsonentity.OperDateEnd != null)
                    if (jsonentity.OperDateEnd.Length > 11)
                    {
                        nrf_m.OperDateEnd = jsonentity.OperDateEnd;
                    }
                    else
                    {
                        nrf_m.OperDateEnd = jsonentity.OperDateEnd + " 23:59:59";
                    }

                nrf_m.CollectDateStart = jsonentity.CollectDateStart;
                if (jsonentity.CollectDateEnd != null)
                    if (jsonentity.CollectDateEnd.Length > 11)
                    {
                        nrf_m.CollectDateEnd = jsonentity.CollectDateEnd;
                    }
                    else
                    {
                        nrf_m.CollectDateEnd = jsonentity.CollectDateEnd + " 23:59:59";
                    }
                DataTable dt = new DataTable();
                int iCount, intPageCount;
                if (page > 0)
                {
                    page = page - 1;
                }
                dt = rfb.GetNRequstFormList(nrf_m, page, rows, out intPageCount, out iCount);

                if (dt != null)
                {
                    sampleTypeBarCodeInfoList = FillListSampleTypeBarCodeInfo(dt);

                    result.rows = sampleTypeBarCodeInfoList;
                    result.total = sampleTypeBarCodeInfoList.Count;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(result);
                    brdv.success = true;

                }
                else
                {
                    brdv.success = false;
                }
            }
            catch (Exception e)
            {

                ZhiFang.Common.Log.Log.Error(e.ToString());
                brdv.ErrorInfo = e.ToString();
                brdv.ResultDataValue = "";
                brdv.success = false;
            }

            return brdv;
        }

        /// <summary>
        /// 转换结果集类型
        /// </summary>
        /// <param name="dt"></param>
        public List<SampleTypeBarCodeInfo> FillListSampleTypeBarCodeInfo(DataTable dt)
        {
            return rfb.GetBarCodeAndCNameByNReuqestFormNo(dt);
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResult AddOrder(Model.SendOrder jsonentity)
        {
            BaseResult br = new BaseResult();

            try
            {
                Model.SendOrder sendOrder = new SendOrder();

                string orderNo = ibSendOrder.GetOrderNo(jsonentity.LabCode, DateTime.Now.ToString());
                if (ibSendOrder.Add(new SendOrder() { OrderNo = orderNo, CreateDate = jsonentity.CreateDate, CreateMan = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"), SampleNum = jsonentity.BarCodeFormNoList.Count, Note = jsonentity.Note, Status = 1, LabCode = jsonentity.LabCode }) > 0)
                {
                    if (jsonentity.BarCodeFormNoList != null)
                    {
                        foreach (var barCodeFormNo in jsonentity.BarCodeFormNoList)
                        {
                            if (ibbcf.UpdateOrderNoByBarCodeFormNo(barCodeFormNo, orderNo) > 0)
                            {
                                br.success = true;
                            }
                        }
                    }

                }
                else
                    br.success = false;
            }
            catch (Exception e)
            {

                br.success = false;
                br.ErrorInfo = e.ToString();
            }


            return br;

        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue ConFirmOrder(string OrderNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (ibSendOrder.IsExist(OrderNo))
                {
                    if (ibSendOrder.OrderConFrim(OrderNo) > 0)
                    {
                        brdv.success = true;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "当前状态是已确认";
                    }
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        /// <summary>
        /// 打印以后调用服务,记录打印状态
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public BaseResultDataValue ConFirmPrint(string OrderNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (ibSendOrder.IsExist(OrderNo))
                {
                    if (ibSendOrder.ConFrimPrint(OrderNo) > 0)
                    {
                        brdv.success = true;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "当前状态是已打印";
                    }
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        /// <summary>
        /// 订单打印中的配置存入Txt
        /// </summary>
        /// <param name="strTxt"></param>
        /// <returns></returns>
        public BaseResultDataValue SaveOrderText(string strTxt)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();


            string path = "";

            if (ConfigHelper.GetConfigString("OrderPrintPara") == "")
            {
                path = @"ui\order\OrderPrintPara.txt";
            }
            else
                path = ConfigHelper.GetConfigString("OrderPrintPara");
            string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + path;

            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }

            FileStream fs = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);
            sr.Write(strTxt);
            sr.Close();
            fs.Close();

            brdv.success = true;
            return brdv;
        }

        /// <summary>
        /// 读取订单打印中的配置
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetOrderText()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            string path = "";

            if (ConfigHelper.GetConfigString("OrderPrintPara") == "")
            {
                path = @"ui\order\OrderPrintPara.txt";
            }
            else
                path = ConfigHelper.GetConfigString("OrderPrintPara");
            string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + path;
            if (!File.Exists(FilePath))
            {
                brdv.success = false;
                brdv.ErrorInfo = "文件不存在";
            }
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            brdv.success = true;
            brdv.ResultDataValue = sr.ReadToEnd();
            return brdv;
        }

        //public 
        #endregion

        #region 个人检验统计
        public BaseResultDataValue GetStaticPersonTestItemPriceList(int page, int rows, string jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.StaticPersonTestItemPrice> EntityLis = new EntityListEasyUI<Model.StaticPersonTestItemPrice>();
            StaticPersonTestItemPrice sptip = JsonConvert.DeserializeObject<StaticPersonTestItemPrice>(jsonentity);
            try
            {
                List<StaticPersonTestItemPrice> list = new List<Model.StaticPersonTestItemPrice>();
                ZhiFang.Model.StaticPersonTestItemPrice model = sptip;
                model.OperdateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1).ToShortDateString();
                DataSet ds = rfb.GetStaticPersonTestItemPriceList(page, rows, model);
                DataSet dsa = rfb.GetStaticPersonTestItemPriceList(model);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = rfb.GetStaticPersonDataTableToList(ds.Tables[0]);
                }
                EntityLis.rows = list;
                EntityLis.total = dsa.Tables[0].Rows.Count;
                brdv.success = true;
                brdv.ErrorInfo = "请求成功!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!" + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }

            return brdv;
        }
        #endregion

        #region 工作量统计
        public BaseResultDataValue GetStaticRecOrgSamplePrice(string StartDate, string EndDate, int rows, int page, string labName, string TestItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.StaticRecOrgSamplePrice> EntityLis = new EntityListEasyUI<Model.StaticRecOrgSamplePrice>();
            try
            {
                List<StaticRecOrgSamplePrice> list = new List<Model.StaticRecOrgSamplePrice>();
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = StartDate;
                model.OperDateEnd = EndDate;
                model.ClientNo = labName;
                model.CName = TestItem;

                DataSet ds = rfb.GetStaticRecOrgSamplePrice(model, rows, page);

                list = rfb.DataTableToStaticRecOrgSamplePriceList(ds.Tables[0]);
                EntityLis.rows = list;
                //EntityLis.total = list.Count; 
                DataSet dsTotal = rfb.GetStaticRecOrgSamplePrice(model);
                EntityLis.total = dsTotal.Tables[0].Rows.Count;
                brdv.success = true;
                brdv.ErrorInfo = "请求成功!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!" + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }

            return brdv;
        }
        #endregion

        #region 条码统计
        public BaseResultDataValue GetBarcodePrice(string StartDate, string EndDate, int rows, int page, string labName, string DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.StaticRecOrgSamplePrice> EntityLis = new EntityListEasyUI<Model.StaticRecOrgSamplePrice>();
            try
            {
                List<StaticRecOrgSamplePrice> list = new List<Model.StaticRecOrgSamplePrice>();
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = StartDate;
                model.OperDateEnd = EndDate;
                model.ClientNo = labName;
                model.DateType = DateType;


                DataSet ds = rfb.GetBarcodePrice(model, rows, page);

                list = rfb.BarcodeDataTableToList(ds.Tables[0]);
                EntityLis.rows = list;
                //EntityLis.total = list.Count; 
                DataSet dsTotal = rfb.GetBarcodePrice(model);
                EntityLis.total = dsTotal.Tables[0].Rows.Count;
                brdv.success = true;
                brdv.ErrorInfo = "请求成功!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!" + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }

            return brdv;
        }
        #endregion

        #region 医院操作人员工作量统计
        public BaseResultDataValue GetOpertorWorkCount(string OperDateSart, string OperDateEnd, int rows, int page, string ClientNo, string Operator, string DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.StaticRecOrgSamplePrice> EntityLis = new EntityListEasyUI<Model.StaticRecOrgSamplePrice>();
            try
            {
                List<StaticRecOrgSamplePrice> list = new List<Model.StaticRecOrgSamplePrice>();
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = OperDateSart;
                model.OperDateEnd = OperDateEnd;
                model.ClientNo = ClientNo;
                model.Operator = Operator;
                model.DateType = DateType;
                DataSet ds = rfb.GetOpertorWorkCount(model, rows, page);

                list = rfb.OperatorWorkDataTableToList(ds.Tables[0]);
                EntityLis.rows = list;

                DataSet dsTotal = rfb.GetOpertorWorkCount(model, 0, 0);
                EntityLis.total = dsTotal.Tables[0].Rows.Count;
                brdv.success = true;
                brdv.ErrorInfo = "请求成功!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!" + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }

            return brdv;
        }
        #endregion

        #region 微信消费
        public BaseResultDataValue OSConsumerUserOrderForm(ConsumerUserOrderFormVO jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
            if (user == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.无法获取采样人信息！请重新登录！ZhiFangUser:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + ".IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            user.GetOrganizationsList();
            try
            {
                brdv = iboscuof.ConsumerUserOrderForm(jsonentity.PayCode, ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"), user.EmplID.ToString(), jsonentity.WeblisSourceOrgID, jsonentity.WeblisSourceOrgName, jsonentity.ConsumerAreaID);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }
        public BaseResultDataValue SaveOSConsumerUserOrderForm(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (jsonentity == null)
                {
                    brdv.ErrorInfo = "参数错误！";
                    brdv.success = false;
                    return brdv;
                }
                long NRequestFormNo;
                BaseResult brdvNF = this.NrequestFormAddOrUpdate_WeiXinConsumer(jsonentity, out NRequestFormNo);
                if (brdvNF.success)
                {
                    brdv = iboscuof.SaveOSUserConsumerForm(NRequestFormNo, jsonentity);
                    if (brdv.success)
                    {
                        brdv.ResultDataValue = "true";
                        brdv.success = true;
                    }
                    else
                    {
                        DeleteNRequestFromByNRequestFromNo(NRequestFormNo.ToString());
                        brdv.ResultDataValue = "false";
                        brdv.success = false;
                        brdv.ErrorInfo = "消费单保存失败！" + brdv.ErrorInfo;
                    }
                }
                else
                {
                    brdv.ResultDataValue = "false";
                    brdv.ErrorInfo = "新增申请单错误！" + brdvNF.ErrorInfo;
                    ZhiFang.Common.Log.Log.Error("SaveOSConsumerUserOrderForm.新增申请单错误！brdvNF.ErrorInfo:" + brdvNF.ErrorInfo);
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("SaveOSConsumerUserOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue UnConsumerUserOrderForm(ConsumerUserOrderFormVO jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("UnConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
            if (user == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("UnConsumerUserOrderForm.无法获取采样人信息！请重新登录！ZhiFangUser:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + ".IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            user.GetOrganizationsList();
            try
            {
                brdv = iboscuof.UnLockUserOrderForm(jsonentity.PayCode, ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"), user.EmplID.ToString(), jsonentity.WeblisSourceOrgID, jsonentity.WeblisSourceOrgName, jsonentity.ConsumerAreaID);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("UnConsumerUserOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue SearchUnConsumerUserOrderFormList(ConsumerUserOrderFormVO jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("SearchUnConsumerUserOrderFormList.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
            if (user == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("SearchUnConsumerUserOrderFormList.无法获取采样人信息！请重新登录！ZhiFangUser:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + ".IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            user.GetOrganizationsList();
            try
            {
                brdv = iboscuof.SearchUnConsumerUserOrderFormList(jsonentity.PayCode, ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"), user.EmplID.ToString(), jsonentity.WeblisSourceOrgID, jsonentity.WeblisSourceOrgName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("UnConsumerUserOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }

        public BaseResult NrequestFormAddOrUpdate_WeiXinConsumer(NrequestCombiItemBarCodeEntity jsonentity, out long NRequestFormNo)
        {
            BaseResult br = new BaseResult();
            NRequestFormNo = 0;
            try
            {
                #region 验证
                #region 身份验证
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    br.success = false;
                    br.ErrorInfo = "无法获取采样人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_WeiXinConsumer.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return br;
                }

                //先登录
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                if (user == null)
                    return null;

                if (user == null)
                {
                    br.success = false;
                    br.ErrorInfo = "无法获取采样人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_WeiXinConsumer.无法获取采样人信息！请重新登录！ZhiFangUser:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + ".IP:" + HttpContext.Current.Request.UserHostAddress);
                    return br;
                }
                #endregion

                #region 检查订单状态
                var checkpaycode = iboscuof.CheckPayCodeIsUseing(jsonentity.PayCode, ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"), user.EmplID.ToString(), jsonentity.NrequestForm.ClientNo, jsonentity.NrequestForm.ClientName);

                if (!checkpaycode.success)
                {
                    return checkpaycode;
                }
                #endregion

                bool b = jsonentity.BarCodeList.GroupBy(l => l.BarCode).Where(g => g.Count() > 1).Count() > 0;

                var barcodelist = jsonentity.BarCodeList.GroupBy(l => l.BarCode);
                foreach (var bg in barcodelist)
                {
                    if (bg.GroupBy(barcode => barcode.ColorName).Count() > 1)
                    {
                        br.success = false;
                        br.ErrorInfo = "输入的条码号有重复！BarCode:" + bg.Key;
                        ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate_WeiXinConsumer.Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "输入的条码号有重复！BarCode:" + bg.Key);
                        return br;
                    }
                }

                //if (b)
                //{
                //    br.success = false;
                //    br.ErrorInfo = "输入的条码号有重复！";
                //    return br;
                //}
                //数据库中条码为唯一值
                string repeatbarcodestr;
                if (ibbcf.IsExistBarCode(jsonentity.flag, jsonentity.BarCodeList, out repeatbarcodestr))
                {
                    br.success = false;
                    br.ErrorInfo = "条码号:'" + repeatbarcodestr + "'已存在！";
                    return br;
                }
                #endregion                

                Model.NRequestForm nrf_m = null;
                Model.NRequestItem nri_m = new Model.NRequestItem();
                Model.BarCodeForm bcf_m = new Model.BarCodeForm();

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion

                //申请单号
                long nRequestFormNo;

                if ((long)jsonentity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;
                NRequestFormNo = nRequestFormNo;

                #region 对象赋值

                #region 组合项目
                IBTestItem ibTest = BLLFactory<IBTestItem>.GetBLL();
                List<NRequestItem> nri_List = new List<NRequestItem>();
                foreach (UiCombiItem uicombiItem in jsonentity.CombiItems)
                {
                    //明细
                    foreach (UiCombiItemDetail uicombiItemDetail in uicombiItem.CombiItemDetailList)
                    {
                        nri_m = new NRequestItem();

                        ////假组套,组套中只包含自己
                        //if (uicombiItem.CombiItemDetailList.Count == 1 && uicombiItem.CombiItemNo == uicombiItemDetail.CombiItemDetailNo)
                        //{

                        //}
                        //else
                        nri_m.CombiItemNo = uicombiItem.CombiItemNo;//uicombiItemDetail.CombiItemDetailNo;
                        nri_m.ParItemNo = uicombiItemDetail.CombiItemDetailNo.ToString();
                        nri_m.NRequestFormNo = nRequestFormNo;

                        nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nri_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nri_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nri_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nri_List.Add(nri_m);
                    }
                }
                #endregion

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.NRequestFormNo = nRequestFormNo;
                //nrf_m.WebLisSourceOrgID = ClientNo;
                //nrf_m.WebLisSourceOrgName = txtClientNo;
                nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                nrf_m.Collecter = user.NameL + user.NameF;
                jsonentity.NrequestForm.CollecterID = user.EmplID.ToString();
                jsonentity.NrequestForm.CollecterName = user.Account;
                #endregion

                #region 条码

                List<Model.BarCodeForm> bcf_List = new List<BarCodeForm>();
                List<string> barcodestringlist = new List<string>();
                foreach (UiBarCode uibc in jsonentity.BarCodeList)
                {
                    bcf_m = new BarCodeForm();

                    bcf_m.BarCode = uibc.BarCode;
                    bcf_m.Color = uibc.ColorName;
                    barcodestringlist.Add(uibc.BarCode);
                    int sampleTypeNo;
                    int.TryParse(uibc.SampleType, out sampleTypeNo);
                    bcf_m.SampleTypeNo = sampleTypeNo;

                    bcf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                    bcf_m.WebLisSourceOrgId = nrf_m.ClientNo;
                    bcf_m.WebLisSourceOrgName = nrf_m.ClientName;
                    bcf_m.ClientNo = nrf_m.ClientNo;
                    bcf_m.ClientName = nrf_m.ClientName;
                    bcf_m.CollectDate = nrf_m.CollectDate;
                    bcf_m.CollectTime = nrf_m.CollectTime;
                    bcf_m.Collecter = user.NameL + user.NameF;
                    bcf_m.CollecterID = user.EmplID;
                    bool flag = false;
                    if (jsonentity.flag == "1")
                    {
                        bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();
                        //1条码对应多个子项目                       
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }

                    }
                    else if (jsonentity.flag == "0")
                    {
                        DataSet ds = ibbcf.GetList(new BarCodeForm() { BarCode = uibc.BarCode });
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            long barCodeFormNo;
                            long.TryParse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString(), out barCodeFormNo);
                            bcf_m.BarCodeFormNo = barCodeFormNo;

                        }
                        else
                            bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();

                        //1条码对应多个子项目                     
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }
                    }
                    if (bcf_m.ItemName != null && bcf_m.ItemName.Length > 0)
                    {
                        bcf_m.ItemName = bcf_m.ItemName.Remove(bcf_m.ItemName.LastIndexOf(','));
                    }
                    if (bcf_m.ItemNo != null && bcf_m.ItemNo.Length > 0)
                    {
                        bcf_m.ItemNo = bcf_m.ItemNo.Remove(bcf_m.ItemNo.LastIndexOf(','));
                    }
                    bcf_List.Add(bcf_m);
                }

                #endregion

                #endregion

                if (jsonentity.flag.Trim().ToString() == "0")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {
                        //先删除
                        rib.DeleteList_ByNRequestFormNo(nRequestFormNo);

                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                            nri.CombiItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString());
                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;
                        }

                    }

                    #endregion

                    #region BarCodeForm
                    if (bcf_List != null && bNRequestItemRusult == true)
                    {

                        foreach (BarCodeForm bcf in bcf_List)
                        {

                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = (user.NameL + user.NameF);
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            int i = 0;
                            if (ibbcf.Exists((long)bcf.BarCodeFormNo))
                            {
                                i = ibbcf.Update(bcf);
                            }
                            else
                                i = ibbcf.Add(bcf);

                            if (i > 0)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && nRequestFormNo > 0 && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        nrf_m.BarCode = (barcodestringlist.Count > 0) ? string.Join(",", barcodestringlist.ToArray()) : "";
                        if (rfb.Update(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;

                        }
                    }

                    #endregion
                }
                else if (jsonentity.flag.Trim().ToString() == "1")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {


                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                            int result = 0;
                            if (int.TryParse(tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString()), out result))
                            {
                                nri.CombiItemNo = result.ToString();
                            }

                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;

                        }

                    }

                    #endregion

                    #region BarCodeForm

                    if (bcf_List != null && bNRequestItemRusult == true)
                    {
                        //ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
                        foreach (BarCodeForm bcf in bcf_List)
                        {
                            //ZhiFang.Common.Log.Log.Debug("bcf.BarCode:" + bcf.BarCode);
                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = (user.NameL + user.NameF);
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            int i = ibbcf.Add(bcf);
                            if (i > 0)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.SerialNo = nRequestFormNo.ToString();
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        nrf_m.ZDY10 = jsonentity.PayCode;//暂定
                        nrf_m.BarCode = (barcodestringlist.Count > 0) ? string.Join(",", barcodestringlist.ToArray()) : "";
                        if (rfb.Add(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;
                        }
                    }

                    #endregion
                }

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    br.success = true;
                else
                    br.success = false;

            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
                //throw;
            }
            return br;
        }

        #endregion

        #region 微信消费采样模块
        public EntityListEasyUI<Model.NRequestFormResultOfConsume> GetNRequestFromListByByDetailsAndRBAC(int page, int rows, string fields, string jsonentity, string sort)
        {
            EntityListEasyUI<Model.NRequestFormResultOfConsume> result = new EntityListEasyUI<Model.NRequestFormResultOfConsume>();

            List<Model.NRequestFormResultOfConsume> nrequestformlist = new List<Model.NRequestFormResultOfConsume>();

            try
            {
                NRequestForm nrequestform = JsonConvert.DeserializeObject<NRequestForm>(jsonentity);
                NRequestForm nrf_m = new NRequestForm();
                if (nrequestform.ClientNo != null && nrequestform.ClientNo.Trim() != "")
                {
                    nrf_m.ClientNo = nrequestform.ClientNo;
                }
                if (nrequestform.WebLisSourceOrgID != null && nrequestform.WebLisSourceOrgID.Trim() != "")
                {
                    nrf_m.WebLisSourceOrgID = nrequestform.WebLisSourceOrgID;
                }
                if (nrequestform.DoctorName != null && nrequestform.DoctorName.Trim() != "")
                {
                    nrf_m.DoctorName = nrequestform.DoctorName;
                }
                if (nrequestform.CName != null && nrequestform.CName.Trim() != "")
                {
                    nrf_m.CName = nrequestform.CName;
                }
                if (nrequestform.PatNo != null && nrequestform.PatNo.Trim() != "")
                {
                    nrf_m.PatNo = nrequestform.PatNo;
                }
                if (nrequestform.OperDateStart != null && nrequestform.OperDateStart != "")
                {
                    nrf_m.OperDateStart = nrequestform.OperDateStart;
                }
                if (nrequestform.OperDateEnd != null && nrequestform.OperDateEnd.Trim() != "")
                {
                    if (nrequestform.OperDateEnd.Trim().Length > 11)
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.OperDateEnd = nrequestform.OperDateEnd.Trim() + " 23:59:59";
                    }
                }
                if (nrequestform.CollectDateStart != null && nrequestform.CollectDateStart.Trim() != "")
                {
                    nrf_m.CollectDateStart = nrequestform.CollectDateStart.Trim();
                }
                if (nrequestform.CollectDateEnd != null && nrequestform.CollectDateEnd.Trim() != "")
                {
                    if (nrequestform.CollectDateEnd.Trim().Length > 11)
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim();
                    }
                    else
                    {
                        nrf_m.CollectDateEnd = nrequestform.CollectDateEnd.Trim() + " 23:59:59";
                    }
                }
                if (nrequestform.BarCode != null && nrequestform.BarCode.Trim() != "")
                {
                    nrf_m.BarCode = nrequestform.BarCode;
                }
                if (nrequestform.OldSerialNo != null && nrequestform.OldSerialNo.Trim() != "")
                {
                    nrf_m.OldSerialNo = nrequestform.OldSerialNo;
                }
                if (nrequestform.ZDY10 != null && nrequestform.ZDY10.Trim() != "")
                {
                    nrf_m.ZDY10 = nrequestform.ZDY10;
                }
                nrf_m.IsOnlyNoBar = nrequestform.IsOnlyNoBar;
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(UserId);
                user.GetPostList();
                if (user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                {

                }
                else
                {
                    if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                    {
                        DataSet ds = user.GetClientListByPost("", -1); ;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            nrf_m.ClientList += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        }
                        nrf_m.ClientList = nrf_m.ClientList.Remove(nrf_m.ClientList.Length - 1);
                    }
                    else
                    {
                        nrf_m.ClientList += " -1 ";
                    }
                }
                DataTable dt = new DataTable();
                int iCount, intPageCount;
                if (page > 0)
                {
                    page = page - 1;
                }
                dt = rfb.GetNRequstFormListByDetailsAndPage(nrf_m, page, rows, out intPageCount, out iCount);
                if (dt != null)
                {
                    nrequestformlist = NRFDataTableConvertByDetailsList(dt);
                    result.rows = nrequestformlist;
                    result.total = iCount;
                }
                else
                {
                    result.rows = null;
                    result.total = 0;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                result.rows = nrequestformlist;
                result.total = 0;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 项目过滤
        /// </summary>
        /// <param name="SuperGroupNo">检验大组</param>
        /// <param name="ItemKey">联想输入</param>
        ///<param name="rows">每页行数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="labcode">机构</param>
        /// <returns></returns>
        public BaseResultDataValue GetTestItem_BarCodePrint(string supergroupno, string itemkey, int rows, int pageindex, string labcode)
        {

            BaseResultDataValue resultObj = new BaseResultDataValue();
            //EntityList<ApplyInputItemEntity> testItemList = new EntityList<ApplyInputItemEntity>();
            IBLL.Common.BaseDictionary.IBLab_TestItem LabTestItem = BLLFactory<IBLab_TestItem>.GetBLL();
            EntityListEasyUI<ApplyInputItemEntity> testItemList = new EntityListEasyUI<ApplyInputItemEntity>();
            DataSet ds;
            int AllItemCount = 0;
            try
            {
                #region 如果医疗机构编码不存在 按照中心项目字典表显示项目
                switch ((TestItemSuperGroupClass)Enum.Parse(typeof(TestItemSuperGroupClass), supergroupno.ToUpper()))
                {
                    case TestItemSuperGroupClass.ALL:

                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 });
                        }

                        break;

                    case TestItemSuperGroupClass.COMBI:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 });

                        }
                        break;
                    //case TestItemSuperGroupClass.OFTEN:
                    //    if (labcode != "")
                    //    {
                    //        ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                    //        AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                    //    }
                    //    else
                    //    {
                    //        ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                    //        AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                    //    }
                    //    break;
                    case TestItemSuperGroupClass.CHARGE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    case TestItemSuperGroupClass.COMBIITEMPROFILE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 });
                        }
                        break;
                    case TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    default:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });

                        }
                        break;

                }
                #endregion


                if (ds != null)
                {
                    if (labcode != null && labcode != "")
                    {
                        //testItemList.list = LabTestItem.ItemEntityDataTableToList(ds.Tables[0]);
                        //testItemList.count = testItemList.list.Count;
                        testItemList.rows = LabTestItem.ItemEntityDataTableToList(ds.Tables[0], labcode);
                        testItemList.total = AllItemCount;//testItemList.rows.Count;
                        resultObj.success = true;
                        resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(testItemList);
                    }
                    else
                    {
                        testItemList.rows = LabTestItem.ItemEntityDataTableToList(ds.Tables[0]);
                        testItemList.total = AllItemCount;//testItemList.rows.Count;
                        resultObj.success = true;
                        resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(testItemList);
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                resultObj.ErrorInfo = e.ToString();
                resultObj.ResultDataValue = "";
                resultObj.success = false;
            }
            return resultObj;
        }

        public BaseResultBool SendBarCodeFromByBarCodeList(string BarCodeList)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (string.IsNullOrWhiteSpace(BarCodeList))
                {
                    brb.success = false;
                    brb.ErrorInfo = "参数错误！";
                    ZhiFang.Common.Log.Log.Error("SendBarCodeFromByBarCodeList.参数错误！BarCodeList为空！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brb.success = false;
                    brb.ErrorInfo = "无法送检人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("SendBarCodeFromByBarCodeList.无送检人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserID");
                string EmployeeName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("EmployeeName");

                brb.success = rfb.SendBarCodeFromByBarCodeList(BarCodeList, UserId, EmployeeName);
                return brb;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("SendBarCodeFromByBarCodeList.异常：" + e.ToString());
                brb.ErrorInfo = "程序异常！";
                brb.success = false;
                return brb;
            }
        }

        public BaseResultBool DeliveryBarCodeFromByBarCodeList(string BarCodeList, bool Flag, string Reason)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (string.IsNullOrWhiteSpace(BarCodeList))
                {
                    brb.success = false;
                    brb.ErrorInfo = "参数错误！";
                    ZhiFang.Common.Log.Log.Error("DeliveryBarCodeFromByBarCodeList.参数错误！BarCodeList为空！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brb.success = false;
                    brb.ErrorInfo = "无法获取物流人员信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("DeliveryBarCodeFromByBarCodeList.无法获取物流人员信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserID");
                string EmployeeName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("EmployeeName");
                brb = rfb.DeliveryBarCodeFromByBarCodeList(BarCodeList, UserId, EmployeeName, Flag, Reason);
                return brb;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("DeliveryBarCodeFromByBarCodeList.异常：" + e.ToString());
                brb.ErrorInfo = "程序异常！";
                brb.success = false;
                return brb;
            }
        }

        public BaseResultBool ReceiveBarCodeFromByBarCodeList(string BarCodeList, bool Flag, string Reason)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (string.IsNullOrWhiteSpace(BarCodeList))
                {
                    brb.success = false;
                    brb.ErrorInfo = "参数错误！";
                    ZhiFang.Common.Log.Log.Error("ReceiveBarCodeFromByBarCodeList.参数错误！BarCodeList为空！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brb.success = false;
                    brb.ErrorInfo = "无法获取签收人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("ReceiveBarCodeFromByBarCodeList.无法获取签收人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brb;
                }
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserID");
                string EmployeeName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("EmployeeName");
                string Account = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                brb = rfb.ReceiveBarCodeFromByBarCodeList(BarCodeList, UserId, EmployeeName, Flag, Reason,Account);
                return brb;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ReceiveBarCodeFromByBarCodeList.异常：" + e.ToString());
                brb.ErrorInfo = "程序异常！";
                brb.success = false;
                return brb;
            }
        }
    }
}
