using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //PGroupControl		
    public partial class PGroupControl : IBSynchData, IBPGroupControl
    {
        IDAL.IDPGroupControl dal = DALFactory.DalFactory<IDAL.IDPGroupControl>.GetDal("B_PGroupControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public PGroupControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SectionControlNo)
        {
            return dal.Exists(SectionControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PGroupControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.PGroupControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SectionControlNo)
        {
            return dal.Delete(SectionControlNo);
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
        public ZhiFang.Model.PGroupControl GetModel(string SectionControlNo)
        {
            return dal.GetModel(SectionControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.PGroupControl GetModelByCache(string SectionControlNo)
        {

            string CacheKey = "B_PGroupControlModel-" + SectionControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SectionControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.PGroupControl)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.PGroupControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.PGroupControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.PGroupControl> modelList = new List<ZhiFang.Model.PGroupControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.PGroupControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.PGroupControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("SectionControlNo") && dt.Rows[n]["SectionControlNo"].ToString() != "")
                    {
                        model.SectionControlNo = dt.Rows[n]["SectionControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("SectionNo") && dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlSectionNo") && dt.Rows[n]["ControlSectionNo"].ToString() != "")
                    {
                        model.ControlSectionNo = int.Parse(dt.Rows[n]["ControlSectionNo"].ToString());
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
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("LabSectionNo") && dt.Rows[n]["LabSectionNo"].ToString() != "")
                    {
                        model.LabSectionNo = dt.Rows[n]["LabSectionNo"].ToString();
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


        ////////2014-1-21
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.PGroup> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.PGroup> modelList = new List<ZhiFang.Model.PGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.PGroup model;
                ZhiFang.Model.PGroupControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.PGroup();
                    a = new Model.PGroupControl();
                    if (dt.Rows[n]["SectionNo"].ToString() != "" && dt.Rows[n]["SectionNo"].ToString() != null)
                    {
                        model.SectionNo = Convert.ToInt32(dt.Rows[n]["SectionNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlSectionNo"].ToString() != "" && dt.Rows[n]["ControlSectionNo"].ToString() != null)
                    {
                        a.ControlSectionNo = Convert.ToInt32(dt.Rows[n]["ControlSectionNo"].ToString());
                    }

                    model.PGroupControl = a;

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
        public DataSet GetList(ZhiFang.Model.PGroupControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.PGroupControl model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion


        #region IBBase<PGroupControl> 成员


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

        #region 字典对照
        public DataSet GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}