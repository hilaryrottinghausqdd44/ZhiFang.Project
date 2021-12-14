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
    /// 业务逻辑类District 的摘要说明。
    /// </summary>
    public class BDistrict 
    {
        private readonly IDDistrict dal = DalFactory<IDDistrict>.GetDal("District");
        public BDistrict()
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
        public bool Exists(int DistrictNo)
        {
            return dal.Exists(DistrictNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.District model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.District model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DistrictNo)
        {

            return dal.Delete(DistrictNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.District GetModel(int DistrictNo)
        {

            return dal.GetModel(DistrictNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.District GetModelByCache(int DistrictNo)
        {

            string CacheKey = "DistrictModel-" + DistrictNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DistrictNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.District)objModel;
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
        public DataSet GetList(Model.District model)
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
        public List<Model.District> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.District> DataTableToList(DataTable dt)
        {
            List<Model.District> modelList = new List<Model.District>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.District model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.District();
                    if (dt.Rows[n]["DistrictNo"].ToString() != "")
                    {
                        model.DistrictNo = int.Parse(dt.Rows[n]["DistrictNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
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
        public List<Model.District> GetModelList(Model.District model)
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
