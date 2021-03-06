using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;

namespace ZhiFang.BLL.Report
{
    /// <summary>
    /// ReportItemFull
    /// </summary>
    public partial class ReportItemFull : ZhiFang.IBLL.Report.IBReportItemFull
    {
        private readonly IDReportItemFull dal = DalFactory<IDReportItemFull>.GetDalByClassName("ReportItemFull");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl");
        private readonly IDResultTestItemControl idrtic = DalFactory<IDResultTestItemControl>.GetDalByClassName("B_ResultTestItemControl");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");
        public ReportItemFull()
        {
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReportFormID, string ReportItemID)
        {
            return dal.Exists(ReportFormID, ReportItemID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportItemFull model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportItemFull model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ReportFormID, string ReportItemID)
        {

            return dal.Delete(ReportFormID, ReportItemID);
        }

        public DataSet GetColumns()
        {
            return dal.GetColumns();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportItemFull GetModel(string ReportFormID, string ReportItemID)
        {
            return dal.GetModel(ReportFormID, ReportItemID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.ReportItemFull GetModelByCache(string ReportFormID, string ReportItemID)
        {

            string CacheKey = "ReportItemFullModel-" + ReportFormID + ReportItemID;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ReportFormID, ReportItemID);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.ReportItemFull)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportItemFull> DataTableToList(DataTable dt)
        {
            List<Model.ReportItemFull> modelList = new List<Model.ReportItemFull>();
            try
            {

                int rowsCount = dt.Rows.Count;
                if (rowsCount > 0)
                {
                    Model.ReportItemFull model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new Model.ReportItemFull();
                        if (dt.Rows[n]["ReportFormID"] != null && dt.Rows[n]["ReportFormID"].ToString() != "")
                        {
                            model.ReportFormID = dt.Rows[n]["ReportFormID"].ToString();
                        }
                        if (dt.Rows[n]["ReportItemID"] != null && dt.Rows[n]["ReportItemID"].ToString() != "")
                        {
                            model.ReportItemID = int.Parse(dt.Rows[n]["ReportItemID"].ToString());
                        }
                        if (dt.Rows[n]["TESTITEMNAME"] != null && dt.Rows[n]["TESTITEMNAME"].ToString() != "")
                        {
                            model.TESTITEMNAME = dt.Rows[n]["TESTITEMNAME"].ToString();
                        }
                        if (dt.Rows[n]["TESTITEMSNAME"] != null && dt.Rows[n]["TESTITEMSNAME"].ToString() != "")
                        {
                            model.TESTITEMSNAME = dt.Rows[n]["TESTITEMSNAME"].ToString();
                        }
                        if (dt.Rows[n]["RECEIVEDATE"] != null && dt.Rows[n]["RECEIVEDATE"].ToString() != "")
                        {
                            model.RECEIVEDATE = DateTime.Parse(dt.Rows[n]["RECEIVEDATE"].ToString());
                        }
                        if (dt.Rows[n]["SECTIONNO"] != null && dt.Rows[n]["SECTIONNO"].ToString() != "")
                        {
                            model.SECTIONNO = dt.Rows[n]["SECTIONNO"].ToString();
                        }
                        if (dt.Rows[n]["TESTTYPENO"] != null && dt.Rows[n]["TESTTYPENO"].ToString() != "")
                        {
                            model.TESTTYPENO = dt.Rows[n]["TESTTYPENO"].ToString();
                        }
                        if (dt.Rows[n]["SAMPLENO"] != null && dt.Rows[n]["SAMPLENO"].ToString() != "")
                        {
                            model.SAMPLENO = dt.Rows[n]["SAMPLENO"].ToString();
                        }
                        if (dt.Rows[n]["PARITEMNO"] != null && dt.Rows[n]["PARITEMNO"].ToString() != "")
                        {
                            model.PARITEMNO = dt.Rows[n]["PARITEMNO"].ToString();
                        }
                        if (dt.Rows[n]["ITEMNO"] != null && dt.Rows[n]["ITEMNO"].ToString() != "")
                        {
                            model.ITEMNO = dt.Rows[n]["ITEMNO"].ToString();
                        }
                        if (dt.Rows[n]["ORIGINALVALUE"] != null && dt.Rows[n]["ORIGINALVALUE"].ToString() != "")
                        {
                            model.ORIGINALVALUE = dt.Rows[n]["ORIGINALVALUE"].ToString();
                        }
                        if (dt.Rows[n]["REPORTVALUE"] != null && dt.Rows[n]["REPORTVALUE"].ToString() != "")
                        {
                            model.REPORTVALUE = dt.Rows[n]["REPORTVALUE"].ToString();
                        }
                        if (dt.Rows[n]["ORIGINALDESC"] != null && dt.Rows[n]["ORIGINALDESC"].ToString() != "")
                        {
                            model.ORIGINALDESC = dt.Rows[n]["ORIGINALDESC"].ToString();
                        }
                        if (dt.Rows[n]["REPORTDESC"] != null && dt.Rows[n]["REPORTDESC"].ToString() != "")
                        {
                            model.REPORTDESC = dt.Rows[n]["REPORTDESC"].ToString();
                        }
                        if (dt.Rows[n]["STATUSNO"] != null && dt.Rows[n]["STATUSNO"].ToString() != "")
                        {
                            model.STATUSNO = dt.Rows[n]["STATUSNO"].ToString();
                        }
                        if (dt.Rows[n]["EQUIPNO"] != null && dt.Rows[n]["EQUIPNO"].ToString() != "")
                        {
                            model.EQUIPNO = dt.Rows[n]["EQUIPNO"].ToString();
                        }
                        if (dt.Rows[n]["MODIFIED"] != null && dt.Rows[n]["MODIFIED"].ToString() != "")
                        {
                            model.MODIFIED = dt.Rows[n]["MODIFIED"].ToString();
                        }
                        if (dt.Rows[n]["REFRANGE"] != null && dt.Rows[n]["REFRANGE"].ToString() != "")
                        {
                            model.REFRANGE = dt.Rows[n]["REFRANGE"].ToString();
                        }
                        if (dt.Rows[n]["ITEMDATE"] != null && dt.Rows[n]["ITEMDATE"].ToString() != "")
                        {
                            model.ITEMDATE = DateTime.Parse(dt.Rows[n]["ITEMDATE"].ToString());
                        }
                        if (dt.Rows[n]["ITEMTIME"] != null && dt.Rows[n]["ITEMTIME"].ToString() != "")
                        {
                            model.ITEMTIME = DateTime.Parse(dt.Rows[n]["ITEMTIME"].ToString());
                        }
                        if (dt.Rows[n]["ISMATCH"] != null && dt.Rows[n]["ISMATCH"].ToString() != "")
                        {
                            model.ISMATCH = dt.Rows[n]["ISMATCH"].ToString();
                        }
                        if (dt.Rows[n]["RESULTSTATUS"] != null && dt.Rows[n]["RESULTSTATUS"].ToString() != "")
                        {
                            model.RESULTSTATUS = dt.Rows[n]["RESULTSTATUS"].ToString();
                        }
                        if (dt.Rows[n]["TESTITEMDATETIME"] != null && dt.Rows[n]["TESTITEMDATETIME"].ToString() != "")
                        {
                            model.TESTITEMDATETIME = DateTime.Parse(dt.Rows[n]["TESTITEMDATETIME"].ToString());
                        }
                        if (dt.Rows[n]["REPORTVALUEALL"] != null && dt.Rows[n]["REPORTVALUEALL"].ToString() != "")
                        {
                            model.REPORTVALUEALL = dt.Rows[n]["REPORTVALUEALL"].ToString();
                        }
                        if (dt.Rows[n]["PARITEMNAME"] != null && dt.Rows[n]["PARITEMNAME"].ToString() != "")
                        {
                            model.PARITEMNAME = dt.Rows[n]["PARITEMNAME"].ToString();
                        }
                        if (dt.Rows[n]["PARITEMSNAME"] != null && dt.Rows[n]["PARITEMSNAME"].ToString() != "")
                        {
                            model.PARITEMSNAME = dt.Rows[n]["PARITEMSNAME"].ToString();
                        }
                        if (dt.Rows[n]["DISPORDER"] != null && dt.Rows[n]["DISPORDER"].ToString() != "")
                        {
                            model.DISPORDER = dt.Rows[n]["DISPORDER"].ToString();
                        }
                        if (dt.Rows[n]["ITEMORDER"] != null && dt.Rows[n]["ITEMORDER"].ToString() != "")
                        {
                            model.ITEMORDER = dt.Rows[n]["ITEMORDER"].ToString();
                        }
                        if (dt.Rows[n]["UNIT"] != null && dt.Rows[n]["UNIT"].ToString() != "")
                        {
                            model.UNIT = dt.Rows[n]["UNIT"].ToString();
                        }
                        if (dt.Rows[n]["SERIALNO"] != null && dt.Rows[n]["SERIALNO"].ToString() != "")
                        {
                            model.SERIALNO = dt.Rows[n]["SERIALNO"].ToString();
                        }
                        if (dt.Rows[n]["ZDY1"] != null && dt.Rows[n]["ZDY1"].ToString() != "")
                        {
                            model.ZDY1 = dt.Rows[n]["ZDY1"].ToString();
                        }
                        if (dt.Rows[n]["ZDY2"] != null && dt.Rows[n]["ZDY2"].ToString() != "")
                        {
                            model.ZDY2 = dt.Rows[n]["ZDY2"].ToString();
                        }
                        if (dt.Rows[n]["ZDY3"] != null && dt.Rows[n]["ZDY3"].ToString() != "")
                        {
                            model.ZDY3 = dt.Rows[n]["ZDY3"].ToString();
                        }
                        if (dt.Rows[n]["ZDY4"] != null && dt.Rows[n]["ZDY4"].ToString() != "")
                        {
                            model.ZDY4 = dt.Rows[n]["ZDY4"].ToString();
                        }
                        if (dt.Rows[n]["ZDY5"] != null && dt.Rows[n]["ZDY5"].ToString() != "")
                        {
                            model.ZDY5 = dt.Rows[n]["ZDY5"].ToString();
                        }
                        if (dt.Rows[n]["HISORDERNO"] != null && dt.Rows[n]["HISORDERNO"].ToString() != "")
                        {
                            model.HISORDERNO = dt.Rows[n]["HISORDERNO"].ToString();
                        }
                        //if (dt.Rows[n]["FORMNO"] != null && dt.Rows[n]["FORMNO"].ToString() != "")
                        //{
                        //    model.FORMNO = int.Parse(dt.Rows[n]["FORMNO"].ToString());
                        //}
                        modelList.Add(model);
                    }
                }

            }
            catch (Exception e)
            {

                ZhiFang.Common.Log.Log.Error(e.ToString());
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }

        public DataSet GetList(Model.ReportItemFull t)
        {
            return dal.GetList(t);
        }
        public DataSet GetList(string where)
        {
            return dal.GetList(where);
        }
        public List<Model.ReportItemFull> GetModelList(Model.ReportItemFull t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion
        public bool CheckReportItemCenter(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                ZhiFang.Common.Log.Log.Info("ReportItemFull转码字段:" + str);
                switch (str.ToUpper())
                {
                    case "SAMPLETYPENO":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportItemFull.Tables[0].Columns.Contains("SampleTypeNo"))
                        {
                            for (int i = 0; i < dsReportItemFull.Tables[0].Rows.Count; i++)
                            {
                                if (dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                {
                                    stringList.Add(dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                                }
                            }
                            if (stringList.Count > 0)
                            {
                                result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                                if (!result)
                                {
                                    for (int j = 0; j < stringList.Count; j++)
                                    {
                                        SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                                        SamplleTypeControl.LabCode = DestiOrgID;
                                        int count = idstc.GetTotalCount(SamplleTypeControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照\r\n", SamplleTypeControl.SampleTypeNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription = String.Format("实验室内样本类型为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                    case "ITEMNO":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportItemFull.Tables[0].Columns.Contains("PARITEMNO"))
                        {
                            for (int count = 0; count < dsReportItemFull.Tables[0].Rows.Count; count++)
                            {
                                if (dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString() != "")
                                {
                                    ZhiFang.Common.Log.Log.Info("项目编号:" + dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString());
                                    l.Add(dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString());
                                }
                                if (dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString() != "")
                                {
                                    ZhiFang.Common.Log.Log.Info("项目编号:" + dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString());
                                    l.Add(dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString());
                                }
                            }
                            if (l.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Info("项目个数:" + l.Count + "实验室编号:" + DestiOrgID);
                                result = idtic.CheckIncludeCenterCode(l, DestiOrgID);
                                if (!result)
                                {
                                    ZhiFang.Common.Log.Log.Info("CheckIncludeCenterCode:" + result);
                                    for (int n = 0; n < l.Count; n++)
                                    {
                                        TestItemControl.ItemNo = l[n].Trim();
                                        TestItemControl.ControlLabNo = DestiOrgID;
                                        int count = idtic.GetTotalCount(TestItemControl);
                                        if (count <= 0)
                                        {
                                            ZhiFang.Common.Log.Log.Info("中心端的ItemNo=" + TestItemControl.ItemNo + "的编号和实验室的未对照\r\n:");
                                            ReturnDescription += String.Format("中心端的ItemNo=" + TestItemControl.ItemNo + "的编号和实验室的未对照", TestItemControl.ItemNo);
                                        }
                                    }
                                    //return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("CheckReportItemCenter实验室内项目编码为空，无法进行对照");
                                //return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// 鄞州人民医院 报告现在转码依据B_ResultTestItemControl ganwh add 2015-09-11
        /// </summary>
        /// <param name="dsReportItemFull"></param>
        /// <param name="DestiOrgID"></param>
        /// <param name="ReturnDescription"></param>
        /// <returns></returns>
        public bool CheckReportItemCenter_yinzhou(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.ResultTestItemControl ResultTestItemControl = new Model.ResultTestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                ZhiFang.Common.Log.Log.Info("ReportItemFull转码字段:" + str);
                switch (str.ToUpper())
                {
                    case "SAMPLETYPENO":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportItemFull.Tables[0].Columns.Contains("SampleTypeNo"))
                        {
                            for (int i = 0; i < dsReportItemFull.Tables[0].Rows.Count; i++)
                            {
                                if (dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                {
                                    stringList.Add(dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                                }
                            }
                            if (stringList.Count > 0)
                            {
                                result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                                if (!result)
                                {
                                    for (int j = 0; j < stringList.Count; j++)
                                    {
                                        SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                                        SamplleTypeControl.LabCode = DestiOrgID;
                                        int count = idstc.GetTotalCount(SamplleTypeControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照\r\n", SamplleTypeControl.SampleTypeNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription = String.Format("实验室内样本类型为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                    case "ITEMNO":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportItemFull.Tables[0].Columns.Contains("PARITEMNO"))
                        {
                            for (int count = 0; count < dsReportItemFull.Tables[0].Rows.Count; count++)
                            {
                                if (dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString() != "")
                                {
                                    ZhiFang.Common.Log.Log.Info("项目编号:" + dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString());
                                    l.Add(dsReportItemFull.Tables[0].Rows[count]["PARITEMNO"].ToString());
                                }
                                if (dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString() != "")
                                {
                                    ZhiFang.Common.Log.Log.Info("项目编号:" + dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString());
                                    l.Add(dsReportItemFull.Tables[0].Rows[count]["ITEMNO"].ToString());
                                }
                            }
                            if (l.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Info("项目个数:" + l.Count + "实验室编号:" + DestiOrgID);
                                result = idrtic.CheckIncludeCenterCode(l, DestiOrgID);
                                if (!result)
                                {
                                    ZhiFang.Common.Log.Log.Info("CheckIncludeCenterCode:" + result);
                                    for (int n = 0; n < l.Count; n++)
                                    {
                                        ResultTestItemControl.ItemNo = l[n].Trim();
                                        ResultTestItemControl.ControlLabNo = DestiOrgID;
                                        int count = idrtic.GetTotalCount(ResultTestItemControl);
                                        if (count <= 0)
                                        {
                                            ZhiFang.Common.Log.Log.Info("中心端的ItemNo=" + TestItemControl.ItemNo + "的编号和实验室的未对照\r\n:");
                                            ReturnDescription += String.Format("中心端的ItemNo=" + TestItemControl.ItemNo + "的编号和实验室的未对照", TestItemControl.ItemNo);
                                        }
                                    }
                                    //return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("CheckReportItemCenter_yinzhou实验室内项目编码为空，无法进行对照");
                                //return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }

        public bool CheckReportItemLab(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportItemFull.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsReportItemFull.Tables[0].Rows.Count; i++)
                {
                    if (dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsReportItemFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeLabCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeControlNo = stringList[j].Trim();
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportItemFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                if (dsReportItemFull.Tables[0].Rows.Count > 0)
                {
                    for (int count = 0; count < dsReportItemFull.Tables[0].Rows.Count; count++)
                    {
                        if (dsReportItemFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                        {
                            l.Add(dsReportItemFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                        }
                    }
                }
                else
                {
                    l.Add(dsReportItemFull.Tables[0].Rows[0]["ParItemNo"].ToString());
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeLabCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ControlItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内ParItemNo={0}的编号和中心的未对照", TestItemControl.ControlItemNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("CheckReportItemLab实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportItemFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportItemFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportItemFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportItemFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportItemFull.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeLabCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderControlNo = ListStr[n].Trim();
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内GenderNo={0}的编号和中心的未对照", GenderType.GenderControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                    return false;
                }
            }
            return true;
        }

        public int DeleteByWhere(string where)
        {
            return dal.DeleteByWhere(where);
        }

        public int BackUpReportItemFullByWhere(string Strwhere)
        {
            return dal.BackUpReportItemFullByWhere(Strwhere);
        }
    }
}

