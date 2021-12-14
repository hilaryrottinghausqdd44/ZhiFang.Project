using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	//SuperGroup		
	public partial class SuperGroup : IBSynchData, IBSuperGroup, IBBatchCopy, IBDataDownload
	{
		IDAL.IDSuperGroup dal;
		IDAL.IDBatchCopy dalCopy;

		public SuperGroup()
		{
			if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
			{
				dal = DALFactory.DalFactory<IDAL.IDSuperGroup>.GetDal("SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
				dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
			}
			else
			{
				dal = DALFactory.DalFactory<IDAL.IDSuperGroup>.GetDal("B_SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
				dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
			}
		}

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SuperGroupNo)
		{
			return dal.Exists(SuperGroupNo);
		}
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.SuperGroup model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.SuperGroup model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SuperGroupNo)
		{
			return dal.Delete(SuperGroupNo);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.SuperGroup GetModel(int SuperGroupNo)
		{
			return dal.GetModel(SuperGroupNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.SuperGroup GetModelByCache(int SuperGroupNo)
		{

			string CacheKey = "B_SuperGroupModel-" + SuperGroupNo;
			object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SuperGroupNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch { }
			}
			return (ZhiFang.Model.SuperGroup)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.SuperGroup> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.SuperGroup> modelList = new List<ZhiFang.Model.SuperGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.SuperGroup model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.SuperGroup();
                    //if (dt.Columns.Contains("SuperGroupID") && dt.Rows[n]["SuperGroupID"].ToString() != "")
					//{
					//    model.SuperGroupID = int.Parse(dt.Rows[n]["SuperGroupID"].ToString());
					//}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("StandCode") && dt.Rows[n]["StandCode"].ToString() != "")
                    {
                        model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    }
                    if (dt.Columns.Contains("ZFStandCode") && dt.Rows[n]["ZFStandCode"].ToString() != "")
                    {
                       model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    if (dt.Columns.Contains("SuperGroupNo") && dt.Rows[n]["SuperGroupNo"].ToString() != "")
					{
						model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
					}
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    } if (dt.Columns.Contains("ShortName") && dt.Rows[n]["ShortName"].ToString() != "")
                    {
                        model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("Visible") && dt.Rows[n]["Visible"].ToString() != "")
					{
						model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
					}
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
					{
						model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
                    if (dt.Columns.Contains("ParentNo") && dt.Rows[n]["ParentNo"].ToString() != "")
					{
						model.ParentNo = int.Parse(dt.Rows[n]["ParentNo"].ToString());
					}
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}


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
		public DataSet GetList(ZhiFang.Model.SuperGroup model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.SuperGroup model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.SuperGroup model, int nowPageNum, int nowPageSize)
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


		#region IBSuperGroup 成员


		public int DeleteList(string SuperGroupIDlist)
		{
			throw new NotImplementedException();
		}
		public DataSet GetList(int ListColCount, int PageIndex, Model.SuperGroup superGroup)
		{
			return GetListByPage(superGroup, ListColCount, PageIndex);
		}

		public DataSet GetParentSuperGroupNolist()
		{
			return dal.GetParentSuperGroupNolist();
		}

		#endregion

		#region IBDataDownload 成员

		public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
		{
			IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());
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
				ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.SuperGroup.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
				strXML = "";
				strXMLSchema = "";
				strMsg = "失败，SuperGroup获取最新字典数据出现异常";
				return 0;
			}
		}

		public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IBBase<SuperGroup> 成员


		public int AddUpdateByDataSet(DataSet ds)
		{
			return dal.AddUpdateByDataSet(ds);
		}

		#endregion

		#region IBSynchData 成员


		public bool Exists(System.Collections.Hashtable ht)
		{
			return dal.Equals(ht);
		}

		public int AddByDataRow(DataRow dr)
		{
			return dal.AddByDataRow(dr);
		}

		public int UpdateByDataRow(DataRow dr)
		{
			return dal.UpdateByDataRow(dr);
		}

		public int DeleteByDataRow(DataRow dr)
		{
			return dalCopy.DeleteByDataRow(dr);
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
