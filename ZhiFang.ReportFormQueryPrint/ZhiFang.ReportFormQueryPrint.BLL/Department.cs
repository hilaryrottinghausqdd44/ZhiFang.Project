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
    /// 业务逻辑类Department 的摘要说明。
    /// </summary>
    public class BDepartment 
    {
        private readonly IDDepartment dal = DalFactory<IDDepartment>.GetDal("Department");
        public BDepartment()
        { }

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
        public bool Exists(int DeptNo)
        {
            return dal.Exists(DeptNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Department model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.Department model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DeptNo)
        {

            return dal.Delete(DeptNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Department GetModel(int DeptNo)
        {

            return dal.GetModel(DeptNo);
        }

        public Model.Department GetModel(string where)
        {

            return dal.GetModel(where);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Model.Department GetModelByCache(int DeptNo)
        {

            string CacheKey = "DepartmentModel-" + DeptNo;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DeptNo);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.Department)objModel;
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
        public DataSet GetList(Model.Department model)
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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string Where, int page, int limit)
        {
            return dal.GetList(Where,  page, limit);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Department> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Department> DataTableToList(DataTable dt)
        {
            List<Model.Department> modelList = new List<Model.Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Department model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Department();
                    if (dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
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
        public List<Model.Department> GetModelList(Model.Department model)
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
    }
}
