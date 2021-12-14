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
    /// 业务逻辑类Doctor 的摘要说明。
    /// </summary>
    public class BDoctor
    {
        private readonly IDDoctor dal = DalFactory<IDDoctor>.GetDal("Doctor");
        public BDoctor()
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
        public bool Exists(int DoctorNo)
        {
            return dal.Exists(DoctorNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Doctor model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.Doctor model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DoctorNo)
        {

            return dal.Delete(DoctorNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Doctor GetModel(int DoctorNo)
        {

            return dal.GetModel(DoctorNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.Doctor GetModelByCache(int DoctorNo)
        {

            string CacheKey = "DoctorModel-" + DoctorNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DoctorNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.Doctor)objModel;
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
        public DataSet GetList(Model.Doctor model)
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
        public List<Model.Doctor> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Doctor> DataTableToList(DataTable dt)
        {
            List<Model.Doctor> modelList = new List<Model.Doctor>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Doctor model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Doctor();
                    if (dt.Rows[n]["DoctorNo"].ToString() != "")
                    {
                        model.DoctorNo = int.Parse(dt.Rows[n]["DoctorNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
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
        public List<Model.Doctor> GetModelList(Model.Doctor model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
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
