using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//Lab_CLIENTELE		
	public partial class Lab_CLIENTELE: IBLab_CLIENTELE,IBSynchData
	{
		IDAL.IDLab_CLIENTELE dal=DALFactory.DalFactory<IDAL.IDLab_CLIENTELE>.GetDal("B_Lab_CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public Lab_CLIENTELE()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode,int LabClIENTNO)
        {
        	return dal.Exists(LabCode,LabClIENTNO);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.Lab_CLIENTELE model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_CLIENTELE model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabClIENTNO)
		{			
			return dal.Delete(LabCode,LabClIENTNO);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string ClIENTIDlist )
		{
			return dal.DeleteList(ClIENTIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_CLIENTELE GetModel(string LabCode,int LabClIENTNO)
		{			
			return dal.GetModel(LabCode,LabClIENTNO);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_CLIENTELE GetModelByCache(string LabCode,int LabClIENTNO)
		{
			
			string CacheKey = "B_Lab_CLIENTELEModel-" + LabCode+LabClIENTNO;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode,LabClIENTNO);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.Lab_CLIENTELE)objModel;
		}

		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_CLIENTELE> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_CLIENTELE> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_CLIENTELE> modelList = new List<ZhiFang.Model.Lab_CLIENTELE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_CLIENTELE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_CLIENTELE();					
									if(dt.Rows[n]["ClIENTID"].ToString()!="")
				{
					model.ClIENTID=int.Parse(dt.Rows[n]["ClIENTID"].ToString());
				}
                                    if (dt.Rows[n]["ControlStatus"].ToString() != "")
                                    {
                                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                                    }
																												model.LabCode= dt.Rows[n]["LabCode"].ToString();
																								if(dt.Rows[n]["LabClIENTNO"].ToString()!="")
				{
					model.LabClIENTNO=int.Parse(dt.Rows[n]["LabClIENTNO"].ToString());
				}
																												model.CNAME= dt.Rows[n]["CNAME"].ToString();
																												model.ENAME= dt.Rows[n]["ENAME"].ToString();
																												model.SHORTCODE= dt.Rows[n]["SHORTCODE"].ToString();
																								if(dt.Rows[n]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(dt.Rows[n]["ISUSE"].ToString());
				}
																												model.LINKMAN= dt.Rows[n]["LINKMAN"].ToString();
																												model.PHONENUM1= dt.Rows[n]["PHONENUM1"].ToString();
																												model.ADDRESS= dt.Rows[n]["ADDRESS"].ToString();
																												model.MAILNO= dt.Rows[n]["MAILNO"].ToString();
																												model.EMAIL= dt.Rows[n]["EMAIL"].ToString();
																												model.PRINCIPAL= dt.Rows[n]["PRINCIPAL"].ToString();
																												model.PHONENUM2= dt.Rows[n]["PHONENUM2"].ToString();
																								if(dt.Rows[n]["CLIENTTYPE"].ToString()!="")
				{
					model.CLIENTTYPE=int.Parse(dt.Rows[n]["CLIENTTYPE"].ToString());
				}
																								if(dt.Rows[n]["BmanNo"].ToString()!="")
				{
					model.BmanNo=int.Parse(dt.Rows[n]["BmanNo"].ToString());
				}
																												model.Romark= dt.Rows[n]["Romark"].ToString();
																								if(dt.Rows[n]["TitleType"].ToString()!="")
				{
					model.TitleType=int.Parse(dt.Rows[n]["TitleType"].ToString());
				}
																								if(dt.Rows[n]["UploadType"].ToString()!="")
				{
					model.UploadType=int.Parse(dt.Rows[n]["UploadType"].ToString());
				}
																								if(dt.Rows[n]["PrintType"].ToString()!="")
				{
					model.PrintType=int.Parse(dt.Rows[n]["PrintType"].ToString());
				}
																								if(dt.Rows[n]["InputDataType"].ToString()!="")
				{
					model.InputDataType=int.Parse(dt.Rows[n]["InputDataType"].ToString());
				}
																								if(dt.Rows[n]["ReportPageType"].ToString()!="")
				{
					model.ReportPageType=int.Parse(dt.Rows[n]["ReportPageType"].ToString());
				}
																												model.ClientArea= dt.Rows[n]["ClientArea"].ToString();
																												model.ClientStyle= dt.Rows[n]["ClientStyle"].ToString();
																												model.FaxNo= dt.Rows[n]["FaxNo"].ToString();
																								if(dt.Rows[n]["AutoFax"].ToString()!="")
				{
					model.AutoFax=int.Parse(dt.Rows[n]["AutoFax"].ToString());
				}
																												model.ClientReportTitle= dt.Rows[n]["ClientReportTitle"].ToString();
																								if(dt.Rows[n]["IsPrintItem"].ToString()!="")
				{
					model.IsPrintItem=int.Parse(dt.Rows[n]["IsPrintItem"].ToString());
				}
																												model.CZDY1= dt.Rows[n]["CZDY1"].ToString();
																												model.CZDY2= dt.Rows[n]["CZDY2"].ToString();
																												model.CZDY3= dt.Rows[n]["CZDY3"].ToString();
																												model.CZDY4= dt.Rows[n]["CZDY4"].ToString();
																												model.CZDY5= dt.Rows[n]["CZDY5"].ToString();
																												model.CZDY6= dt.Rows[n]["CZDY6"].ToString();
																												model.LinkManPosition= dt.Rows[n]["LinkManPosition"].ToString();
																												model.LinkMan1= dt.Rows[n]["LinkMan1"].ToString();
																												model.LinkManPosition1= dt.Rows[n]["LinkManPosition1"].ToString();
																												model.ClientCode= dt.Rows[n]["ClientCode"].ToString();
																												model.CWAccountDate= dt.Rows[n]["CWAccountDate"].ToString();
																												model.NFClientType= dt.Rows[n]["NFClientType"].ToString();
																												model.RelationName= dt.Rows[n]["RelationName"].ToString();
																												model.WebLisSourceOrgID= dt.Rows[n]["WebLisSourceOrgID"].ToString();
																												model.GroupName= dt.Rows[n]["GroupName"].ToString();
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
		public DataSet GetList(ZhiFang.Model.Lab_CLIENTELE model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_CLIENTELE model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_CLIENTELE model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_CLIENTELE model, int nowPageNum, int nowPageSize)
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