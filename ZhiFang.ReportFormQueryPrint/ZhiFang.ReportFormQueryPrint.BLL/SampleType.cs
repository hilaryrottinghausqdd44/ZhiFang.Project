using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BSampleType 
    {
        private readonly IDSampleType dal = DalFactory<IDSampleType>.GetDal("SampleType");
        public BSampleType()
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
        public bool Exists(int SampleTypeNo)
        {
            return dal.Exists(SampleTypeNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.SampleType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.SampleType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SampleTypeNo)
        {

            return dal.Delete(SampleTypeNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.SampleType GetModel(int SampleTypeNo)
        {

            return dal.GetModel(SampleTypeNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.SampleType GetModelByCache(int SampleTypeNo)
        {

            string CacheKey = "SampleTypeModel-" + SampleTypeNo ;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SampleTypeNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.SampleType)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<Model.SampleType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SampleType> DataTableToList(DataTable dt)
        {
            List<Model.SampleType> modelList = new List<Model.SampleType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.SampleType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.SampleType();
                    if (dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.SampleType model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SampleType> GetModelList(Model.SampleType model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion  成员方法
    }
}
