using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
	/// <summary>
	/// 业务逻辑类PGroup 的摘要说明。
	/// </summary>
    public class BPGroup 
	{
        private readonly IDPGroup dal = DalFactory<IDPGroup>.GetDal("PGroup");
        public BPGroup()
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
		public bool Exists(int SectionNo,int Visible)
		{
			return dal.Exists(SectionNo,Visible);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.PGroup model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.PGroup model)
		{
            return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int SectionNo, int Visible)
		{

            return dal.Delete(SectionNo, Visible);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.PGroup GetModel(int SectionNo,int Visible)
		{
			
			return dal.GetModel(SectionNo,Visible);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.PGroup GetModelByCache(int SectionNo,int Visible)
		{
			
			string CacheKey = "PGroupModel-" + SectionNo+Visible;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SectionNo,Visible);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.PGroup)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PGroup model)
        {
            return dal.GetList(model);
        }
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PGroup> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PGroup> DataTableToList(DataTable dt)
		{
			List<Model.PGroup> modelList = new List<Model.PGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.PGroup model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.PGroup();
					if(dt.Rows[n]["SectionNo"].ToString()!="")
					{
						model.SectionNo=int.Parse(dt.Rows[n]["SectionNo"].ToString());
					}
					if(dt.Rows[n]["SuperGroupNo"].ToString()!="")
					{
						model.SuperGroupNo=int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
					}
					model.CName=dt.Rows[n]["CName"].ToString();
					model.ShortName=dt.Rows[n]["ShortName"].ToString();
					model.ShortCode=dt.Rows[n]["ShortCode"].ToString();
					model.SectionDesc=dt.Rows[n]["SectionDesc"].ToString();
					if(dt.Rows[n]["SectionType"].ToString()!="")
					{
						model.SectionType=int.Parse(dt.Rows[n]["SectionType"].ToString());
					}
					if(dt.Rows[n]["Visible"].ToString()!="")
					{
						model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
					}
					if(dt.Rows[n]["DispOrder"].ToString()!="")
					{
						model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
					if(dt.Rows[n]["onlinetime"].ToString()!="")
					{
						model.onlinetime=int.Parse(dt.Rows[n]["onlinetime"].ToString());
					}
					if(dt.Rows[n]["KeyDispOrder"].ToString()!="")
					{
						model.KeyDispOrder=int.Parse(dt.Rows[n]["KeyDispOrder"].ToString());
					}
					if(dt.Rows[n]["dummygroup"].ToString()!="")
					{
						model.dummygroup=int.Parse(dt.Rows[n]["dummygroup"].ToString());
					}
					if(dt.Rows[n]["uniontype"].ToString()!="")
					{
						model.uniontype=int.Parse(dt.Rows[n]["uniontype"].ToString());
					}
					if(dt.Rows[n]["SectorTypeNo"].ToString()!="")
					{
						model.SectorTypeNo=int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
					}
					if(dt.Rows[n]["SampleRule"].ToString()!="")
					{
						model.SampleRule=int.Parse(dt.Rows[n]["SampleRule"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.PGroup> GetModelList(Model.PGroup model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法

        #region IBPGroup 成员


        public Model.PGroup GetModel(string ClientNo, int SectionNo, int Visible)
        {
            return dal.GetModel(ClientNo, SectionNo, Visible);
        }

        #endregion
    }
}

