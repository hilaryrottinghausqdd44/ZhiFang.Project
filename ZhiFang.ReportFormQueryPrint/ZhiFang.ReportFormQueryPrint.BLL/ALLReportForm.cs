using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Common;
using Newtonsoft.Json;
using ZhiFang.ReportFormQueryPrint.Model.VO;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BALLReportForm
    {
        private readonly IDALLReportForm dal = DalFactory<IDALLReportForm>.GetDal("ALLReportForm");
        private readonly IDReportItem dri = DalFactory<IDReportItem>.GetDal("ReportItem");
        private readonly IDReportMarrow drm = DalFactory<IDReportMarrow>.GetDal("ReportMarrow");
        private readonly IDReportMicro drc = DalFactory<IDReportMicro>.GetDal("ReportMicro");
        protected readonly IDAL.IDReportDrugGene idrdruggene = DalFactory<IDReportDrugGene>.GetDal("ReportDrugGene");
        /// <summary>
        /// 骨髓项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetMarrowItemList(string FormNo)
        {
            return dal.GetMarrowItemList(FormNo);
        }
        /// <summary>
        /// 生化表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromInfo(string FormNo)
        {
            return dal.GetFromInfo(FormNo);
        }
        /// <summary>
        /// 生化表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public ZhiFang.ReportFormQueryPrint.Model.ReportForm GetFromInfoModel(string FormNo)
        {
            return dal.GetFromInfoModel(FormNo);
        }
        /// <summary>
        /// 微生物表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroInfo(string FormNo)
        {
            return dal.GetMicroItemList(FormNo);
        }
        /// <summary>
        /// 生化表单项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromItemList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }

        public DataTable GetReportItemList(string FormNo)
        {
            return dal.GetReportItemList(FormNo);
        }


        /// <summary>
        /// 微生物表单项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroItemList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroList(string FormNo)
        {
            return dal.GetMicroItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroGroupList(string FormNo)
        {
            return dal.GetMicroItemGroupList(FormNo);
        }

        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroList(string FormNo, string ItemNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo, string ItemNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo, string ItemNo, string MicroNo)
        {
            return dal.GetFromItemList(FormNo);
        }

        public DataTable GetFromPGroupInfo(int SectionNo)
        {
            return dal.GetFromPGroupInfo(SectionNo);
        }

        public DataTable GetFromGraphList(string FormNo)
        {
            return dal.GetFromGraphList(FormNo);
        }
        public DataTable GetFromUserImage(string UserName)
        {
            IDPUser dalPUser = DalFactory<IDPUser>.GetDal("PUser");
            ZhiFang.ReportFormQueryPrint.Model.PUser pm = new ZhiFang.ReportFormQueryPrint.Model.PUser();
            pm.CName = UserName;
            return dalPUser.GetList(pm).Tables[0];
        }
        public int GetCountFormFull(string strWhere)
        {
            return dal.GetCountFormFull(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_FormFull(string fields, string strWhere)
        {
            return dal.GetList_FormFull(fields, strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_ItemFull(string strWhere)
        {
            return dal.GetList_ItemFull(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_MicroFull(string strWhere)
        {
            return dal.GetList_MicroFull(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_MarrowFull(string strWhere)
        {
            return dal.GetList_MarrowFull(strWhere);
        }

        /// <summary>
        /// 历史对比2
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetReportValue(string[] p, string aa)
        {
            return dal.GetReportValue(p, aa);
        }

        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="PatNo"></param>
        /// <param name="st"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultMhistory(string ReportFormID, string PatNo, string st, string Where) {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            DataTable dtrf = brf.GetListByDataSource(ReportFormID);
            DataSet ds = dal.ResultMhistory(ReportFormID, PatNo, Where);
            if (dtrf != null && dtrf.Rows.Count > 0)
            {
                #region      
                DataTable dtri = new DataTable();
                DataTable dtrg = new DataTable();
                dtrf.TableName = "frform";
                int sectiontype = Convert.ToInt32(st);
                if (dtrf.Columns.Contains("SECTIONTYPE") && dtrf.Rows[0]["SECTIONTYPE"] != null && dtrf.Rows[0]["SECTIONTYPE"].ToString().Trim() != "")
                {
                    sectiontype = Convert.ToInt32(dtrf.Rows[0]["SECTIONTYPE"].ToString());
                }
                if (sectiontype == 2 || sectiontype == 4)
                {
                    if (dtrf.Columns.Contains("STestType") && dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                    {
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                        {
                            sectiontype = 2;
                        }
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                        {
                            sectiontype = 2;
                        }
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                        {
                            sectiontype = 1;
                        }
                    }
                }
                #endregion
                #region  数据准备 
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    case SectionType.Normal:
                        #region Normal                               
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                            {
                                dt1 = idrdruggene.GetReportItemFullList(b);
                            }
                            else
                            {
                                dt1 = dri.GetReportItemFullList(b);
                            }
                            dt1.TableName = "table" + i;
                            ds.Tables.Add(dt1.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                            {
                                dt1 = idrdruggene.GetReportItemFullList(b);
                            }
                            else
                            {
                                dt1 = dri.GetReportItemFullList(b);
                            }



                            dt1.TableName = "table" + i;
                            ds.Tables.Add(dt1.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.Micro:
                        #region Micro                         
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt3 = drc.GetReportMicroList(b);
                            dt3.TableName = "table" + i;
                            ds.Tables.Add(dt3.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage                               
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt4 = drc.GetReportMicroList(b);
                            dt4.TableName = "table" + i;
                            ds.Tables.Add(dt4.Copy());
                        }
                        break;
                    #endregion
                    //case SectionType.CellMorphology:
                    //    #region CellMorphology
                    //       DataSet ds5=dal.ResultMhistory(ReportFormID, PatNo, Where);
                    //        for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)
                    //        {
                    //            Int64 rfid5 = (Int64)ds5.Tables[0].Rows[i]["ReportFormID"];
                    //            string b = rfid5 + "";
                    //            DataTable dt5 = drm.GetReportMarrowItemList(b);
                    //            dt5.TableName="table"+i;
                    //            ds.Tables.Add(dt5.Copy());      
                    //        }
                    //break;
                    //    #endregion  

                    default:
                        DataSet ds6 = dal.ResultMhistory(ReportFormID, PatNo, Where);
                        for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt6 = dri.GetReportItemList(b);
                            dt6.TableName = "table" + i;
                            ds.Tables.Add(dt6.Copy());
                        }
                        break;
                }
                #endregion


            }
            return ds;
        }

        public DataSet ResultDataTimeMhistory(string PatNo, string Where) {
            var res = dal.ResultDataTimeMhistory(PatNo, Where);
            return res;
        }

        /// <summary>
        /// 查询report报告单结果是否为空
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public bool ReportIsPrintNullValues(string ReportFormID, string st)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            DataTable dtrf = brf.GetListByDataSource(ReportFormID);
            bool ds = false;
            if (dtrf != null && dtrf.Rows.Count > 0)
            {
                #region      
                DataTable dtri = new DataTable();
                DataTable dtrg = new DataTable();
                dtrf.TableName = "frform";
                int sectiontype = Convert.ToInt32(st);
                if (dtrf.Columns.Contains("SECTIONTYPE") && dtrf.Rows[0]["SECTIONTYPE"] != null && dtrf.Rows[0]["SECTIONTYPE"].ToString().Trim() != "")
                {
                    sectiontype = Convert.ToInt32(dtrf.Rows[0]["SECTIONTYPE"].ToString());
                }
                if (sectiontype == 2 || sectiontype == 4)
                {
                    if (dtrf.Columns.Contains("STestType") && dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                    {
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                        {
                            sectiontype = 2;
                        }
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                        {
                            sectiontype = 2;
                        }
                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                        {
                            sectiontype = 1;
                        }
                    }
                }
                #endregion
                #region  数据准备 
                DataTable dt1 = new DataTable();
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    case SectionType.Normal:
                        #region Normal                               

                        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        {
                            dt1 = idrdruggene.GetReportItemFullList(ReportFormID);
                        }
                        else
                        {
                            dt1 = dri.GetReportItemFullList(ReportFormID);
                        }


                        break;
                    #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        {
                            dt1 = idrdruggene.GetReportItemFullList(ReportFormID);
                        }
                        else
                        {
                            dt1 = dri.GetReportItemFullList(ReportFormID);
                        }
                        break;
                    #endregion
                    case SectionType.Micro:
                        #region Micro                         
                        dt1 = drc.GetReportMicroList(ReportFormID);
                        break;
                    #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage                               
                        dt1 = drc.GetReportMicroList(ReportFormID);
                        break;
                    #endregion
                    default:
                        dt1 = dri.GetReportItemList(ReportFormID);
                        break;
                }
                #endregion
                for (var i = 0; i < dt1.Rows.Count; i++) {
                    if (dt1.Rows[i]["ReportValue"] != null && dt1.Rows[i]["ReportDesc"] != null)
                    {
                        ds = true;
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 技师站多项目历史对比
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet LabStarResultMhistory(string Where)
        {
            DAL.MSSQL.LabStar.ALLReport aLLReportForm = new DAL.MSSQL.LabStar.ALLReport();
            DataSet ds = aLLReportForm.ResultMhistory(Where);
            DataSet allds = new DataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataTable dataTalbe = ds.Tables[0].Clone();
                    dataTalbe.TableName = "formtable" + i;
                    dataTalbe.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                    allds.Tables.Add(dataTalbe.Copy());
                    string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                    DataTable dt = aLLReportForm.GetReportItemList(b);
                    dt.TableName = "itemtable" + i;
                    allds.Tables.Add(dt.Copy());
                }
            }
            else
            {
                return ds;
            }
            return allds;
        }

        /// <summary>
        /// 技师站多项目历史对比(新)
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet NewLabStarResultMhistory(string Where)
        {
            DAL.MSSQL.LabStar.ALLReport aLLReportForm = new DAL.MSSQL.LabStar.ALLReport();

            #region 新
            Where += " order by GTestDate DESC";
            DataSet ds = aLLReportForm.ResultMhistory(Where);
            DataSet allds = new DataSet();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //构造table，行列转换，第一列为列头，其他每一列为一条信息，行数为固定行加上项目个数
                DataTable newDataTable = new DataTable();
                //第一列样本信息
                newDataTable.Columns.Add("SampleInfo");//第一列相当于列头
                newDataTable.Columns.Add("itemno");//第二列为项目号，前几行固定行用不到可以设置个固定值
                newDataTable.Columns.Add("ItemCname");//第三列为项目名，前几行固定行用不到可以设置个固定值
                newDataTable.Columns.Add("SName");//第四列为项目简称，前几行固定行用不到可以设置个固定值
                newDataTable.Columns.Add("ItemDispOrder", typeof(int));//第五列为项目排序字段
                List<string> ReportFormIDList = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //1.循环reportform将列头构造出来，列头就是报告单里id
                    string ReportFormID = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                    newDataTable.Columns.Add(ReportFormID);
                    ReportFormIDList.Add(ReportFormID);
                }

                #region 构造前4行固定信息--日期，小组，样本号，样本类型
                for (int i = 0; i < 4; i++)
                {
                    DataRow dr = newDataTable.NewRow();
                    dr["itemno"] = "-1";
                    dr["ItemCname"] = "无";
                    dr["SName"] = "";
                    dr["ItemDispOrder"] = i-4;
                    switch (i)
                    {
                        case 0:
                            dr["SampleInfo"] = "日期";
                            //填充后几列的信息
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                dr[ds.Tables[0].Rows[j]["ReportFormID"].ToString()] = Convert.ToDateTime(ds.Tables[0].Rows[j]["GTestDate"].ToString()).ToString("yyyy-MM-dd"); ;
                            }
                            break;
                        case 1:
                            dr["SampleInfo"] = "检验小组";
                            //填充后几列的信息
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                dr[ds.Tables[0].Rows[j]["ReportFormID"].ToString()] = ds.Tables[0].Rows[j]["SectionName"].ToString();
                            }
                            break;
                        case 2:
                            dr["SampleInfo"] = "小组样本号";
                            //填充后几列的信息
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                dr[ds.Tables[0].Rows[j]["ReportFormID"].ToString()] = ds.Tables[0].Rows[j]["GSampleNo"].ToString();
                            }
                            break;
                        case 3:
                            dr["SampleInfo"] = "样本类型";
                            //填充后几列的信息
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                dr[ds.Tables[0].Rows[j]["ReportFormID"].ToString()] = ds.Tables[0].Rows[j]["GSampleType"].ToString();
                            }
                            break;
                        default:
                            break;
                    }
                    newDataTable.Rows.Add(dr);
                }
                #endregion
                #region 构造填充项目行的信息
                //查出所有testitem
                DataTable itemDataTable = aLLReportForm.GetReportItemListByWhere("ReportFormID in (" + string.Join(",", ReportFormIDList) + ")");
                //以项目名称分组
                IEnumerable<IGrouping<string, DataRow>> result = itemDataTable.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr["ItemCname"].ToString());//按A分组
                
                foreach (IGrouping<string, DataRow> ig in result)
                {
                    //一个项目是一行
                    DataRow dr = newDataTable.NewRow();
                    dr["SampleInfo"] = ig.Key;//项目名称
                    dr["itemno"] = ig.First()["ItemNo"].ToString();//每一行的项目编码都一样
                    dr["ItemCname"] = ig.Key;
                    dr["SName"] = ig.First()["SName"].ToString();
                    dr["ItemDispOrder"] = int.Parse(ig.First()["ItemDispOrder"].ToString());
                    foreach (var dr1 in ig) {
                        dr[dr1["ReportFormID"].ToString()] = dr1["ItemValue"].ToString();//按照项目的ReportFormID去找对应的列赋值
                    }
                    newDataTable.Rows.Add(dr);
                }
                newDataTable.DefaultView.Sort = "ItemDispOrder ASC";//排序
                newDataTable = newDataTable.DefaultView.ToTable();
                #endregion
                allds.Tables.Add(newDataTable.Copy());
            }
            #endregion
            return allds;
        }

        /// <summary>
        /// 技师站历史对比
        /// </summary>
        /// <param name="p"></param>
        /// <param name="aa"></param>
        /// <returns></returns>
        public DataTable LabStarGetReportValue(string[] p, string aa)
        {
            DAL.MSSQL.LabStar.ALLReport aLLReportForm = new DAL.MSSQL.LabStar.ALLReport();
            return aLLReportForm.GetReportValue(p, aa);
        }
        public int UpdateTestFormPrintCount(string TestFormID)
        {
            DAL.MSSQL.LabStar.ALLReport aLLReportForm = new DAL.MSSQL.LabStar.ALLReport();
            return aLLReportForm.UpdateTestFormPrintCount(TestFormID);
        }
        public List<SampleStateVo> SampleStateTailAfter(string ReportFormId) {
            DataSet reportFormState = dal.SampleStateTailAfter(ReportFormId);
            List<SampleStateVo> ssvlist = new List<SampleStateVo>();
            if (reportFormState.Tables[0].Rows.Count > 0) {            
                SampleStateVo stateVo;
                for (int i = 0; i < reportFormState.Tables[0].Rows.Count; i++)
                {                   
                    string operators = reportFormState.Tables[0].Rows[i]["operator"].ToString();
                    string AffirmTime = reportFormState.Tables[0].Rows[i]["AffirmTime"].ToString();
                    string operatorexplain = reportFormState.Tables[0].Rows[i]["operatorexplain"].ToString();
                    stateVo = new SampleStateVo("条码打印",operators, AffirmTime, operatorexplain,"");
                    ssvlist.Add(stateVo);
                    string collecter = reportFormState.Tables[0].Rows[i]["collecter"].ToString();
                    string Collectdate = reportFormState.Tables[0].Rows[i]["Collectdate"].ToString();
                    string collecterexplain = reportFormState.Tables[0].Rows[i]["collecterexplain"].ToString();
                    stateVo = new SampleStateVo("样本采集", collecter, Collectdate, collecterexplain, "");
                    ssvlist.Add(stateVo);
                    string NurseSender = reportFormState.Tables[0].Rows[i]["NurseSender"].ToString();
                    string NurseSendTime = reportFormState.Tables[0].Rows[i]["NurseSendTime"].ToString();
                    string NurseSenderexplain = reportFormState.Tables[0].Rows[i]["NurseSenderexplain"].ToString();
                    stateVo = new SampleStateVo("样本送检", NurseSender, NurseSendTime, NurseSenderexplain, "");
                    ssvlist.Add(stateVo);
                    string NurseSendCarrier = reportFormState.Tables[0].Rows[i]["NurseSendCarrier"].ToString();
                    string arrivetime = reportFormState.Tables[0].Rows[i]["arrivetime"].ToString();
                    string NurseSendCarrierexplain = reportFormState.Tables[0].Rows[i]["NurseSendCarrierexplain"].ToString();
                    stateVo = new SampleStateVo("样本送达", NurseSendCarrier, arrivetime, NurseSendCarrierexplain, "");
                    ssvlist.Add(stateVo);
                    string incepter = reportFormState.Tables[0].Rows[i]["incepter"].ToString();
                    string inceptTime = reportFormState.Tables[0].Rows[i]["inceptTime"].ToString();
                    string incepterexplain = reportFormState.Tables[0].Rows[i]["incepterexplain"].ToString();
                    stateVo = new SampleStateVo("样本签收", incepter, inceptTime, incepterexplain, "");
                    ssvlist.Add(stateVo);
                    string Technician = reportFormState.Tables[0].Rows[i]["Technician"].ToString();
                    string Receivedate = reportFormState.Tables[0].Rows[i]["Receivedate"].ToString();
                    string Technicianexplain = reportFormState.Tables[0].Rows[i]["Technicianexplain"].ToString();
                    stateVo = new SampleStateVo("样本核收", Technician, Receivedate, Technicianexplain, "");
                    ssvlist.Add(stateVo);
                    string Technician2 = reportFormState.Tables[0].Rows[i]["Technician"].ToString();
                    string Testdate = reportFormState.Tables[0].Rows[i]["Testdate"].ToString();
                    string Testexplain = reportFormState.Tables[0].Rows[i]["Testexplain"].ToString();
                    stateVo = new SampleStateVo("样本检验", Technician2, Testdate, Testexplain, "");
                    ssvlist.Add(stateVo);
                    string checker = reportFormState.Tables[0].Rows[i]["checker"].ToString();
                    string Checkdate = reportFormState.Tables[0].Rows[i]["Checkdate"].ToString();
                    string checkerexplain = reportFormState.Tables[0].Rows[i]["checkerexplain"].ToString();
                    stateVo = new SampleStateVo("样本报告", checker, Checkdate, checkerexplain, "");
                    ssvlist.Add(stateVo);
                }
            }

            return ssvlist;
        }

    }
}
