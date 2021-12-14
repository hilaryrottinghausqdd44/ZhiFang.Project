using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//TestType		
	public partial class TestType:IBSynchData, IBTestType,IBBatchCopy,IBDataDownload
	{
        IDAL.IDTestType dal;
        IDAL.IDBatchCopy dalCopy;
        
		public TestType()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDTestType>.GetDal("TestType", ZhiFang.Common.Dictionary.DBSource.LisDB());
                //dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("TestType", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDTestType>.GetDal("S_Dic_TestType", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("S_Dic_TestType", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TestTypeNo)
        {
        	return dal.Exists(TestTypeNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.TestType model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.TestType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int TestTypeNo)
		{			
			return dal.Delete(TestTypeNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string TestTypeIDlist )
		{
			return dal.DeleteList(TestTypeIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.TestType GetModel(int TestTypeNo)
		{			
			return dal.GetModel(TestTypeNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.TestType GetModelByCache(int TestTypeNo)
		{
			
			string CacheKey = "S_Dic_TestTypeModel-" + TestTypeNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TestTypeNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.TestType)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.TestType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.TestType> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.TestType> modelList = new List<ZhiFang.Model.TestType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.TestType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.TestType();					
									if(dt.Rows[n]["TestTypeID"].ToString()!="")
				{
					model.TestTypeID=int.Parse(dt.Rows[n]["TestTypeID"].ToString());
				}
																								if(dt.Rows[n]["TestTypeNo"].ToString()!="")
				{
					model.TestTypeNo=int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
				}
																												model.CName= dt.Rows[n]["CName"].ToString();
																												model.TestTypeDesc= dt.Rows[n]["TestTypeDesc"].ToString();
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
		public DataSet GetList(ZhiFang.Model.TestType model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.TestType model)
		{
			return dal.GetTotalCount(model);
		}
		
        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }
		#endregion


        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("S_Dic_TestType", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.TestType.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，TestType获取最新字典数据出现异常" ;
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<TestType> 成员


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