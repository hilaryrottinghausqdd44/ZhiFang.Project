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
	/// 业务逻辑类CLIENTELE 的摘要说明。
	/// </summary>
	public class BCLIENTELE 
	{
        private readonly IDCLIENTELE dal = DalFactory<IDCLIENTELE>.GetDal("CLIENTELE");
        public BCLIENTELE()
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
		public bool Exists(int ClIENTNO)
		{
			return dal.Exists(ClIENTNO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.CLIENTELE model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.CLIENTELE model)
		{
            return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ClIENTNO)
		{

            return dal.Delete(ClIENTNO);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CLIENTELE GetModel(int ClIENTNO)
		{
			
			return dal.GetModel(ClIENTNO);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.CLIENTELE GetModelByCache(int ClIENTNO)
		{
			
			string CacheKey = "CLIENTELEModel-" + ClIENTNO;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ClIENTNO);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.CLIENTELE)objModel;
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
        public DataSet GetList(Model.CLIENTELE model)
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
		public List<Model.CLIENTELE> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.CLIENTELE> GetModelList(Model.CLIENTELE model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.CLIENTELE> DataTableToList(DataTable dt)
		{
			List<Model.CLIENTELE> modelList = new List<Model.CLIENTELE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.CLIENTELE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.CLIENTELE();
					if(dt.Rows[n]["ClIENTNO"].ToString()!="")
					{
						model.ClIENTNO=int.Parse(dt.Rows[n]["ClIENTNO"].ToString());
					}
					model.CNAME=dt.Rows[n]["CNAME"].ToString();
					model.ENAME=dt.Rows[n]["ENAME"].ToString();
					model.SHORTCODE=dt.Rows[n]["SHORTCODE"].ToString();
					if(dt.Rows[n]["ISUSE"].ToString()!="")
					{
						model.ISUSE=int.Parse(dt.Rows[n]["ISUSE"].ToString());
					}
					model.LINKMAN=dt.Rows[n]["LINKMAN"].ToString();
					model.PHONENUM1=dt.Rows[n]["PHONENUM1"].ToString();
					model.ADDRESS=dt.Rows[n]["ADDRESS"].ToString();
					model.MAILNO=dt.Rows[n]["MAILNO"].ToString();
					model.EMAIL=dt.Rows[n]["EMAIL"].ToString();
					model.PRINCIPAL=dt.Rows[n]["PRINCIPAL"].ToString();
					model.PHONENUM2=dt.Rows[n]["PHONENUM2"].ToString();
					if(dt.Rows[n]["CLIENTTYPE"].ToString()!="")
					{
						model.CLIENTTYPE=int.Parse(dt.Rows[n]["CLIENTTYPE"].ToString());
					}
					if(dt.Rows[n]["bmanno"].ToString()!="")
					{
						model.bmanno=int.Parse(dt.Rows[n]["bmanno"].ToString());
					}
					model.romark=dt.Rows[n]["romark"].ToString();
					if(dt.Rows[n]["titletype"].ToString()!="")
					{
						model.titletype=int.Parse(dt.Rows[n]["titletype"].ToString());
					}
					if(dt.Rows[n]["uploadtype"].ToString()!="")
					{
						model.uploadtype=int.Parse(dt.Rows[n]["uploadtype"].ToString());
					}
					if(dt.Rows[n]["printtype"].ToString()!="")
					{
						model.printtype=int.Parse(dt.Rows[n]["printtype"].ToString());
					}
					if(dt.Rows[n]["InputDataType"].ToString()!="")
					{
						model.InputDataType=int.Parse(dt.Rows[n]["InputDataType"].ToString());
					}
					if(dt.Rows[n]["reportpagetype"].ToString()!="")
					{
						model.reportpagetype=int.Parse(dt.Rows[n]["reportpagetype"].ToString());
					}
					model.clientarea=dt.Rows[n]["clientarea"].ToString();
					model.clientstyle=dt.Rows[n]["clientstyle"].ToString();
					model.FaxNo=dt.Rows[n]["FaxNo"].ToString();
					if(dt.Rows[n]["AutoFax"].ToString()!="")
					{
						model.AutoFax=int.Parse(dt.Rows[n]["AutoFax"].ToString());
					}
					model.ClientReportTitle=dt.Rows[n]["ClientReportTitle"].ToString();
					if(dt.Rows[n]["IsPrintItem"].ToString()!="")
					{
						model.IsPrintItem=int.Parse(dt.Rows[n]["IsPrintItem"].ToString());
					}
					model.CZDY1=dt.Rows[n]["CZDY1"].ToString();
					model.CZDY2=dt.Rows[n]["CZDY2"].ToString();
					model.CZDY3=dt.Rows[n]["CZDY3"].ToString();
					model.CZDY4=dt.Rows[n]["CZDY4"].ToString();
					model.CZDY5=dt.Rows[n]["CZDY5"].ToString();
					model.CZDY6=dt.Rows[n]["CZDY6"].ToString();
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
			return GetList("");
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public DataSet GetList(string where,string fields) {

            return dal.GetList(where,fields);
        }

		#endregion  成员方法
	}
}

