using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Lab_ChargeType		
	public partial class Lab_ChargeType:IBSynchData, IBLab_ChargeType	{
		IDAL.IDLab_ChargeType dal=DALFactory.DalFactory<IDAL.IDLab_ChargeType>.GetDal("B_Lab_ChargeType", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public Lab_ChargeType()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode,int LabChargeNo)
        {
        	return dal.Exists(LabCode,LabChargeNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Lab_ChargeType model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_ChargeType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabChargeNo)
		{			
			return dal.Delete(LabCode,LabChargeNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string ChargeIDlist )
		{
			return dal.DeleteList(ChargeIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_ChargeType GetModel(string LabCode,int LabChargeNo)
		{			
			return dal.GetModel(LabCode,LabChargeNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_ChargeType GetModelByCache(string LabCode,int LabChargeNo)
		{
			
			string CacheKey = "B_Lab_ChargeTypeModel-" + LabCode+LabChargeNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode,LabChargeNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Lab_ChargeType)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_ChargeType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_ChargeType> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_ChargeType> modelList = new List<ZhiFang.Model.Lab_ChargeType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_ChargeType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_ChargeType();					
									if(dt.Rows[n]["ChargeID"].ToString()!="")
				{
					model.ChargeID=int.Parse(dt.Rows[n]["ChargeID"].ToString());
				}
																												model.LabCode= dt.Rows[n]["LabCode"].ToString();
																								if(dt.Rows[n]["LabChargeNo"].ToString()!="")
				{
					model.LabChargeNo=int.Parse(dt.Rows[n]["LabChargeNo"].ToString());
				}
																												model.CName= dt.Rows[n]["CName"].ToString();
																												model.ChargeTypeDesc= dt.Rows[n]["ChargeTypeDesc"].ToString();
																								if(dt.Rows[n]["Discount"].ToString()!="")
				{
					model.Discount=decimal.Parse(dt.Rows[n]["Discount"].ToString());
				}
																								if(dt.Rows[n]["Append"].ToString()!="")
				{
					model.Append=decimal.Parse(dt.Rows[n]["Append"].ToString());
				}
																												model.ShortCode= dt.Rows[n]["ShortCode"].ToString();
																								if(dt.Rows[n]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
				}
																								if(dt.Rows[n]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
				}
																												model.HisOrderCode= dt.Rows[n]["HisOrderCode"].ToString();
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
		public DataSet GetList(ZhiFang.Model.Lab_ChargeType model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_ChargeType model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_ChargeType model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_ChargeType model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        
		#endregion


        #region IBBase<Lab_ChargeType> 成员


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