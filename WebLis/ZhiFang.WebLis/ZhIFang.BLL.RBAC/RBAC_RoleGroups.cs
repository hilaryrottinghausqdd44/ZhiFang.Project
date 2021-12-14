using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Maticsoft.Common;
using Maticsoft.Model;
namespace ZhIFang.BLL.RBAC {
	 	//RBAC_RoleGroups
		public partial class RBAC_RoleGroups
	{
   		     
		private readonly Maticsoft.DAL.RBAC_RoleGroups dal=new Maticsoft.DAL.RBAC_RoleGroups();
		public RBAC_RoleGroups()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RoleGroupNo,string RoleGroupType)
		{
			return dal.Exists(RoleGroupNo,RoleGroupType);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.RBAC_RoleGroups model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.RBAC_RoleGroups model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RoleGroupID)
		{
			
			return dal.Delete(RoleGroupID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string RoleGroupIDlist )
		{
			return dal.DeleteList(RoleGroupIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.RBAC_RoleGroups GetModel(int RoleGroupID)
		{
			
			return dal.GetModel(RoleGroupID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.RBAC_RoleGroups GetModelByCache(int RoleGroupID)
		{
			
			string CacheKey = "RBAC_RoleGroupsModel-" + RoleGroupID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(RoleGroupID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.RBAC_RoleGroups)objModel;
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
		public List<Maticsoft.Model.RBAC_RoleGroups> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.RBAC_RoleGroups> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.RBAC_RoleGroups> modelList = new List<Maticsoft.Model.RBAC_RoleGroups>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.RBAC_RoleGroups model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.RBAC_RoleGroups();					
													if(dt.Rows[n]["RoleGroupID"].ToString()!="")
				{
					model.RoleGroupID=int.Parse(dt.Rows[n]["RoleGroupID"].ToString());
				}
																																if(dt.Rows[n]["RoleGroupOrder"].ToString()!="")
				{
					model.RoleGroupOrder=int.Parse(dt.Rows[n]["RoleGroupOrder"].ToString());
				}
																																				model.RoleGroupNo= dt.Rows[n]["RoleGroupNo"].ToString();
																																model.RoleGroupName= dt.Rows[n]["RoleGroupName"].ToString();
																												if(dt.Rows[n]["RoleGroupEnabled"].ToString()!="")
				{
					model.RoleGroupEnabled=int.Parse(dt.Rows[n]["RoleGroupEnabled"].ToString());
				}
																																				model.RoleGroupDesc= dt.Rows[n]["RoleGroupDesc"].ToString();
																																model.RoleGroupType= dt.Rows[n]["RoleGroupType"].ToString();
																						
				
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