using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //DepartmentControl		
    public partial class DepartmentControl : IBDepartmentControl, IBSynchData
    {
        IDAL.IDDepartmentControl dal = DALFactory.DalFactory<IDAL.IDDepartmentControl>.GetDal("B_DepartmentControl", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public DepartmentControl()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string DepartmentControlNo)
        {
            return dal.Exists(DepartmentControlNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.DepartmentControl model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.DepartmentControl model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string DepartmentControlNo)
        {
            return dal.Delete(DepartmentControlNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.DepartmentControl GetModel(string DepartmentControlNo)
        {
            return dal.GetModel(DepartmentControlNo);
        }


        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.DepartmentControl GetModelByCache(string DepartmentControlNo)
        {

            string CacheKey = "B_DepartmentControlModel-" + DepartmentControlNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DepartmentControlNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.DepartmentControl)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.DepartmentControl> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.DepartmentControl());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.DepartmentControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.DepartmentControl> modelList = new List<ZhiFang.Model.DepartmentControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.DepartmentControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.DepartmentControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("DepartmentControlNo") && dt.Rows[n]["DepartmentControlNo"].ToString() != "")
                    {
                        model.DepartmentControlNo = dt.Rows[n]["DepartmentControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("DeptNo") && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlDeptNo") && dt.Rows[n]["ControlDeptNo"].ToString() != "")
                    {
                        model.ControlDeptNo = int.Parse(dt.Rows[n]["ControlDeptNo"].ToString());
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

                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("LabDeptNo") && dt.Rows[n]["LabDeptNo"].ToString() != "")
                    {
                        model.LabDeptNo = dt.Rows[n]["LabDeptNo"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
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
        public List<ZhiFang.Model.Department> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.Department> modelList = new List<ZhiFang.Model.Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Department model;
                ZhiFang.Model.DepartmentControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Department();
                    a = new Model.DepartmentControl();
                    if (dt.Rows[n]["DeptNo"].ToString() != null && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = Convert.ToInt32(dt.Rows[n]["DeptNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlDeptNo"].ToString() != null && dt.Rows[n]["ControlDeptNo"].ToString() != "")
                    {
                        a.ControlDeptNo = Convert.ToInt32(dt.Rows[n]["ControlDeptNo"].ToString());
                    }

                    model.DepartmentControl = a;

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
        public DataSet GetList(ZhiFang.Model.DepartmentControl model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.DepartmentControl model)
        {
            return dal.GetTotalCount(model);
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

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

        #endregion

        #region 字典对照
        public DataSet GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}