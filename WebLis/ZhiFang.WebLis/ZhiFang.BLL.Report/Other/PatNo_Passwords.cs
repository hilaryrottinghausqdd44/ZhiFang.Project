using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.IBLL.Report;
namespace ZhiFang.BLL.Report
{
	/// <summary>
	/// PatNo_Passwords
	/// </summary>
    public partial class PatNo_Passwords : ZhiFang.IBLL.Report.IBPatNo_Passwords
	{
        private readonly IDPatNo_Passwords dal = DalFactory<IDPatNo_Passwords>.GetDalByClassName("PatNo_Passwords");
		public PatNo_Passwords()
		{}
		#region  Method

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
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.PatNo_Passwords model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Model.PatNo_Passwords model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.PatNo_Passwords GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.PatNo_Passwords GetModelByCache(int Id)
		{
			
			string CacheKey = "PatNo_PasswordsModel-" + Id;
			object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.PatNo_Passwords)objModel;
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PatNo_Passwords> DataTableToList(DataTable dt)
		{
			List<Model.PatNo_Passwords> modelList = new List<Model.PatNo_Passwords>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.PatNo_Passwords model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.PatNo_Passwords();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["PatNo"]!=null && dt.Rows[n]["PatNo"].ToString()!="")
					{
					model.PatNo=dt.Rows[n]["PatNo"].ToString();
					}
					if(dt.Rows[n]["Passwords"]!=null && dt.Rows[n]["Passwords"].ToString()!="")
					{
					model.Passwords=dt.Rows[n]["Passwords"].ToString();
					}
					if(dt.Rows[n]["AddTime"]!=null && dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					if(dt.Rows[n]["UpdateTime"]!=null && dt.Rows[n]["UpdateTime"].ToString()!="")
					{
						model.UpdateTime=DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        #region IBPatNo_Passwords 成员


        int IBPatNo_Passwords.Delete(int Id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBLLBase<PatNo_Passwords> 成员


        public DataSet GetList(Model.PatNo_Passwords t)
        {
            return dal.GetList(t);
        }

        public List<Model.PatNo_Passwords> GetModelList(Model.PatNo_Passwords t)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBLLBase<PatNo_Passwords> 成员


        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

