using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Maticsoft.Common;
using Maticsoft.Model;
namespace ZhIFang.BLL.RBAC {
	 	//RBAC_Operates
		public partial class RBAC_Operates
	{
   		     
		private readonly Maticsoft.DAL.RBAC_Operates dal=new Maticsoft.DAL.RBAC_Operates();
		public RBAC_Operates()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SN,int Type)
		{
			return dal.Exists(SN,Type);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.RBAC_Operates model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.RBAC_Operates model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.RBAC_Operates GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.RBAC_Operates GetModelByCache(int ID)
		{
			
			string CacheKey = "RBAC_OperatesModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.RBAC_Operates)objModel;
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
		public List<Maticsoft.Model.RBAC_Operates> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.RBAC_Operates> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.RBAC_Operates> modelList = new List<Maticsoft.Model.RBAC_Operates>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.RBAC_Operates model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.RBAC_Operates();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.SN= dt.Rows[n]["SN"].ToString();
																																model.CName= dt.Rows[n]["CName"].ToString();
																																model.EName= dt.Rows[n]["EName"].ToString();
																																model.SName= dt.Rows[n]["SName"].ToString();
																																model.OperateColor= dt.Rows[n]["OperateColor"].ToString();
																												if(dt.Rows[n]["Type"].ToString()!="")
				{
					model.Type=int.Parse(dt.Rows[n]["Type"].ToString());
				}
																																				model.Descr= dt.Rows[n]["Descr"].ToString();
																						
				
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
#endregion
   
	}
}