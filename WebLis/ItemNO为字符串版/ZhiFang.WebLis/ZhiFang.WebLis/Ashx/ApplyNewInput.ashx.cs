using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Web.Services;
using ZhiFang.Common.Public;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// ApplyNewInput 的摘要说明
    /// </summary>

    [WebService(Namespace = "ZhiFang.WebLis.Ashx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ApplyNewInput : IHttpHandler
    {
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        ZhiFang.Model.NRequestForm nrf_m = new Model.NRequestForm();
        ZhiFang.Model.NRequetFormList nrfList_m = new Model.NRequetFormList();
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            try
            {
                string type = context.Request.QueryString["type"];
                if (type == "delete")
                {
                    Model.BarCodeForm BarCodeForm = new Model.BarCodeForm();
                    Model.NRequestItem NRequestItem = new Model.NRequestItem();
                    Model.NRequestForm NRequestForm = new Model.NRequestForm();
                    string barcode = context.Request.QueryString["row"];
                    bool delCount = false;
                    BarCodeForm.BarCode = barcode;
                    DataSet ds = ibbcf.GetList(BarCodeForm);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string WebLisFlag = ds.Tables[0].Rows[0]["WebLisFlag"].ToString();
                        if (WebLisFlag != "5")
                        {
                            long BarCodeFormNo = Convert.ToInt64(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString());
                            NRequestItem.BarCodeFormNo = BarCodeFormNo;
                            DataSet dsNitem = rib.GetList(NRequestItem);
                            string NrequestItemNoList = "";
                            for (int i = 0; i < dsNitem.Tables[0].Rows.Count; i++)
                            {
                                NrequestItemNoList += "'" + dsNitem.Tables[0].Rows[i]["NRequestItemNo"].ToString().Trim() + "',";
                            }
                            if (NrequestItemNoList != "")
                            {
                                NrequestItemNoList = NrequestItemNoList.Remove(NrequestItemNoList.Length - 1);
                            }
                            long NRequestFormNo = long.Parse(dsNitem.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                            NRequestForm.NRequestFormNo = NRequestFormNo;
                            int count = ibbcf.Delete(BarCodeFormNo);
                            count = rfb.Delete(NRequestFormNo);
                            bool result = rib.DeleteList(NrequestItemNoList);
                            if (count == 1 && result == true)
                            {
                                context.Response.Write(true);
                            }
                        }
                        else
                        {
                            //text.Response.Write("<script type='text/javascript'>alert('该申请单信息已被签收，不能删除！');</script>");
                            context.Response.Write("err");
                        }
                    }
                }
                else if (type == "ColorNo")
                {
                    string itemno = context.Request.QueryString["itemno"];
                    string labcode = context.Request.QueryString["labcode"];
                    //GetTestItemColorTotal(string itemno, string labcode)
                    //采样管颜色条码
                    string colorarry = "";
                    IBLab_TestItem blti = BLLFactory<IBLab_TestItem>.GetBLL();
                    //if (!string.IsNullOrEmpty(itemno))
                    //{
                    //    context.Response.Write("err");
                    //    return;
                    //}

                    Dictionary<string, string> color = blti.GetColor(itemno, labcode);
                    foreach (var l in color)
                    {
                        colorarry += l.Key + "=" + l.Value.ToString() + "|";
                    }
                    if (colorarry != "")
                    {
                        colorarry.Remove(colorarry.LastIndexOf('|'));
                        ZhiFang.Common.Log.Log.Info("colorarry:" + colorarry.Remove(colorarry.LastIndexOf('|')));
                        context.Response.Write(colorarry);
                        return;
                    }
                    else
                    {
                        colorarry = null;
                        context.Response.Write("err");
                        return;
                    }

                }
                else if (type == "SampleLis")
                {
                    //根据检验大组获取检验类型
                    // public string GetSampleList(string CubeColor)
                    string CubeColor = Convert.ToString(System.Web.HttpUtility.UrlDecode(context.Request.QueryString["CubeColor"]));

                    IBLL.Common.BaseDictionary.IBSamplingGroup ibsg = BLLFactory<IBSamplingGroup>.GetBLL();
                    IBLL.Common.BaseDictionary.IBSampleType ibst = BLLFactory<IBSampleType>.GetBLL();
                    Model.SamplingGroup SamplingGroup = new Model.SamplingGroup();
                    Model.SampleType SampleType = new Model.SampleType();
                    SamplingGroup.CubeColor = CubeColor;
                    DataSet ds = ibsg.GetList(SamplingGroup);
                    string SampleTypeCName = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SampleType.SampleTypeNo = Convert.ToInt32(ds.Tables[0].Rows[i]["SampleTypeNo"].ToString().Trim());
                        DataSet dsSample = ibst.GetList(SampleType);
                        SampleTypeCName = dsSample.Tables[0].Rows[0]["CName"].ToString();
                    }
                    context.Response.Write(SampleTypeCName);

                }
                else if (type == "itemClientno")
                {
                    //GetGroupSubItemListByClientNo(string ItemNo, string labcode)
                    string ItemNo = context.Request.QueryString["itemno"];
                    string labcode = context.Request.QueryString["labcode"];
                    string error;
                    IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
                    IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
                    DataSet dsitem = groupitem.GetGroupItemList(ItemNo.Trim(), labcode);
                    string tmpSubItemList = groupitem.GetSubItemList_No_CName(ItemNo.Trim(), labcode);
                    context.Response.Write(tmpSubItemList.Substring(0, tmpSubItemList.Length - 1));

                }
                else if (type == "CombiItem")
                {
                    #region  CombiItem
                    //  GetCombiItemListByClientNo(string CombiItemNo, string labcode)
                    string CombiItemNo = context.Request.QueryString["CombiItemNo"];
                    string labcode = context.Request.QueryString["labcode"];
                    string error;
                    IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
                    IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
                    DataSet dsitem = groupitem.GetGroupItemList(CombiItemNo.Trim(), labcode);
                    string tmplistNo = "";
                    if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                    {
                        for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                        {
                            string flagc = "";
                            string flag = "";
                            if (dsitem.Tables[0].Rows[ii]["isCombiItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() == "1")
                            {
                                flagc = "#66FF33";
                                flag = "1";
                            }
                            else
                            {
                                if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                                {
                                    //flag = "#996633";
                                }
                            }
                            if (dsitem.Tables[0].Rows[ii]["IsProfile"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IsProfile"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IsProfile"].ToString().Trim() == "1")
                            {
                                flagc = "Blue";
                                flag = "2";
                            }
                            tmplistNo += dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "," + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";") + "," + flagc + "," + flag + "|";
                        }
                        tmplistNo = tmplistNo.Substring(0, tmplistNo.Length - 1);

                        context.Response.Write(tmplistNo);
                    }
                    #endregion
                }
                else if (type == "fquyu")
                {
                    // 返回区域编号
                    //GetClientEleArea(string clientno)
                    string clientno = context.Request.QueryString["clientno"];
                    Model.CLIENTELE CLIENTELE = new Model.CLIENTELE();
                    IBLL.Common.BaseDictionary.IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
                    CLIENTELE = ibc.GetModel(Convert.ToInt32(clientno));
                    ZhiFang.Common.Log.Log.Info("输出区域:" + CLIENTELE.AreaID.ToString());
                    context.Response.Write(CLIENTELE.AreaID.ToString());
                }
                else if (type == "fdanwei")
                {  //返回送检区域总单位
                    //GetClientEleClient(string AreaID)
                    string AreaID = context.Request.QueryString["AreaID"];
                    Model.ClientEleArea modelCEA = new Model.ClientEleArea();
                    IBLL.Common.BaseDictionary.IBClientEleArea CEA = BLLFactory<IBClientEleArea>.GetBLL();
                    modelCEA = CEA.GetModel(Convert.ToInt32(AreaID));
                    ZhiFang.Common.Log.Log.Info("输出医疗机构:" + modelCEA.ClientNo.ToString());
                    context.Response.Write(modelCEA.ClientNo.ToString());
                }
                else if (type == "save1")
                {
                    #region save1

                    //  SaveTestByClientNo1(string txtApplyNO, string txtClientNo, string BarCodeInputFlag, string txtBarCode, string txtName, string txtAge, string SampleType, string txtCollecter, string hiddenClient, string hiddenSampleType, string SelectGenderType, string GenderName, string txtBingLiNO, string SelectAgeUnit, string AgeUnitName, string SelectFolkType, string FolkName, string hiddenDistrict, string DistrictName, string hiddenWardNo, string WardName, string txtBed, string hiddenDepartment, string DeptName, string hiddenDoctorNo, string DoctorName, string txtResult, string txtCharge, string Selectjztype, string ClinicTypeName, string DDLTestType, string TestTypeName, string CollectTime, string OperTime, string hiddenFlag, bool cacheflag, string selectitem, string SelectItemValue, string SelectCombiItemNo, string SelectCombiItemCName, string SelectCombiItemValue, string SelectAllTestItem)
                    string txtApplyNO = context.Request.QueryString["txtApplyNO"];
                    string txtClientNo = context.Request.QueryString["txtClientNo"];
                    string BarCodeInputFlag = context.Request.QueryString["BarCodeInputFlag"];
                    string txtBarCode = context.Request.QueryString["barcodeo"];
                    string txtName = context.Request.QueryString["txtName"];
                    string txtAge = context.Request.QueryString["txtAge"];
                    string SampleType = context.Request.QueryString["SampleType"];
                    string txtCollecter = context.Request.QueryString["txtCollecter"];
                    string hiddenClient = context.Request.QueryString["hiddenClient"];
                    string hiddenSampleType = context.Request.QueryString["hiddenSampleType"];
                    string SelectGenderType = context.Request.QueryString["SelectGenderType"];
                    string GenderName = context.Request.QueryString["GenderName"];
                    string txtBingLiNO = context.Request.QueryString["txtBingLiNO"];
                    string SelectAgeUnit = context.Request.QueryString["SelectAgeUnit"];
                    string AgeUnitName = context.Request.QueryString["AgeUnitName"];
                    string SelectFolkType = context.Request.QueryString["SelectFolkType"];
                    string FolkName = context.Request.QueryString["FolkName"];
                    string hiddenDistrict = context.Request.QueryString["hiddenDistrict"];
                    string DistrictName = context.Request.QueryString["DistrictName"];
                    string hiddenWardNo = context.Request.QueryString["hiddenWardNo"];
                    string WardName = context.Request.QueryString["WardName"];
                    string txtBed = context.Request.QueryString["txtBed"];
                    string hiddenDepartment = context.Request.QueryString["hiddenDepartment"];
                    string DeptName = context.Request.QueryString["DeptName"];
                    string hiddenDoctorNo = context.Request.QueryString["hiddenDoctorNo"];
                    string DoctorName = context.Request.QueryString["DoctorName"];
                    string txtResult = context.Request.QueryString["txtResult"];
                    string txtCharge = context.Request.QueryString["txtCharge"];
                    string Selectjztype = context.Request.QueryString["Selectjztype"];
                    string ClinicTypeName = context.Request.QueryString["ClinicTypeName"];
                    string DDLTestType = context.Request.QueryString["DDLTestType"];
                    string TestTypeName = context.Request.QueryString["TestTypeName"];
                    string CollectTime = context.Request.QueryString["CollectTime"];
                    string OperTime = context.Request.QueryString["OperTime"];
                    string hiddenFlag = context.Request.QueryString["hiddenFlag"];
                    bool cacheflag = Convert.ToBoolean(context.Request.QueryString["cacheflag"]);
                    string selectitem = context.Request.QueryString["selectitem"];
                    string SelectItemValue = context.Request.QueryString["SelectItemValue"];
                    string SelectCombiItemNo = context.Request.QueryString["SelectCombiItemNo"];
                    string SelectCombiItemCName = context.Request.QueryString["SelectCombiItemCName"];
                    string SelectCombiItemValue = context.Request.QueryString["SelectCombiItemValue"];
                    string SelectAllTestItem = context.Request.QueryString["SelectAllTestItem"];
                    try
                    {
                        IBNRequestForm nrf = ZhiFang.BLLFactory.BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
                        IBNRequestItem nri = ZhiFang.BLLFactory.BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
                        IBBarCodeForm bcf = ZhiFang.BLLFactory.BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
                        IBTestItem testitem = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
                        IBLab_TestItem labtestitem = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
                        IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
                        if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") != string.Empty)
                        {
                            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                            ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId");
                            user.GetOrganizationsList();
                            Model.NRequestForm nrf_m = new Model.NRequestForm();
                            Model.NRequestItem nri_m = new Model.NRequestItem();
                            Model.BarCodeForm bcf_m = new Model.BarCodeForm();
                            try
                            {
                                #region 必填项
                                if (txtApplyNO.Trim() == "")
                                {
                                    context.Response.Write("申请号不能为空！@txtApplyNO");
                                    return;
                                }
                                if (txtClientNo.Trim() == "")
                                {
                                    context.Response.Write("送检单位不能为空！@txtClientNo");
                                    return;
                                }
                                if (BarCodeInputFlag.Trim() == "2")
                                {
                                    //if (txtBarCode.Value.Trim() == "")
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "alert('送检单位不能为空！');document.getElementById('txtClientNo').focus();", true);
                                    //    return false;
                                    //}
                                }
                                else
                                {
                                    if (txtBarCode.Trim() == "")
                                    {
                                        context.Response.Write("条码号不能为空！@txtBarCode");
                                        return;
                                    }
                                }
                                if (txtName.Trim() == "")
                                {
                                    context.Response.Write("姓名不能为空！@txtName");
                                    return;
                                }
                                if (txtAge.Trim() == "")
                                {
                                    context.Response.Write("年龄不能为空！@txtAge");
                                    return;
                                }
                                if (SampleType.Trim() == "")
                                {
                                    context.Response.Write("样本类型不能为空！@SampleType");
                                    return;
                                }
                                if (txtCollecter.Trim() == "")
                                {
                                    context.Response.Write("采样人不能为空！@txtCollecter");
                                    return;
                                }
                                if (hiddenClient.Trim() == "")
                                {
                                    context.Response.Write("送检单位不能为空！@txtClientNo");
                                    return;
                                }
                                if (hiddenSampleType.Trim() == "")
                                {
                                    context.Response.Write("样本类型不能为空！@SampleType");
                                    return;
                                }
                                if (selectitem.Trim() == "")
                                {
                                    context.Response.Write("必须选择项目！");
                                    return;
                                }
                                #endregion
                            }
                            catch (Exception ebt)
                            {
                                ZhiFang.Common.Log.Log.Debug("必填项异常！详细信息：" + ebt.ToString());
                                throw ebt;
                            }
                            try
                            {
                                #region 对象赋值
                                string SerialNo = txtApplyNO.Replace(",", ",,").Replace("‘", "’‘");     //申请单号
                                if (SerialNo == "")
                                {
                                    context.Response.Write("SerialNo为空！@txtApplyNO");
                                    return;
                                }
                                else
                                {
                                    nrf_m.SerialNo = SerialNo;
                                }
                                string SampleTypeNo = hiddenSampleType.Trim(); //样本类型
                                if (SampleTypeNo != "")
                                {
                                    try { nrf_m.SampleTypeNo = int.Parse(SampleTypeNo); }
                                    catch { }
                                    try { nrf_m.SampleTypeName = SampleType; }
                                    catch { }
                                    //ViewState["SampleTypeName"] = SampleType.Trim();
                                }
                                string PatNo = txtBingLiNO.Replace(",", ",,");       //病历号
                                if (PatNo != "")
                                {
                                    try { nrf_m.PatNo = PatNo; }
                                    catch { }
                                }
                                string CName = txtName.Replace(",", ",,");           //姓名
                                if (CName != "")
                                {
                                    try { nrf_m.CName = CName; }
                                    catch { }
                                    //ViewState["CName"] = CName;
                                }
                                string GenderNo = SelectGenderType;//性别
                                if (GenderNo != "")
                                {
                                    try { nrf_m.GenderNo = int.Parse(GenderNo); nrf_m.GenderName = GenderName; }
                                    catch { }
                                    //ViewState["Gender"] = SelectGenderType.Items[SelectGenderType.SelectedIndex].Text.Trim();
                                }
                                string Age = txtAge.Replace(",", ",,");              //年龄
                                if (Age != "")
                                {
                                    try { nrf_m.Age = int.Parse(Age); }
                                    catch { }
                                    //ViewState["Age"] = Age;
                                }
                                string AgeUnitNo = SelectAgeUnit;                 //年龄单位
                                if (AgeUnitNo != "")
                                {
                                    try { nrf_m.AgeUnitNo = int.Parse(AgeUnitNo); nrf_m.AgeUnitName = AgeUnitName; }
                                    catch { }
                                    //ViewState["AgeUnit"] = SelectAgeUnit.Items[SelectAgeUnit.SelectedIndex].Text.Trim();
                                }
                                string FolkNo = SelectFolkType;   //民族
                                if (FolkNo != "")
                                {
                                    try { nrf_m.FolkNo = int.Parse(FolkNo); nrf_m.FolkName = FolkName; }
                                    catch { }
                                }
                                string DistrictNo = hiddenDistrict;//病区
                                if (DistrictNo != "")
                                {
                                    try { nrf_m.DistrictNo = int.Parse(DistrictNo); nrf_m.DistrictName = DistrictName; }
                                    catch { }
                                }

                                string WardNo = hiddenWardNo;   //病房
                                if (WardNo != "")
                                {
                                    try { nrf_m.WardNo = int.Parse(WardNo); nrf_m.WardName = WardName; }
                                    catch { }
                                }
                                string Bed = txtBed;              //床位
                                if (Bed != "")
                                {
                                    nrf_m.Bed = Bed;
                                }
                                string DeptNo = hiddenDepartment; //科室
                                if (DeptNo != "")
                                {
                                    try { nrf_m.DeptNo = int.Parse(DeptNo); nrf_m.DeptName = DeptName; }
                                    catch { }
                                    //ViewState["Department"] = this.SelectDepartment.Value.ToString();
                                }
                                string Doctor = hiddenDoctorNo;     //医生
                                if (Doctor != "")
                                {
                                    try { nrf_m.Doctor = int.Parse(Doctor); nrf_m.DoctorName = DoctorName; }
                                    catch { }
                                    //Values += "'" + this.SelectDoctor.Value.Trim() + "',";
                                }
                                string Diag = txtResult.Replace(",", ",,");          //诊断结果
                                if (Diag != "")
                                {
                                    nrf_m.Diag = Diag;
                                }
                                string Charge = txtCharge;        //收费  == 费用 ???
                                if (Charge != "")
                                {
                                    try { nrf_m.Charge = decimal.Parse(Charge); }
                                    catch { }
                                }
                                string Operator = txtCollecter;                       //采样人//////////////////登陆者？？？？
                                if (Operator != "")
                                {
                                    try
                                    {
                                        nrf_m.Operator = Operator;
                                        nrf_m.CollecterName = Operator;
                                        nrf_m.Collecter = user.NameL + user.NameF;

                                        bcf_m.Collecter = user.NameL + user.NameF;
                                        bcf_m.CollecterID = user.EmplID;
                                    }
                                    catch { }
                                }
                                string jztype = Selectjztype;            //就诊类型
                                if (jztype != "")
                                {
                                    try
                                    {
                                        nrf_m.jztype = int.Parse(jztype);
                                        nrf_m.ClinicTypeName = ClinicTypeName;
                                    }
                                    catch { }
                                }
                                string ClientNo = hiddenClient;        //送检单位
                                if (ClientNo != "")
                                {
                                    try
                                    {
                                        nrf_m.WebLisSourceOrgID = ClientNo;
                                        nrf_m.WebLisSourceOrgName = txtClientNo;
                                        nrf_m.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                        nrf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();

                                        ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId:user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim() =" + user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim());

                                        nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                        nri_m.WebLisSourceOrgID = ClientNo;
                                        nri_m.WebLisSourceOrgName = txtClientNo;

                                        bcf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                        bcf_m.WebLisSourceOrgId = ClientNo;
                                        bcf_m.WebLisSourceOrgName = txtClientNo;
                                        bcf_m.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                                        bcf_m.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                                    }
                                    catch
                                    {

                                    }
                                    //ViewState["ClientName"] = txtClientNo.Value.Trim();
                                }
                                string strTestTypeNo = DDLTestType;//检验类型
                                if (strTestTypeNo != "")
                                {
                                    try { nrf_m.TestTypeNo = int.Parse(strTestTypeNo); nrf_m.TestTypeName = TestTypeName; }
                                    catch { }
                                }
                                //string CollectTime = txtCollectTime.Value.Trim() + " " + this.txtCollectTimeH.Value.Trim() + ":" + this.txtCollectTimeM.Value.Trim() + ":00";             //采样日期  采样时间
                                if (CollectTime != "" && CollectTime != ":00")
                                {
                                    DateTime cTime;
                                    try
                                    {
                                        cTime = DateTime.Parse(CollectTime);
                                    }
                                    catch
                                    {
                                        context.Response.Write("时间格式有误！@txtCollectTime");
                                        return;
                                    }
                                    //nrf_m.CollectDate = cTime.ToString("yyyy-MM-dd");
                                    //nrf_m.CollectTime = cTime.ToString("HH:mm:ss");

                                    //bcf_m.CollectDate = cTime.ToString("yyyy-MM-dd");
                                    //bcf_m.CollectTime = cTime.ToString("HH:mm:ss");

                                    nrf_m.CollectDate = cTime;
                                    nrf_m.CollectTime = cTime;

                                    bcf_m.CollectDate = cTime;
                                    bcf_m.CollectTime = cTime;

                                    //ViewState["Time"] = cTime.ToString("yyyy-MM-dd") + " " + cTime.ToString("HH:mm");
                                }
                                //string OperTime = txtoTime.Value.Trim() + " " + this.txtoTimeH.Value.Trim() + ":" + this.txtoTimeM.Value.Trim() + ":00";             //采样日期  采样时间
                                if (OperTime != "" && OperTime != ":00")
                                {
                                    DateTime oTime;
                                    try
                                    {
                                        oTime = DateTime.Parse(OperTime);
                                        //nrf_m.OperDate = oTime.ToString("yyyy-MM-dd");
                                        //nrf_m.OperTime = oTime.ToString("HH:mm:ss");

                                        nrf_m.OperDate = oTime;
                                        nrf_m.OperTime = oTime;
                                    }
                                    catch
                                    {

                                    }
                                }
                                #endregion
                            }
                            catch (Exception ebt)
                            {
                                ZhiFang.Common.Log.Log.Debug("对象赋值项异常！详细信息：" + ebt.ToString());
                                throw ebt;
                            }
                            //插入新纪录
                            string NRequestFormNotmp = nrf.GetNCode(100);
                            string NRequestFormNo = "";
                            DataSet dSet = new DataSet();
                            string BarCodeFormNo = bcf.GetNewBarCodeFormNo(int.Parse(hiddenClient));
                            string NewBarCode = "";
                            Model.NRequestForm nrf_m1 = new Model.NRequestForm();
                            nrf_m1.SerialNo = nrf_m.SerialNo;
                            DataSet dt = nrf.GetList(nrf_m1);
                            if (dt.Tables[0].Rows.Count == 0)
                            {
                                nrf_m.NRequestFormNo = Convert.ToInt64(NRequestFormNotmp);

                            }
                            else
                            {
                                nrf_m.NRequestFormNo = Convert.ToInt64(dt.Tables[0].Rows[0]["NRequestFormNo"]);
                            }
                            nri_m.BarCodeFormNo = Convert.ToInt64(BarCodeFormNo);
                            nri_m.NRequestFormNo = nrf_m.NRequestFormNo;
                            if (hiddenFlag == "0" || hiddenFlag == "")
                            {
                                try
                                {

                                    //hiddenSelectTest.Value;  用户选择的申请单
                                    string[] FormList = selectitem.Split(';');
                                    for (int Formi = 0; Formi < FormList.Length; Formi++)
                                    {
                                        if (FormList[Formi] == "")
                                        {
                                            continue;
                                        }
                                        if (FormList[Formi].Split(':')[1].ToString().Trim() == "")
                                        {
                                            //nri_m.ParItemNo = int.Parse(tic.GetCenterCode(hiddenClient, FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim()));
                                            nri_m.ParItemNo = tic.GetCenterCode(hiddenClient, FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim());
                                            if (nri.Add(nri_m) <= 0)
                                            {
                                                context.Response.Write("写入NRequestItem表错误！");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            nri_m.CombiItemNo = tic.GetCenterCode(hiddenClient, FormList[Formi].Split(':')[1].ToString().Trim());
                                            //nri_m.ParItemNo = int.Parse(tic.GetCenterCode(txtClientNo, FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim()));
                                            //string[] tmpsub = FormList[Formi].Split(':')[0].ToString().Trim().Split(',');
                                            //if (tmpsub.Length >= 4 && (tmpsub[3].Trim() == "1" || tmpsub[3].Trim() == "2"))
                                            //{
                                            //    string[] tmpsubitemlist = testitem.GetSubItemList_No(tmpsub[0].Trim()).Split('|');
                                            //    for (int i = 0; i < tmpsubitemlist.Length; i++)
                                            //    {
                                            //        nri_m.ParItemNo = int.Parse(tmpsub[i].ToString().Trim());
                                            //        if (nri.Add(nri_m) <= 0)
                                            //        {
                                            //            return "写入NRequestItem表错误！";
                                            //        }
                                            //    }
                                            //}
                                            //else
                                            //{
                                            if (nri_m.CombiItemNo != null)
                                            {
                                                //nri_m.ParItemNo = int.Parse(FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim());
                                                nri_m.ParItemNo = FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim();
                                                DataSet tmpds = nri.GetList(new Model.NRequestItem { NRequestFormNo = nri_m.NRequestFormNo, ParItemNo = nri_m.ParItemNo, CombiItemNo = nri_m.CombiItemNo });
                                                if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
                                                {

                                                }
                                                else
                                                {
                                                    if (nri.Add(nri_m) <= 0)
                                                    {
                                                        context.Response.Write("写入NRequestItem表错误！");
                                                        return;
                                                    }
                                                }
                                            }


                                            //}
                                        }
                                    }
                                }
                                catch (Exception ee)
                                {
                                    ZhiFang.Common.Log.Log.Debug("新增异常！详细信息：" + ee.ToString());
                                    throw ee;
                                }
                                #region 新增NRequestForm
                                try
                                {
                                    if (dt.Tables[0].Rows.Count == 0)
                                    {

                                        try
                                        {
                                            nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNotmp);
                                        }
                                        catch (Exception eee)
                                        {
                                            ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNotmp=" + NRequestFormNotmp, eee);
                                            context.Response.Write("程序异常！");
                                            return;
                                        }
                                        //sql = "insert into NRequestForm ( NRequestFormNo," + Columns + ",ClientNo,ClientName) values(" + NRequestFormNotmp + "," + Values + ",'" + user.getDeptOrgCode()[0].Trim() + "','" + user.getDeptPosName()[0].Trim() + "') ";
                                        if (nrf.Add(nrf_m) > 0)//向NRequestForm插入信息
                                        {
                                            if (cacheflag)
                                            {
                                                Cookie.CookieHelper.Write("tmpclientNo", hiddenClient);
                                                Cookie.CookieHelper.Write("ClientName", txtClientNo.Trim());
                                                Cookie.CookieHelper.Write("SelectItemValue", SelectItemValue);
                                                Cookie.CookieHelper.Write("SelectCombiItemNo", SelectCombiItemNo);
                                                Cookie.CookieHelper.Write("SelectCombiItemCName", SelectCombiItemCName);
                                                Cookie.CookieHelper.Write("SelectCombiItemValue", SelectCombiItemValue);
                                                Cookie.CookieHelper.Write("SelectAllTestItem", SelectAllTestItem);
                                                Cookie.CookieHelper.Write("CheckBoxFlag", "1");
                                            }
                                            else
                                            {
                                                Cookie.CookieHelper.Write("tmpclientNo", "");
                                                Cookie.CookieHelper.Write("ClientName", "");
                                                Cookie.CookieHelper.Write("SelectItemValue", "");
                                                Cookie.CookieHelper.Write("SelectCombiItemNo", "");
                                                Cookie.CookieHelper.Write("SelectCombiItemCName", "");
                                                Cookie.CookieHelper.Write("SelectCombiItemValue", "");
                                                Cookie.CookieHelper.Write("SelectAllTestItem", "");
                                                Cookie.CookieHelper.Write("CheckBoxFlag", "0");
                                            }
                                            NRequestFormNo = NRequestFormNotmp;
                                            try
                                            {
                                                nri_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                                            }
                                            catch (Exception eee)
                                            {
                                                ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNo=" + NRequestFormNo, eee);
                                                context.Response.Write("程序异常！");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            //插入申请单失败
                                            context.Response.Write("插入申请单失败！");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        nrf_m.NRequestFormNo = Convert.ToInt64(dt.Tables[0].Rows[0]["NRequestFormNo"]);
                                    }

                                    #region 生成条码号 barcodefrom
                                    //取得当前最大条码号

                                    if (BarCodeInputFlag.Trim() == "2")
                                    {
                                        NewBarCode = bcf.GetNewBarCode(txtClientNo);
                                    }
                                    else
                                    {
                                        NewBarCode = txtBarCode;
                                    }

                                    if (NewBarCode == "")
                                    {
                                        context.Response.Write("没有查询到条码！");//没有查询到条码
                                        return;
                                    }
                                    else
                                    {
                                        bcf_m.BarCode = NewBarCode;
                                    }
                                    bcf_m.PrintCount = 0;
                                    bcf_m.IsAffirm = 1;


                                    try
                                    {
                                        bcf_m.BarCodeFormNo = long.Parse(BarCodeFormNo.Trim());

                                        //string BarCodeFormNo = bcf.Add_ReturnBarCodeFormNo(bcf_m);
                                        if (bcf.Add(bcf_m) > 0)
                                        {
                                            //ViewState["BarCodeFormNo"] = NewBarCode;


                                        }
                                        else
                                        {
                                            //插入条码号失败
                                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "alert('插入条码号失败！');", true);
                                            context.Response.Write("插入条码号失败！");
                                            return;
                                        }
                                    }
                                    catch (Exception eee)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("条码编码：BarCodeFormNo=" + BarCodeFormNo + ",BarCodeFormNo=" + BarCodeFormNo, eee);
                                        context.Response.Write("程序异常！");
                                        return;
                                    }
                                    #endregion
                                }
                                catch (Exception ebt)
                                {
                                    ZhiFang.Common.Log.Log.Debug("新增异常！详细信息：" + ebt.ToString());
                                    throw ebt;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 更新NRequestForm
                                try
                                {
                                    dSet = nrf.GetList(new Model.NRequestForm { SerialNo = txtApplyNO.Replace(",", ",,").Replace("‘", "’‘") });
                                    if (dSet != null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
                                    {
                                        NRequestFormNo = dSet.Tables[0].Rows[0]["NRequestFormNo"].ToString();
                                        try
                                        {
                                            nri_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                                        }
                                        catch (Exception eee)
                                        {
                                            ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo, eee);
                                            context.Response.Write("程序异常！");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        //插入申请单失败
                                        context.Response.Write("未找到所要更新的申请单！");
                                        return;
                                    }
                                    try
                                    {
                                        nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                                    }
                                    catch (Exception eee)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo, eee);
                                        context.Response.Write("程序异常！");
                                        return;
                                    }
                                    int UpDateFlag = nrf.Update(nrf_m);
                                    NewBarCode = txtBarCode;
                                    bcf_m.BarCode = txtBarCode;
                                    bcf.Update(bcf_m);
                                    //ViewState["BarCodeFormNo"] = GetPara("BarCodeNo");
                                    DataSet dsbarcode = bcf.GetList(new Model.BarCodeForm { BarCode = txtBarCode });
                                    try
                                    {
                                        nri_m.BarCodeFormNo = long.Parse(dsbarcode.Tables[0].Rows[0]["BarCodeFormNo"].ToString());
                                    }
                                    catch
                                    {
                                        nri_m.BarCodeFormNo = -1;
                                    }
                                    DataSet dtItem = nri.GetList(new Model.NRequestItem { NRequestFormNo = long.Parse(NRequestFormNo) });
                                    string BarCodeLists = "";
                                    if (dtItem != null && dtItem.Tables.Count > 0 && dtItem.Tables[0].Rows.Count > 0)
                                    {
                                        for (int b = 0; b < dtItem.Tables[0].Rows.Count; b++)
                                        {
                                            BarCodeLists += dtItem.Tables[0].Rows[b]["BarCodeFormNo"].ToString() + ",";
                                        }
                                    }
                                    //sql = "delete from NRequestItem where NRequestFormNo='" + NRequestFormNo + "'";//首先清空原有项目信息
                                    nri.DeleteList_ByNRequestFormNo(long.Parse(NRequestFormNo));
                                }
                                catch (Exception ebt)
                                {
                                    ZhiFang.Common.Log.Log.Debug("更新异常！详细信息：" + ebt.ToString());
                                    throw ebt;
                                }
                                #endregion
                            }

                            context.Response.Write("1@" + NewBarCode);
                            return;
                        }
                        else
                        {
                            context.Response.Write("未登录，请登陆后继续！");
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        ZhiFang.Common.Log.Log.Debug("程序异常！详细信息：" + e.ToString());
                        context.Response.Write("程序异常！");
                        return;
                    }
                    #endregion
                }
                else if (type == "GetItem")
                {
                    //返回项目
                    string SuperGroupNo = context.Request.QueryString["SuperGroupNo"];
                    string ItemKey = Convert.ToString(System.Web.HttpUtility.UrlDecode(context.Request.QueryString["ItemKey"]));
                    int ListRowCount = Convert.ToInt32(context.Request.QueryString["ListRowCount"]);
                    int ListColCount = Convert.ToInt32(context.Request.QueryString["ListColCount"]);
                    int PageIndex = Convert.ToInt32(context.Request.QueryString["PageIndex"]);
                    string labcode = context.Request.QueryString["labcode"];

                    //GetItem(SuperGroupNo, ItemKey, Convert.ToInt32(ListRowCount), Convert.ToInt32(ListColCount), Convert.ToInt32(PageIndex), labcode);
                    //SuperGroupNo = "COMBI";
                    //ItemKey = "";
                    //ListRowCount = 9;
                    //PageIndex = 5;
                    //PageIndex = 1;
                    //labcode = "1";
                    ZhiFang.Common.Log.Log.Info("检验大组编号:" + SuperGroupNo);
                    IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
                    IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
                    IBLL.Common.BaseDictionary.IBSuperGroupControl ibsgc = BLLFactory<IBSuperGroupControl>.GetBLL();
                    Model.SuperGroupControl SuperGroupControl = new Model.SuperGroupControl();
                    DataSet ds;
                    string pageindextd = "";
                    int AllItemCount = 0;


                    string innerHtm = "";
                    ds = testitem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                    AllItemCount = testitem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1 });

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string td = "";
                        string tr = "";
                        string table = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string tmptd = "";
                            string tmpattributes = "";
                            string tmpattributes1 = "";
                            string tmpSubList = "";
                            string tmpSubItemList = "";
                            tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            if (ds.Tables[0].Rows[i]["IsProfile"] != DBNull.Value && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1")
                            {
                                string tmplisthtml = "";
                                string tmplisthtml1 = "";
                                DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                                {
                                    for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                    {
                                        tmplisthtml += @"<tr style=\'background-color=#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplisthtml1 += @"<tr style=$$$background-color=#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color=#ffffff\' >" + tmplisthtml + "</table>";
                                    tmplisthtml1 = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color=#ffffff$$$ >" + tmplisthtml1 + "</table>";
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                    if (tmpSubItemList != null && tmpSubItemList != "")
                                    {
                                        tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                    }
                                    tmpattributes = " style=\"background-color:#00FFFF\" onmouseover=\"showpic('" + tmplisthtml + "');\"  ";
                                    tmpattributes1 = " style=\"background-color:#00FFFF\" onmouseover=\"showpic('" + tmplisthtml1 + "');\"  ";
                                }

                                tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList + "')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";

                            }

                            if (ds.Tables[0].Rows[i]["isCombiItem"] != DBNull.Value && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                            {
                                string tmplisthtml = "";
                                string tmplistNo = "";

                                DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                                {
                                    for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                    {
                                        string flagc = "";
                                        string flag = "";
                                        if (dsitem.Tables[0].Rows[ii]["isCombiItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() == "1")
                                        {
                                            flagc = "#66FF33";
                                            flag = "1";

                                        }
                                        else
                                        {
                                            if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                                            {
                                                //flag = "#996633";
                                            }
                                        }
                                        if (dsitem.Tables[0].Rows[ii]["IsProfile"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IsProfile"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IsProfile"].ToString().Trim() == "1")
                                        {
                                            flagc = "#00ffff";
                                            flag = "2";
                                        }
                                        tmplisthtml += @"<tr style=\'background-color=#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplistNo += dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "," + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "," + flagc + "," + flag + "|";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 350px;font-size:12px;background-color=#ffffff\' >" + tmplisthtml + "</table>";
                                    if (tmplistNo.Trim().Length > 0)
                                    {
                                        tmplistNo = tmplistNo.Substring(0, tmplistNo.Length - 1);
                                    }
                                    else
                                    {
                                        tmplistNo = "";
                                    }
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                    if (tmpSubItemList != null && tmpSubItemList != "")
                                    {
                                        tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1).Replace("\"", "!!!");
                                    }
                                }
                                tmpattributes = " style=\"background-color:#66FF33\" onmouseover=\"showpic('" + tmplisthtml + "');\" ";
                                tmpSubList = tmplistNo;

                                tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue_Group('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpSubList + "','" + tmpSubItemList + "')\" ><input style='display:none' type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            }

                            if ((i + 1) % ListColCount != 0)
                            {
                                td += tmptd;
                            }
                            else
                            {
                                tr += "<tr height='35'>" + td + tmptd + "</tr>";
                                td = "";
                            }
                        }
                        //tr += "<tr height='35'>" + td + "</tr>";
                        int AllPageCount = ((int)AllItemCount / (ListRowCount * ListColCount)) + 1;
                        if (PageIndex > AllPageCount)
                        {
                            PageIndex = AllPageCount;
                        }
                        if (PageIndex <= 0)
                        {
                            PageIndex = 1;
                        }
                        int startindex = 1;
                        int endindex = 1;
                        if (PageIndex > 5)
                        {
                            startindex = PageIndex - 4;
                        }
                        if (PageIndex + 4 > AllPageCount)
                        {
                            endindex = AllPageCount;
                        }
                        else
                        {
                            endindex = PageIndex + 4;
                        }
                        //int pageindexdomain = (int)PageIndex / 10 ;

                        for (int i = startindex; i <= endindex; i++)
                        {
                            string focusstyle = "";
                            if (i == PageIndex)
                            {
                                focusstyle = "font-weight:bold;font-size:16;";
                            }
                            pageindextd += "<td onmouseover=\"this.style.backgroundColor='#a3f1f5';\" onmouseout=\"this.style.backgroundColor='Transparent';\" style='cursor:pointer;" + focusstyle + "' onclick=\"javascript:GetItemListByPageIndex('" + SuperGroupNo + "',''," + i.ToString() + ");\">" + i.ToString() + "</td>";
                        }
                        pageindextd = "<td><a href=\"javascript:GetItemListByPageIndex('" + SuperGroupNo + "','',1);\">第一页</td><td><a href=\"javascript:GetItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex - 1).ToString() + ");\">上一页</td>" + pageindextd + "<td><a href=\"javascript:GetItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex + 1).ToString() + ");\">下一页</td><td><a href=\"javascript:GetItemListByPageIndex('" + SuperGroupNo + "',''," + AllPageCount.ToString() + ");\">最后一页</td>";

                        table = "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\">" + tr + "<tr><td colspan='" + ListColCount.ToString() + "'><table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" style=\"font-size:12px\" width='100%' ><tr>" + pageindextd + "</tr></table></td></tr></table>";
                        innerHtm = table;
                    }
                    else
                    {
                        innerHtm = "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr><td>暂无</td></tr></table>";
                    }

                    context.Response.Write(innerHtm);
                }
                else
                {

                    string ClientNo = context.Request.Form["txtClientNo"];
                    string PatientName = context.Request.Form["txtPatientName"];
                    string SelectDoctor = context.Request.Form["SelectDoctor"];
                    string PatientID = context.Request.Form["txtPatientID"];
                    string StartDate = context.Request.Form["txtStartDate"];
                    string EndDate = context.Request.Form["txtEndDate"];
                    string CollectStartDate = context.Request.Form["txtCollectStartDate"];
                    string CollectEndDate = context.Request.Form["txtCollectEndDate"];
                    string chkOnlyNoPrintBarCode = context.Request.Form["chkOnlyNoPrintBarCode"];

                    if (!string.IsNullOrEmpty(ClientNo))
                    {
                        nrf_m.ClientNo = ClientNo.Split('@')[1];
                    }
                    if (!string.IsNullOrEmpty(SelectDoctor))
                    {
                        nrf_m.DoctorName = SelectDoctor;
                    }
                    if (!string.IsNullOrEmpty(PatientName))
                    {
                        nrf_m.CName = PatientName;
                    }
                    if (!string.IsNullOrEmpty(PatientID))
                    {
                        nrf_m.PatNo = PatientID;
                    }
                    if (!string.IsNullOrEmpty(StartDate))
                    {
                        nrf_m.OperDateStart = StartDate;
                    }
                    if (!string.IsNullOrEmpty(EndDate))
                    {
                        nrf_m.OperDateEnd = EndDate;
                    }
                    if (!string.IsNullOrEmpty(CollectEndDate))
                    {
                        nrf_m.CollectDateEnd = CollectEndDate;
                    }
                    if (!string.IsNullOrEmpty(CollectStartDate))
                    {
                        nrf_m.CollectDateStart = CollectStartDate;
                    }
                    if (chkOnlyNoPrintBarCode == "checked")
                    {
                        nrf_m.IsOnlyNoBar = true;
                    }
                    DataTable dt = new DataTable();

                    //查询申请
                    dt = rfb.GetAll(nrf_m);
                    List<ZhiFang.Model.NRequetFormList> ls = new List<ZhiFang.Model.NRequetFormList>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataSet dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { BarCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString().Trim()) });
                        string htmltmp = "";
                        if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            //DataTable dtt = sqldbweblis.ExecDT("select dbo.TestItem.CName from dbo.TestItem INNER JOIN dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where BarCodeFormNo=" + dr["BarCodeFormNo"].ToString().Trim());
                            //string htmltmp = "";
                            for (int j = 0; j < dsri.Tables[0].Rows.Count; j++)
                            {
                                htmltmp += dsri.Tables[0].Rows[j]["CName"].ToString().Trim() + ",";
                            }
                            if (htmltmp.Length > 0)
                            {
                                htmltmp = htmltmp.Substring(0, htmltmp.Length - 1);
                            }
                        }
                        ls.Add(new ZhiFang.Model.NRequetFormList
                        {
                            BarCode = dr["BarCode"].ToString(),
                            CName = dr["CName"].ToString(),
                            Sex = dr["Sex"].ToString(),
                            Age = dr["Age"].ToString(),
                            SampleName = dr["SampleName"].ToString(),
                            ItemName = htmltmp,
                            DoctorName = dr["DoctorName"].ToString(),
                            OperTime = dr["OperTime"].ToString().Trim().Substring(0, dr["OperTime"].ToString().Trim().Length - 3),
                            CollectTime = dr["CollectTime"].ToString().Trim().Substring(0, dr["CollectTime"].ToString().Trim().Length - 3),
                            WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString(),
                            ClientName = dr["ClientName"].ToString(),
                            Diag = dr["Diag"].ToString(),
                        });
                    }
                    int page = Convert.ToInt32(context.Request.Form["page"]);//当前页码
                    int row = Convert.ToInt32(context.Request.Form["rows"]);//每页行数
                    List<ZhiFang.Model.NRequetFormList> Result = new List<ZhiFang.Model.NRequetFormList>();
                    int rowCount = page * row;
                    if (ls.Count >= rowCount)
                    {
                        for (int i = rowCount - row; i < rowCount; i++)
                        {
                            Result.Add(ls[i]);
                        }
                    }
                    else
                    {
                        if (ls.Count > rowCount - row)
                        {
                            for (int i = rowCount - row; i < ls.Count; i++)
                            {
                                Result.Add(ls[i]);
                            }
                        }
                    }
                    string jsin = JsonConvert.SerializeObject(new { total = ls.Count, rows = Result });
                    context.Response.ContentType = "text/json;charset=UTF-8;";
                    context.Response.Write(jsin);
                }

            }
            catch
            {
                context.Response.Write(false);
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 返回区域编号
        /// </summary>
        /// <param name="clientno"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetClientEleArea(string clientno)
        {
            try
            {
                Model.CLIENTELE CLIENTELE = new Model.CLIENTELE();
                IBLL.Common.BaseDictionary.IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
                CLIENTELE = ibc.GetModel(Convert.ToInt32(clientno));
                ZhiFang.Common.Log.Log.Info("输出区域:" + CLIENTELE.AreaID.ToString());
                return CLIENTELE.AreaID.ToString();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.Data + ex.StackTrace + "----" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 返回送检区域总单位
        /// </summary>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetClientEleClient(string AreaID)
        {
            Model.ClientEleArea modelCEA = new Model.ClientEleArea();
            IBLL.Common.BaseDictionary.IBClientEleArea CEA = BLLFactory<IBClientEleArea>.GetBLL();
            try
            {
                modelCEA = CEA.GetModel(Convert.ToInt32(AreaID));
                ZhiFang.Common.Log.Log.Info("输出医疗机构:" + modelCEA.ClientNo.ToString());
                return modelCEA.ClientNo.ToString();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.Data + ex.StackTrace + "----" + ex.Message);
                return null;
            }
        }





        #region 隐藏
        [AjaxPro.AjaxMethod]
        public string SelectList(string LibClass, string value, string col)
        {
            try
            {
                string error;
                IBLL.Common.BaseDictionary.IBCLIENTELE clientele = BLLFactory<IBCLIENTELE>.GetBLL();
                IBLL.Common.BaseDictionary.IBSampleType sampletype = BLLFactory<IBSampleType>.GetBLL();
                IBLL.Common.BaseDictionary.IBDistrict district = BLLFactory<IBDistrict>.GetBLL();
                IBLL.Common.BaseDictionary.IBWardType wardtype = BLLFactory<IBWardType>.GetBLL();
                IBLL.Common.BaseDictionary.IBDepartment department = BLLFactory<IBDepartment>.GetBLL();
                IBLL.Common.BaseDictionary.IBDoctor doctor = BLLFactory<IBDoctor>.GetBLL();
                string sql = " 1=1 ";
                DataSet ds = new DataSet();
                switch (LibClass)
                {
                    #region clientele
                    case "Client":
                        if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") != string.Empty)
                        {
                            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));

                            sql = " 1=1 ";
                            Model.CLIENTELE c = new Model.CLIENTELE();
                            if (value != "")
                            {
                                c.ClienteleLikeKey = value;
                            }

                            ds = user.GetClientListByPost(value, 10);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    #endregion
                    #region SampleType
                    case "SampleType":
                        Model.SampleType st = new Model.SampleType();
                        if (value != "")
                        {
                            st.SampleTypeLikeKey = value;
                        }
                        ds = sampletype.GetList(10, st);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                        }
                        else
                        {
                            return null;
                        }
                    #endregion
                    #region District
                    case "District":

                        Model.District d = new Model.District();
                        if (value != "")
                        {
                            d.DistricLikeKey = value;
                        }
                        ds = district.GetList(10, d);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                        }
                        else
                        {
                            return null;
                        }

                    #endregion
                    #region WardType
                    case "WardType":
                        Model.WardType wt = new Model.WardType();
                        if (value != "")
                        {
                            wt.WardTypeLikeKey = value;
                        }
                        ds = wardtype.GetList(10, wt);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                        }
                        else
                        {
                            return null;
                        }
                    #endregion
                    #region Department
                    case "Department":

                        Model.Department dept = new Model.Department();

                        if (value != "")
                        {
                            dept.DepartmentLikeKey = value;
                        }
                        ds = department.GetList(10, dept);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                        }
                        else
                        {
                            return null;
                        }
                    #endregion
                    #region Doctor
                    case "Doctor":
                        Model.Doctor doctor_m = new Model.Doctor();
                        if (value != "")
                        {
                            doctor_m.DoctorLikeKey = value;
                        }
                        ds = doctor.GetList(10, doctor_m);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return GetSelectItemList_Html(ds.Tables[0], col, false, LibClass);
                        }
                        else
                        {
                            return null;
                        }
                    #endregion
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetSelectItemList_Html(DataTable dt, string col, bool listtype, string LibClass)
        {
            try
            {
                string t = "";
                string tr = "";
                string td = "";
                string td1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string c = "#a3f1f5";
                    if (i == 0 && listtype)
                    {
                        c = "#999999";
                    }
                    td = "";
                    td1 = "";
                    td = "<td style='padding:2px;font-size:12px'>" + dt.Rows[i]["CNAME"].ToString().Trim().Replace("|", ";") + "&nbsp;" + "</td>";
                    td1 = "<td style='padding:2px;font-size:12px'>" + dt.Rows[i]["ShortCode"].ToString() + "&nbsp;" + "</td>";
                    tr += "<tr BgColor='" + c + "' id='selecttr_" + i + "' onmouseover=\"this.style.backgroundColor='#AAA';\" onmousedown=\"this.style.backgroundColor='" + c + "';GetTmpClient('" + dt.Rows[i][col].ToString() + "','" + dt.Rows[i]["CNAME"].ToString().Trim().Replace("|", ";") + "')\" onmouseout=\"this.style.backgroundColor='#a3f1f5';\" onclick=\"GetTmpClient('" + dt.Rows[i][col].ToString() + "','" + dt.Rows[i]["CNAME"].ToString().Trim().Replace("|", ";") + "')\" >" + td + td1 + "</tr>";

                }
                if (listtype)
                {
                    td = "<tr BgColor='#996633'><td style='padding:2px;font-size:12px;cursor:pointer' colspan='2' onmousedown=\"ItemAdd('Add" + LibClass + ".aspx');\" align='center' ><a href='# ' style='color:#ffffff' >新增</a></td></tr>";
                    tr += td;
                }
                t = "<table border=\"1\" cellpadding=\"0\" cellspacing=\"0\" width='200'>" + tr + "</table>";
                return t;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return "";
            }
        }
        #endregion
    }
}