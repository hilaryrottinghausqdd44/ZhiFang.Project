using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//LogInfo
    public partial class LogInfo :IBSynchData, IBLogInfo
    {
        IDAL.IDLogInfo dal = DALFactory.DalFactory<IDAL.IDLogInfo>.GetDal("D_LogInfo", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public LogInfo()
        { }

        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.LogInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.LogInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.LogInfo GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.LogInfo GetModelByCache(int Id)
        {

            string CacheKey = "D_LogInfoModel-" + Id;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.LogInfo)objModel;
        }

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.LogInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.LogInfo> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.LogInfo> modelList = new List<ZhiFang.Model.LogInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.LogInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.LogInfo();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.TableName = dt.Rows[n]["TableName"].ToString();
                    if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    }
                    model.UserID = dt.Rows[n]["UserID"].ToString();
                    model.UserName = dt.Rows[n]["UserName"].ToString();
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
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
        public DataSet GetList(ZhiFang.Model.LogInfo model)
        {
            return dal.GetList(model);
        }
        #endregion


        #region IBBase<LogInfo> 成员


        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(LogInfo model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBLogInfo 成员


        public DataSet GetListByTimeStampe(Model.LogInfo model)
        {
            return dal.GetListByTimeStampe(model);
        }

        #endregion

        #region IBBase<LogInfo> 成员


        public int GetTotalCount(Model.LogInfo model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<LogInfo> 成员


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