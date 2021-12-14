using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.RBAC
{	
	public class BRBACRoleModule : BaseBLL<ZhiFang.Entity.RBAC.RBACRoleModule>,IBRBACRoleModule
    {
       public  IBRBACRole IBRBACRole { set; get; }

        public IList<RBACRoleModule> GetRBACRoleModuleByRBACUserCodeAndModuleID(string strUserCode, long longModuleID)
        {
            return ((IDRBACRoleModuleDao)base.DBDao).GetRBACRoleModuleByRBACUserCodeAndModuleID(strUserCode, longModuleID);
        }


        /// <summary>
        /// 模块权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleID"></param>
        /// <returns></returns>
        public IList<RBACRoleModule> RBAC_BA_GetRBACRoleModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID)
        {
            IList<RBACRoleModule>  roleModuleList = new List<RBACRoleModule>();
            IList<RBACRole> roleList = IBRBACRole.SearchRoleByHREmpID(longHREmpID);
            foreach (var tmp in roleList)
            {
                 EntityList<RBACRoleModule> tmpList  = ((IDRBACRoleModuleDao)base.DBDao).GetListByHQL("rbacrolemodule.RBACRole.Id=" +  tmp.Id + " and rbacrolemodule.RBACModule.Id=" + longModuleID, 0, 0);
                 roleModuleList = roleModuleList.Concat(tmpList.list).ToList();
            }
            return roleModuleList;
        }

        /// <summary>
        /// 根据员工ID是否具有该模块的权限
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleID"></param>
        /// <returns></returns>
        public bool RBAC_BA_GetModuleRightByHREmpID(long longHREmpID, long longModuleID)
        {
            RBACRoleModule entity = new RBACRoleModule();
            //entity.RBACRole.Id
            //entity.RBACModule.Id
            bool r = false;
            IList<RBACRoleModule> roleModuleList = new List<RBACRoleModule>();
            IList<RBACRole> roleList = IBRBACRole.SearchRoleByHREmpID(longHREmpID);
            foreach (var tmp in roleList)
            {
                IList<RBACRoleModule> tmpList = ((IDRBACRoleModuleDao)base.DBDao).GetListByHQL("rbacrolemodule.RBACRole.Id=" + tmp.Id + " and rbacrolemodule.RBACModule.Id=" + longModuleID, 0, 0).list;
                if (tmpList.Count > 0)
                {
                    r = true;
                }
            }
            return r;
        }
    } 
}