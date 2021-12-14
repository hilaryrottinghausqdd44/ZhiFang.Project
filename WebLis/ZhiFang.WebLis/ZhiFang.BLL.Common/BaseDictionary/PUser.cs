using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//PUser		
	public partial class PUser:IBSynchData, IBPUser,IBBatchCopy,IBDataDownload
	{
        IDAL.IDPUser dal;
        IDAL.IDBatchCopy dalCopy;
        
		public PUser()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDPUser>.GetDal("PUser", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("PUser", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDPUser>.GetDal("U_Dic_PUser", ZhiFang.Common.Dictionary.DBSource.LisDB()); 
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("U_Dic_PUser", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserNo)
        {
        	return dal.Exists(UserNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.PUser model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.PUser model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int UserNo)
		{			
			return dal.Delete(UserNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string UserIDlist )
		{
			return dal.DeleteList(UserIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.PUser GetModel(int UserNo)
		{			
			return dal.GetModel(UserNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.PUser GetModelByCache(int UserNo)
		{
			
			string CacheKey = "B_PUserModel-" + UserNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.PUser)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.PUser> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.PUser> modelList = new List<ZhiFang.Model.PUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.PUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.PUser();					
									if(dt.Rows[n]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
				}
																								if(dt.Rows[n]["UserNo"].ToString()!="")
				{
					model.UserNo=int.Parse(dt.Rows[n]["UserNo"].ToString());
				}
																												model.CName= dt.Rows[n]["CName"].ToString();
																												model.Password= dt.Rows[n]["Password"].ToString();
																												model.ShortCode= dt.Rows[n]["ShortCode"].ToString();
																								if(dt.Rows[n]["Gender"].ToString()!="")
				{
					model.Gender=int.Parse(dt.Rows[n]["Gender"].ToString());
				}
																								if(dt.Rows[n]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
				}
																												model.Role= dt.Rows[n]["Role"].ToString();
																												model.Resume= dt.Rows[n]["Resume"].ToString();
																								if(dt.Rows[n]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
				}
																								if(dt.Rows[n]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
				}
																												model.HisOrderCode= dt.Rows[n]["HisOrderCode"].ToString();
																																if(dt.Rows[n]["userimage"].ToString()!="")
				{
					model.userimage= (byte[])dt.Rows[n]["userimage"];
				}
																								model.usertype= dt.Rows[n]["usertype"].ToString();
																								if(dt.Rows[n]["DeptNo"].ToString()!="")
				{
					model.DeptNo=int.Parse(dt.Rows[n]["DeptNo"].ToString());
				}
																								if(dt.Rows[n]["SectorTypeNo"].ToString()!="")
				{
					model.SectorTypeNo=int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
				}
																												model.UserImeName= dt.Rows[n]["UserImeName"].ToString();
																								if(dt.Rows[n]["IsManager"].ToString()!="")
				{
					model.IsManager=int.Parse(dt.Rows[n]["IsManager"].ToString());
				}
																												model.PassWordS= dt.Rows[n]["PassWordS"].ToString();
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
			return GetList(new ZhiFang.Model.PUser());
		}
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.PUser model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.PUser model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.PUser model, int nowPageNum, int nowPageSize)
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
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("U_Dic_PUser", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.PUser.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，PUser获取最新字典数据出现异常" ;
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<PUser> 成员


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
