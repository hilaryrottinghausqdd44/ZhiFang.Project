using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// 业务逻辑类SickType 的摘要说明。
    /// </summary>
    public class BSickType 
    {
        private readonly IDSickType dal = DalFactory<IDSickType>.GetDal("SickType");
        public BSickType()
        { }
        #region  成员方法

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
        public bool Exists(int SickTypeNo)
        {
            return dal.Exists(SickTypeNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.SickType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.SickType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SickTypeNo)
        {

            return dal.Delete(SickTypeNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.SickType GetModel(int SickTypeNo)
        {

            return dal.GetModel(SickTypeNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.SickType GetModelByCache(int SickTypeNo)
        {

            string CacheKey = "SickTypeModel-" + SickTypeNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SickTypeNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.SickType)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.SickType model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SickType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SickType> DataTableToList(DataTable dt)
        {
            List<Model.SickType> modelList = new List<Model.SickType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.SickType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.SickType();
                    if (dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SickType> GetModelList(Model.SickType model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法
    }
}
