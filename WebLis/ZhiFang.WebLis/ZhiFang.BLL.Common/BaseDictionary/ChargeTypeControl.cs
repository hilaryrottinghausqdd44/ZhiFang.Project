using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//ChargeTypeControl		
	public partial class ChargeTypeControl:IBSynchData, IBChargeTypeControl	{
		IDAL.IDChargeTypeControl dal=DALFactory.DalFactory<IDAL.IDChargeTypeControl>.GetDal("B_ChargeTypeControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public ChargeTypeControl()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ChargeControlNo)
        {
        	return dal.Exists(ChargeControlNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.ChargeTypeControl model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.ChargeTypeControl model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string ChargeControlNo)
		{			
			return dal.Delete(ChargeControlNo);
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
		public ZhiFang.Model.ChargeTypeControl GetModel(string ChargeControlNo)
		{			
			return dal.GetModel(ChargeControlNo);
		}
		

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.ChargeTypeControl GetModelByCache(string ChargeControlNo)
		{
			
			string CacheKey = "B_ChargeTypeControlModel-" + ChargeControlNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ChargeControlNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.ChargeTypeControl)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.ChargeTypeControl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.ChargeTypeControl> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.ChargeTypeControl> modelList = new List<ZhiFang.Model.ChargeTypeControl>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.ChargeTypeControl model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.ChargeTypeControl();					
									if(dt.Rows[n]["Id"].ToString()!="")
				{
					model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
				}
																												model.ChargeControlNo= dt.Rows[n]["ChargeControlNo"].ToString();
																								if(dt.Rows[n]["ChargeNo"].ToString()!="")
				{
					model.ChargeNo=int.Parse(dt.Rows[n]["ChargeNo"].ToString());
				}
																												model.ControlLabNo= dt.Rows[n]["ControlLabNo"].ToString();
																								if(dt.Rows[n]["ControlChargeNo"].ToString()!="")
				{
					model.ControlChargeNo=int.Parse(dt.Rows[n]["ControlChargeNo"].ToString());
				}
																								if(dt.Rows[n]["DTimeStampe"].ToString()!="")
				{
					model.DTimeStampe=DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
				}
																								if(dt.Rows[n]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
				}
																								if(dt.Rows[n]["UseFlag"].ToString()!="")
				{
					model.UseFlag=int.Parse(dt.Rows[n]["UseFlag"].ToString());
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
		public DataSet GetList(ZhiFang.Model.ChargeTypeControl model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.ChargeTypeControl model)
		{
			return dal.GetTotalCount(model);
		}
		#endregion


        #region IBBase<ChargeTypeControl> 成员


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