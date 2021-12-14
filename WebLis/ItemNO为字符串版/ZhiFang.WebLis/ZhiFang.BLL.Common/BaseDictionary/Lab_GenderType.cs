using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_GenderType		
    public partial class Lab_GenderType : IBLab_GenderType, IBSynchData
    {
        IDAL.IDLab_GenderType dal = DALFactory.DalFactory<IDAL.IDLab_GenderType>.GetDal("B_Lab_GenderType", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_GenderType()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode, int LabGenderNo)
        {
            return dal.Exists(LabCode, LabGenderNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_GenderType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_GenderType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabGenderNo)
        {
            return dal.Delete(LabCode, LabGenderNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string GenderIDlist)
        {
            return dal.DeleteList(GenderIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_GenderType GetModel(string LabCode, int LabGenderNo)
        {
            return dal.GetModel(LabCode, LabGenderNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_GenderType GetModelByCache(string LabCode, int LabGenderNo)
        {

            string CacheKey = "b_lab_GenderTypeModel-" + LabCode + LabGenderNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabCode, LabGenderNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_GenderType)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_GenderType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(new ZhiFang.Model.Lab_GenderType());
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_GenderType> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_GenderType> modelList = new List<ZhiFang.Model.Lab_GenderType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_GenderType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_GenderType();
                    if (dt.Rows[n]["GenderID"].ToString() != "")
                    {
                        model.GenderID = int.Parse(dt.Rows[n]["GenderID"].ToString());
                    }
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Rows[n]["LabGenderNo"].ToString() != "")
                    {
                        model.LabGenderNo = int.Parse(dt.Rows[n]["LabGenderNo"].ToString());
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    } if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
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
        public DataSet GetList(ZhiFang.Model.Lab_GenderType model)
        {
            return dal.GetList(model);
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_GenderType model)
        {
            return dal.GetListByLike(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Lab_GenderType model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Lab_GenderType model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
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

    }
}