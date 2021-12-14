using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.RBAC
{
    public class RBAC_RoleModuleLink
    {
        private readonly ZhiFang.DAL.RBAC.RBAC_RoleModuleLinkDAO dal = new ZhiFang.DAL.RBAC.RBAC_RoleModuleLinkDAO();
        public RBAC_RoleModuleLink()
        { }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.RBAC_RoleModuleLinkModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.RBAC_RoleModuleLinkModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.RBAC_RoleModuleLinkModel GetModel(long Id)
        {

            return dal.GetModel(Id);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC_RoleModuleLinkModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC_RoleModuleLinkModel> DataTableToList(DataTable dt)
        {
            List<Model.RBAC_RoleModuleLinkModel> modelList = new List<Model.RBAC_RoleModuleLinkModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.RBAC_RoleModuleLinkModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public bool SaveRoleModule(string roleId, List<string> ModuleIdlist, List<Model.Modules> modulealllist)
        {
            List<Model.RBAC_RoleModuleLinkModel> modulelist = new List<Model.RBAC_RoleModuleLinkModel>();
            foreach (var moduleid in ModuleIdlist)
            {
                if (modulealllist.Count(a => a.ID.ToString() == moduleid) <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("SaveRoleModule.未能查找到模块。moduleid:" + moduleid);
                    continue;
                }
                var module = modulealllist.Where(a => a.ID.ToString() == moduleid).First();
                if (modulelist.Count(a => a.ModuleId == module.ID) <= 0)
                    modulelist.Add(new Model.RBAC_RoleModuleLinkModel() { Id = ZhiFang.Tools.GUIDHelp.GetGUIDLong(), ModuleId = module.ID, ModuleSN = module.SN, RoleId = long.Parse(roleId) });
                int count = module.SN.Length / 2;
                if (count > 1)
                {
                    for (int i = 1; i < count; i++)
                    {
                        string psn = module.SN.Substring(0, i * 2);
                        if (modulealllist.Count(a => a.SN == psn) <= 0)
                        {
                            ZhiFang.Common.Log.Log.Error("SaveRoleModule.未能查找到P模块。psn:" + psn);
                            continue;
                        }
                        var pmodule = modulealllist.Where(a => a.SN == psn).First();
                        if (modulelist.Count(a => a.ModuleId == pmodule.ID) <= 0)
                            modulelist.Add(new Model.RBAC_RoleModuleLinkModel() { Id = ZhiFang.Tools.GUIDHelp.GetGUIDLong(), ModuleId = pmodule.ID, ModuleSN = pmodule.SN, RoleId = long.Parse(roleId),DataAddTime=DateTime.Now });
                    }
                }
            }
            dal.DeleteListwhere(" RoleId=" + roleId);

            modulelist.ForEach(a =>
            {
                this.Add(a);
            });
            return true;
        }
    }
}
