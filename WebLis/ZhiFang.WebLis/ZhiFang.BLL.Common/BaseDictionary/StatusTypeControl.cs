using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//StatusTypeControl		
	public partial class StatusTypeControl:IBSynchData, IBStatusTypeControl	{
		IDAL.IDStatusTypeControl dal=DALFactory.DalFactory<IDAL.IDStatusTypeControl>.GetDal("B_StatusTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public StatusTypeControl()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string StatusControlNo)
        {
        	return dal.Exists(StatusControlNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.StatusTypeControl model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.StatusTypeControl model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string StatusControlNo)
		{			
			return dal.Delete(StatusControlNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.StatusTypeControl GetModel(string StatusControlNo)
		{			
			return dal.GetModel(StatusControlNo);
		}
		

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.StatusTypeControl GetModelByCache(string StatusControlNo)
		{
			
			string CacheKey = "B_StatusTypeControlModel-" + StatusControlNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StatusControlNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.StatusTypeControl)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.StatusTypeControl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<ZhiFang.Model.StatusTypeControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.StatusTypeControl> modelList = new List<ZhiFang.Model.StatusTypeControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.StatusTypeControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.StatusTypeControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("StatusControlNo") && dt.Rows[n]["StatusControlNo"].ToString() != "")
                    {
                        model.StatusControlNo = dt.Rows[n]["StatusControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("StatusNo") && dt.Rows[n]["StatusNo"].ToString() != "")
                    {
                        model.StatusNo = int.Parse(dt.Rows[n]["StatusNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlStatusNo") && dt.Rows[n]["ControlStatusNo"].ToString() != "")
                    {
                        model.ControlStatusNo = int.Parse(dt.Rows[n]["ControlStatusNo"].ToString());
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
		public DataSet GetList(ZhiFang.Model.StatusTypeControl model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.StatusTypeControl model)
		{
			return dal.GetTotalCount(model);
		}
		#endregion


        #region IBBase<StatusTypeControl> 成员


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