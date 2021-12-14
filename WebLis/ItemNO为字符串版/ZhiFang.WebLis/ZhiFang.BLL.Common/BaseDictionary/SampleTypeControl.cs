using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //SampleTypeControl		
    public partial class SampleTypeControl : IBSynchData, IBSampleTypeControl
    {
        IDAL.IDSampleTypeControl dal = DALFactory.DalFactory<IDAL.IDSampleTypeControl>.GetDal("B_SampleTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public SampleTypeControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SampleTypeControlNo)
        {
            return dal.Exists(SampleTypeControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.SampleTypeControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.SampleTypeControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SampleTypeControlNo)
        {
            return dal.Delete(SampleTypeControlNo);
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
        public ZhiFang.Model.SampleTypeControl GetModel(string SampleTypeControlNo)
        {
            return dal.GetModel(SampleTypeControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.SampleTypeControl GetModelByCache(string SampleTypeControlNo)
        {

            string CacheKey = "B_SampleTypeControlModel-" + SampleTypeControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SampleTypeControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.SampleTypeControl)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SampleTypeControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SampleTypeControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.SampleTypeControl> modelList = new List<ZhiFang.Model.SampleTypeControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SampleTypeControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SampleTypeControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("SampleTypeControlNo") && dt.Rows[n]["SampleTypeControlNo"].ToString() != "")
                    {
                        model.SampleTypeControlNo = dt.Rows[n]["SampleTypeControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("SampleTypeNo") && dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlSampleTypeNo") && dt.Rows[n]["ControlSampleTypeNo"].ToString() != "")
                    {
                        model.ControlSampleTypeNo = dt.Rows[n]["ControlSampleTypeNo"].ToString();
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
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("LabSampleTypeNo") && dt.Rows[n]["LabSampleTypeNo"].ToString() != "")
                    {
                        model.LabSampleTypeNo = dt.Rows[n]["LabSampleTypeNo"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        ////////
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SampleType> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.SampleType> modelList = new List<ZhiFang.Model.SampleType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SampleType model;
                ZhiFang.Model.SampleTypeControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SampleType();
                    a = new Model.SampleTypeControl();

                    model.SampleTypeNo = Convert.ToInt32(dt.Rows[n]["SampleTypeNo"].ToString());
                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlSampleTypeNo"].ToString() != null && dt.Rows[n]["ControlSampleTypeNo"].ToString() != "")
                    {
                        a.ControlSampleTypeNo = dt.Rows[n]["ControlSampleTypeNo"].ToString();
                    }

                    model.SampleTypeControl = a;

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
        public DataSet GetList(ZhiFang.Model.SampleTypeControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.SampleTypeControl model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion


        #region IBBase<SampleTypeControl> 成员


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
                Model.SampleTypeControl model = new Model.SampleTypeControl();
                model.SampleTypeNo = int.Parse(CenterNo.Trim());
                model.ControlLabNo = LabCode.Trim();
                model.ControlSampleTypeNo = "-1";
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlSampleTypeNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SampleTypeControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.SampleTypeControl model = new Model.SampleTypeControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlSampleTypeNo = LabPrimaryNo.Trim();
                model.SampleTypeNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["SampleTypeNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SampleTypeControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
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

        #region IBSampleTypeControl 成员


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
        public DataSet GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}
