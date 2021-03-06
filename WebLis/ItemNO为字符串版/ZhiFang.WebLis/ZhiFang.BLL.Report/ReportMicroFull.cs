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
    /// ReportMicroFull
    /// </summary>
    public partial class ReportMicroFull : ZhiFang.IBLL.Report.IBReportMicroFull
    {
        private readonly IDReportMicroFull dal = DalFactory<IDReportMicroFull>.GetDalByClassName("ReportMicroFull");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl.");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");

        public ReportMicroFull()
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
        public int Add(Model.ReportMicroFull model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportMicroFull model)
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportMicroFull GetModel(string ReportFormID, string ReportItemID)
        {

            return dal.GetModel(ReportFormID, ReportItemID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.ReportMicroFull GetModelByCache(string ReportFormID, string ReportItemID)
        {

            string CacheKey = "ReportMicroFullModel-" + ReportFormID + ReportItemID;
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
            return (Model.ReportMicroFull)objModel;
        }

        public DataSet GetColumns()
        {
            return dal.GetColumns();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportMicroFull> DataTableToList(DataTable dt)
        {
            List<Model.ReportMicroFull> modelList = new List<Model.ReportMicroFull>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.ReportMicroFull model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.ReportMicroFull();
                    if (dt.Rows[n]["ReportFormID"] != null && dt.Rows[n]["ReportFormID"].ToString() != "")
                    {
                        model.ReportFormID = dt.Rows[n]["ReportFormID"].ToString();
                    }
                    if (dt.Rows[n]["ReportItemID"] != null && dt.Rows[n]["ReportItemID"].ToString() != "")
                    {
                        model.ReportItemID = int.Parse(dt.Rows[n]["ReportItemID"].ToString());
                    }
                    if (dt.Rows[n]["ResultNo"] != null && dt.Rows[n]["ResultNo"].ToString() != "")
                    {
                        model.ResultNo = int.Parse(dt.Rows[n]["ResultNo"].ToString());
                    }
                    if (dt.Rows[n]["ItemNo"] != null && dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = int.Parse(dt.Rows[n]["ItemNo"].ToString());
                    }
                    if (dt.Rows[n]["ItemName"] != null && dt.Rows[n]["ItemName"].ToString() != "")
                    {
                        model.ItemName = dt.Rows[n]["ItemName"].ToString();
                    }
                    if (dt.Rows[n]["DescNo"] != null && dt.Rows[n]["DescNo"].ToString() != "")
                    {
                        model.DescNo = int.Parse(dt.Rows[n]["DescNo"].ToString());
                    }
                    if (dt.Rows[n]["DescName"] != null && dt.Rows[n]["DescName"].ToString() != "")
                    {
                        model.DescName = dt.Rows[n]["DescName"].ToString();
                    }
                    if (dt.Rows[n]["MicroNo"] != null && dt.Rows[n]["MicroNo"].ToString() != "")
                    {
                        model.MicroNo = int.Parse(dt.Rows[n]["MicroNo"].ToString());
                    }
                    if (dt.Rows[n]["MicroDesc"] != null && dt.Rows[n]["MicroDesc"].ToString() != "")
                    {
                        model.MicroDesc = dt.Rows[n]["MicroDesc"].ToString();
                    }
                    if (dt.Rows[n]["MicroName"] != null && dt.Rows[n]["MicroName"].ToString() != "")
                    {
                        model.MicroName = dt.Rows[n]["MicroName"].ToString();
                    }
                    if (dt.Rows[n]["AntiNo"] != null && dt.Rows[n]["AntiNo"].ToString() != "")
                    {
                        model.AntiNo = int.Parse(dt.Rows[n]["AntiNo"].ToString());
                    }
                    if (dt.Rows[n]["AntiName"] != null && dt.Rows[n]["AntiName"].ToString() != "")
                    {
                        model.AntiName = dt.Rows[n]["AntiName"].ToString();
                    }
                    if (dt.Rows[n]["Suscept"] != null && dt.Rows[n]["Suscept"].ToString() != "")
                    {
                        model.Suscept = dt.Rows[n]["Suscept"].ToString();
                    }
                    if (dt.Rows[n]["SusQuan"] != null && dt.Rows[n]["SusQuan"].ToString() != "")
                    {
                        model.SusQuan = decimal.Parse(dt.Rows[n]["SusQuan"].ToString());
                    }
                    if (dt.Rows[n]["RefRange"] != null && dt.Rows[n]["RefRange"].ToString() != "")
                    {
                        model.RefRange = dt.Rows[n]["RefRange"].ToString();
                    }
                    if (dt.Rows[n]["SusDesc"] != null && dt.Rows[n]["SusDesc"].ToString() != "")
                    {
                        model.SusDesc = dt.Rows[n]["SusDesc"].ToString();
                    }
                    if (dt.Rows[n]["AntiUnit"] != null && dt.Rows[n]["AntiUnit"].ToString() != "")
                    {
                        model.AntiUnit = dt.Rows[n]["AntiUnit"].ToString();
                    }
                    if (dt.Rows[n]["ItemDate"] != null && dt.Rows[n]["ItemDate"].ToString() != "")
                    {
                        model.ItemDate = DateTime.Parse(dt.Rows[n]["ItemDate"].ToString());
                    }
                    if (dt.Rows[n]["ItemTime"] != null && dt.Rows[n]["ItemTime"].ToString() != "")
                    {
                        model.ItemTime = DateTime.Parse(dt.Rows[n]["ItemTime"].ToString());
                    }
                    if (dt.Rows[n]["ItemDesc"] != null && dt.Rows[n]["ItemDesc"].ToString() != "")
                    {
                        model.ItemDesc = dt.Rows[n]["ItemDesc"].ToString();
                    }
                    if (dt.Rows[n]["EquipNo"] != null && dt.Rows[n]["EquipNo"].ToString() != "")
                    {
                        model.EquipNo = int.Parse(dt.Rows[n]["EquipNo"].ToString());
                    }
                    if (dt.Rows[n]["Modified"] != null && dt.Rows[n]["Modified"].ToString() != "")
                    {
                        model.Modified = int.Parse(dt.Rows[n]["Modified"].ToString());
                    }
                    if (dt.Rows[n]["IsMatch"] != null && dt.Rows[n]["IsMatch"].ToString() != "")
                    {
                        model.IsMatch = int.Parse(dt.Rows[n]["IsMatch"].ToString());
                    }
                    if (dt.Rows[n]["CheckType"] != null && dt.Rows[n]["CheckType"].ToString() != "")
                    {
                        model.CheckType = int.Parse(dt.Rows[n]["CheckType"].ToString());
                    }
                    if (dt.Rows[n]["SerialNo"] != null && dt.Rows[n]["SerialNo"].ToString() != "")
                    {
                        model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
                    }
                    if (dt.Rows[n]["FormNo"] != null && dt.Rows[n]["FormNo"].ToString() != "")
                    {
                        model.FormNo = int.Parse(dt.Rows[n]["FormNo"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public DataSet GetList(Model.ReportMicroFull t)
        {
            return dal.GetList(t);
        }

        public List<Model.ReportMicroFull> GetModelList(Model.ReportMicroFull t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataSet GetReportMicroGroupList(string FormNo)
        {
            return dal.GetReportMicroGroupList(FormNo);
        }

        #endregion

        #region IBLLBase<ReportMicroFull> 成员


        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool CheckReportMicroCenter(DataSet dsReportMicroFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportMicroFull.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsReportMicroFull.Tables[0].Rows.Count; i++)
                {

                    if (dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
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
                                ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照", SamplleTypeControl.SampleTypeNo);
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
            if (dsReportMicroFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsReportMicroFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeCenterCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("中心端的ParItemNo={0}的编号和实验室的未对照", TestItemControl.ItemNo);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription = String.Format("CheckReportMicroCenter实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportMicroFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportMicroFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeCenterCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderNo = Convert.ToInt32(ListStr[n].Trim());
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室的未对照", GenderType.GenderNo);
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

        public bool CheckReportMicroLab(DataSet dsReportMicroFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportMicroFull.Tables[0].Columns.Contains("SampleType"))
            {
                for (int i = 0; i < dsReportMicroFull.Tables[0].Rows.Count; i++)
                {
                    if (dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsReportMicroFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
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
                                ReturnDescription = String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo);
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
            if (dsReportMicroFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsReportMicroFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsReportMicroFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
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
                    ReturnDescription += String.Format("CheckReportMicroLab实验室内项目编码为空，无法进行对照");
                    return false;
                }
            }
            if (dsReportMicroFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportMicroFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportMicroFull.Tables[0].Rows[count]["GenderNo"].ToString());
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

        public int DeleteByWhere(string Strwhere)
        {
            return dal.DeleteByWhere(Strwhere);
        }
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

         public int BackUpReportMicroFullByWhere(string Strwhere)
        {
            return dal.BackUpReportMicroFullByWhere(Strwhere);
        }
    }
}

