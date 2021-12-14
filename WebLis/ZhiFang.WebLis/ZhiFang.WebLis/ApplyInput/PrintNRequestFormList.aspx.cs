using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.Model;
using System.Reflection;
using Tools;

namespace ZhiFang.WebLis.ApplyInput
{
    public partial class PrintNRequestFormList : ZhiFang.WebLis.Class.BasePage
    {
        public int PageSize = 25;  //单页显示最大申请单数：25
        protected string loginID = "";  //登录用户编号
        protected string loginName = "";  //登录用户姓名     
        private readonly Dictionary dic = BLLFactory<Dictionary>.GetBLL("Dictionary");
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
        private readonly IBSickType ibst = BLLFactory<IBSickType>.GetBLL();
        ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            string ClientNo = "";
            string txtStartDate = "";
            string txtEndDate = "";
            string txtCollectStartDate = "";
            string txtCollectEndDate = "";
            string SelectDoctor = "";
            string txtPatientID = "";
            string txtPatientName = "";
            string txtPersonID = "";
            string txtBarCode = "";
            string SickTypeNo = "";
            string WebLisFlagList = "";
            List<string> OutParItemNo = new List<string>();
            List<string> InParItemNo = new List<string>();
            if (!base.CheckCookies("ZhiFangUser"))
            {
                string alertStr = "未登录，请登陆后继续！";
                ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
                return;
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
                    //if (!user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISADMIN") return true; else return false; }))
                    //{
                    //    if (user.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER") return true; else return false; }) || user.PostList.Exists(l => { if (l.ToUpper() == "WEBLISAPPLYINPUT") return true; else return false; }))
                    //    {
                    //        if (user.OrganizationsList.Count > 0)
                    //        {
                    //            if (user.OrganizationsList.ElementAt(0).Value != null && user.OrganizationsList.ElementAt(0).Value.Count() > 0)
                    //            {
                    //                if (base.ReadQueryString("ClientNo") != null)
                    //                {
                    //                    ClientNo = base.ReadQueryString("ClientNo");
                    //                }
                    //                else
                    //                {
                    //                    foreach (var c in user.OrganizationsList)
                    //                    {
                    //                        ClientNo += "'" + c.Value + "',";
                    //                    }
                    //                    DataSet ds = user.GetClientListByPost("", -1); ;
                    //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //                    {
                    //                        ClientNo += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                    //                    }
                    //                    ClientNo = ClientNo.Substring(0, ClientNo.LastIndexOf(','));
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ClientNo = "-1";
                    //            Response.Write("客户编码输入错误！");
                    //            Response.End();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ClientNo = " -1 ";
                    //        Response.Write("客户编码输入错误！");
                    //        Response.End();
                    //    }
                    //}
                    //else
                    //{
                    //    if (base.ReadQueryString("ClientNo") != null)
                    //    {
                    //        ClientNo = base.ReadQueryString("ClientNo");
                    //        this.Label1.Text = ibc.GetModel(long.Parse(ClientNo)).CNAME + "标本外送清单";

                    //    }
                    //    else
                    //    {
                    //        this.Label1.Text = "标本外送清单";
                    //    }
                    //}


                    if (base.ReadQueryString("ClientNo") != null)
                    {
                        ClientNo = base.ReadQueryString("ClientNo");
                        this.Label1.Text = ibc.GetModel(long.Parse(ClientNo)).CNAME + "标本外送清单";
                    }
                    else
                    {
                        this.Label1.Text = "标本外送清单";
                        foreach (var c in user.OrganizationsList)
                        {
                            ClientNo += "'" + c.Value + "',";
                        }
                        DataSet ds = user.GetClientListByPost("", -1); ;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClientNo += " '" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        }
                        ClientNo = ClientNo.Substring(0, ClientNo.LastIndexOf(','));
                    }
                    #endregion
                    if (base.ReadQueryString("ClientNo") != null)
                        this.Label4.Text = ibc.GetModel(long.Parse(ClientNo)).CNAME;
                    #region 开单时间
                    if (base.ReadQueryString("txtStartDate") != null)
                    {
                        txtStartDate = base.ReadQueryString("txtStartDate");
                        this.Label3.Text = txtStartDate;
                    }
                    if (base.ReadQueryString("txtEndDate") != null)
                    {
                        txtEndDate = base.ReadQueryString("txtEndDate");
                        this.Label3.Text += "--" + txtEndDate;
                    }
                    #endregion
                    #region 采样时间
                    if (base.ReadQueryString("txtCollectStartDate") != null)
                    {
                        txtCollectStartDate = base.ReadQueryString("txtCollectStartDate");
                    }
                    if (base.ReadQueryString("txtCollectEndDate") != null)
                    {
                        txtCollectEndDate = base.ReadQueryString("txtCollectEndDate");
                    }
                    #endregion
                    #region 医生
                    if (base.ReadQueryString("SelectDoctor") != null)
                    {
                        SelectDoctor = base.ReadQueryString("SelectDoctor");
                    }
                    #endregion
                    #region 病历号
                    if (base.ReadQueryString("txtPatientID") != null)
                    {
                        txtPatientID = base.ReadQueryString("txtPatientID");
                    }
                    #endregion
                    #region 姓名
                    if (base.ReadQueryString("txtPatientName") != null)
                    {
                        txtPatientName = base.ReadQueryString("txtPatientName");
                    }
                    #endregion
                    #region 就诊类型
                    if (base.ReadQueryString("SickTypeNo") != null)
                    {
                        SickTypeNo = base.ReadQueryString("SickTypeNo");
                    }
                    #endregion

                    #region 申请单状态
                    if (base.ReadQueryString("WebLisFlagList") != null && base.ReadQueryString("WebLisFlagList") != "-1")
                    {
                        WebLisFlagList = base.ReadQueryString("WebLisFlagList");
                    }
                    #endregion
                    #region 身份证
                    if (base.ReadQueryString("PersonID") != null)
                    {
                        txtPersonID = base.ReadQueryString("PersonID");
                    }
                    #endregion

                    #region 条码号(一个单子一个条码的情况下)
                    if (base.ReadQueryString("BarCode") != null)
                    {
                        txtBarCode = base.ReadQueryString("BarCode");
                    }
                    #endregion

                    #region 排除的明细项目
                    if (base.ReadQueryString("OutParItemNo") != null && base.ReadQueryString("OutParItemNo").Trim() != "")
                    {
                        OutParItemNo = base.ReadQueryString("OutParItemNo").Trim().Split(',').ToList();
                    }
                    #endregion

                    #region 包含的明细项目
                    if (base.ReadQueryString("InParItemNo") != null && base.ReadQueryString("InParItemNo").Trim() != "")
                    {
                        InParItemNo = base.ReadQueryString("InParItemNo").Trim().Split(',').ToList();
                    }
                    #endregion

                    PrintNRequestForm(txtPatientName, txtCollectStartDate, txtCollectEndDate, txtStartDate, txtEndDate, SelectDoctor, ClientNo, txtPatientID, "", SickTypeNo, WebLisFlagList,txtPersonID, txtBarCode, OutParItemNo, InParItemNo);
                }
                catch (Exception eee)
                {
                    Response.Write(eee.ToString());
                    Response.End();
                }
            }
        }
        public void PrintNRequestForm(string PatientName, string CollectStartDate, string CollectEndDate, string AddStartDate, string AddEndDate, string Doctor, string WebLisSourceOrgID, string PatNo, string SampleTypeNo, string SickTypeNo, string WebLisFlagList, string PersonID, string BarCode, List<string> OutParItemNo, List<string> InParItemNo)
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
            if (PersonID.Trim() != "")
            {
                nrf_m.PersonID = PersonID.Trim();
            }
            if (BarCode.Trim() != "")
            {
                nrf_m.BarCode = BarCode.Trim();
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

            if (!string.IsNullOrWhiteSpace(SickTypeNo))
            {
                nrf_m.SickTypeList = SickTypeNo.Trim();
            }
            if (!string.IsNullOrWhiteSpace(WebLisFlagList))
            {
                nrf_m.WeblisflagList = WebLisFlagList.Trim();
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
                //FillList(dt);
                FillList(dt, OutParItemNo, InParItemNo);
            }
        }

        #region 向页面填充申请单信息
        /// <summary>
        /// 向页面填充申请单信息 
        /// </summary>
        /// <param name="dt"></param>
        public void FillList(DataTable dt)
        {
            //List_Table
            int k = 0;
            int i = 0;
            //dt.DefaultView.Sort = " OperDate asc ";
            //DataView dv= dt.DefaultView;

            string orderstr = "LABUPLOADDATE";
            #region 排序字段
            if (base.ReadQueryString("OrderField") != null && base.ReadQueryString("OrderField").Trim() != "")
            {
                orderstr = base.ReadQueryString("OrderField");
            }
            #endregion
            var drlist = dt.AsEnumerable().OrderBy(a => a[orderstr]);

            foreach (DataRow dr in drlist)
            {
                i++;
                string b = rfb.GetBarCodeByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                foreach (var a in b.Split(','))
                {
                    barcode += a + "</br>";
                    k++;
                }
                //barcode = barcode.Substring(0, barcode.Length - barcode.LastIndexOf("</br>"));
                System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = i.ToString();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    tc.InnerHtml = Convert.ToDateTime(dr["OperTime"].ToString().Trim()).ToShortDateString();
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = barcode;
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["jztype"].ToString().Trim().Length > 0)
                {
                    SickType st = ibst.GetModel(int.Parse(dr["jztype"].ToString().Trim()));
                    if (st != null)
                    {
                        tc.InnerHtml = st.CName;
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                    //if (dr["jztype"].ToString().Trim() == "1")
                    //{
                    //    tc.InnerHtml = "住院";
                    //}
                    //if (dr["jztype"].ToString().Trim() == "2")
                    //{
                    //    tc.InnerHtml = "门诊";
                    //}
                    //if (dr["jztype"].ToString().Trim() == "3")
                    //{
                    //    tc.InnerHtml = "体检";
                    //}
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["PatNo"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["CName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["Sex"].ToString().Trim();
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["Age"].ToString().Trim() == "200")
                {
                    tc.InnerHtml = "成人";
                }
                else
                {
                    tc.InnerHtml = dr["Age"].ToString().Trim() + dr["AgeUnitName"].ToString().Trim();
                }
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                //tc.InnerHtml = dr["SampleName"].ToString().Trim();
                tc.InnerHtml = dr["DoctorName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                DataSet dsri = new DataSet();
                string htmltmp = "";
                try
                {
                    dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = long.Parse(dr["NRequestFormNo"].ToString().Trim()) });

                    DataTable ndt = new DataTable();
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {
                        //去重复列
                        //for (int d = 0; d < dsri.Tables[0].Rows.Count; d++)
                        //{ 
                        //ndt = GetDistinctTable(dsri.Tables[0], dsri.Tables[0].Columns[d].ColumnName);
                        //去掉重复行
                        ndt = GetDistinctTable(dsri.Tables[0], "");

                        //}
                        //DataTable dtt = sqldbweblis.ExecDT("select dbo.TestItem.CName from dbo.TestItem INNER JOIN dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where BarCodeFormNo=" + dr["BarCodeFormNo"].ToString().Trim());
                        //string htmltmp = "";
                        DataSet nds = new DataSet();
                        nds.Tables.Add(ndt);
                        List<string> itemcname = new List<string>();
                        for (int j = 0; j < nds.Tables[0].Rows.Count; j++)
                        {
                            itemcname.Add(nds.Tables[0].Rows[j]["CName"].ToString().Trim());
                            //htmltmp += nds.Tables[0].Rows[j]["CName"].ToString().Trim() + ",</br>";
                        }
                        if (itemcname.Count > 0)
                        {
                            htmltmp = string.Join(",</br>", itemcname.ToArray());
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                }

                tc.InnerHtml = htmltmp;

                tr.Cells.Add(tc);



                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();
                //tr.Cells.Add(tc);


                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["ClientName"].ToString().Trim();
                //tr.Cells.Add(tc);

                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //string Diag = dr["Diag"].ToString().Trim();

                //Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                //tc.InnerHtml = Diag;
                //tr.Cells.Add(tc);
                tr.Style.Add("font-size", "12px");
                tr.Style.Add("padding-top", "2px");
                List_Table.Rows.Add(tr);
                System.Web.UI.HtmlControls.HtmlTableRow trline = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tcline = new System.Web.UI.HtmlControls.HtmlTableCell();
                tcline.ColSpan = 10;
                tcline.InnerHtml = "<hr />";
                trline.Cells.Add(tcline);
                List_Table.Rows.Add(trline);
            }
            //this.Label6.Text = dt.Rows.Count.ToString();//获取总条数
            this.Label6.Text = k.ToString();//同上
            //this.Label6.Text = 3.ToString();//测试
        }
        /// <summary>
        /// 获取对固定列不重复的新DataTable
        /// </summary>
        /// <param name="dt">含有重复数据的DataTable</param>
        /// <param name="colName">需要验证重复的列名</param>
        /// <returns>新的DataTable，colName列不重复，表格式保持不变</returns>
        private DataTable GetDistinctTable(DataTable dt, string colName)
        {
            try
            {
                colName = "CName";
                DataView dv = dt.DefaultView;
                DataTable dtCardNo = dv.ToTable(true, colName);
                DataTable Pointdt = new DataTable();
                Pointdt = dv.ToTable();
                Pointdt.Clear();
                var query = from t in dt.AsEnumerable()
                            group t by new { t1 = t.Field<string>("CName") } into m
                            select new
                            {
                                CName = m.Key.t1
                            };
                Pointdt = DataTableHelper.CopyToDataTablea(query);
                return Pointdt;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 向页面填充申请单信息 
        /// </summary>
        /// <param name="dt"></param>
        public void FillList(DataTable dt, List<string> OutParItemNo, List<string> InParItemNo)
        {
            //List_Table
            int k = 0;
            int i = 0;
            //dt.DefaultView.Sort = " OperDate asc ";
            //DataView dv= dt.DefaultView;
            List<string> listbarcode = new List<string>();
            string orderstr = "LABUPLOADDATE";
            #region 排序字段
            if (base.ReadQueryString("OrderField") != null && base.ReadQueryString("OrderField").Trim() != "")
            {
                orderstr = base.ReadQueryString("OrderField");
            }
            #endregion
            var drlist = dt.AsEnumerable().OrderBy(a => a[orderstr]);

            foreach (DataRow dr in drlist)
            {
                var b = rfb.GetBarCodeListByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                var barcode = "";
                foreach (var a in b)
                {
                    #region 组套项目包含验证
                    if (OutParItemNo.Count > 0)
                    {
                        bool flag = false;
                        foreach (var opi in OutParItemNo)
                        {
                            //ZhiFang.Common.Log.Log.Debug("1.opi=" + opi);
                            if (a.ItemNo.IndexOf(opi) >= 0)
                            {
                                //ZhiFang.Common.Log.Log.Debug("2.opi=" + opi + ",ItemNo = " + a.ItemNo);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            //ZhiFang.Common.Log.Log.Debug("3");
                            continue;
                        }
                    }
                    if (InParItemNo.Count > 0)
                    {
                        bool flag = false;
                        foreach (var opi in InParItemNo)
                        {
                            //ZhiFang.Common.Log.Log.Debug("4.opi=" + opi);
                            if (a.ItemNo.IndexOf(opi)>= 0)
                            {
                                //ZhiFang.Common.Log.Log.Debug("5.opi=" + opi + ",ItemNo = " + a.ItemNo);
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                           // ZhiFang.Common.Log.Log.Debug("6");
                            continue;
                        }
                    }
                    #endregion
                    barcode += a.BarCode + "</br>";
                    if (!listbarcode.Contains(a.BarCode))
                    {
                        listbarcode.Add(a.BarCode);
                    }
                    k++;
                }
                if (barcode == "")//一个条码没有则跳出
                    continue;
                //barcode = barcode.Substring(0, barcode.Length - barcode.LastIndexOf("</br>"));
                System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();
                i++;
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = i.ToString();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["OperTime"].ToString().Trim().Length > 0)
                {
                    tc.InnerHtml = Convert.ToDateTime(dr["OperTime"].ToString().Trim()).ToShortDateString();
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = barcode;
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["jztype"].ToString().Trim().Length > 0)
                {
                    SickType st = ibst.GetModel(int.Parse(dr["jztype"].ToString().Trim()));
                    if (st != null)
                    {
                        tc.InnerHtml = st.CName;
                    }
                    else
                    {
                        tc.InnerHtml = "&nbsp;";
                    }
                }
                else
                {
                    tc.InnerHtml = "&nbsp;";
                }
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["PatNo"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["CName"].ToString().Trim();
                tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                tc.InnerHtml = dr["Sex"].ToString().Trim();
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                if (dr["Age"].ToString().Trim() == "200")
                {
                    tc.InnerHtml = "成人";
                }
                else
                {
                    tc.InnerHtml = dr["Age"].ToString().Trim() + dr["AgeUnitName"].ToString().Trim();
                }
                tr.Cells.Add(tc);
                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                //tc.InnerHtml = dr["SampleName"].ToString().Trim();
                tc.InnerHtml = dr["DoctorName"].ToString().Trim();
                tr.Cells.Add(tc);

                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["PersonID"].ToString().Trim();
                //tr.Cells.Add(tc);

                tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                tc.Align = "center";
                DataSet dsri = new DataSet();
                string htmltmp = "";
                try
                {
                    dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { NRequestFormNo = long.Parse(dr["NRequestFormNo"].ToString().Trim()) });
                    if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                    {

                        //去重复列
                        //for (int d = 0; d < dsri.Tables[0].Rows.Count; d++)
                        //{ 
                        //ndt = GetDistinctTable(dsri.Tables[0], dsri.Tables[0].Columns[d].ColumnName);
                        //去掉重复行
                        //ndt = GetDistinctTable(dsri.Tables[0], "");

                        //}
                        //DataTable dtt = sqldbweblis.ExecDT("select dbo.TestItem.CName from dbo.TestItem INNER JOIN dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where BarCodeFormNo=" + dr["BarCodeFormNo"].ToString().Trim());
                        //string htmltmp = "";
                        List<string> itemcname = new List<string>();
                        //for (int aaa=0; aaa < dsri.Tables[0].Columns.Count; aaa++)
                        //{
                        //    ZhiFang.Common.Log.Log.Debug("dsri.Tables[0].Columns:" + dsri.Tables[0].Columns[aaa].ColumnName);
                        //}
                        for (int j = 0; j < dsri.Tables[0].Rows.Count; j++)
                        {
                            if (OutParItemNo.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("1.OutParItemNo:" + string.Join(",", OutParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                if (OutParItemNo.Contains(dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim()))
                                {
                                    ZhiFang.Common.Log.Log.Debug("2.OutParItemNo:" + string.Join(",", OutParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                    continue;
                                }
                            }
                            if (InParItemNo.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("1.InParItemNo:" + string.Join(",", InParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                if (!InParItemNo.Contains(dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim()))
                                {
                                    ZhiFang.Common.Log.Log.Debug("2.InParItemNo:" + string.Join(",", InParItemNo) + ",ItemNo:" + dsri.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                                    continue;
                                }
                            }
                            if (!itemcname.Contains(dsri.Tables[0].Rows[j]["CName"].ToString().Trim()))
                            {
                                itemcname.Add(dsri.Tables[0].Rows[j]["CName"].ToString().Trim());
                            }                 
                        }
                        if (itemcname.Count > 0)
                        {
                            htmltmp = string.Join(",</br>", itemcname.ToArray());
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.StackTrace + e.ToString());
                }

                tc.InnerHtml = htmltmp;

                tr.Cells.Add(tc);



                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["WebLisSourceOrgName"].ToString().Trim();
                //tr.Cells.Add(tc);


                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //tc.InnerHtml = dr["ClientName"].ToString().Trim();
                //tr.Cells.Add(tc);

                //tc = new System.Web.UI.HtmlControls.HtmlTableCell();
                //tc.Align = "center";
                //string Diag = dr["Diag"].ToString().Trim();

                //Diag = ZhiFang.Tools.Tools.StringLength(Diag, 8);
                //tc.InnerHtml = Diag;
                //tr.Cells.Add(tc);
                tr.Style.Add("font-size", "12px");
                tr.Style.Add("padding-top", "2px");
                List_Table.Rows.Add(tr);
                System.Web.UI.HtmlControls.HtmlTableRow trline = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tcline = new System.Web.UI.HtmlControls.HtmlTableCell();
                tcline.ColSpan = 11;
                tcline.InnerHtml = "<hr />";
                trline.Cells.Add(tcline);
                List_Table.Rows.Add(trline);
            }
            BarCodeList.Text = string.Join(",", listbarcode);
            //this.Label6.Text = dt.Rows.Count.ToString();//获取总条数
            this.Label6.Text = k.ToString();//同上
            //this.Label6.Text = 3.ToString();//测试
        }
        #endregion
    }
}