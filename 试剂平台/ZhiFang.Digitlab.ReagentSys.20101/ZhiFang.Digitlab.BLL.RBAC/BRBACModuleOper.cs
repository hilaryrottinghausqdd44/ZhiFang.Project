using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.RBAC;

namespace ZhiFang.Digitlab.BLL.RBAC
{	
	public class BRBACModuleOper : ZhiFang.Digitlab.BLL.BaseBLL<RBACModuleOper>, IBRBACModuleOper
    {
        public IBRBACRoleRight IBRBACRoleRight { get; set; }
        #region IBRBACModuleOper 成员

        public IList<Entity.RBACModuleOper> SearchModuleOperIDByModuleID(long longModuleID)
        {
            return ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(longModuleID);
        }

        public IList<Entity.RBACModuleOper> SearchModuleOperByRoleID(long longRoleID)
        {
            return ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperByRoleID(longRoleID);
        }

        public IList<Entity.RBACModuleOper> SearchModuleOperByHREmpID(long longHREmpID)
        {
            return ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
        }

        public IList<Entity.RBACModuleOper> SearchModuleOperByUserCode(string strUserCode)
        {
            return ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperByUserCode(strUserCode);
        }

        /// <summary>
        /// 模块操作权限判定服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public bool JudgeModuleOperByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID)
        {
            IList<RBACModuleOper> moduleOper = ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
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
            IList<RBACModuleOper> rbacmolist = ((IDAO.IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(RBACModuleId);
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
                    throw new Exception("删除检验小组模块操作权限失败！"); 
                }
             }
            DBDao.DeleteByHql(" From RBACModuleOper rbacmoduleoper where rbacmoduleoper.RBACModule.Id=" + RBACModuleId);
            return true;
        }

        #endregion
    } 
}