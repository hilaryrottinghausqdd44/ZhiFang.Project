using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_Doctor		
    public partial class Lab_Doctor : IBSynchData, IBLab_Doctor
    {
        IDAL.IDLab_Doctor dal = DALFactory.DalFactory<IDAL.IDLab_Doctor>.GetDal("B_Lab_Doctor", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_Doctor()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode, int LabDoctorNo)
        {
            return dal.Exists(LabCode, LabDoctorNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_Doctor model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_Doctor model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabDoctorNo)
        {
            return dal.Delete(LabCode, LabDoctorNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string DoctorIDlist)
        {
            return dal.DeleteList(DoctorIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_Doctor GetModel(string LabCode, int LabDoctorNo)
        {
            return dal.GetModel(LabCode, LabDoctorNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_Doctor GetModelByCache(string LabCode, int LabDoctorNo)
        {

            string CacheKey = "B_Lab_DoctorModel-" + LabCode + LabDoctorNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabCode, LabDoctorNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_Doctor)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_Doctor> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
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
        public List<ZhiFang.Model.Lab_Doctor> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_Doctor> modelList = new List<ZhiFang.Model.Lab_Doctor>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_Doctor model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_Doctor();
                    if (dt.Rows[n]["DoctorID"].ToString() != "")
                    {
                        model.DoctorID = int.Parse(dt.Rows[n]["DoctorID"].ToString());
                    }
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Rows[n]["LabDoctorNo"].ToString() != "")
                    {
                        model.LabDoctorNo = int.Parse(dt.Rows[n]["LabDoctorNo"].ToString());
                    }
                    if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
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
        public DataSet GetList(ZhiFang.Model.Lab_Doctor model)
        {
            return dal.GetList(model);
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_Doctor model)
        {
            return dal.GetListByLike(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Lab_Doctor model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Lab_Doctor model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #endregion


        #region IBBase<Lab_Doctor> 成员


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
    }
}