using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.RBAC
{
    public class BRBACEmpOptions : BaseBLL<ZhiFang.Entity.RBAC.RBACEmpOptions>, IBRBACEmpOptions
    {
        public IBRBACRoleModule IBRBACRoleModule { get; set; }

        /// <summary>
        /// 根据员工ID查询该人员的常用模块列表
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <returns></returns>
        public IList<RBACEmpOptions> SearchRBACEmpOptionsByEmpID(string strEmpID)
        {
            //RBACEmpOptions r = new RBACEmpOptions();
            //r.HREmployee.Id;
            //获取员工的所有常用模块
            IList < RBACEmpOptions> list = ((IDRBACEmpOptionsDao)DBDao).GetListByHQL("HREmployee.Id=" + Convert.ToInt64(strEmpID), 0, 0).list;
            if (list.Count < 1)
            {
                return null;
            }

            foreach (RBACEmpOptions tmp in list)
            {
                if (IBRBACRoleModule.RBAC_BA_GetRBACRoleModuleByHREmpIDAndModuleID(Convert.ToInt64(strEmpID), tmp.Default.Id).Count > 0)
                {
                    tmp.RightFlag = true;             
                }
                else
                {
                    tmp.RightFlag=false;  
                }
            }      
            return list;
        }
    } 
}