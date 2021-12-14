using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Equipment		
	public partial class Equipment:IBSynchData, IBEquipment,IBBatchCopy,IBDataDownload
	{
        IDAL.IDEquipment dal;
        IDAL.IDBatchCopy dalCopy;
        
		public Equipment()
		{
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDEquipment>.GetDal("Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDEquipment>.GetDal("B_Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int EquipNo)
        {
        	return dal.Exists(EquipNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Equipment model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Equipment model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int EquipNo)
		{			
			return dal.Delete(EquipNo);
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
		public ZhiFang.Model.Equipment GetModel(int EquipNo)
		{			
			return dal.GetModel(EquipNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Equipment GetModelByCache(int EquipNo)
		{
			
			string CacheKey = "B_EquipmentModel-" + EquipNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(EquipNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Equipment)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Equipment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Equipment> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Equipment> modelList = new List<ZhiFang.Model.Equipment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Equipment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Equipment();					
									if(dt.Rows[n]["EquipID"].ToString()!="")
				{
					model.EquipID=int.Parse(dt.Rows[n]["EquipID"].ToString());
				}
																								if(dt.Rows[n]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(dt.Rows[n]["EquipNo"].ToString());
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
		public DataSet GetList(ZhiFang.Model.Equipment model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Equipment model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Equipment model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        
        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }
		#endregion


        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_Equipment", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml结构文件
                strXML = dsAll.GetXml();//数据内容xml文件
                strMsg = "通过服务获取XML成功";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.Equipment.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，Equipment获取最新字典数据出现异常" ;
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<Equipment> 成员


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

        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}