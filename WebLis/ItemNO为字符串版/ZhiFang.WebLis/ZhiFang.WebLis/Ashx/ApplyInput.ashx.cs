using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Report;
using System.Collections.Generic;


namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.WebLis.Ashx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ApplyInput : IHttpHandler
    {
        //SelectList联想选择

        IBLL.Common.BaseDictionary.IBClientEleArea CEA = BLLFactory<IBClientEleArea>.GetBLL();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        //取配置颜色
        string[] ItemColorSet;
        //申请细项颜色            
        string TestItemColor;
        //申请组合项目颜色
        string ProfileItemColor;
        //申请组合项目细项颜色
        string ProfileTestItemColor;
        //申请组套项目颜色
        string CombiItemColor;
        //申请组套项目细项颜色
        string CombiTestItemColor;
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SelectList_Client(string LibClass, string value, string col, string ClientNo)
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
                IBLL.Common.BaseDictionary.IBLab_District Lab_District = BLLFactory<IBLab_District>.GetBLL();
                IBLL.Common.BaseDictionary.IBLab_Department Lab_Department = BLLFactory<IBLab_Department>.GetBLL();
                IBLL.Common.BaseDictionary.IBLab_Doctor Lab_Doctor = BLLFactory<IBLab_Doctor>.GetBLL();
                Model.Lab_District LabDistrict = new Model.Lab_District();
                Model.Lab_Department labDepartment = new Model.Lab_Department();
                Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
                Model.Lab_Doctor LabDoctor = new Model.Lab_Doctor();
                Model.CLIENTELE CLIENTELE = new Model.CLIENTELE();
                string sql = " 1=1 ";
                DataSet ds = new DataSet();
                string ClientNoSelect = "";
                if (ClientNo != "")
                {
                    ClientNoSelect = ClientNo;
                }
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
                                ds = user.GetClientListByPost(value, 10);
                            }
                            else
                            {
                                BusinessLogicClientControl.Account = user.Account;
                                BusinessLogicClientControl.Flag = 0;
                                BusinessLogicClientControl.SelectedFlag = true;
                                ds = iblcc.GetList(BusinessLogicClientControl);
                            }


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
                            st.SearchLikeKey = value;
                        }
                        ds = sampletype.GetListByPage(st, 0, 30);

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

                        if (value != "")
                        {
                            LabDistrict.SearchLikeKey = value;
                        }
                        LabDistrict.LabCode = ClientNoSelect;
                        ds = Lab_District.GetListByPage(LabDistrict, 0, 50);
                        //ds = district.GetList(10, d);

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
                        labDepartment.LabCode = ClientNoSelect;
                        if (value != "")
                        {
                            labDepartment.SearchLikeKey = value;
                        }
                        ds = Lab_Department.GetListByPage(labDepartment, 0, 50);
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
                        if (value != "")
                        {
                            LabDoctor.SearchLikeKey = value;
                        }
                        LabDoctor.LabCode = ClientNoSelect;
                        ds = Lab_Doctor.GetListByPage(LabDoctor, 0, 50);
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
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

                            ds = user.GetClientListByPost(value, 0);
                            DataTable newdt = new DataTable();
                            newdt = ds.Tables[0].Clone();
                            DataRow[] dss = ds.Tables[0].Select("", " ClIENTNO ");
                            for (int i = 0; i < dss.Length; i++)
                            {
                                newdt.ImportRow((DataRow)dss[i]);
                            }
                            ds = new DataSet();
                            ds.Tables.Add(newdt);
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
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }
        public bool IsReusable
        {
            get
            {
                return false;
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
                    //tr.Cells.Add(td);
                    //tr.Cells.Add(td1);
                    //tr.BgColor = c;
                    //tr.Attributes.Add("onmouseover", "this.style.backgroundColor='#AAA';");
                    //tr.Attributes.Add("onmousedown", "this.style.backgroundColor='" + c + "';GetTmpClient('" + dt.Rows[i][col].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
                    ////
                    //tr.Attributes.Add("onmouseout", "this.style.backgroundColor='#a3f1f5';");
                    //tr.Attributes.Add("onclick", "GetTmpClient('" + dt.Rows[i][col].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
                    //tr.Attributes.Add("onclick", "alert('test');");

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
        /// <summary>
        /// 项目过滤
        /// </summary>
        /// <param name="SuperGroupNo">检验大组</param>
        /// <param name="ItemKey">联想输入</param>
        /// <param name="ListRowCount">行</param>
        /// <param name="ListColCount">列</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="labcode">机构</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string TestItemListByClientNo(string SuperGroupNo, string ItemKey, int ListRowCount, int ListColCount, int PageIndex, string labcode)
        {
            try
            {
                string error;
                ZhiFang.Common.Log.Log.Info("检验大组编号:" + SuperGroupNo);
                //医疗机构字典
                IBLL.Common.BaseDictionary.IBLab_TestItem LabTestItem = BLLFactory<IBLab_TestItem>.GetBLL();
                IBLL.Common.BaseDictionary.IBLab_GroupItem LabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
                IBLL.Common.BaseDictionary.IBSuperGroupControl ibsgc = BLLFactory<IBSuperGroupControl>.GetBLL();
                //中心字典
                IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
                IBLL.Common.BaseDictionary.IBGroupItem CenterGroupItem = BLLFactory<IBGroupItem>.GetBLL();
                Model.SuperGroupControl SuperGroupControl = new Model.SuperGroupControl();
                Model.GroupItem zxGroupM = new Model.GroupItem();
                DataSet ds;
                string pageindextd = "";
                int AllItemCount = 0;
                string SuperGroup = SuperGroupNo;
                try
                {
                    int result = Convert.ToInt32(SuperGroupNo);
                    if (labcode != "")
                    {
                        SuperGroupControl.ControlLabNo = labcode;
                        SuperGroupControl.SuperGroupNo = result;
                        DataSet dsSuperGroup = ibsgc.GetList(SuperGroupControl);
                        if (dsSuperGroup.Tables != null && dsSuperGroup.Tables[0].Rows.Count > 0)
                        {
                            SuperGroupNo = dsSuperGroup.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString();
                        }
                    }
                    SuperGroup = "8";
                }
                catch
                {

                }
                #region 如果医疗机构编码不存在 按照中心项目字典表显示项目
                switch ((ZhiFang.Common.Dictionary.TestItemSuperGroupClass)Enum.Parse(typeof(ZhiFang.Common.Dictionary.TestItemSuperGroupClass), SuperGroup.ToUpper()))
                {
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.ALL:
                        if (ItemKey.Trim() == "")
                        {
                            if (labcode != "")
                            {
                                ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, IsDoctorItem = 1, Visible = 1 });
                            }
                            else
                            {
                                ds = CenterTestItem.GetListByPage(new Model.TestItem { IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { IsDoctorItem = 1, Visible = 1 });
                            }

                        }
                        else
                        {
                            if (labcode != "")
                            {
                                ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 });
                            }
                            else
                            {
                                ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 });
                            }
                        }
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN:
                        if (labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE:
                        if (labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI://组套组合
                        if (labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });

                        }
                        break;
                    default:
                        if (ItemKey.Trim() == "")
                        {
                            if (labcode != "")
                            {
                                ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, LabSuperGroupNo = Convert.ToInt32(SuperGroupNo), IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, LabSuperGroupNo = Convert.ToInt32(SuperGroupNo), IsDoctorItem = 1, Visible = 1 });
                            }
                            else
                            {
                                ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = Convert.ToInt32(SuperGroupNo), UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = Convert.ToInt32(SuperGroupNo), UseFlag = 1, IsDoctorItem = 1, Visible = 1 });
                            }
                        }
                        else
                        {
                            if (labcode != "")
                            {
                                ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, LabSuperGroupNo = Convert.ToInt32(SuperGroupNo), CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 });
                            }
                            else
                            {
                                ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = Convert.ToInt32(SuperGroupNo), TestItemLikeKey = "1", CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 }, PageIndex - 1, ListRowCount * ListColCount);
                                AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = Convert.ToInt32(SuperGroupNo), CName = ItemKey, EName = ItemKey, ShortCode = ItemKey, ShortName = ItemKey, IsDoctorItem = 1, Visible = 1 });
                            }
                        }
                        break;
                }
                #endregion
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //取配置颜色
                    ItemColorSet = ConfigHelper.GetConfigString("ApplyColor").Split(new char[] { ';' });
                    //申请细项颜色            
                    TestItemColor = (ItemColorSet[0].Trim() == "" || ItemColorSet[0].Trim().ToLower() == "no") ? "#ffffff" : ItemColorSet[0].Trim();
                    //申请组合项目颜色
                    ProfileItemColor = (ItemColorSet[1].Split(',')[0].Trim() == "" || ItemColorSet[1].Split(',')[0].Trim().ToLower() == "no") ? "Blue" : ItemColorSet[1].Split(',')[0].Trim();
                    //申请组合项目细项颜色
                    ProfileTestItemColor = (ItemColorSet[1].Split(',')[1].Trim() == "" || ItemColorSet[1].Split(',')[1].Trim().ToLower() == "no") ? "#6699ff" : ItemColorSet[1].Split(',')[1].Trim();
                    //申请组套项目颜色
                    CombiItemColor = (ItemColorSet[2].Split(',')[0].Trim() == "" || ItemColorSet[2].Split(',')[0].Trim().ToLower() == "no") ? "#Blue" : ItemColorSet[2].Split(',')[0].Trim();
                    //申请组套项目细项颜色
                    CombiTestItemColor = (ItemColorSet[2].Split(',')[1].Trim() == "" || ItemColorSet[2].Split(',')[1].Trim().ToLower() == "no") ? "#6699ff" : ItemColorSet[2].Split(',')[1].Trim();


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

                        //------------------------------------------------------------------------------------------------------------------------------------
                        if (ds.Tables[0].Rows[i]["IsProfile"] != DBNull.Value && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1")
                        {
                            DataSet dsitem = new DataSet();
                            string tmplisthtml = "";
                            string tmplisthtml1 = "";
                            if (labcode != "")
                            {
                                dsitem = LabGroupItem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                            }
                            else
                            {
                                dsitem = CenterGroupItem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                            }
                            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                            {
                                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                {
                                    tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    tmplisthtml1 += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                }
                                tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                tmplisthtml1 = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml1 + "</table>";
                                tmpSubItemList = LabGroupItem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                if (tmpSubItemList != null && tmpSubItemList != "")
                                {
                                    tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                }
                                tmpattributes = " style=\"background-color:" + ProfileItemColor + "\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                tmpattributes1 = " style=\"background-color:" + ProfileItemColor + "\" onmouseover=\"showpic('" + tmplisthtml1 + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                            }

                            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList.Substring(0, tmpSubItemList.Length - 1) + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList + "')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";

                        }
                        //------------------------------------------------------------------------------------------------------------------------------------
                        if (ds.Tables[0].Rows[i]["isCombiItem"] != DBNull.Value && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                        {
                            string tmplisthtml = "";
                            string tmplistNo = "";
                            DataSet dsitem = new DataSet();
                            if (labcode != "")
                            {
                                GetLabGroupItemList_html(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode, ref tmplisthtml, ref tmplistNo);
                                if (tmplistNo.IndexOf('|') > -1)
                                {
                                    tmplistNo = tmplistNo.Substring(0, tmplistNo.LastIndexOf('|'));
                                }
                            }
                            else
                            {
                                GetCenterGroupItemList_html(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), ref tmplisthtml, ref tmplistNo);
                            }

                            tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                            tmpattributes = " style=\"background-color:" + CombiItemColor + "\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                            tmpSubList = tmplistNo;

                            tmpSubItemList = LabGroupItem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                            if (tmpSubItemList != null && tmpSubItemList != "")
                            {
                                tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1).Replace("\"", "!!!");
                            }
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
                    tr += "<tr height='35'>" + td + "</tr>";
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
                    return table;
                }
                else
                {
                    return "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr><td>暂无</td></tr></table>";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString() + e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// 获取中心组套子项列表(HTML形式)
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <param name="tmplisthtml"></param>
        /// <param name="tmplistNo"></param>
        private void GetCenterGroupItemList_html(string ItemNo, ref string tmplisthtml, ref string tmplistNo)
        {
            IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem CenterGroupItem = BLLFactory<IBGroupItem>.GetBLL();

            DataSet tmpds = CenterGroupItem.GetGroupItemList(ItemNo.Trim());

            if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
            {
                DataTable tmpdt = tmpds.Tables[0];
                for (int i = 0; i < tmpdt.Rows.Count; i++)
                {
                    string bcolor = TestItemColor;
                    string itemflag = "";

                    if (tmpdt.Rows[i]["isCombiItem"] != DBNull.Value && tmpdt.Rows[i]["isCombiItem"].ToString().Trim() != "" && tmpdt.Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        bcolor = CombiItemColor;
                        itemflag = "1";
                        GetCenterGroupItemList_html(tmpdt.Rows[i]["ItemNo"].ToString().Trim(), ref tmplisthtml, ref tmplistNo);
                    }
                    else
                    {
                        if (tmpdt.Rows[i]["IsProfile"] != DBNull.Value && tmpdt.Rows[i]["IsProfile"].ToString().Trim() != "" && tmpdt.Rows[i]["IsProfile"].ToString().Trim() == "1")
                        {
                            bcolor = ProfileItemColor;
                            itemflag = "2";
                            GetCenterGroupItemList_html(tmpdt.Rows[i]["ItemNo"].ToString().Trim(), ref tmplisthtml, ref tmplistNo);
                        }
                        else
                        {
                            if (tmpdt.Rows[i]["Color"] != DBNull.Value && tmpdt.Rows[i]["Color"].ToString().Trim() != "")
                            {
                                bcolor = ZhiFang.BLL.Common.Lib.ItemColor()[tmpdt.Rows[i]["Color"].ToString().Trim()].ColorValue;
                            }
                        }
                    }
                    tmplisthtml += @"<tr style=\'background-color:" + bcolor + @"\' ><td>" + tmpdt.Rows[i]["ItemNo"].ToString().Trim() + "</td><td>" + tmpdt.Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + tmpdt.Rows[i]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                    tmplistNo += tmpdt.Rows[i]["ItemNo"].ToString().Trim() + "," + tmpdt.Rows[i]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "," + bcolor + "," + itemflag + "|";

                }
                tmplistNo = tmplistNo.Substring(0, tmplistNo.LastIndexOf('|'));
            }
        }
        /// <summary>
        /// 获取实验室组套子项列表(HTML形式)
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <param name="labcode"></param>
        /// <param name="tmplisthtml"></param>
        /// <param name="tmplistNo"></param>
        private void GetLabGroupItemList_html(string ItemNo, string labcode, ref string tmplisthtml, ref string tmplistNo)
        {
            IBLL.Common.BaseDictionary.IBLab_GroupItem LabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
            DataSet tmpds = LabGroupItem.GetGroupItemList(ItemNo.Trim(), labcode);

            if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
            {
                DataTable tmpdt = tmpds.Tables[0];
                for (int i = 0; i < tmpdt.Rows.Count; i++)
                {
                    string bcolor = TestItemColor;
                    string itemflag = "";

                    if (tmpdt.Rows[i]["isCombiItem"] != DBNull.Value && tmpdt.Rows[i]["isCombiItem"].ToString().Trim() != "" && tmpdt.Rows[i]["isCombiItem"].ToString().Trim() == "1")
                    {
                        bcolor = CombiItemColor;
                        itemflag = "1";
                        GetLabGroupItemList_html(tmpdt.Rows[i]["ItemNo"].ToString().Trim(), labcode, ref tmplisthtml, ref tmplistNo);
                    }
                    else
                    {
                        if (tmpdt.Rows[i]["IsProfile"] != DBNull.Value && tmpdt.Rows[i]["IsProfile"].ToString().Trim() != "" && tmpdt.Rows[i]["IsProfile"].ToString().Trim() == "1")
                        {
                            bcolor = ProfileItemColor;
                            itemflag = "2";
                            GetLabGroupItemList_html(tmpdt.Rows[i]["ItemNo"].ToString().Trim(), labcode, ref tmplisthtml, ref tmplistNo);
                        }
                        else
                        {
                            if (tmpdt.Rows[i]["Color"] != DBNull.Value && tmpdt.Rows[i]["Color"].ToString().Trim() != "")
                            {
                                bcolor = ZhiFang.BLL.Common.Lib.ItemColor()[tmpdt.Rows[i]["Color"].ToString().Trim()].ColorValue;
                            }
                            tmplistNo += tmpdt.Rows[i]["ItemNo"].ToString().Trim() + "," + tmpdt.Rows[i]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "," + bcolor + "," + itemflag + "|";
                        }
                    }
                    tmplisthtml += @"<tr style=\'background-color:" + bcolor + @"\' ><td>" + tmpdt.Rows[i]["ItemNo"].ToString().Trim() + "</td><td>" + tmpdt.Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + tmpdt.Rows[i]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";


                }
            }
        }

        [AjaxPro.AjaxMethod]
        public string TestItemList_JiaYin(string SuperGroupNo, string ItemKey, int ListRowCount, int ListColCount, int PageIndex)
        {
            try
            {
                string error;
                IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
                IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
                DataSet ds;
                string pageindextd = "";
                int AllItemCount = 0;
                string superGroup = "";
                if (SuperGroupNo != "ALL" && SuperGroupNo != "DOCTORCOMBICHARGE" && SuperGroupNo != "OFTEN" && SuperGroupNo != "CHARGE")
                {
                    superGroup = "SUPERGROUP";
                }
                else
                    superGroup = SuperGroupNo;

                switch ((ZhiFang.Common.Dictionary.TestItemSuperGroupClass)Enum.Parse(typeof(ZhiFang.Common.Dictionary.TestItemSuperGroupClass), superGroup.ToUpper()))
                {
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.ALL:
                        if (ItemKey.Trim() == "")
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE });
                            AllItemCount = testitem.GetListCount(ZhiFang.Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE);
                        }
                        else
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE, TestItemLikeKey = ItemKey });
                            AllItemCount = testitem.GetListCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE, TestItemLikeKey = ItemKey });
                        }
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN:
                        ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.OFTEN });
                        AllItemCount = 0;
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI:

                        if (ItemKey.Trim() == "")
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI });
                            AllItemCount = testitem.GetListCount(ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI);
                        }
                        else
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, TestItemLikeKey = ItemKey });
                            AllItemCount = testitem.GetListCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, TestItemLikeKey = ItemKey });
                        }
                        break;
                    case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE:

                        if (ItemKey.Trim() == "")
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE });
                            AllItemCount = testitem.GetListCount(ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE);
                        }
                        else
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, TestItemLikeKey = ItemKey });
                            AllItemCount = testitem.GetListCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.CHARGE, TestItemLikeKey = ItemKey });
                        }
                        break;
                    default:
                        if (ItemKey.Trim() == "")
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(SuperGroupNo) });
                            AllItemCount = testitem.GetListCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(SuperGroupNo) });
                        }
                        else
                        {
                            ds = testitem.GetList(ListRowCount * ListColCount, PageIndex - 1, new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(SuperGroupNo), TestItemLikeKey = ItemKey });
                            AllItemCount = testitem.GetListCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(SuperGroupNo), TestItemLikeKey = ItemKey });
                        }
                        break;
                }
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
                        //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                        tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                        if (ds.Tables[0].Rows[i]["IsProfile"] != DBNull.Value && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1")
                        {
                            string tmplisthtml = "";
                            string tmplisthtml1 = "";
                            DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                            {
                                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                {
                                    tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    tmplisthtml1 += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                }
                                tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                tmplisthtml1 = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml1 + "</table>";
                                tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                tmpattributes1 = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml1 + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                            }

                            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList.Substring(0, tmpSubItemList.Length - 1) + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList + "')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";

                        }

                        if (ds.Tables[0].Rows[i]["isCombiItem"] != DBNull.Value && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                        {
                            string tmplisthtml = "";
                            string tmplistNo = "";

                            DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                            {
                                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                {
                                    string flagc = "";
                                    string flag = "";
                                    if (dsitem.Tables[0].Rows[ii]["isCombiItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() == "1")
                                    {
                                        //if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                                        //{
                                        //    //flag = "#009933";
                                        //}
                                        //else
                                        //{
                                        flagc = "#006666";
                                        flag = "1";
                                        //}
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
                                    tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    tmplistNo += dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "," + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "," + flagc + "," + flag + "|";
                                }
                                tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                if (tmplistNo.Trim().Length > 0)
                                {
                                    tmplistNo = tmplistNo.Substring(0, tmplistNo.Length - 1);
                                }
                                else
                                {
                                    tmplistNo = "";
                                }
                                tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1).Replace("\"", "!!!");
                            }
                            tmpattributes = " style=\"background-color:006666\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
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
                    tr += "<tr height='35'>" + td + "</tr>";
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
                    return table;
                }
                else
                {
                    return "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr><td>暂无</td></tr></table>";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return null;
            }
        }
        [AjaxPro.AjaxMethod]
        public string ParentTestItemList(string SuperGroupNo, string ItemKey, int ListColCount, int PageIndex)
        {
            //string error;
            IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBSuperGroup sg = BLLFactory<IBSuperGroup>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            DataSet ds;
            string pageindextd = "";
            int AllItemCount = 0;
            DataSet tmpds = sg.GetList(ListColCount, PageIndex - 1, new Model.SuperGroup { ParentNo = int.Parse(SuperGroupNo) });
            AllItemCount = sg.GetTotalCount(new Model.SuperGroup { ParentNo = int.Parse(SuperGroupNo) });

            if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
            {
                string td = "";
                string tr = "";
                string table = "";
                for (int j = 0; j < tmpds.Tables[0].Rows.Count; j++)
                {
                    if (ItemKey.Trim() == "")
                    {
                        ds = testitem.GetList(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(tmpds.Tables[0].Rows[j]["SuperGroupNo"].ToString()) });
                    }
                    else
                    {
                        ds = testitem.GetList(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(tmpds.Tables[0].Rows[j]["SuperGroupNo"].ToString()), TestItemLikeKey = ItemKey });
                    }
                    string subtable = "";
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string subtr = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string tmptd = "";
                            string tmpattributes = "";
                            string tmpattributes1 = "";
                            string tmpSubList = "";
                            string tmpSubItemList = "";
                            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            if (ds.Tables[0].Rows[i]["IsProfile"] != DBNull.Value && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["IsProfile"].ToString().Trim() == "1")
                            {
                                string tmplisthtml = "";
                                string tmplisthtml1 = "";
                                DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                                {
                                    for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                    {
                                        tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplisthtml1 += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                    tmplisthtml1 = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml1 + "</table>";

                                    tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpattributes1 = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml1 + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                    tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                }
                                //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList.Substring(0, tmpSubItemList.Length - 1) + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                                tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList + "')\" >" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            }
                            if (ds.Tables[0].Rows[i]["isCombiItem"] != DBNull.Value && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() != "" && ds.Tables[0].Rows[i]["isCombiItem"].ToString().Trim() == "1")
                            {
                                string tmplisthtml = "";
                                string tmplistNo = "";

                                DataSet dsitem = groupitem.GetGroupItemList(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                                {
                                    for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                                    {
                                        string flagc = "";
                                        string flag = "";
                                        if (dsitem.Tables[0].Rows[ii]["isCombiItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() == "1")
                                        {
                                            //if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                                            //{
                                            //    //flag = "#009933";
                                            //}
                                            //else
                                            //{
                                            flagc = "#006666";
                                            flag = "1";
                                            //}
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
                                        tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplistNo += dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "," + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";") + "," + flagc + "," + flag + "|";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                    tmplistNo = tmplistNo.Substring(0, tmplistNo.Length - 1);

                                    tmpattributes = " style=\"background-color:006666\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpSubList = tmplistNo;
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim());
                                    tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                }
                                tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue_Group('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpSubList + "','" + tmpSubItemList + "')\" ><input style='display:none' type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            }
                            subtr += "<tr height='35'>" + tmptd + "</tr>";
                        }
                        subtable += "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr style=\"background-color:006633\"><td width='" + (int)(100 / ListColCount) + "%'>" + tmpds.Tables[0].Rows[j]["CName"].ToString() + "</td></tr>" + subtr + "</table>";
                    }
                    else
                    {
                        subtable += "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr style=\"background-color:006633\"><td width='" + (int)(100 / ListColCount) + "%'>" + tmpds.Tables[0].Rows[j]["CName"].ToString() + "</td></tr><tr><td>暂无</td></tr></table>";
                    }
                    td += "<td width='" + (int)(100 / ListColCount) + "%' valign='top' >" + subtable + "</td>";
                }
                tr = "<tr>" + td + "</tr>";
                //table="<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\">"+tr+"</table>";
                int AllPageCount = ((int)AllItemCount / (ListColCount)) + 1;
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
                    pageindextd += "<td onmouseover=\"this.style.backgroundColor='#a3f1f5';\" onmouseout=\"this.style.backgroundColor='Transparent';\" style='cursor:pointer;" + focusstyle + "' onclick=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + i.ToString() + ");\">" + i.ToString() + "</td>";
                }
                pageindextd = "<td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "','',1);\">第一页</td><td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex - 1).ToString() + ");\">上一页</td>" + pageindextd + "<td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex + 1).ToString() + ");\">下一页</td><td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + AllPageCount.ToString() + ");\">最后一页</td>";

                table = "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\">" + tr + "<tr><td colspan='" + ListColCount.ToString() + "'><table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" style=\"font-size:12px\" width='100%' ><tr>" + pageindextd + "</tr></table></td></tr></table>";
                return table;
            }
            else
            {
                return "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr><td>暂无</td></tr></table>";
            }
        }
        [AjaxPro.AjaxMethod]
        public string GetCombiItemList(string CombiItemNo)
        {
            string error;
            IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(CombiItemNo.Trim());
            string tmplistNo = "";
            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
            {
                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                {
                    string flagc = "";
                    string flag = "";
                    if (dsitem.Tables[0].Rows[ii]["isCombiItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["isCombiItem"].ToString().Trim() == "1")
                    {
                        //if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                        //{
                        //    //flag = "#009933";
                        //}
                        //else
                        //{
                        flagc = "#006666";
                        flag = "1";
                        //}
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
            }
            return tmplistNo;
        }
        [AjaxPro.AjaxMethod]
        public string GetSampleList(string CubeColor)
        {
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
            return SampleTypeCName;
        }
        [AjaxPro.AjaxMethod]
        public string GetSample()
        {
            IBLL.Common.BaseDictionary.IBSampleType ibst = BLLFactory<IBSampleType>.GetBLL();
            Model.SampleType SampleType = new Model.SampleType();
            DataSet ds = ibst.GetAllList();
            string sampleList = "";
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sampleList += ds.Tables[0].Rows[i]["CName"].ToString().Trim() + ",";
                }
                if (sampleList != "")
                {
                    sampleList = sampleList.Remove(sampleList.Length - 1);
                }

            }
            return sampleList;

        }
        [AjaxPro.AjaxMethod]
        public string GetSubItemList(string ItemNo)
        {
            string error;
            IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(ItemNo.Trim());
            string tmpattributes = "";
            string tmplisthtml = "";
            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
            {
                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                {
                    //tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                    tmplisthtml += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                }
                //tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                tmplisthtml = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml + "</table>";
            }
            //tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
            tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "," + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "," + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
            return tmpattributes.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###");

        }
        [AjaxPro.AjaxMethod]
        public string GetGroupSubItemList(string ItemNo)
        {
            string error;
            IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(ItemNo.Trim());
            string tmpSubItemList = groupitem.GetSubItemList_No_CName(ItemNo.Trim());
            return tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
        }
        [AjaxPro.AjaxMethod]
        public string ParentTestItemListByClientNo(string SuperGroupNo, string ItemKey, int ListColCount, int PageIndex, string labcode)
        {
            string error;
            IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBLab_SuperGroup sg = BLLFactory<IBLab_SuperGroup>.GetBLL();
            IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
            DataSet ds;
            string pageindextd = "";
            int AllItemCount = 0;
            DataSet tmpds = sg.GetList(ListColCount, PageIndex - 1, new Model.Lab_SuperGroup { ParentNo = int.Parse(SuperGroupNo), LabCode = labcode });
            AllItemCount = sg.GetTotalCount(new Model.Lab_SuperGroup { ParentNo = int.Parse(SuperGroupNo), LabCode = labcode });

            if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
            {
                string td = "";
                string tr = "";
                string table = "";
                for (int j = 0; j < tmpds.Tables[0].Rows.Count; j++)
                {
                    if (ItemKey.Trim() == "")
                    {
                        ds = testitem.GetList(new Model.Lab_TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(tmpds.Tables[0].Rows[j]["SuperGroupNo"].ToString()), LabCode = labcode });
                    }
                    else
                    {
                        ds = testitem.GetList(new Model.Lab_TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP, SuperGroupNo = int.Parse(tmpds.Tables[0].Rows[j]["SuperGroupNo"].ToString()), TestItemLikeKey = ItemKey, LabCode = labcode });
                    }
                    string subtable = "";
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string subtr = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string tmptd = "";
                            string tmpattributes = "";
                            string tmpattributes1 = "";
                            string tmpSubList = "";
                            string tmpSubItemList = "";
                            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','','')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
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
                                        tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplisthtml1 += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                    tmplisthtml1 = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml1 + "</table>";

                                    tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpattributes1 = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml1 + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                    tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                }
                                //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "','" + tmpSubItemList.Substring(0, tmpSubItemList.Length - 1) + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
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
                                            //if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                                            //{
                                            //    //flag = "#009933";
                                            //}
                                            //else
                                            //{
                                            flagc = "#006666";
                                            flag = "1";
                                            //}
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
                                        tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                                        tmplistNo += dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "," + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";") + "," + flagc + "," + flag + "|";
                                    }
                                    tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                                    tmplistNo = tmplistNo.Substring(0, tmplistNo.Length - 1);

                                    tmpattributes = " style=\"background-color:006666\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
                                    tmpSubList = tmplistNo;
                                    tmpSubItemList = groupitem.GetSubItemList_No_CName(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), labcode);
                                    tmpSubItemList = tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
                                }
                                tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue_Group('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "','" + tmpSubList + "','" + tmpSubItemList + "')\" ><input style='display:none' type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
                            }
                            subtr += "<tr height='35'>" + tmptd + "</tr>";
                        }
                        subtable += "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr style=\"background-color:006633\"><td width='" + (int)(100 / ListColCount) + "%'>" + tmpds.Tables[0].Rows[j]["CName"].ToString() + "</td></tr>" + subtr + "</table>";
                    }
                    else
                    {
                        subtable += "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr style=\"background-color:006633\"><td width='" + (int)(100 / ListColCount) + "%'>" + tmpds.Tables[0].Rows[j]["CName"].ToString() + "</td></tr><tr><td>暂无</td></tr></table>";
                    }
                    td += "<td width='" + (int)(100 / ListColCount) + "%' valign='top' >" + subtable + "</td>";
                }
                tr = "<tr>" + td + "</tr>";
                //table="<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\">"+tr+"</table>";
                int AllPageCount = ((int)AllItemCount / (ListColCount)) + 1;
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
                    pageindextd += "<td onmouseover=\"this.style.backgroundColor='#a3f1f5';\" onmouseout=\"this.style.backgroundColor='Transparent';\" style='cursor:pointer;" + focusstyle + "' onclick=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + i.ToString() + ");\">" + i.ToString() + "</td>";
                }
                pageindextd = "<td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "','',1);\">第一页</td><td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex - 1).ToString() + ");\">上一页</td>" + pageindextd + "<td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + (PageIndex + 1).ToString() + ");\">下一页</td><td><a href=\"javascript:GetParentItemListByPageIndex('" + SuperGroupNo + "',''," + AllPageCount.ToString() + ");\">最后一页</td>";

                table = "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\">" + tr + "<tr><td colspan='" + ListColCount.ToString() + "'><table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" style=\"font-size:12px\" width='100%' ><tr>" + pageindextd + "</tr></table></td></tr></table>";
                return table;
            }
            else
            {
                return "<table  width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-size:12px\"><tr><td>暂无</td></tr></table>";
            }
        }
        [AjaxPro.AjaxMethod]
        public string GetCombiItemListByClientNo(string CombiItemNo, string labcode)
        {
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
                        //if (dsitem.Tables[0].Rows[ii]["IschargeItem"] != DBNull.Value && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() != "" && dsitem.Tables[0].Rows[ii]["IschargeItem"].ToString().Trim() == "1")
                        //{
                        //    //flag = "#009933";
                        //}
                        //else
                        //{
                        flagc = "#006666";
                        flag = "1";
                        //}
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
            }
            return tmplistNo;
        }
        [AjaxPro.AjaxMethod]
        public string GetSubItemListByClientNo(string ItemNo, string labcode)
        {
            string error;
            IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(ItemNo.Trim(), labcode);
            string tmpattributes = "";
            string tmplisthtml = "";
            if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
            {
                for (int ii = 0; ii < dsitem.Tables[0].Rows.Count; ii++)
                {
                    //tmplisthtml += @"<tr style=\'background-color:#6699ff\' ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                    tmplisthtml += @"<tr style=$$$background-color:#6699ff$$$ ><td>" + dsitem.Tables[0].Rows[ii]["ItemNo"].ToString().Trim() + "</td><td>" + dsitem.Tables[0].Rows[ii]["CName"].ToString().Trim().Replace("|", ";").Replace("\"", "!!!") + "</td><td>" + dsitem.Tables[0].Rows[ii]["EName"].ToString().Trim().Replace("|", ";") + "</td></tr>";
                }
                //tmplisthtml = @"<table border=\'0\' cellpadding=\'0\' cellspacing=\'1\' style=\'width: 300px;font-size:12px;background-color:#ffffff\' >" + tmplisthtml + "</table>";
                tmplisthtml = @"<table border=$$$0$$$ cellpadding=$$$0$$$ cellspacing=$$$1$$$ style=$$$width: 300px;font-size:12px;background-color:#ffffff$$$ >" + tmplisthtml + "</table>";
            }
            //tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
            tmpattributes = " style=\"background-color:Blue\" onmouseover=\"showpic('" + tmplisthtml + "');\" onmouseout=\"document.getElementById('FloatDiv').style.display = 'none';\" ";
            //tmptd = "<td " + tmpattributes + " width='" + (int)(100 / ListColCount) + "%'  ondblclick=\"SetSelectItemValue('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "," + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Replace("|", ";").Trim().Replace("\"", "!!!") + "," + tmpattributes1.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###") + "')\" ><input type='checkbox' name='checkbox_testitem' id='" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'>" + ds.Tables[0].Rows[i]["CName"].ToString().Trim().Trim().Replace("|", ";") + "</td>";
            return tmpattributes.Replace("\"", "!!!").Replace("('", @"@@@").Replace("')", @"###");

        }
        [AjaxPro.AjaxMethod]
        public string GetGroupSubItemListByClientNo(string ItemNo, string labcode)
        {
            string error;
            IBLL.Common.BaseDictionary.IBLab_TestItem testitem = BLLFactory<IBLab_TestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBLab_GroupItem groupitem = BLLFactory<IBLab_GroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(ItemNo.Trim(), labcode);
            string tmpSubItemList = groupitem.GetSubItemList_No_CName(ItemNo.Trim(), labcode);
            return tmpSubItemList.Substring(0, tmpSubItemList.Length - 1);
        }
        [AjaxPro.AjaxMethod]
        public string GetBarcode(string ClientNo)
        {
            IBBarCodeForm BarCodeForm = ZhiFang.BLLFactory.BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
            if (ClientNo.Trim() == "")
            {
                return "-1";
            }
            else
            {
                return BarCodeForm.GetNewBarCode(ClientNo);
            }
        }
        [AjaxPro.AjaxMethod]
        public string GetClientByBarcode(string barcode)
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("InputBarCde_Client_Index") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("InputBarCde_Client_Index") != "")
            {
                if (barcode.Trim() != "")
                {
                    string[] a = ZhiFang.Common.Public.ConfigHelper.GetConfigString("InputBarCde_Client_Index").Trim().Split(',');
                    if (a.Length == 2)
                    {
                        if (barcode.Length >= Convert.ToInt32(a[0]) + Convert.ToInt32(a[1]))
                        {
                            Model.CLIENTELE c = new Model.CLIENTELE();
                            IBLL.Common.BaseDictionary.IBCLIENTELE cbll = BLLFactory<IBCLIENTELE>.GetBLL();
                            c.WebLisSourceOrgId = barcode.Substring(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]));
                            DataSet ds = cbll.GetList(c);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                return ds.Tables[0].Rows[0]["CName"].ToString() + "," + ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                return "";
            }
            return "";
        }
        [AjaxPro.AjaxMethod]
        public string CheckBarcode(string barcode)
        {
            try
            {
                IBBarCodeForm bbf = BLLFactory.BLLFactory<IBBarCodeForm>.GetBLL();
                int i = bbf.GetTotalCount(new Model.BarCodeForm() { BarCodeFormNo = null, BarCode = barcode });
                return i.ToString();
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return "-1";
            }
        }
        #region 保存申请单
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string SaveTest(string txtApplyNO, string txtClientNo, string BarCodeInputFlag, string txtBarCode, string txtName, string txtAge, string SampleType, string txtCollecter, string hiddenClient, string hiddenSampleType, string SelectGenderType, string GenderName, string txtBingLiNO, string SelectAgeUnit, string AgeUnitName, string SelectFolkType, string FolkName, string hiddenDistrict, string DistrictName, string hiddenWardNo, string WardName, string txtBed, string hiddenDepartment, string DeptName, string hiddenDoctorNo, string DoctorName, string txtResult, string txtCharge, string Selectjztype, string ClinicTypeName, string DDLTestType, string TestTypeName, string CollectTime, string OperTime, string hiddenFlag, bool cacheflag, string selectitem, string SelectItemValue, string SelectCombiItemNo, string SelectCombiItemCName, string SelectCombiItemValue, string SelectAllTestItem)
        {
            string error;
            IBLL.Report.IBNRequestForm nrf = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            IBLL.Report.IBNRequestItem nri = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
            IBLL.Report.IBBarCodeForm bcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
            IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") != string.Empty)
            {
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                user.GetOrganizationsList();
                Model.NRequestForm nrf_m = new Model.NRequestForm();
                Model.NRequestItem nri_m = new Model.NRequestItem();
                Model.BarCodeForm bcf_m = new Model.BarCodeForm();
                #region 必填项
                if (txtApplyNO.Trim() == "")
                {
                    return "申请号不能为空！@txtApplyNO";
                }
                if (txtClientNo.Trim() == "")
                {
                    return "送检单位不能为空！@txtClientNo";
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
                        return "条码号不能为空！@txtBarCode";
                    }
                }
                if (txtName.Trim() == "")
                {
                    return "姓名不能为空！@txtName";
                }
                if (txtAge.Trim() == "")
                {
                    return "年龄不能为空！@txtAge";
                }
                if (SampleType.Trim() == "")
                {
                    return "样本类型不能为空！@SampleType";
                }
                if (txtCollecter.Trim() == "")
                {
                    return "采样人不能为空！@txtCollecter";
                }
                if (hiddenClient.Trim() == "")
                {
                    return "送检单位不能为空！@txtClientNo";
                }
                if (hiddenSampleType.Trim() == "")
                {
                    return "样本类型不能为空！@SampleType";
                }
                if (selectitem.Trim() == "")
                {
                    return "必须选择项目！";
                }
                #endregion
                #region 对象赋值
                string SerialNo = txtApplyNO.Replace(",", ",,");     //申请单号
                if (SerialNo == "")
                {
                    return "SerialNo为空！@txtApplyNO";
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

                        nri_m.WebLisSourceOrgID = ClientNo;
                        nri_m.WebLisSourceOrgName = txtClientNo;

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
                        return "时间格式有误！@txtCollectTime";
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
                string NRequestFormNo = "";
                DataSet dSet = new DataSet();
                string NewBarCode = "";
                #region 新增
                if (hiddenFlag == "0" || hiddenFlag == "")
                {
                    //插入新纪录
                    string NRequestFormNotmp = nrf.GetNCode(100);
                    nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNotmp);
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
                        nri_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                    }
                    else
                    {
                        //插入申请单失败
                        return "插入申请单失败！";
                    }

                    #region 生成条码号
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
                        return "没有查询到条码！";//没有查询到条码
                    }
                    else
                    {
                        bcf_m.BarCode = NewBarCode;
                    }
                    bcf_m.PrintCount = 0;
                    bcf_m.IsAffirm = 1;
                    string BarCodeFormNo = bcf.GetNewBarCodeFormNo(int.Parse(ClientNo));
                    bcf_m.BarCodeFormNo = long.Parse(BarCodeFormNo.Trim());
                    if (bcf.Add(bcf_m) > 0)
                    {
                        //ViewState["BarCodeFormNo"] = NewBarCode;
                        nri_m.BarCodeFormNo = long.Parse(BarCodeFormNo);
                    }
                    else
                    {
                        //插入条码号失败
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "alert('插入条码号失败！');", true);
                        return "插入条码号失败！";
                    }
                    #endregion
                }
                #endregion
                else
                #region 更新
                {
                    int UpDateFlag = nrf.Update(nrf_m);
                    Model.NRequestForm nrf_m1 = new Model.NRequestForm();
                    nrf_m1.SerialNo = SerialNo;
                    dSet = nrf.GetList(nrf_m1);
                    if (dSet != null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
                    {
                        NRequestFormNo = dSet.Tables[0].Rows[0][0].ToString();
                        nri_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                    }
                    else
                    {
                        //插入申请单失败
                        return "未找到所要更新的申请单！";
                    }
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
                #endregion

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
                        //nri_m.ParItemNo = int.Parse(FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim());
                        nri_m.ParItemNo = FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim();
                        if (nri.Add(nri_m) <= 0)
                        {
                            return "写入NRequestItem表错误！";
                        }
                    }
                    else
                    {
                        nri_m.CombiItemNo = FormList[Formi].Split(':')[1].ToString().Trim();
                        string[] tmpsub = FormList[Formi].Split(':')[0].ToString().Trim().Split(',');
                        if (tmpsub.Length >= 4 && (tmpsub[3].Trim() == "1" || tmpsub[3].Trim() == "2"))
                        {
                            string[] tmpsubitemlist = groupitem.GetSubItemList_No(tmpsub[0].Trim()).Split('|');
                            for (int i = 0; i < tmpsubitemlist.Length; i++)
                            {
                                //nri_m.ParItemNo = int.Parse(tmpsub[i].ToString().Trim());
                                nri_m.ParItemNo = tmpsub[i].ToString().Trim();
                                if (nri.Add(nri_m) <= 0)
                                {
                                    return "写入NRequestItem表错误！";
                                }
                            }
                        }
                        else
                        {
                            //nri_m.ParItemNo = int.Parse(tmpsub[0].Split(',')[0].ToString().Trim());
                            nri_m.ParItemNo = tmpsub[0].Split(',')[0].ToString().Trim();
                            if (nri.Add(nri_m) <= 0)
                            {
                                return "写入NRequestItem表错误！";
                            }
                        }
                    }
                }
                return "1@" + NewBarCode;
            }
            else
            {
                return "未登录，请登陆后继续！";
            }
        }
        #endregion
        #region 保存申请单
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string SaveTest_JiaYin(string clientNo, string txtApplyNO, string txtClientNo, string BarCodeInputFlag, string txtBarCode, string txtName, string txtAge, string SampleType, string txtCollecter, string hiddenClient, string ClientName, string hiddenSampleType, string SelectGenderType, string GenderName, string txtBingLiNO, string SelectAgeUnit, string AgeUnitName, string SelectFolkType, string FolkName, string hiddenDistrict, string DistrictName, string hiddenWardNo, string WardName, string txtBed, string hiddenDepartment, string DeptName, string hiddenDoctorNo, string DoctorName, string txtResult, string txtCharge, string Selectjztype, string ClinicTypeName, string DDLTestType, string TestTypeName, string CollectTime, string OperTime, string hiddenFlag, bool cacheflag, string selectitem, string SelectItemValue, string SelectCombiItemNo, string SelectCombiItemCName, string SelectCombiItemValue, string SelectAllTestItem, string txtService)
        {
            try
            {
                string error;
                IBNRequestForm nrf = ZhiFang.BLLFactory.BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
                IBNRequestItem nri = ZhiFang.BLLFactory.BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
                IBBarCodeForm bcf = ZhiFang.BLLFactory.BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
                IBTestItem testitem = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
                IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
                //B_DepartmentControl
                IBDepartmentControl decl = ZhiFang.BLLFactory.BLLFactory<IBDepartmentControl>.GetBLL("BaseDictionary.DepartmentControl");
                IBDistrictControl dicl = ZhiFang.BLLFactory.BLLFactory<IBDistrictControl>.GetBLL("BaseDictionary.DistrictControl");
                IBDoctorControl docl = ZhiFang.BLLFactory.BLLFactory<IBDoctorControl>.GetBLL("BaseDictionary.DoctorControl");
                IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") != string.Empty)
                {
                    ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                    ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId");
                    ZhiFang.Common.Log.Log.Info("hiddenClient:" + hiddenClient);
                    user.GetOrganizationsList();
                    Model.NRequestForm nrf_m = new Model.NRequestForm();
                    Model.NRequestItem nri_m = new Model.NRequestItem();
                    Model.BarCodeForm bcf_m = new Model.BarCodeForm();
                    Model.TestItemControl tic_m = new Model.TestItemControl();
                    Model.DoctorControl docl_m = new Model.DoctorControl();
                    Model.DistrictControl dicl_m = new Model.DistrictControl();
                    Model.DepartmentControl decl_m = new Model.DepartmentControl();
                    try
                    {
                        #region 必填项
                        if (txtApplyNO.Trim() == "")
                        {
                            return "申请号不能为空！@txtApplyNO";
                        }
                        //if (txtClientNo.Trim() == "")
                        //{
                        //    return "送检单位不能为空！@txtClientNo";
                        //}
                        if (BarCodeInputFlag.Trim().IndexOf('2') > -1)
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
                                return "条码号不能为空！@txtBarCode";
                            }
                        }
                        if (txtName.Trim() == "")
                        {
                            return "姓名不能为空！@txtName";
                        }
                        if (txtAge.Trim() == "")
                        {
                            return "年龄不能为空！@txtAge";
                        }
                        if (SampleType.Trim() == "")
                        {
                            return "样本类型不能为空！@SampleType";
                        }
                        if (txtCollecter.Trim() == "")
                        {
                            return "采样人不能为空！@txtCollecter";
                        }
                        //if (hiddenClient.Trim() == "")
                        //{
                        //    return "送检单位不能为空！@txtClientNo";
                        //}
                        if (hiddenSampleType.Trim() == "")
                        {
                            return "样本类型不能为空！@SampleType";
                        }
                        if (selectitem.Trim() == "")
                        {
                            return "必须选择项目！";
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
                            return "SerialNo为空！@txtApplyNO";
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
                        string strService = txtService;   //服务站
                        if (strService != "")
                        {
                            try { nrf_m.zdy5 = strService; nri_m.zdy5 = strService; }
                            catch { }
                        }
                        string DistrictNo = hiddenDistrict;//病区
                        if (DistrictNo != "")
                        {
                            try
                            {
                                nrf_m.DistrictNo = int.Parse(DistrictNo); nrf_m.DistrictName = DistrictName;
                                DataSet dicl_ds = new DataSet();
                                dicl_m.ControlLabNo = clientNo;
                                dicl_m.ControlDistrictNo = (int)nrf_m.DistrictNo;
                                dicl_ds = dicl.GetList(dicl_m);
                                nrf_m.DistrictNo = int.Parse(dicl_ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                            }
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
                            try
                            {
                                nrf_m.DeptNo = int.Parse(DeptNo); nrf_m.DeptName = DeptName;
                                DataSet decl_ds = new DataSet();
                                decl_m.ControlLabNo = clientNo;
                                decl_m.ControlDeptNo = (int)nrf_m.DeptNo;
                                decl_ds = decl.GetList(decl_m);
                                nrf_m.DeptNo = int.Parse(decl_ds.Tables[0].Rows[0]["DeptNo"].ToString());
                            }
                            catch { }
                            //ViewState["Department"] = this.SelectDepartment.Value.ToString();
                        }
                        string Doctor = hiddenDoctorNo;     //医生
                        if (Doctor != "")
                        {
                            try
                            {
                                nrf_m.Doctor = int.Parse(Doctor); nrf_m.DoctorName = DoctorName;
                                DataSet dcl_ds = new DataSet();
                                docl_m.ControlLabNo = clientNo;
                                docl_m.ControlDoctorNo = (int)nrf_m.Doctor;
                                dcl_ds = docl.GetList(docl_m);
                                nrf_m.Doctor = int.Parse(dcl_ds.Tables[0].Rows[0]["DoctorNo"].ToString());
                            }
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
                                nrf_m.WebLisSourceOrgID = clientNo;
                                nrf_m.WebLisSourceOrgName = ClientName;
                                nrf_m.ClientNo = hiddenClient;
                                nrf_m.ClientName = txtClientNo;

                                //ZhiFang.Common.Log.Log.Info("初始化WeblisOrgId:user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim() =" + user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim());

                                nri_m.WebLisOrgID = ClientNo;
                                nri_m.WebLisSourceOrgID = clientNo;
                                nri_m.WebLisSourceOrgName = ClientName;

                                bcf_m.WebLisOrgID = ClientNo;
                                bcf_m.WebLisSourceOrgId = clientNo;
                                bcf_m.WebLisSourceOrgName = ClientName;
                                bcf_m.ClientNo = hiddenClient;
                                bcf_m.ClientName = txtClientNo;
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Error(ex.ToString());
                            }
                            //ViewState["ClientName"] = txtClientNo.Value.Trim();
                        }
                        else
                        {
                            nrf_m.ClientNo = clientNo;
                            nrf_m.ClientName = ClientName;
                            nri_m.ClientName = ClientName;
                            nri_m.WebLisOrgID = clientNo;
                            bcf_m.ClientName = clientNo;
                            bcf_m.ClientNo = clientNo;
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
                                return "时间格式有误！@txtCollectTime";
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
                    string NRequestFormNo = "";
                    DataSet dSet = new DataSet();
                    string NewBarCode = "";
                    if (hiddenFlag == "0" || hiddenFlag == "")
                    {
                        #region 新增
                        try
                        {
                            //插入新纪录
                            string NRequestFormNotmp = nrf.GetNCode(100);
                            try
                            {
                                nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNotmp);
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNotmp=" + NRequestFormNotmp);
                                return "程序异常！";
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
                                    ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNo=" + NRequestFormNo);
                                    return "程序异常！";
                                }
                            }
                            else
                            {
                                //插入申请单失败
                                return "插入申请单失败！";
                            }

                            #region 生成条码号
                            //取得当前最大条码号

                            if (BarCodeInputFlag.Trim().IndexOf('2') > -1)
                            {
                                NewBarCode = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
                                //NewBarCode = bcf.GetNewBarCode(ClientName);
                            }
                            else
                            {
                                NewBarCode = txtBarCode;
                            }

                            if (NewBarCode == "")
                            {
                                return "没有查询到条码！";//没有查询到条码
                            }
                            else
                            {
                                bcf_m.BarCode = NewBarCode;
                            }
                            bcf_m.PrintCount = 0;
                            bcf_m.IsAffirm = 1;

                            string BarCodeFormNo = bcf.GetNewBarCodeFormNo(int.Parse(clientNo));
                            try
                            {
                                bcf_m.BarCodeFormNo = Int64.Parse(BarCodeFormNo.Trim());

                                //string BarCodeFormNo = bcf.Add_ReturnBarCodeFormNo(bcf_m);
                                if (bcf.Add(bcf_m) > 0)
                                {
                                    //ViewState["BarCodeFormNo"] = NewBarCode;

                                    nri_m.BarCodeFormNo = Int64.Parse(BarCodeFormNo);
                                }
                                else
                                {
                                    //插入条码号失败
                                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "alert('插入条码号失败！');", true);
                                    return "插入条码号失败！";
                                }
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNotmp=" + NRequestFormNotmp + ",BarCodeFormNo=" + BarCodeFormNo + "错误信息:" + eee.ToString());
                                return "程序异常！";
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
                        #region 更新
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
                                    ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo);
                                    return "程序异常！";
                                }
                            }
                            else
                            {
                                //插入申请单失败
                                return "未找到所要更新的申请单！";
                            }
                            try
                            {
                                nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo);
                                return "程序异常！";
                            }
                            int UpDateFlag = nrf.Update(nrf_m);
                            NewBarCode = txtBarCode;
                            bcf_m.BarCode = txtBarCode;

                            DataSet dtItem = nri.GetList(new Model.NRequestItem { NRequestFormNo = long.Parse(NRequestFormNo) });
                            string BarCodeLists = "";
                            if (dtItem != null && dtItem.Tables.Count > 0 && dtItem.Tables[0].Rows.Count > 0)
                            {
                                for (int b = 0; b < dtItem.Tables[0].Rows.Count; b++)
                                {
                                    BarCodeLists += dtItem.Tables[0].Rows[b]["BarCodeFormNo"].ToString() + ",";

                                    bcf_m.BarCodeFormNo = Convert.ToInt64(dtItem.Tables[0].Rows[b]["BarCodeFormNo"].ToString());
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
                            //nri_m.ParItemNo = int.Parse(FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim());
                            nri_m.ParItemNo = FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim();
                            tic_m.ControlItemNo = nri_m.ParItemNo.ToString();
                            tic_m.ControlLabNo = hiddenClient;
                            DataSet tic_ds = new DataSet();
                            //芜湖录入中心 不转码
                            //tic_ds = tic.GetList(tic_m);
                            //nri_m.ParItemNo = int.Parse(tic_ds.Tables[0].Rows[0]["ItemNo"].ToString());

                            if (nri.Add(nri_m) <= 0)
                            {
                                return "写入NRequestItem表错误！";
                            }
                        }
                        else
                        {
                            nri_m.CombiItemNo = FormList[Formi].Split(':')[1].ToString().Trim();
                            string[] tmpsub = FormList[Formi].Split(':')[0].ToString().Trim().Split(',');
                            if (tmpsub.Length >= 4 && (tmpsub[3].Trim() == "1" || tmpsub[3].Trim() == "2"))
                            {
                                string[] tmpsubitemlist = groupitem.GetSubItemList_No(tmpsub[0].Trim()).Split('|');
                                for (int i = 0; i < tmpsubitemlist.Length; i++)
                                {
                                    //nri_m.ParItemNo = int.Parse(tmpsub[i].ToString().Trim());
                                    nri_m.ParItemNo = tmpsub[i].ToString().Trim();
                                    tic_m.ControlItemNo = nri_m.ParItemNo.ToString();
                                    tic_m.ControlLabNo = hiddenClient;
                                    DataSet tic_ds = new DataSet();
                                    //芜湖录入中心 不转码
                                    //tic_ds = tic.GetList(tic_m);
                                    //nri_m.ParItemNo = int.Parse(tic_ds.Tables[0].Rows[0]["ItemNo"].ToString());
                                    if (nri.Add(nri_m) <= 0)
                                    {
                                        return "写入NRequestItem表错误！";
                                    }
                                }
                            }
                            else
                            {
                                if (nri_m.CombiItemNo != null && FormList[Formi].Split(':')[0].ToString().Trim().Split(',').Length > 1)
                                {
                                    //nri_m.ParItemNo = int.Parse(FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim());
                                    nri_m.ParItemNo = FormList[Formi].Split(':')[0].ToString().Trim().Split(',')[0].Trim();
                                    tic_m.ControlItemNo = nri_m.ParItemNo.ToString();
                                    tic_m.ControlLabNo = hiddenClient;
                                    DataSet tic_ds = new DataSet();
                                    //tic_ds = tic.GetList(tic_m);
                                    //nri_m.ParItemNo = int.Parse(tic_ds.Tables[0].Rows[0]["ItemNo"].ToString());
                                    DataSet tmpds = nri.GetList(new Model.NRequestItem { NRequestFormNo = nri_m.NRequestFormNo, ParItemNo = nri_m.ParItemNo });
                                    if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
                                    {

                                    }
                                    else
                                    {
                                        if (nri.Add(nri_m) <= 0)
                                        {
                                            return "写入NRequestItem表错误！";
                                        }
                                    }
                                }

                            }
                        }
                    }
                    return "1@" + NewBarCode;
                }
                else
                {
                    return "未登录，请登陆后继续！";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("程序异常！详细信息：" + e.ToString());
                return "程序异常！";
            }
        }
        #endregion
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string SaveTestByClientNoAndMoreBarcode(string txtApplyNO, string txtClientNo, string BarCodeInputFlag, string txtBarCode, string txtName, string txtAge, string SampleType, string txtCollecter, string hiddenClient, string hiddenSampleType, string SelectGenderType, string GenderName, string txtBingLiNO, string SelectAgeUnit, string AgeUnitName, string SelectFolkType, string FolkName, string hiddenDistrict, string DistrictName, string hiddenWardNo, string WardName, string txtBed, string hiddenDepartment, string DeptName, string hiddenDoctorNo, string DoctorName, string txtResult, string txtCharge, string Selectjztype, string ClinicTypeName, string DDLTestType, string TestTypeName, string CollectTime, string OperTime, string hiddenFlag, bool cacheflag, string selectitem, string SelectItemValue, string SelectCombiItemNo, string SelectCombiItemCName, string SelectCombiItemValue, string SelectAllTestItem)
        {
            //ZhiFang.Common.Log.Log.Info("SelectItemValue：" + SelectItemValue);
            //ZhiFang.Common.Log.Log.Info("SelectCombiItemNo：" + SelectCombiItemNo);
            //ZhiFang.Common.Log.Log.Info("SelectCombiItemCName：" + SelectCombiItemCName);
            //ZhiFang.Common.Log.Log.Info("SelectCombiItemValue：" + SelectCombiItemValue);
            //ZhiFang.Common.Log.Log.Info("SelectAllTestItem：" + SelectAllTestItem);
            ZhiFang.Common.Log.Log.Info("selectitem：" + selectitem);
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

                    user.GetOrganizationsList();
                    Model.NRequestForm nrf_m = new Model.NRequestForm();
                    Model.NRequestItem nri_m = new Model.NRequestItem();
                    Model.BarCodeForm bcf_m = new Model.BarCodeForm();
                    ZhiFang.Common.Log.Log.Info("验证必填阶段Star");
                    try
                    {
                        #region 必填项
                        if (txtApplyNO.Trim() == "")
                        {
                            return "申请号不能为空！@txtApplyNO";
                        }
                        if (txtClientNo.Trim() == "")
                        {
                            return "送检单位不能为空！@txtClientNo";
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
                                return "条码号不能为空！@txtBarCode";
                            }
                        }
                        if (txtName.Trim() == "")
                        {
                            return "姓名不能为空！@txtName";
                        }
                        if (txtAge.Trim() == "")
                        {
                            return "年龄不能为空！@txtAge";
                        }
                        //if (SelectGenderType.Trim() == "")
                        //{
                        //    return "性别不能为空！@SelectGenderType";
                        //}
                        //if (SampleType.Trim() == "")
                        //{
                        //    return "样本类型不能为空！@SampleType";
                        //}
                        if (txtCollecter.Trim() == "")
                        {
                            return "采样人不能为空！@txtCollecter";
                        }
                        if (hiddenClient.Trim() == "")
                        {
                            return "送检单位不能为空！@txtClientNo";
                        }
                        //if (hiddenSampleType.Trim() == "")
                        //{
                        //    return "样本类型不能为空！@SampleType";
                        //}
                        ZhiFang.Common.Log.Log.Info("项目：参数selectitem" + selectitem);
                        if (selectitem.Trim() == "")
                        {
                            return "必须选择项目！";
                        }


                        #endregion
                    }
                    catch (Exception ebt)
                    {
                        ZhiFang.Common.Log.Log.Debug("必填项异常！详细信息：" + ebt.ToString());
                        throw ebt;
                    }
                    ZhiFang.Common.Log.Log.Info("验证必填阶段End");
                    ZhiFang.Common.Log.Log.Info("赋值阶段Star");
                    try
                    {
                        #region 对象赋值
                        string SerialNo = txtApplyNO.Replace(",", ",,").Replace("‘", "’‘");     //申请单号
                        if (SerialNo == "")
                        {
                            return "SerialNo为空！@txtApplyNO";
                        }
                        else
                        {
                            nrf_m.SerialNo = SerialNo;
                        }
                        string SampleTypeNo = hiddenSampleType; //样本类型
                        if (SampleTypeNo != "")
                        {
                            try { nrf_m.SampleTypeNo = int.Parse(SampleTypeNo.Trim()); }
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
                                return "时间格式有误！@txtCollectTime";
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
                    ZhiFang.Common.Log.Log.Info("赋值阶段End");
                    ZhiFang.Common.Log.Log.Info("录入阶段Star");
                    //插入新纪录
                    string NRequestFormNotmp = "";// nrf.GetNCode(100);
                    //BLL出错 临时解决 谢鑫
                    NRequestFormNotmp = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();

                    ZhiFang.Common.Log.Log.Info("NRequestFormNotmp：" + NRequestFormNotmp);
                    string NRequestFormNo = "";
                    DataSet dSet = new DataSet();
                    string NewBarCode = "";
                    Model.NRequestForm nrf_m1 = new Model.NRequestForm();
                    nrf_m1.SerialNo = nrf_m.SerialNo;
                    ZhiFang.Common.Log.Log.Info("nrf_m1.SerialNo:" + nrf_m1.SerialNo);
                    DataSet dt = nrf.GetList(nrf_m1);
                    if (dt.Tables[0].Rows.Count == 0)
                    {
                        ZhiFang.Common.Log.Log.Info("NRequestFormNotmp:ToInt64;" + Convert.ToInt64(NRequestFormNotmp));
                        nrf_m.NRequestFormNo = Convert.ToInt64(NRequestFormNotmp);

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("dt.Tables[0].Rows[0][\"NRequestFormNo\"]:ToInt64;" + dt.Tables[0].Rows[0]["NRequestFormNo"]);
                        nrf_m.NRequestFormNo = Convert.ToInt64(dt.Tables[0].Rows[0]["NRequestFormNo"]);
                    }
                    nri_m.NRequestFormNo = nrf_m.NRequestFormNo;
                    ZhiFang.Common.Log.Log.Info("录入Item阶段Star：hiddenFlag：" + hiddenFlag);

                    #region 项目对象化
                    ZhiFang.Common.Log.Log.Info("项目对象化开始");
                    Dictionary<string, Dictionary<string, string[]>> inputitem = new Dictionary<string, Dictionary<string, string[]>>();
                    string itemlist = "";
                    string[] FormList = selectitem.Split(';');
                    foreach (var a in selectitem.Split(';'))
                    {
                        if (a.Trim() != "")
                        {
                            var tmpv = a.Split(':');
                            if (tmpv.Length > 1 && tmpv[1].Trim() != "")
                            {
                                string iname = tmpv[0].Split(',')[1];
                                string icode = tmpv[0].Split(',')[0];
                                string icolor = tmpv[0].Split(',')[2];
                                if (inputitem.ContainsKey(tmpv[1].Trim()))
                                {
                                    if (!inputitem[tmpv[1]].ContainsKey(icode))
                                    {
                                        inputitem[tmpv[1]].Add(icode, new string[] { iname, icolor });
                                    }
                                }
                                else
                                {
                                    itemlist += tmpv[1].Trim() + ",";
                                    var aaa = new Dictionary<string, string[]>();
                                    aaa.Add(icode, new string[] { iname, icolor });
                                    inputitem.Add(tmpv[1].Trim(), aaa);
                                }
                                itemlist += icode + ",";
                            }
                            else
                            {
                                string iname = tmpv[1].Split(',')[1];
                                string icode = tmpv[1].Split(',')[0];
                                string icolor = tmpv[0].Split(',')[2];
                                var aaa = new Dictionary<string, string[]>();
                                aaa.Add(icode, new string[] { iname, icolor });
                                inputitem.Add(icode, aaa);
                                itemlist += icode + ",";
                            }
                        }
                    }
                    ZhiFang.Common.Log.Log.Info("项目对象化结束");
                    #endregion

                    if (hiddenFlag == "0" || hiddenFlag == "")
                    {
                        #region 新增NRequestForm
                        ZhiFang.Common.Log.Log.Info("新增");
                        try
                        {
                            #region 存储条码号
                            //取得当前最大条码号
                            ZhiFang.Common.Log.Log.Info("存储条码号开始");
                            NewBarCode = txtBarCode;
                            Dictionary<string, List<string>> dicbarcode = new Dictionary<string, List<string>>();
                            var vBarcode = NewBarCode.Split('|');
                            #region 重复条码验证(参数条码判断)
                            if (vBarcode.Length != vBarcode.GroupBy(a => a.Split(':')[1]).Count())
                            {
                                return "插入条码号失败！输入的条码号重复！";
                                ZhiFang.Common.Log.Log.Info("插入条码号失败！输入的条码号重复！" + NewBarCode);
                            }
                            #endregion
                            #region 重复条码验证(数据库条码判断)
                            foreach (string bar in vBarcode)
                            {
                                string BarCode = bar.Split(':')[1];
                                if (bcf.GetTotalCount(new Model.BarCodeForm() { BarCode = BarCode }) > 0)
                                {
                                    return "插入条码号失败！条码号=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[1] + "'--条码号重复！";
                                    ZhiFang.Common.Log.Log.Info("插入条码号失败！条码号=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "'--条码号重复！");
                                }
                            }
                            #endregion
                            foreach (string bar in vBarcode)
                            {
                                string BarCodeFormNo = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
                                try
                                {
                                    bcf_m.BarCodeFormNo = long.Parse(BarCodeFormNo.Trim());
                                    bcf_m.BarCode = bar.Split(':')[1];
                                    if (bcf.Add(bcf_m) <= 0)
                                    {
                                        return "插入条码号失败！";
                                        ZhiFang.Common.Log.Log.Info("插入条码号失败！");
                                    }
                                    dicbarcode.Add(bar.Split(':')[0], new List<string>() { BarCodeFormNo, bar.Split(':')[2] });
                                    ZhiFang.Common.Log.Log.Info("插入条码号成功！Barcode=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "'");
                                }
                                catch (Exception eee)
                                {
                                    ZhiFang.Common.Log.Log.Debug("条码编码：Barcode=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "',BarCodeFormNo=" + BarCodeFormNo, eee);
                                    return "程序异常！";
                                }
                            }
                            ZhiFang.Common.Log.Log.Info("存储条码号结束");
                            #endregion

                            #region 存储项目
                            ZhiFang.Common.Log.Log.Info("存储项目开始");
                            string PclientNo = CEA.GetPClientNoBySClientNo(hiddenClient);
                            DataSet dsitemmap = tic.GetCenterCodeMapList(PclientNo, itemlist);
                            ZhiFang.Common.Log.Log.Info("所属区域主实验室编码：" + PclientNo + "@@@ItemList:" + itemlist);
                            //项目字典对照集合
                            //Dictionary<string, string> dicItemMap = new Dictionary<string, string>();
                            foreach (var pi in inputitem)
                            {
                                if (dsitemmap != null && dsitemmap.Tables.Count > 0 && dsitemmap.Tables[0].Rows.Count > 0)
                                {
                                    if (dsitemmap.Tables[0].Select(" ControlItemNo='" + pi.Key + "' ").Count() > 0)
                                    {
                                        nri_m.CombiItemNo = dsitemmap.Tables[0].Select(" ControlItemNo='" + pi.Key + "' ").ElementAt(0)["ItemNo"].ToString();
                                    }
                                    else
                                    {
                                        ZhiFang.Common.Log.Log.Info("未取得实验室项目对照对照信息！所属区域主实验室编码：" + PclientNo + "@@@ControlItemNo:" + pi.Key);
                                    }
                                }

                                foreach (var i in pi.Value)
                                {
                                    if (dsitemmap.Tables[0].Select(" ControlItemNo='" + i.Key + "' ").Count() > 0)
                                    {
                                        nri_m.ParItemNo = dsitemmap.Tables[0].Select(" ControlItemNo='" + i.Key + "' ").ElementAt(0)["ItemNo"].ToString();
                                        if (dicbarcode.ContainsKey(i.Value[1]))
                                        {
                                            var barcode = dicbarcode[i.Value[1]];
                                            nri_m.BarCodeFormNo = long.Parse(barcode[0]);
                                            nri_m.SampleTypeNo = int.Parse(barcode[1]);
                                            if (nri.Add(nri_m) <= 0)
                                            {
                                                return "写入NRequestItem表错误！";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ZhiFang.Common.Log.Log.Info("未取得实验室项目对照对照信息！所属区域主实验室编码：" + PclientNo + "@@@ControlItemNo:" + i.Key);
                                    }
                                }
                            }
                            #endregion
                        }
                        catch (Exception ee)
                        {
                            ZhiFang.Common.Log.Log.Debug("新增异常！详细信息：" + ee.ToString());
                            throw ee;
                        }

                        ZhiFang.Common.Log.Log.Info("新增NRequestForm开始");
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
                                    return "程序异常！";
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
                                        return "程序异常！";
                                    }
                                }
                                else
                                {
                                    //插入申请单失败
                                    return "插入申请单失败！";
                                }
                            }
                            else
                            {
                                nrf_m.NRequestFormNo = Convert.ToInt64(dt.Tables[0].Rows[0]["NRequestFormNo"]);
                            }
                        }
                        catch (Exception ebt)
                        {
                            ZhiFang.Common.Log.Log.Debug("新增异常！详细信息：" + ebt.ToString());
                            throw ebt;
                        }
                        ZhiFang.Common.Log.Log.Info("新增NRequestForm结束");
                        #endregion
                    }
                    else
                    {
                        #region 更新NRequestForm
                        ZhiFang.Common.Log.Log.Info("修改");
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
                                    return "程序异常！";
                                }
                            }
                            else
                            {
                                //插入申请单失败
                                return "未找到所要更新的申请单！";
                            }
                            try
                            {
                                nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo, eee);
                                return "程序异常！";
                            }
                            int UpDateFlag = nrf.Update(nrf_m);
                            //NewBarCode = txtBarCode;
                            //bcf_m.BarCode = txtBarCode;
                            //bcf.Update(bcf_m);
                            //ViewState["BarCodeFormNo"] = GetPara("BarCodeNo");

                            #region 存储条码号
                            NewBarCode = txtBarCode;
                            Dictionary<string, List<string>> dicbarcode = new Dictionary<string, List<string>>();
                            var vBarcode = NewBarCode.Split('|');
                            #region 重复条码验证(参数条码判断)
                            if (vBarcode.Length != vBarcode.GroupBy(a => a.Split(':')[1]).Count())
                            {
                                return "插入条码号失败！输入的条码号重复！";
                                ZhiFang.Common.Log.Log.Info("插入条码号失败！输入的条码号重复！" + NewBarCode);
                            }
                            #endregion
                            if (bcf.DeleteBarCodeByNRequestFormNo(NRequestFormNo))
                            {
                                #region 重复条码验证(数据库条码判断)
                                foreach (string bar in vBarcode)
                                {
                                    string BarCode = bar.Split(':')[1];
                                    if (bcf.GetTotalCount(new Model.BarCodeForm() { BarCode = BarCode }) > 0)
                                    {
                                        return "插入条码号失败！条码号=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[1] + "'--条码号重复！";
                                        ZhiFang.Common.Log.Log.Info("插入条码号失败！条码号=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "'--条码号重复！");
                                    }
                                }
                                #endregion
                                ZhiFang.Common.Log.Log.Info("存储条码号开始");
                                foreach (string bar in vBarcode)
                                {
                                    string BarCodeFormNo = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
                                    try
                                    {
                                        bcf_m.BarCodeFormNo = long.Parse(BarCodeFormNo.Trim());
                                        bcf_m.BarCode = bar.Split(':')[1];
                                        if (bcf.Add(bcf_m) <= 0)
                                        {
                                            return "插入条码号失败！";
                                            ZhiFang.Common.Log.Log.Info("插入条码号失败！");
                                        }
                                        dicbarcode.Add(bar.Split(':')[0], new List<string>() { BarCodeFormNo, bar.Split(':')[2] });
                                        ZhiFang.Common.Log.Log.Info("插入条码号成功！Barcode=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "'");
                                    }
                                    catch (Exception eee)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("条码编码：Barcode=" + bar.Split(':')[1] + ",条码颜色='" + bar.Split(':')[0] + "',BarCodeFormNo=" + BarCodeFormNo, eee);
                                        return "程序异常！";
                                    }
                                }
                                ZhiFang.Common.Log.Log.Info("存储条码号结束");
                            #endregion
                                if (nri.DeleteList_ByNRequestFormNo(long.Parse(NRequestFormNo)))
                                {
                                    #region 存储项目
                                    ZhiFang.Common.Log.Log.Info("存储项目开始");
                                    string PclientNo = CEA.GetPClientNoBySClientNo(hiddenClient);
                                    DataSet dsitemmap = tic.GetCenterCodeMapList(PclientNo, itemlist);
                                    //项目字典对照集合
                                    //Dictionary<string, string> dicItemMap = new Dictionary<string, string>();
                                    foreach (var pi in inputitem)
                                    {
                                        if (dsitemmap != null && dsitemmap.Tables.Count > 0 && dsitemmap.Tables[0].Rows.Count > 0)
                                        {
                                            if (dsitemmap.Tables[0].Select(" ControlItemNo=" + pi.Key).Count() > 0)
                                            {
                                                nri_m.CombiItemNo = dsitemmap.Tables[0].Select(" ControlItemNo=" + pi.Key).ElementAt(0)["ItemNo"].ToString();
                                            }
                                        }

                                        foreach (var i in pi.Value)
                                        {
                                            nri_m.ParItemNo = dsitemmap.Tables[0].Select(" ControlItemNo=" + i.Key).ElementAt(0)["ItemNo"].ToString();

                                            if (dicbarcode.ContainsKey(i.Value[1]))
                                            {
                                                var barcode = dicbarcode[i.Value[1]];
                                                nri_m.BarCodeFormNo = long.Parse(barcode[0]);
                                                nri_m.SampleTypeNo = int.Parse(barcode[1]);

                                                if (nri.Add(nri_m) <= 0)
                                                {
                                                    return "写入NRequestItem表错误！";
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    return "项目删除失败！";
                                }
                            }
                            else
                            {
                                return "条码删除失败！";
                            }

                        }
                        catch (Exception ebt)
                        {
                            ZhiFang.Common.Log.Log.Debug("更新异常！详细信息：" + ebt.ToString());
                            throw ebt;
                        }
                        #endregion
                    }

                    return "1@" + NewBarCode;
                }
                else
                {
                    return "未登录，请登陆后继续！";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("程序异常！详细信息：" + e.ToString());
                return "程序异常！";
            }
        }
        #region 保存申请单—客户
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string SaveTestByClientNo(string txtApplyNO, string txtClientNo, string BarCodeInputFlag, string txtBarCode, string txtName, 
            string txtAge, string SampleType, string txtCollecter, string hiddenClient, string hiddenSampleType, string SelectGenderType, string GenderName, 
            string txtBingLiNO, string SelectAgeUnit, string AgeUnitName, string SelectFolkType, string FolkName, string hiddenDistrict, string DistrictName, string hiddenWardNo, 
            string WardName, string txtBed, string hiddenDepartment, string DeptName, string hiddenDoctorNo, string DoctorName, string txtResult, string txtCharge,
            string Selectjztype, string ClinicTypeName, string DDLTestType, string TestTypeName, string CollectTime, string OperTime, 
            string hiddenFlag, bool cacheflag, string selectitem, string SelectItemValue, string SelectCombiItemNo, string SelectCombiItemCName, 
            string SelectCombiItemValue, string SelectAllTestItem)
        {
            try
            {
                //string error;
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
                            return "申请号不能为空！@txtApplyNO";
                        }
                        if (txtClientNo.Trim() == "")
                        {
                            return "送检单位不能为空！@txtClientNo";
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
                                return "条码号不能为空！@txtBarCode";
                            }
                        }
                        if (txtName.Trim() == "")
                        {
                            return "姓名不能为空！@txtName";
                        }
                        if (txtAge.Trim() == "")
                        {
                            return "年龄不能为空！@txtAge";
                        }
                        if (SampleType.Trim() == "")
                        {
                            return "样本类型不能为空！@SampleType";
                        }
                        if (txtCollecter.Trim() == "")
                        {
                            return "采样人不能为空！@txtCollecter";
                        }
                        if (hiddenClient.Trim() == "")
                        {
                            return "送检单位不能为空！@txtClientNo";
                        }
                        if (hiddenSampleType.Trim() == "")
                        {
                            return "样本类型不能为空！@SampleType";
                        }
                        if (selectitem.Trim() == "")
                        {
                            return "必须选择项目！";
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
                            return "SerialNo为空！@txtApplyNO";
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
                                return "时间格式有误！@txtCollectTime";
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
                    string NRequestFormNo = "";
                    DataSet dSet = new DataSet();
                    string NewBarCode = "";
                    if (hiddenFlag == "0" || hiddenFlag == "")
                    {
                        #region 新增
                        try
                        {
                            DataSet dt = nrf.GetList(nrf_m);
                            //插入新纪录
                            string NRequestFormNotmp = nrf.GetNCode(100);
                            try
                            {
                                nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNotmp);
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNotmp=" + NRequestFormNotmp, eee);
                                return "程序异常！";
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
                                    return "程序异常！";
                                }
                            }
                            else
                            {
                                //插入申请单失败
                                return "插入申请单失败！";
                            }

                            #region 生成条码号
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
                                return "没有查询到条码！";//没有查询到条码
                            }
                            else
                            {
                                bcf_m.BarCode = NewBarCode;
                            }
                            bcf_m.PrintCount = 0;
                            bcf_m.IsAffirm = 1;

                            string BarCodeFormNo = bcf.GetNewBarCodeFormNo(int.Parse(hiddenClient));
                            try
                            {
                                bcf_m.BarCodeFormNo = long.Parse(BarCodeFormNo.Trim());

                                //string BarCodeFormNo = bcf.Add_ReturnBarCodeFormNo(bcf_m);
                                if (bcf.Add(bcf_m) > 0)
                                {
                                    //ViewState["BarCodeFormNo"] = NewBarCode;

                                    nri_m.BarCodeFormNo = long.Parse(BarCodeFormNo);
                                }
                                else
                                {
                                    //插入条码号失败
                                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "alert('插入条码号失败！');", true);
                                    return "插入条码号失败！";
                                }
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("申请单编码：NRequestFormNotmp=" + NRequestFormNotmp + ",BarCodeFormNo=" + BarCodeFormNo, eee);
                                return "程序异常！";
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
                        #region 更新
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
                                    ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo);
                                    return "程序异常！";
                                }
                            }
                            else
                            {
                                //插入申请单失败
                                return "未找到所要更新的申请单！";
                            }
                            try
                            {
                                nrf_m.NRequestFormNo = Int64.Parse(NRequestFormNo);
                            }
                            catch (Exception eee)
                            {
                                ZhiFang.Common.Log.Log.Debug("更新申请单编码：NRequestFormNo=" + NRequestFormNo);
                                return "程序异常！";
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
                                return "写入NRequestItem表错误！";
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
                                //nri_m.ParItemNo = nri_m.CombiItemNo.Value;
                                nri_m.ParItemNo = nri_m.CombiItemNo.ToString();
                                DataSet tmpds = nri.GetList(new Model.NRequestItem { NRequestFormNo = nri_m.NRequestFormNo, ParItemNo = nri_m.ParItemNo });
                                if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    if (nri.Add(nri_m) <= 0)
                                    {
                                        return "写入NRequestItem表错误！";
                                    }
                                }
                            }

                            //}
                        }
                    }
                    return "1@" + NewBarCode;
                }
                else
                {
                    return "未登录，请登陆后继续！";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("程序异常！详细信息：" + e.ToString());
                return "程序异常！";
            }
        }
        #endregion
        #region 打印条码
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string PrintBarCode(string barcode)
        {
            try
            {
                IBBarCodeForm ibbf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
                DataSet ds = ibbf.GetList(new Model.BarCodeForm() { BarCode = barcode.ToString().Trim() });
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains("PrintCount") && ds.Tables[0].Rows[0]["PrintCount"].ToString().Trim() != "")
                {
                    return ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCount"]) + 1, BarCode = barcode.ToString().Trim() }).ToString();
                }
                else
                {
                    return ibbf.UpdateByBarCode(new Model.BarCodeForm() { PrintCount = 1, BarCode = barcode.ToString().Trim() }).ToString();
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return "-1";
            }
        }
        #endregion
        [AjaxPro.AjaxMethod]
        public string aaa(string SuperGroupNo, string ItemKey, int ListRowCount, int ListColCount, int PageIndex)
        {
            return SuperGroupNo + "@@@" + ItemKey + "@@@" + ListRowCount + "@@@" + ListColCount + "@@@" + PageIndex + "@@@";
        }
        [AjaxPro.AjaxMethod]
        public string GetTestItemColor(string itemno, string labcode)
        {
            try
            {
                IBLab_TestItem blti = BLLFactory<IBLab_TestItem>.GetBLL();
                string color = blti.GetGroupItemColor(itemno, labcode);
                color = color.Substring(0, color.Length - 1);
                return color;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 采样管颜色条码
        /// </summary>
        /// <param name="itemno"></param>
        /// <param name="labcode"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetTestItemColorTotal(string itemno, string labcode)
        {
            ZhiFang.Common.Log.Log.Info("采样管颜色条码,项目编码" + itemno + "医疗机构编码:" + labcode);
            try
            {
                string colorarry = "";
                IBLab_TestItem blti = BLLFactory<IBLab_TestItem>.GetBLL();
                Dictionary<string, string> color = blti.GetColor(itemno, labcode);
                foreach (var l in color)
                {
                    colorarry += l.Key + "=" + l.Value.ToString() + "|";
                    ZhiFang.Common.Log.Log.Info("颜色:" + colorarry);
                }
                if (colorarry != "")
                {
                    ZhiFang.Common.Log.Log.Info("return颜色:" + colorarry.Remove(colorarry.LastIndexOf('|')));
                    return colorarry.Remove(colorarry.LastIndexOf('|'));
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("颜色Error：" + e.ToString());
                return null;
            }
        }
        [AjaxPro.AjaxMethod]
        public string GetItemColor(string itemno, string labcode)
        {
            IBLL.Common.BaseDictionary.IBGroupItem groupitem = BLLFactory<IBGroupItem>.GetBLL();
            DataSet dsitem = groupitem.GetGroupItemList(itemno.Trim());
            if (dsitem != null || dsitem.Tables[0].Rows.Count > 0)
            {

            }
            return null;
        }
        [AjaxPro.AjaxMethod]
        public string GetItemSampleTypeListByColorValue(string ColorValue)
        {
            Dictionary<string, ZhiFang.BLL.Common.ColorSampleType> itemcolor = ZhiFang.BLL.Common.Lib.ItemColor();
            var colorsampletype = itemcolor.Values.Where(a => a.ColorValue == ColorValue);
            if (colorsampletype.Count() > 0)
            {
                string SampleTypeList = "";
                foreach (var sampletype in colorsampletype.ElementAt(0).SampleType)
                {
                    SampleTypeList += "{CName:\"" + sampletype.CName.ToString().Trim() + "\",SampleTypeID:" + sampletype.SampleTypeID.ToString().Trim() + "},";

                }
                if (SampleTypeList != "")
                {
                    SampleTypeList = SampleTypeList.Remove(SampleTypeList.Length - 1);
                }
                SampleTypeList = "[" + SampleTypeList + "]";
                return SampleTypeList;
            }
            return null;
        }
        /// <summary>
        /// 根据客户编码获取区域ID
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
        /// 根据区域ID获取区域总客户编码
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
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetDictionaryList(string DictionaryName, string Field_value, string Field_name)
        {
            IBLL.Common.BaseDictionary.IBSampleType ibst = BLLFactory<IBSampleType>.GetBLL();
            Model.SampleType SampleType = new Model.SampleType();
            DataSet ds = ibst.GetAllList();
            string sampleList = "";
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Columns.Contains(Field_value) && dt.Columns.Contains(Field_name))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sampleList += "{Name:\"" + ds.Tables[0].Rows[i][Field_name].ToString().Trim() + "\",Value:" + ds.Tables[0].Rows[i][Field_value].ToString().Trim() + "},";//ds.Tables[0].Rows[i]["CName"].ToString().Trim() + ",";
                    }
                    if (sampleList != "")
                    {
                        sampleList = sampleList.Remove(sampleList.Length - 1);
                    }
                    sampleList = "[" + sampleList + "]";
                }
                else
                {
                    return "-1";
                }
            }
            return sampleList;
        }
    }
}
