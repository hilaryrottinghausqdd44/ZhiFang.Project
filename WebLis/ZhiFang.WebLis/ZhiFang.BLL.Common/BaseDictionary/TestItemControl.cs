using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //B_TestItemControl
    public partial class TestItemControl : IBSynchData, IBTestItemControl, IBBatchCopy
    {
        IDAL.IDTestItemControl dal = DALFactory.DalFactory<IDAL.IDTestItemControl>.GetDal("B_TestItemControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDBatchCopy dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_TestItemControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
        public TestItemControl()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItemControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItemControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ItemControlNo)
        {
            return dal.Delete(ItemControlNo);
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
        public ZhiFang.Model.TestItemControl GetModel(string ItemNo, string LabCode, string LabItemNo)
        {

            return dal.GetModel(ItemNo, LabCode, LabItemNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.TestItemControl GetModelByCache(string ItemNo, string LabCode, string LabItemNo)
        {

            string CacheKey = "B_TestItemControlModel-" + ItemNo + "-" + LabCode + "-" + LabItemNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ItemNo, LabCode, LabItemNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.TestItemControl)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.TestItemControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.TestItemControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.TestItemControl> modelList = new List<ZhiFang.Model.TestItemControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.TestItemControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.TestItemControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("ItemControlNo") && dt.Rows[n]["ItemControlNo"].ToString() != "")
                    {
                        model.ItemControlNo = dt.Rows[n]["ItemControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("ItemNo") && dt.Rows[n]["ItemNo"].ToString() != "")
                    {
                        model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlItemNo") && dt.Rows[n]["ControlItemNo"].ToString() != "")
                    {
                        model.ControlItemNo = dt.Rows[n]["ControlItemNo"].ToString();
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

                    #region
                    if (dt.Columns.Contains("CenterItemNo") && dt.Rows[n]["CenterItemNo"].ToString() != "")
                    {
                        model.CenterItemNo = dt.Rows[n]["CenterItemNo"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    if (dt.Columns.Contains("LabItemNo") && dt.Rows[n]["LabItemNo"].ToString() != "")
                    {
                        model.LabItemNo = dt.Rows[n]["LabItemNo"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("ShortName") && dt.Rows[n]["ShortName"].ToString() != "")
                    {
                        model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    }
                    #endregion
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        ////////2014-1-21
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.TestItem> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.TestItem> modelList = new List<ZhiFang.Model.TestItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.TestItem model;
                ZhiFang.Model.TestItemControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.TestItem();
                    a = new Model.TestItemControl();

                    model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    a.ControlItemNo = dt.Rows[n]["ControlItemNo"].ToString();
                    model.TestItemControl = a;

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        //////////

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
        public DataSet GetList(ZhiFang.Model.TestItemControl model)
        {
            return dal.GetList(model);
        }
        #endregion


        #region IBBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }

        #endregion

        #region IBBase<TestItemControl> 成员


        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(ZhiFang.Model.TestItemControl model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBTestItemControl 成员

        public bool Exists(string ItemControlNo)
        {
            return dal.Exists(ItemControlNo);
        }

        #endregion

        #region IBBase<TestItemControl> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

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

        #region IBTestItemControl 成员


        public string GetCenterCode(string LabCode, string LabItemNo)
        {
            DataSet ds = dal.GetList(new Model.TestItemControl { ControlLabNo = LabCode, ControlItemNo = LabItemNo });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ItemNo"].ToString().Trim();
            }
            else
            {
                return "-1";
            }
        }

        public string GetClientCode(string LabCode, string ItemNo)
        {
            DataSet ds = dal.GetList(new Model.TestItemControl { ControlLabNo = LabCode, ItemNo = ItemNo });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ControlItemNo"].ToString().Trim();
            }
            else
            {
                return "-1";
            }
        }

        #endregion

        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
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
                Model.TestItemControl model = new Model.TestItemControl();
                model.ItemNo = CenterNo.Trim();
                model.ControlLabNo = LabCode.Trim();
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LabCodeNo = ds.Tables[0].Rows[0]["ControlItemNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                LabCodeNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.TestItemControl.GetLabCodeNo异常 ||| CenterNo=" + CenterNo, ex);
            }
            return LabCodeNo;
        }

        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            DataSet ds = new DataSet();
            try
            {
                Model.TestItemControl model = new Model.TestItemControl();
                model.ControlLabNo = LabCode.Trim();
                model.ControlItemNo = LabPrimaryNo.Trim();
                ds = dal.GetList(model);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CenterNo = ds.Tables[0].Rows[0]["ItemNo"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.TestItemControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
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

        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeLabCode(l, LabCode);
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            return dal.CheckIncludeCenterCode(l, LabCode);
        }

        public DataSet GetCenterCodeMapList(string hiddenClient, string itemlist)
        {
            List<string> itemno = new List<string>();
            string[] itemlista = itemlist.TrimEnd(',').Split(',');
            foreach (var a in itemlista)
            {
                itemno.Add(a);
            }
            return dal.GetCenterNo(hiddenClient, itemno);
        }

        public DataSet GetLabItemCodeMapListByNRequestLabCodeAndFormNo(string LabCode, string NRequestFormNo)
        {
            return dal.GetLabItemCodeMapListByNRequestLabCodeAndFormNo(LabCode, NRequestFormNo);
        }

        #endregion

        #region 项目字典对照
        public DataSet GetListByPage(ZhiFang.Model.TestItemControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.TestItemControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetResultListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetResultListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}
