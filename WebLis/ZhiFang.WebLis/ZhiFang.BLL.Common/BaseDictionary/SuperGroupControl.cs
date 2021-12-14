using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //SuperGroupControl		
    public partial class SuperGroupControl : IBSynchData, IBSuperGroupControl
    {
        IDAL.IDSuperGroupControl dal = DALFactory.DalFactory<IDAL.IDSuperGroupControl>.GetDal("B_SuperGroupControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public SuperGroupControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SuperGroupControlNo)
        {
            return dal.Exists(SuperGroupControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SuperGroupControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SuperGroupControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SuperGroupControlNo)
        {
            return dal.Delete(SuperGroupControlNo);
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
        public ZhiFang.Model.SuperGroupControl GetModel(string SuperGroupControlNo)
        {
            return dal.GetModel(SuperGroupControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.SuperGroupControl GetModelByCache(string SuperGroupControlNo)
        {

            string CacheKey = "B_SuperGroupControlModel-" + SuperGroupControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SuperGroupControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.SuperGroupControl)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SuperGroupControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SuperGroupControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.SuperGroupControl> modelList = new List<ZhiFang.Model.SuperGroupControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SuperGroupControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SuperGroupControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("SuperGroupControlNo") && dt.Rows[n]["SuperGroupControlNo"].ToString() != "")
                    {
                        model.SuperGroupControlNo = dt.Rows[n]["SuperGroupControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("SuperGroupNo") && dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    {
                        model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlSuperGroupNo") && dt.Rows[n]["ControlSuperGroupNo"].ToString() != "")
                    {
                        model.ControlSuperGroupNo = int.Parse(dt.Rows[n]["ControlSuperGroupNo"].ToString());
                    }
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
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
                    if (dt.Columns.Contains("LabSuperGroupNo") && dt.Rows[n]["LabSuperGroupNo"].ToString() != "")
                    {
                        model.LabSuperGroupNo = dt.Rows[n]["LabSuperGroupNo"].ToString();
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
        public List<ZhiFang.Model.SuperGroup> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.SuperGroup> modelList = new List<ZhiFang.Model.SuperGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SuperGroup model;
                ZhiFang.Model.SuperGroupControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SuperGroup();
                    a = new Model.SuperGroupControl();
                    if (dt.Rows[n]["SuperGroupNo"].ToString() != null && dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    {
                        model.SuperGroupNo = Convert.ToInt32(dt.Rows[n]["SuperGroupNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlSuperGroupNo"].ToString() != null && dt.Rows[n]["ControlSuperGroupNo"].ToString() != "")
                    {
                        a.ControlSuperGroupNo = Convert.ToInt32(dt.Rows[n]["ControlSuperGroupNo"].ToString());
                    }

                    model.SuperGroupControl = a;

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
        public DataSet GetList(ZhiFang.Model.SuperGroupControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.SuperGroupControl model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion


        #region IBBase<SuperGroupControl> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion

        #region IBTransCodeControl 成员

        public string GetLabCodeNo(string LabCode, string CenterNo)
        {
            string LabCodeNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.SuperGroupControl model = new Model.SuperGroupControl();
                model.SuperGroupNo = int.Parse(CenterNo.Trim());
                model.ControlLabNo = LabCode.Trim();
                model.ControlSuperGroupNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SuperGroupControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.SuperGroupControl model = new Model.SuperGroupControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlSuperGroupNo = int.Parse(LabPrimaryNo.Trim());
                model.SuperGroupNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["SuperGroupNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SuperGroupControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
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

        #region IBSuperGroupControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeLabCode(l, LabCode);
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeCenterCode(l, LabCode);
        }

        #endregion

        #region 字典对照
        public DataSet GetListByPage(ZhiFang.Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}