using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//BUSINESSMANControl		
	public partial class BUSINESSMANControl: IBBUSINESSMANControl,IBSynchData	{
		IDAL.IDBUSINESSMANControl dal=DALFactory.DalFactory<IDAL.IDBUSINESSMANControl>.GetDal("B_BUSINESSMANControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public BUSINESSMANControl()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BMANControlNo)
        {
        	return dal.Exists(BMANControlNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.BUSINESSMANControl model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.BUSINESSMANControl model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string BMANControlNo)
		{			
			return dal.Delete(BMANControlNo);
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
		public ZhiFang.Model.BUSINESSMANControl GetModel(string BMANControlNo)
		{			
			return dal.GetModel(BMANControlNo);
		}
		

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.BUSINESSMANControl GetModelByCache(string BMANControlNo)
		{
			
			string CacheKey = "B_BUSINESSMANControlModel-" + BMANControlNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BMANControlNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.BUSINESSMANControl)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.BUSINESSMANControl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(new ZhiFang.Model.BUSINESSMANControl());
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.BUSINESSMANControl> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.BUSINESSMANControl> modelList = new List<ZhiFang.Model.BUSINESSMANControl>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.BUSINESSMANControl model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.BUSINESSMANControl();					
									if(dt.Rows[n]["Id"].ToString()!="")
				{
					model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
				}
																												model.BMANControlNo= dt.Rows[n]["BMANControlNo"].ToString();
																								if(dt.Rows[n]["BMANNO"].ToString()!="")
				{
					model.BMANNO=int.Parse(dt.Rows[n]["BMANNO"].ToString());
				}
																												model.ControlLabNo= dt.Rows[n]["ControlLabNo"].ToString();
																								if(dt.Rows[n]["ControlBMANNO"].ToString()!="")
				{
					model.ControlBMANNO=int.Parse(dt.Rows[n]["ControlBMANNO"].ToString());
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
		public DataSet GetList(ZhiFang.Model.BUSINESSMANControl model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.BUSINESSMANControl model)
		{
			return dal.GetTotalCount(model);
		}
        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }
		#endregion


        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion
    }
}