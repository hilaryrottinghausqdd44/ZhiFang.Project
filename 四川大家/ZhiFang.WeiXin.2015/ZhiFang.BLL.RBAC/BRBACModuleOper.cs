using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.RBAC
{	
	public class BRBACModuleOper : BaseBLL<RBACModuleOper>, IBRBACModuleOper
    {
        public IBRBACRoleRight IBRBACRoleRight { get; set; }
        #region IBRBACModuleOper 成员

        public IList<RBACModuleOper> SearchModuleOperIDByModuleID(long longModuleID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(longModuleID);
        }

        public IList<RBACModuleOper> SearchModuleOperByRoleID(long longRoleID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByRoleID(longRoleID);
        }

        public IList<RBACModuleOper> SearchModuleOperByHREmpID(long longHREmpID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
        }

        public IList<RBACModuleOper> SearchModuleOperByUserCode(string strUserCode)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByUserCode(strUserCode);
        }

        /// <summary>
        /// 模块操作权限判定服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public bool JudgeModuleOperByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID)
        {
            IList<RBACModuleOper> moduleOper = ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
            moduleOper = moduleOper.Where(p => p.Id == longModuleOperID).ToList();
            return (moduleOper.Count>0)?true:false;
        }

        /// <summary>
        /// 根据Session中的人员ID返回新增的模块列表
        /// </summary>
        /// <returns></returns>
        public IList<RBACModuleOper> RBAC_BA_GetNewModuleListBySessionHREmpID(long longHREmpID)
        {

            return null;
        }

        public bool DeleteByRBACModuleId(long RBACModuleId)
        {
            IList<RBACModuleOper> rbacmolist = ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(RBACModuleId);
            if (rbacmolist != null && rbacmolist.Count>0)
             {
                List<long> rbacmoidlist=new List<long>();
                foreach(var rbacmo in rbacmolist)
                {
                    rbacmoidlist.Add(rbacmo.Id);
                }

                if (!IBRBACRoleRight.DeleteByRBACModuleOperIDList(rbacmoidlist))
                {
                    return false;
                    throw new Exception("删除模块操作权限失败！"); 
                }
             }
            DBDao.DeleteByHql(" From RBACModuleOper rbacmoduleoper where rbacmoduleoper.RBACModule.Id=" + RBACModuleId);
            return true;
        }

        #endregion
    } 
}