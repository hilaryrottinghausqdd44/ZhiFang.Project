using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//DistrictControl		
	public partial class DistrictControl:IBSynchData, IBDistrictControl	{
		IDAL.IDDistrictControl dal=DALFactory.DalFactory<IDAL.IDDistrictControl>.GetDal("B_DistrictControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public DistrictControl()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string DistrictControlNo)
        {
        	return dal.Exists(DistrictControlNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.DistrictControl model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.DistrictControl model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string DistrictControlNo)
		{			
			return dal.Delete(DistrictControlNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.DistrictControl GetModel(string DistrictControlNo)
		{			
			return dal.GetModel(DistrictControlNo);
		}
		

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.DistrictControl GetModelByCache(string DistrictControlNo)
		{
			
			string CacheKey = "B_DistrictControlModel-" + DistrictControlNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DistrictControlNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.DistrictControl)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.DistrictControl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<ZhiFang.Model.DistrictControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.DistrictControl> modelList = new List<ZhiFang.Model.DistrictControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.DistrictControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.DistrictControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("DistrictControlNo") && dt.Rows[n]["DistrictControlNo"].ToString() != "")
                    {
                        model.DistrictControlNo = dt.Rows[n]["DistrictControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("DistrictNo") && dt.Rows[n]["DistrictNo"].ToString() != "")
                    {
                        model.DistrictNo = int.Parse(dt.Rows[n]["DistrictNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlDistrictNo") && dt.Rows[n]["ControlDistrictNo"].ToString() != "")
                    {
                        model.ControlDistrictNo = int.Parse(dt.Rows[n]["ControlDistrictNo"].ToString());
                    }
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("LabDistrictNo") && dt.Rows[n]["LabDistrictNo"].ToString() != "")
                    {
                        model.LabDistrictNo = dt.Rows[n]["LabDistrictNo"].ToString();
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("CenterCName") && dt.Rows[n]["CenterCName"].ToString() != "")
                    {
                        model.CenterCName = dt.Rows[n]["CenterCName"].ToString();
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }


        ////////2014-1-21
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.District > ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.District> modelList = new List<ZhiFang.Model.District>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.District model;
                ZhiFang.Model.DistrictControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.District();
                    a = new Model.DistrictControl();
                    if (dt.Rows[n]["DistrictNo"].ToString() != null && dt.Rows[n]["DistrictNo"].ToString()!="")
                    {
                        model.DistrictNo =Convert.ToInt32 ( dt.Rows[n]["DistrictNo"].ToString());
                    }
                    
                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlDistrictNo"].ToString() != "" && dt.Rows[n]["ControlDistrictNo"].ToString()!=null)
                    {
                        a.ControlDistrictNo =Convert .ToInt32 ( dt.Rows[n]["ControlDistrictNo"].ToString());
                    }

                    model.DistrictControl = a;

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
		public DataSet GetList(ZhiFang.Model.DistrictControl model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.DistrictControl model)
		{
			return dal.GetTotalCount(model);
		}
		#endregion


        #region IBBase<DistrictControl> 成员


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
        #region 字典对照
        public DataSet GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion
    }
}