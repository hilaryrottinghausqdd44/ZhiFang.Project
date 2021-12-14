using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Maticsoft.Common;
using Maticsoft.Model;
namespace ZhIFang.BLL.RBAC {
	 	//RBAC_RoleModules
		public partial class RBAC_RoleModules
	{
   		     
		private readonly Maticsoft.DAL.RBAC_RoleModules dal=new Maticsoft.DAL.RBAC_RoleModules();
		public RBAC_RoleModules()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			return dal.Exists(ModuleID,EmplID,DeptID,PositionID,PostID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(Maticsoft.Model.RBAC_RoleModules model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.RBAC_RoleModules model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			
			return dal.Delete(ModuleID,EmplID,DeptID,PositionID,PostID);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.RBAC_RoleModules GetModel(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			
			return dal.GetModel(ModuleID,EmplID,DeptID,PositionID,PostID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.RBAC_RoleModules GetModelByCache(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			
			string CacheKey = "RBAC_RoleModulesModel-" + ModuleID+EmplID+DeptID+PositionID+PostID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ModuleID,EmplID,DeptID,PositionID,PostID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.RBAC_RoleModules)objModel;
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
		public List<Maticsoft.Model.RBAC_RoleModules> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.RBAC_RoleModules> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.RBAC_RoleModules> modelList = new List<Maticsoft.Model.RBAC_RoleModules>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.RBAC_RoleModules model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.RBAC_RoleModules();					
													if(dt.Rows[n]["SN"].ToString()!="")
				{
					model.SN=int.Parse(dt.Rows[n]["SN"].ToString());
				}
																																if(dt.Rows[n]["ModuleID"].ToString()!="")
				{
					model.ModuleID=int.Parse(dt.Rows[n]["ModuleID"].ToString());
				}
																																if(dt.Rows[n]["EmplID"].ToString()!="")
				{
					model.EmplID=int.Parse(dt.Rows[n]["EmplID"].ToString());
				}
																																if(dt.Rows[n]["DeptID"].ToString()!="")
				{
					model.DeptID=int.Parse(dt.Rows[n]["DeptID"].ToString());
				}
																																if(dt.Rows[n]["PositionID"].ToString()!="")
				{
					model.PositionID=int.Parse(dt.Rows[n]["PositionID"].ToString());
				}
																																if(dt.Rows[n]["PostID"].ToString()!="")
				{
					model.PostID=int.Parse(dt.Rows[n]["PostID"].ToString());
				}
																																if(dt.Rows[n]["AccAbility"].ToString()!="")
				{
					model.AccAbility=int.Parse(dt.Rows[n]["AccAbility"].ToString());
				}
																																if(dt.Rows[n]["OpAbility"].ToString()!="")
				{
					model.OpAbility=int.Parse(dt.Rows[n]["OpAbility"].ToString());
				}
																																				model.Validity= dt.Rows[n]["Validity"].ToString();
																						
				
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