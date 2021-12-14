using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IBLL.Common;

namespace BLL
{
	/// <summary>
	/// 业务逻辑类MicroOperationStepLib 的摘要说明。
	/// </summary>
    public class MicroOperationStepLib : IBLL.IBMicroOperationStepLib
	{
        private readonly IDMicroOperationStepLib dal = DalFactory<IDMicroOperationStepLib>.GetDalByClassName("MicroOperationStepLib");
        private readonly IDataCache dataCache = DalFactory<IDataCache>.GetDalByClassName("DataCache");
        private readonly IConfigHelper ConfigHelper = DalFactory<IConfigHelper>.GetDalByClassName("ConfigHelper");
        public MicroOperationStepLib()
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
        /// 得到最大Sort
        /// </summary>
        public int GetMaxSort(int SampleTypeNo, int ParentStepId)
        {
            return dal.GetMaxSort(SampleTypeNo, ParentStepId);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.MicroOperationStepLib model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Model.MicroOperationStepLib model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int Id)
		{
			
			return dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.MicroOperationStepLib GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.MicroOperationStepLib GetModelByCache(int Id)
		{
			
			string CacheKey = "MicroOperationStepLibModel-" + Id;
            object objModel = dataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache").Value;
                        dataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.MicroOperationStepLib)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
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
		public List<Model.MicroOperationStepLib> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.MicroOperationStepLib> GetModelList(Model.MicroOperationStepLib model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.MicroOperationStepLib> DataTableToList(DataTable dt)
		{
			List<Model.MicroOperationStepLib> modelList = new List<Model.MicroOperationStepLib>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.MicroOperationStepLib model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.MicroOperationStepLib();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					model.StepName=dt.Rows[n]["StepName"].ToString();
					model.StepInfo=dt.Rows[n]["StepInfo"].ToString();
					if(dt.Rows[n]["ParentStepId"].ToString()!="")
					{
						model.ParentStepId=int.Parse(dt.Rows[n]["ParentStepId"].ToString());
					}
					if(dt.Rows[n]["Icon"].ToString()!="")
					{
						model.Icon=(byte[])dt.Rows[n]["Icon"];
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					if(dt.Rows[n]["BackStepId"].ToString()!="")
					{
						model.BackStepId=int.Parse(dt.Rows[n]["BackStepId"].ToString());
					}
					if(dt.Rows[n]["NextStepId"].ToString()!="")
					{
						model.NextStepId=int.Parse(dt.Rows[n]["NextStepId"].ToString());
					}
					model.valueClass=dt.Rows[n]["valueClass"].ToString();
					model.InputClass=dt.Rows[n]["InputClass"].ToString();
					if(dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
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
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.MicroOperationStepLib model)
        {
            return dal.GetList(model);
        }

		#endregion  成员方法
	}
}

