using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Lab_Equipment		
	public partial class Lab_Equipment:IBSynchData, IBLab_Equipment	{
		IDAL.IDLab_Equipment dal=DALFactory.DalFactory<IDAL.IDLab_Equipment>.GetDal("B_Lab_Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public Lab_Equipment()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode,int LabEquipNo)
        {
        	return dal.Exists(LabCode,LabEquipNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Lab_Equipment model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_Equipment model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabEquipNo)
		{			
			return dal.Delete(LabCode,LabEquipNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string EquipIDlist )
		{
			return dal.DeleteList(EquipIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_Equipment GetModel(string LabCode,int LabEquipNo)
		{			
			return dal.GetModel(LabCode,LabEquipNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_Equipment GetModelByCache(string LabCode,int LabEquipNo)
		{
			
			string CacheKey = "B_Lab_EquipmentModel-" + LabCode+LabEquipNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode,LabEquipNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Lab_Equipment)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_Equipment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_Equipment> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_Equipment> modelList = new List<ZhiFang.Model.Lab_Equipment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_Equipment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_Equipment();					
									if(dt.Rows[n]["EquipID"].ToString()!="")
				{
					model.EquipID=int.Parse(dt.Rows[n]["EquipID"].ToString());
				}
																												model.LabCode= dt.Rows[n]["LabCode"].ToString();
																								if(dt.Rows[n]["LabEquipNo"].ToString()!="")
				{
					model.LabEquipNo=int.Parse(dt.Rows[n]["LabEquipNo"].ToString());
				}
																												model.CName= dt.Rows[n]["CName"].ToString();
																												model.ShortName= dt.Rows[n]["ShortName"].ToString();
																												model.ShortCode= dt.Rows[n]["ShortCode"].ToString();
																								if(dt.Rows[n]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(dt.Rows[n]["SectionNo"].ToString());
				}
																												model.Computer= dt.Rows[n]["Computer"].ToString();
																												model.ComProgram= dt.Rows[n]["ComProgram"].ToString();
																												model.ComPort= dt.Rows[n]["ComPort"].ToString();
																												model.BaudRate= dt.Rows[n]["BaudRate"].ToString();
																												model.Parity= dt.Rows[n]["Parity"].ToString();
																												model.DataBits= dt.Rows[n]["DataBits"].ToString();
																												model.StopBits= dt.Rows[n]["StopBits"].ToString();
																								if(dt.Rows[n]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
				}
																								if(dt.Rows[n]["DoubleDir"].ToString()!="")
				{
					model.DoubleDir=int.Parse(dt.Rows[n]["DoubleDir"].ToString());
				}
																												model.LicenceKey= dt.Rows[n]["LicenceKey"].ToString();
																												model.LicenceType= dt.Rows[n]["LicenceType"].ToString();
																								if(dt.Rows[n]["LicenceDate"].ToString()!="")
				{
					model.LicenceDate=DateTime.Parse(dt.Rows[n]["LicenceDate"].ToString());
				}
																												model.SQH= dt.Rows[n]["SQH"].ToString();
																								if(dt.Rows[n]["SNo"].ToString()!="")
				{
					model.SNo=int.Parse(dt.Rows[n]["SNo"].ToString());
				}
																								if(dt.Rows[n]["SickType"].ToString()!="")
				{
					model.SickType=int.Parse(dt.Rows[n]["SickType"].ToString());
				}
																								if(dt.Rows[n]["UseImmPlate"].ToString()!="")
				{
					model.UseImmPlate=int.Parse(dt.Rows[n]["UseImmPlate"].ToString());
				}
																								if(dt.Rows[n]["ImmCalc"].ToString()!="")
				{
					model.ImmCalc=int.Parse(dt.Rows[n]["ImmCalc"].ToString());
				}
																												model.CommPara= dt.Rows[n]["CommPara"].ToString();
																												model.ReagentPara= dt.Rows[n]["ReagentPara"].ToString();
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
		public DataSet GetList(ZhiFang.Model.Lab_Equipment model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_Equipment model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_Equipment model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_Equipment model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        
		#endregion


        #region IBBase<Lab_Equipment> 成员


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