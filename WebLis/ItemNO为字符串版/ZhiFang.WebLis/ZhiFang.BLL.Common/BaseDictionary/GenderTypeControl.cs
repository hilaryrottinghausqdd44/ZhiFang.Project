using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //GenderTypeControl		
    public partial class GenderTypeControl : IBGenderTypeControl, IBSynchData
    {
        IDAL.IDGenderTypeControl dal = DALFactory.DalFactory<IDAL.IDGenderTypeControl>.GetDal("B_GenderTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public GenderTypeControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GenderControlNo)
        {
            return dal.Exists(GenderControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.GenderTypeControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.GenderTypeControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string GenderControlNo)
        {
            return dal.Delete(GenderControlNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.GenderTypeControl GetModel(string GenderControlNo)
        {
            return dal.GetModel(GenderControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.GenderTypeControl GetModelByCache(string GenderControlNo)
        {

            string CacheKey = "B_GenderTypeControlModel-" + GenderControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GenderControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.GenderTypeControl)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.GenderTypeControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.GenderTypeControl());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.GenderTypeControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.GenderTypeControl> modelList = new List<ZhiFang.Model.GenderTypeControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.GenderTypeControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.GenderTypeControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("GenderControlNo") && dt.Rows[n]["GenderControlNo"].ToString() != "")
                    {
                        model.GenderControlNo = dt.Rows[n]["GenderControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("GenderNo") && dt.Rows[n]["GenderNo"].ToString() != "")
                    {
                        model.GenderNo = int.Parse(dt.Rows[n]["GenderNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlGenderNo") && dt.Rows[n]["ControlGenderNo"].ToString() != "")
                    {
                        model.ControlGenderNo = int.Parse(dt.Rows[n]["ControlGenderNo"].ToString());
                    }
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }

                    if (dt.Columns.Contains("LabGenderNo") && dt.Rows[n]["LabGenderNo"].ToString() != "")
                    {
                        model.LabGenderNo = dt.Rows[n]["LabGenderNo"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        ////////2014-1-21
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.GenderType> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.GenderType> modelList = new List<ZhiFang.Model.GenderType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.GenderType model;
                ZhiFang.Model.GenderTypeControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.GenderType();
                    a = new Model.GenderTypeControl();

                    if (dt.Rows[n]["GenderNo"].ToString() != "" && dt.Rows[n]["GenderNo"].ToString() != null)
                    {
                        model.GenderNo = Convert.ToInt32(dt.Rows[n]["GenderNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlLabNo"].ToString() != "" && dt.Rows[n]["ControlLabNo"].ToString() != null)
                    {
                        a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }

                    model.GenderTypeControl = a;

                    modelList.Add(model);
                }
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
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.GenderTypeControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.GenderTypeControl model)
        {
            return dal.GetTotalCount(model);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion


        #region IBGenderTypeControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeLabCode(l, LabCode);
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeCenterCode(l, LabCode);
        }

        #endregion

        #region IBTransCodeControl 成员

        public string GetLabCodeNo(string LabCode, string CenterNo)
        {
            string LabCodeNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.GenderTypeControl model = new Model.GenderTypeControl();
                model.GenderNo = int.Parse(CenterNo.Trim());
                model.ControlLabNo = LabCode.Trim();
                model.ControlGenderNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlGenderNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.GenderTypeControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.GenderTypeControl model = new Model.GenderTypeControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlGenderNo = int.Parse(LabPrimaryNo.Trim());
                model.GenderNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["GenderNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.GenderTypeControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
            }
            return CenterNo;
        }

        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            return dal.GetLabCodeNo(LabCode, CenterNoList);
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            return dal.GetCenterNo(LabCode, LabPrimaryNoList);
        }
        #endregion

        #region 字典对照
        public DataSet GetListByPage(ZhiFang.Model.GenderTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.GenderTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}