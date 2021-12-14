using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //LabDownLoadInfo		
    public partial class LabDownLoadInfo :IBSynchData, IBLabDownLoadInfo
    {
        
        IDAL.IDLabDownLoadInfo dal = DALFactory.DalFactory<IDAL.IDLabDownLoadInfo>.GetDal("D_LabDownLoadInfo", ZhiFang.Common.Dictionary.DBSource.LisDB());
        
        public LabDownLoadInfo()
        {
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TableName)
        {
            return dal.Exists(TableName);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.LabDownLoadInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.LabDownLoadInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string TableName)
        {
            return dal.Delete(TableName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.LabDownLoadInfo GetModel(string TableName)
        {
            return dal.GetModel(TableName);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.LabDownLoadInfo GetModelByCache(string TableName)
        {

            string CacheKey = "D_LabDownLoadInfoModel-" + TableName;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TableName);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.LabDownLoadInfo)objModel;
        }

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.LabDownLoadInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.LabDownLoadInfo> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.LabDownLoadInfo> modelList = new List<ZhiFang.Model.LabDownLoadInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.LabDownLoadInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.LabDownLoadInfo();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    model.TableName = dt.Rows[n]["TableName"].ToString();
                    if (dt.Rows[n]["ServerTime"].ToString() != "")
                    {
                        model.ServerTime = int.Parse(dt.Rows[n]["ServerTime"].ToString());
                    }
                    if (dt.Rows[n]["LocalTime"].ToString() != "")
                    {
                        model.LocalTime = DateTime.Parse(dt.Rows[n]["LocalTime"].ToString());
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["DownLoadCount"].ToString() != "")
                    {
                        model.DownLoadCount = int.Parse(dt.Rows[n]["DownLoadCount"].ToString());
                    }
                    model.MsgRemark = dt.Rows[n]["MsgRemark"].ToString();
                    if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    }
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
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
        public DataSet GetList(ZhiFang.Model.LabDownLoadInfo model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.LabDownLoadInfo model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.LabDownLoadInfo model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #endregion


        #region IBLabDownLoadInfo 成员


        public string GetMaxDTimeStampe()
        {
            return dal.GetMaxDTimeStampe();
        }

        #endregion

        #region IBBase<LabDownLoadInfo> 成员


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