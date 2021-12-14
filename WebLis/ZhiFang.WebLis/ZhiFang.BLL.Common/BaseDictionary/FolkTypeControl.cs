using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //FolkTypeControl		
    public partial class FolkTypeControl : IBSynchData, IBFolkTypeControl
    {
        IDAL.IDFolkTypeControl dal = DALFactory.DalFactory<IDAL.IDFolkTypeControl>.GetDal("B_FolkTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public FolkTypeControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string FolkControlNo)
        {
            return dal.Exists(FolkControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.FolkTypeControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.FolkTypeControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string FolkControlNo)
        {
            return dal.Delete(FolkControlNo);
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
        public ZhiFang.Model.FolkTypeControl GetModel(string FolkControlNo)
        {
            return dal.GetModel(FolkControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.FolkTypeControl GetModelByCache(string FolkControlNo)
        {

            string CacheKey = "B_FolkTypeControlModel-" + FolkControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(FolkControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.FolkTypeControl)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.FolkTypeControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.FolkTypeControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.FolkTypeControl> modelList = new List<ZhiFang.Model.FolkTypeControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.FolkTypeControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.FolkTypeControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("FolkControlNo") && dt.Rows[n]["FolkControlNo"].ToString() != "")
                    {
                        model.FolkControlNo = dt.Rows[n]["FolkControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("FolkNo") && dt.Rows[n]["FolkNo"].ToString() != "")
                    {
                        model.FolkNo = int.Parse(dt.Rows[n]["FolkNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlFolkNo") && dt.Rows[n]["ControlFolkNo"].ToString() != "")
                    {
                        model.ControlFolkNo = int.Parse(dt.Rows[n]["ControlFolkNo"].ToString());
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
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    if (dt.Columns.Contains("LabFolkNo") && dt.Rows[n]["LabFolkNo"].ToString() != "")
                    {
                        model.LabFolkNo = dt.Rows[n]["LabFolkNo"].ToString();
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
        public List<ZhiFang.Model.FolkType > ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.FolkType> modelList = new List<ZhiFang.Model.FolkType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.FolkType model;
                ZhiFang.Model.FolkTypeControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.FolkType();
                    a = new Model.FolkTypeControl();
                    if (dt.Rows[n]["FolkNo"].ToString() != null && dt.Rows[n]["FolkNo"].ToString()!="")
                    {
                        model.FolkNo = Convert.ToInt32(dt.Rows[n]["FolkNo"].ToString());
                    }                   
                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlFolkNo"].ToString() != null && dt.Rows[n]["ControlFolkNo"].ToString()!="")
                    {
                        a.ControlFolkNo = Convert.ToInt32(dt.Rows[n]["ControlFolkNo"].ToString());
                    }                    
                    model.FolkTypeControl = a;

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
        public DataSet GetList(ZhiFang.Model.FolkTypeControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.FolkTypeControl model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion


        #region IBBase<FolkTypeControl> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBTransCodeControl 成员

        public string GetLabCodeNo(string LabCode, string CenterNo)
        {
            string LabCodeNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.FolkTypeControl model = new Model.FolkTypeControl();
                model.FolkNo = int.Parse(CenterNo.Trim());
                model.ControlLabNo = LabCode.Trim();
                model.ControlFolkNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlFolkNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.FolkTypeControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.FolkTypeControl model = new Model.FolkTypeControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlFolkNo = int.Parse(LabPrimaryNo.Trim());
                model.FolkNo = -1;
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["FolkNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.FolkTypeControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
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

        #region IBFolkTypeControl 成员


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
        public DataSet GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion

    }
}