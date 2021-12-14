using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Lab_BUSINESSMAN		
	public partial class Lab_BUSINESSMAN: IBLab_BUSINESSMAN,IBSynchData
	{
		IDAL.IDLab_BUSINESSMAN dal=DALFactory.DalFactory<IDAL.IDLab_BUSINESSMAN>.GetDal("B_Lab_BUSINESSMAN", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public Lab_BUSINESSMAN()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode,int LabBMANNO)
        {
        	return dal.Exists(LabCode,LabBMANNO);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabBMANNO)
		{			
			return dal.Delete(LabCode,LabBMANNO);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string BNANIDlist )
		{
			return dal.DeleteList(BNANIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_BUSINESSMAN GetModel(string LabCode,int LabBMANNO)
		{			
			return dal.GetModel(LabCode,LabBMANNO);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_BUSINESSMAN GetModelByCache(string LabCode,int LabBMANNO)
		{
			
			string CacheKey = "B_Lab_BUSINESSMANModel-" + LabCode+LabBMANNO;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode,LabBMANNO);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Lab_BUSINESSMAN)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_BUSINESSMAN> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(new ZhiFang.Model.Lab_BUSINESSMAN());
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_BUSINESSMAN> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_BUSINESSMAN> modelList = new List<ZhiFang.Model.Lab_BUSINESSMAN>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_BUSINESSMAN model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_BUSINESSMAN();					
									if(dt.Rows[n]["BNANID"].ToString()!="")
				{
					model.BNANID=int.Parse(dt.Rows[n]["BNANID"].ToString());
				}
																												model.CNAME= dt.Rows[n]["CNAME"].ToString();
																												model.LabCode= dt.Rows[n]["LabCode"].ToString();
																								if(dt.Rows[n]["LabBMANNO"].ToString()!="")
				{
					model.LabBMANNO=int.Parse(dt.Rows[n]["LabBMANNO"].ToString());
				}
																												model.SHORTCODE= dt.Rows[n]["SHORTCODE"].ToString();
																								if(dt.Rows[n]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(dt.Rows[n]["ISUSE"].ToString());
				}
																												model.IDCODE= dt.Rows[n]["IDCODE"].ToString();
																												model.ADDRESS= dt.Rows[n]["ADDRESS"].ToString();
																												model.PHONENUM= dt.Rows[n]["PHONENUM"].ToString();
																												model.ROMARK= dt.Rows[n]["ROMARK"].ToString();
																								if(dt.Rows[n]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
				}
																								if(dt.Rows[n]["DTimeStampe"].ToString()!="")
				{
					model.DTimeStampe=DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
				}
																								if(dt.Rows[n]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
				}
																												model.StandCode= dt.Rows[n]["StandCode"].ToString();
																												model.ZFStandCode= dt.Rows[n]["ZFStandCode"].ToString();
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
		public DataSet GetList(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_BUSINESSMAN model, int nowPageNum, int nowPageSize)
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