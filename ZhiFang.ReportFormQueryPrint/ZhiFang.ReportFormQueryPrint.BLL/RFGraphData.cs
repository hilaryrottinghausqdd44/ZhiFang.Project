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
	/// 业务逻辑类RFGraphData 的摘要说明。
	/// </summary>
    public class BRFGraphData 
	{
        private readonly IDRFGraphData dal = DalFactory<IDRFGraphData>.GetDal("RFGraphData");
        public BRFGraphData()
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
        public bool Exists(string GraphName, int GraphNo, string FormNo)
		{
			return dal.Exists(GraphName,GraphNo,FormNo);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.RFGraphData model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.RFGraphData model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string GraphName, int GraphNo, string FormNo)
		{
			
			return dal.Delete(GraphName,GraphNo,FormNo);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo)
		{
			
			return dal.GetModel(GraphName,GraphNo,FormNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
        public Model.RFGraphData GetModelByCache(string GraphName, int GraphNo, string FormNo)
		{
			
			string CacheKey = "RFGraphDataModel-" + GraphName+GraphNo+FormNo;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GraphName,GraphNo,FormNo);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return ( Model.RFGraphData)objModel;
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
        public DataSet GetListByReportFormId(string ReportFormId)
        {
            return dal.GetListByReportFormId(ReportFormId);
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
		public List< Model.RFGraphData> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List< Model.RFGraphData> DataTableToList(DataTable dt)
		{
			List< Model.RFGraphData> modelList = new List< Model.RFGraphData>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				 Model.RFGraphData model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new  Model.RFGraphData();
					model.GraphName=dt.Rows[n]["GraphName"].ToString();
					if(dt.Rows[n]["GraphNo"].ToString()!="")
					{
						model.GraphNo=int.Parse(dt.Rows[n]["GraphNo"].ToString());
					}
					if(dt.Rows[n]["EquipNo"].ToString()!="")
					{
						model.EquipNo=int.Parse(dt.Rows[n]["EquipNo"].ToString());
					}
					if(dt.Rows[n]["PointType"].ToString()!="")
					{
						model.PointType=int.Parse(dt.Rows[n]["PointType"].ToString());
					}
					if(dt.Rows[n]["ShowPoint"].ToString()!="")
					{
						model.ShowPoint=int.Parse(dt.Rows[n]["ShowPoint"].ToString());
					}
					if(dt.Rows[n]["MColor"].ToString()!="")
					{
						model.MColor=int.Parse(dt.Rows[n]["MColor"].ToString());
					}
					model.SColor=dt.Rows[n]["SColor"].ToString();
					if(dt.Rows[n]["ShowAxis"].ToString()!="")
					{
						model.ShowAxis=int.Parse(dt.Rows[n]["ShowAxis"].ToString());
					}
					if(dt.Rows[n]["ShowLable"].ToString()!="")
					{
						model.ShowLable=int.Parse(dt.Rows[n]["ShowLable"].ToString());
					}
					if(dt.Rows[n]["MinX"].ToString()!="")
					{
						model.MinX=decimal.Parse(dt.Rows[n]["MinX"].ToString());
					}
					if(dt.Rows[n]["MaxX"].ToString()!="")
					{
						model.MaxX=decimal.Parse(dt.Rows[n]["MaxX"].ToString());
					}
					if(dt.Rows[n]["MinY"].ToString()!="")
					{
						model.MinY=decimal.Parse(dt.Rows[n]["MinY"].ToString());
					}
					if(dt.Rows[n]["MaxY"].ToString()!="")
					{
						model.MaxY=decimal.Parse(dt.Rows[n]["MaxY"].ToString());
					}
					if(dt.Rows[n]["ShowTitle"].ToString()!="")
					{
						model.ShowTitle=int.Parse(dt.Rows[n]["ShowTitle"].ToString());
					}
					model.STitle=dt.Rows[n]["STitle"].ToString();
					model.GraphValue=dt.Rows[n]["GraphValue"].ToString();
					model.GraphMemo=dt.Rows[n]["GraphMemo"].ToString();
					model.GraphF1=dt.Rows[n]["GraphF1"].ToString();
					model.GraphF2=dt.Rows[n]["GraphF2"].ToString();
					if(dt.Rows[n]["ChartTop"].ToString()!="")
					{
						model.ChartTop=int.Parse(dt.Rows[n]["ChartTop"].ToString());
					}
					if(dt.Rows[n]["ChartHeight"].ToString()!="")
					{
						model.ChartHeight=int.Parse(dt.Rows[n]["ChartHeight"].ToString());
					}
					if(dt.Rows[n]["ChartLeft"].ToString()!="")
					{
						model.ChartLeft=int.Parse(dt.Rows[n]["ChartLeft"].ToString());
					}
					if(dt.Rows[n]["ChartWidth"].ToString()!="")
					{
						model.ChartWidth=int.Parse(dt.Rows[n]["ChartWidth"].ToString());
					}
					if(dt.Rows[n]["Graphjpg"].ToString()!="")
					{
						model.Graphjpg=(byte[])dt.Rows[n]["Graphjpg"];
					}
					if(dt.Rows[n]["FormNo"].ToString()!="")
					{
						model.FormNo=dt.Rows[n]["FormNo"].ToString();
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RFGraphData> GetModelList(Model.RFGraphData model)
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
        public DataSet GetList(Model.RFGraphData model)
        {
            return dal.GetList(model);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
		//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法

		/// <summary>
		/// 参数isNeedPDF是否过滤pointType为8的数据，true为不过滤，false为过滤
		/// </summary>
		public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            return dal.GetListByReportPublicationID(ReportPublicationID);
        }

		/// <summary>
		/// 参数PointType为要过滤指定类型的数据，格式1,2,3
		/// </summary>
		public DataSet GetListByReportPublicationIDAndPointType(string ReportPublicationID,string PointType)
        {
			return dal.GetListByReportPublicationIDAndPointType(ReportPublicationID, PointType);
        }

    }
}

