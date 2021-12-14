using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;

namespace ZhiFang.BLL.Report
{
	/// <summary>
	/// 业务逻辑类Station_PrinterList 的摘要说明。
	/// </summary>
    public class Station_PrinterList : ZhiFang.IBLL.Report.IBStation_PrinterList
	{
        private readonly IDStation_PrinterList dal = DalFactory<IDStation_PrinterList>.GetDalByClassName("Station_PrinterList");
		public Station_PrinterList()
		{}
		#region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.Station_PrinterList model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Model.Station_PrinterList model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int id)
		{
			
			return dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Station_PrinterList GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.Station_PrinterList GetModelByCache(int id)
		{
			
			string CacheKey = "Station_PrinterListModel-" + id;
			object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.Station_PrinterList)objModel;
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.Station_PrinterList> DataTableToList(DataTable dt)
		{
			List<Model.Station_PrinterList> modelList = new List<Model.Station_PrinterList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Station_PrinterList model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.Station_PrinterList();
					if(dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.StationName=dt.Rows[n]["StationName"].ToString();
					model.PrinterName=dt.Rows[n]["PrinterName"].ToString();
					model.PageSize=dt.Rows[n]["PageSize"].ToString();
					if(dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
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
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        public DataSet GetList(Model.Station_PrinterList model)
        {
            return dal.GetList(model);
        }

        public List<Model.Station_PrinterList> GetModelList(Model.Station_PrinterList model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }

        public int Delete(Model.Station_PrinterList model)
        {
            DataSet ds = dal.GetList(model);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    return dal.Delete(Int32.Parse(ds.Tables[0].Rows[0]["id"].ToString().Trim()));
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}

