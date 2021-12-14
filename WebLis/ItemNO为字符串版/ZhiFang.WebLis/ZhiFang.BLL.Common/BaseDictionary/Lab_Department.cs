using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_Department		
    public partial class Lab_Department : IBLab_Department, IBSynchData 
    {
      
        IDAL.IDLab_Department dal = DALFactory.DalFactory<IDAL.IDLab_Department>.GetDal("B_Lab_Department", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_Department()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode, int LabDeptNo)
        {
            return dal.Exists(LabCode, LabDeptNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_Department model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_Department model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabDeptNo)
        {
            return dal.Delete(LabCode, LabDeptNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_Department GetModel(string LabCode, int LabDeptNo)
        {
            return dal.GetModel(LabCode, LabDeptNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_Department GetModelByCache(string LabCode, int LabDeptNo)
        {

            string CacheKey = "B_Lab_DepartmentModel-" + LabCode + LabDeptNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabCode, LabDeptNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_Department)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_Department> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.Lab_Department());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_Department> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_Department> modelList = new List<ZhiFang.Model.Lab_Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_Department model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_Department();
                    if (dt.Rows[n]["DepartmentID"].ToString() != "")
                    {
                        model.DepartmentID = int.Parse(dt.Rows[n]["DepartmentID"].ToString());
                    } if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Rows[n]["LabDeptNo"].ToString() != "")
                    {
                        model.LabDeptNo = int.Parse(dt.Rows[n]["LabDeptNo"].ToString());
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
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_Department model)
        {
            return dal.GetList(model);
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_Department model)
        {
            return dal.GetListByLike(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Lab_Department model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Lab_Department model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }
        public bool Exists(System.Collections.Hashtable ht)
        {
           //return dal.Exists(ht);
            throw new NotImplementedException();
        }
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        public int AddByDataRow(DataRow dr)
        {
            //return dal.AddByDataRow(dr);
            throw new NotImplementedException();
        }
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        public int UpdateByDataRow(DataRow dr)
        {
           // return dal.UpdateByDataRow(dr);
            throw new NotImplementedException();
        }

        #endregion

    }
}