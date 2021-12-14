using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//DoctorControl		
	public partial class DoctorControl:IBSynchData, IBDoctorControl	{
		IDAL.IDDoctorControl dal=DALFactory.DalFactory<IDAL.IDDoctorControl>.GetDal("B_DoctorControl", ZhiFang.Common.Dictionary.DBSource.LisDB());
		
		public DoctorControl()
		{}
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string DoctorControlNo)
        {
        	return dal.Exists(DoctorControlNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.DoctorControl model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.DoctorControl model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string DoctorControlNo)
		{			
			return dal.Delete(DoctorControlNo);
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
		public ZhiFang.Model.DoctorControl GetModel(string DoctorControlNo)
		{			
			return dal.GetModel(DoctorControlNo);
		}
		

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZhiFang.Model.DoctorControl GetModelByCache(string DoctorControlNo)
		{
			
			string CacheKey = "B_DoctorControlModel-" + DoctorControlNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DoctorControlNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.DoctorControl)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZhiFang.Model.DoctorControl> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(null);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<ZhiFang.Model.DoctorControl> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.DoctorControl> modelList = new List<ZhiFang.Model.DoctorControl>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.DoctorControl model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.DoctorControl();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("DoctorControlNo") && dt.Rows[n]["DoctorControlNo"].ToString() != "")
                    {
                        model.DoctorControlNo = dt.Rows[n]["DoctorControlNo"].ToString();
                    }
                    if (dt.Columns.Contains("DoctorNo") && dt.Rows[n]["DoctorNo"].ToString() != "")
                    {
                        model.DoctorNo = int.Parse(dt.Rows[n]["DoctorNo"].ToString());
                    }
                    if (dt.Columns.Contains("ControlLabNo") && dt.Rows[n]["ControlLabNo"].ToString() != "")
                    {
                        model.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    }
                    if (dt.Columns.Contains("ControlDoctorNo") && dt.Rows[n]["ControlDoctorNo"].ToString() != "")
                    {
                        model.ControlDoctorNo = int.Parse(dt.Rows[n]["ControlDoctorNo"].ToString());
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
                    if (dt.Columns.Contains("LabDoctorNo") && dt.Rows[n]["LabDoctorNo"].ToString() != "")
                    {
                        model.LabDoctorNo = dt.Rows[n]["LabDoctorNo"].ToString();
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
        public List<ZhiFang.Model.Doctor> ControlDataTableToList(DataTable dt, int ControlLabNo)
        {

            List<ZhiFang.Model.Doctor> modelList = new List<ZhiFang.Model.Doctor>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Doctor model;
                ZhiFang.Model.DoctorControl a;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Doctor();
                    a = new Model.DoctorControl();
                    if (dt.Rows[n]["DoctorNo"].ToString() != null && dt.Rows[n]["DoctorNo"].ToString() != "")
                    {
                        model.DoctorNo = Convert.ToInt32(dt.Rows[n]["DoctorNo"].ToString());
                    }

                    model.CName = dt.Rows[n]["CName"].ToString();
                    a.ControlLabNo = dt.Rows[n]["ControlLabNo"].ToString();
                    if (dt.Rows[n]["ControlDoctorNo"].ToString() != "" && dt.Rows[n]["ControlDoctorNo"].ToString() != null)
                    {
                        a.ControlDoctorNo = Convert.ToInt32(dt.Rows[n]["ControlDoctorNo"].ToString());
                    }

                    model.DoctorControl = a;

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
		public DataSet GetList(ZhiFang.Model.DoctorControl model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.DoctorControl model)
		{
			return dal.GetTotalCount(model);
		}
		#endregion


        #region IBBase<DoctorControl> 成员


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
        public DataSet GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }

        public DataSet B_lab_GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize)
        {
            return dal.B_lab_GetListByPage(model, nowPageNum, nowPageSize);
        }
        #endregion

      
    }
}