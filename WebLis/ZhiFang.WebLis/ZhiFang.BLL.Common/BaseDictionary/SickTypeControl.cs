using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //SickTypeControl		
    public partial class SickTypeControl : IBSickTypeControl, IBSynchData
    {
        IDAL.IDSickTypeControl dal = DALFactory.DalFactory<IDAL.IDSickTypeControl>.GetDal("B_SickTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public SickTypeControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SickTypeControlNo)
        {
            return dal.Exists(SickTypeControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SickTypeControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SickTypeControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SickTypeControlNo)
        {
            return dal.Delete(SickTypeControlNo);
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
        public ZhiFang.Model.SickTypeControl GetModel(string SickTypeControlNo)
        {
            return dal.GetModel(SickTypeControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.SickTypeControl GetModelByCache(string SickTypeControlNo)
        {

            string CacheKey = "B_SickTypeControlModel-" + SickTypeControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SickTypeControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.SickTypeControl)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SickTypeControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.SickTypeControl());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SickTypeControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.SickTypeControl> modelList = new List<ZhiFang.Model.SickTypeControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SickTypeControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SickTypeControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("SickTypeControlNo") && dt.Rows[n]["SickTypeControlNo"].ToString() != "")
                    {
                        model.SickTypeControlNo = dt.Rows[n]["SickTypeControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("SickTypeNo") && dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlSickTypeNo") && dt.Rows[n]["ControlSickTypeNo"].ToString() != "")
                    {
                        model.ControlSickTypeNo = int.Parse(dt.Rows[n]["ControlSickTypeNo"].ToString());
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
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("LabSickTypeNo") && dt.Rows[n]["LabSickTypeNo"].ToString() != "")
                    {
                        model.LabSickTypeNo = dt.Rows[n]["LabSickTypeNo"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
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
        public List<ZhiFang.Model.SickType> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.SickType> modelList = new List<ZhiFang.Model.SickType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SickType model;
                ZhiFang.Model.SickTypeControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SickType();
                    a = new Model.SickTypeControl();
                    if (dt.Rows[n]["SickTypeNo"].ToString() != "" && dt.Rows[n]["SickTypeNo"].ToString() != null)
                    {
                        model.SickTypeNo = Convert.ToInt32(dt.Rows[n]["SickTypeNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlSickTypeNo"].ToString() != "" && dt.Rows[n]["ControlSickTypeNo"].ToString() != null)
                    {
                        a.ControlSickTypeNo = Convert.ToInt32(dt.Rows[n]["ControlSickTypeNo"].ToString());
                    }

                    model.SickTypeControl = a;

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
        public DataSet GetList(ZhiFang.Model.SickTypeControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.SickTypeControl model)
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


        #region IBSickTypeControl 成员


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
                Model.SickTypeControl model = new Model.SickTypeControl();
                model.SickTypeNo = int.Parse(CenterNo.Trim());
                model.ControlLabNo = LabCode.Trim();
                model.ControlSickTypeNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlSickTypeNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SickTypeControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.SickTypeControl model = new Model.SickTypeControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlSickTypeNo = int.Parse(LabPrimaryNo.Trim());
                model.SickTypeNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["SickTypeNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SickTypeControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
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
        public DataSet GetListByPage(ZhiFang.Model.SickTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.SickTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}