using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	//Lab_FolkType		
	public partial class Lab_FolkType : IBSynchData, IBLab_FolkType
	{
		IDAL.IDLab_FolkType dal = DALFactory.DalFactory<IDAL.IDLab_FolkType>.GetDal("B_Lab_FolkType", ZhiFang.Common.Dictionary.DBSource.LisDB());

		public Lab_FolkType()
		{ }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string LabCode, int LabFolkNo)
		{
			return dal.Exists(LabCode, LabFolkNo);
		}
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_FolkType model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_FolkType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode, int LabFolkNo)
		{
			return dal.Delete(LabCode, LabFolkNo);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string FolkIDlist)
		{
			return dal.DeleteList(FolkIDlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_FolkType GetModel(string LabCode, int LabFolkNo)
		{
			return dal.GetModel(LabCode, LabFolkNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.Lab_FolkType GetModelByCache(string LabCode, int LabFolkNo)
		{

			string CacheKey = "B_Lab_FolkTypeModel-" + LabCode + LabFolkNo;
			object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(LabCode, LabFolkNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch { }
			}
			return (ZhiFang.Model.Lab_FolkType)objModel;
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_FolkType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.Lab_FolkType> DataTableToList(DataTable dt)
		{
			List<ZhiFang.Model.Lab_FolkType> modelList = new List<ZhiFang.Model.Lab_FolkType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.Lab_FolkType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.Lab_FolkType();
					if (dt.Rows[n]["FolkID"].ToString() != "")
					{
						model.FolkID = int.Parse(dt.Rows[n]["FolkID"].ToString());
					}
					model.LabCode = dt.Rows[n]["LabCode"].ToString();
					if (dt.Rows[n]["LabFolkNo"].ToString() != "")
					{
						model.LabFolkNo = int.Parse(dt.Rows[n]["LabFolkNo"].ToString());
                    } if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
					model.CName = dt.Rows[n]["CName"].ToString();
					model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
					if (dt.Rows[n]["Visible"].ToString() != "")
					{
						model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
					}
					if (dt.Rows[n]["DispOrder"].ToString() != "")
					{
						model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
					model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
					if (dt.Rows[n]["AddTime"].ToString() != "")
					{
						model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					model.StandCode = dt.Rows[n]["StandCode"].ToString();
					model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
					if (dt.Rows[n]["UseFlag"].ToString() != "")
					{
						model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
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
		public DataSet GetList(ZhiFang.Model.Lab_FolkType model)
		{
			return dal.GetList(model);
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_FolkType model)
		{
			return dal.GetListByLike(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.Lab_FolkType model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.Lab_FolkType model, int nowPageNum, int nowPageSize)
		{
			if (nowPageNum >= 0 && nowPageSize > 0)
			{
				return dal.GetListByPage(model, nowPageNum, nowPageSize);
			}
			else
				return null;
		}

		#endregion


		#region IBBase<Lab_FolkType> 成员


		public int AddUpdateByDataSet(DataSet ds)
		{
			return dal.AddUpdateByDataSet(ds);
		}
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
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