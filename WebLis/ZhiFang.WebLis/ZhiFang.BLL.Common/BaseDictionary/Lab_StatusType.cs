using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Lab_StatusType		
	public partial class Lab_StatusType:IBSynchData, IBLab_StatusType	{
		IDAL.IDLab_StatusType dal=DALFactory.DalFactory<IDAL.IDLab_StatusType>.GetDal("B_Lab_StatusType", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public Lab_StatusType()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode,int LabStatusNo)
        {
        	return dal.Exists(LabCode,LabStatusNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Lab_StatusType model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_StatusType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabStatusNo)
		{			
			return dal.Delete(LabCode,LabStatusNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string StatusIDlist )
		{
			return dal.DeleteList(StatusIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_StatusType GetModel(string LabCode,int LabStatusNo)
		{			
			return dal.GetModel(LabCode,LabStatusNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_StatusType GetModelByCache(string LabCode,int LabStatusNo)
		{
			
			string CacheKey = "B_Lab_StatusTypeModel-" + LabCode+LabStatusNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode,LabStatusNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Lab_StatusType)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_StatusType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_StatusType> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_StatusType> modelList = new List<ZhiFang.Model.Lab_StatusType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_StatusType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_StatusType();					
									if(dt.Rows[n]["StatusID"].ToString()!="")
				{
					model.StatusID=int.Parse(dt.Rows[n]["StatusID"].ToString());
				}
																												model.LabCode= dt.Rows[n]["LabCode"].ToString();
																								if(dt.Rows[n]["LabStatusNo"].ToString()!="")
				{
					model.LabStatusNo=int.Parse(dt.Rows[n]["LabStatusNo"].ToString());
				}
																												model.CName= dt.Rows[n]["CName"].ToString();
																												model.StatusDesc= dt.Rows[n]["StatusDesc"].ToString();
																												model.StatusColor= dt.Rows[n]["StatusColor"].ToString();
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
		public DataSet GetList(ZhiFang.Model.Lab_StatusType model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_StatusType model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_StatusType model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_StatusType model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        
		#endregion


        #region IBBase<Lab_StatusType> 成员


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